using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Utility_Library
{
    public class clsGeneralUtility
    {
        public enum enCustomDateFormat : byte { NumericFormat = 0, DateAppreviatedMonthName = 1, DateTimeCustomFormat = 2 }
        public enum enCustomNumberFormat : byte { With4ZerosAfterFraction = 0, NoJustZerosAfterFraction = 1 }

        private static byte[] SaltForUsernameHash = new byte[16];

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr windowHandle, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr windowHandle, int index, int newStyle);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr windowHandle, IntPtr insertAfterHandle,
                              int x, int y, int width, int height, int flags);

        private const int _ExtendedStyleIndex = -20;
        private const int _ClientEdgeExtendedStyle = 0x00000200;
        private const int _NoSizeFlag = 0x0001;
        private const int _NoMoveFlag = 0x0002;
        private const int _NoZOrderFlag = 0x0004;
        private const int _FrameChangedFlag = 0x0020;

        /// <summary>
        /// Change Win32 style to remove the MDI client 3d border (sunken)
        /// </summary>
        public static void RemoveMdiClientBorder(Form frm)
        {
            MdiClient mdiClient = frm.Controls.OfType<MdiClient>().FirstOrDefault();
            if (mdiClient == null)
            {
                return;
            }

            int currentExtendedStyle = GetWindowLong(mdiClient.Handle, _ExtendedStyleIndex);
            int updatedExtendedStyle = currentExtendedStyle & ~_ClientEdgeExtendedStyle;

            SetWindowLong(mdiClient.Handle, _ExtendedStyleIndex, updatedExtendedStyle);

            SetWindowPos(mdiClient.Handle, IntPtr.Zero, 0, 0, 0, 0, _NoSizeFlag | _NoMoveFlag | _NoZOrderFlag | _FrameChangedFlag);
        }

        public static string HashWithSaltPassword(string Password, ref byte[] Salt)
        {
            if (Salt == null)
            {
                Salt = new byte[32];

                RandomNumberGenerator rn = RandomNumberGenerator.Create();
                rn.GetBytes(Salt);
                rn.Dispose();
            }

            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(Password, Salt, 50000, HashAlgorithmName.SHA256);
            byte[] HashWithSalt = PBKDF2.GetBytes(32);
            PBKDF2.Dispose();

            return Convert.ToBase64String(HashWithSalt);
         }

        /// <summary>
        /// Hash username to search for the hashed value of it in the database to find specific user when login or when checking for an entered username if it is exists.
        /// </summary>
        
        public static string HashUsernameForLookUp(string StringToHash)
        {
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(StringToHash, SaltForUsernameHash, 1, HashAlgorithmName.SHA256);
            byte[] HashWithSalt = PBKDF2.GetBytes(16);
            PBKDF2.Dispose();

            return Convert.ToBase64String(HashWithSalt);
        }

        private static void _GetAppriateEnKeySize(ref byte KeySize)
        {
            if (KeySize > 32 || KeySize > 16)
                KeySize = 32;

            else if (KeySize > 8)
                KeySize = 16;

            else
                KeySize = 8;
        }

        /// <summary>
        /// Generate encryption key for one time and store it in credntial manager using CredentialManager class and reuse the encryption key when you want.
        /// </summary>
        public static byte[] GenerateEncryptionKey(byte KeySize)
        {
            _GetAppriateEnKeySize(ref KeySize);
            byte[] EncryptionKey = new byte[KeySize];

            RandomNumberGenerator rn = RandomNumberGenerator.Create();
            rn.GetBytes(EncryptionKey);
            rn.Dispose();

            return EncryptionKey;
        }

        /// <summary>
        /// Generates a new IV for each encrypted field and each time that field's value is updated, following security best practices.
        /// </summary>
        public static byte[] GenerateNewInitializationVector()
        {
            byte[] IV = new byte[16];

            RandomNumberGenerator rn = RandomNumberGenerator.Create();
            rn.GetBytes(IV);
            rn.Dispose();

            return IV;
        }

        public static class CredentialManager
        {
            private const int Credential_GenericType = 1;
            private const int CredentialLocation_LocalMachine = 2;

            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
            private struct Credential
            {
                public int Flags;
                public int Type;

                public string TargetName;
                public string Comment;

                public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;

                public int CredentialBlobSize;
                public IntPtr CredentialBlob_Ptr;

                public int CredentialPersistLocation;

                public int AttributeCount;
                public IntPtr Attributes;

                public string TargetAlias;
                public string UserName;
            }

            [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern bool CredWrite(ref Credential Credential, uint Flags);

            [DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            private static extern bool CredRead(string TargetName, int Type, int Flags, out IntPtr Credential);

            [DllImport("Advapi32.dll")]

            private static extern void CredFree(IntPtr Credential);

            /// <summary>
            /// Store encryption key securly in windows credintial manager one time and re use in application using GetEncryptionKeyMethod.
            /// </summary>
            public static void StoreEncryptionKeySecurly(string KeyName, byte[] EncryptionKey)
            {
                IntPtr CredentialBlob_Ptr = Marshal.AllocCoTaskMem(EncryptionKey.Length);

                Marshal.Copy(EncryptionKey, 0, CredentialBlob_Ptr, EncryptionKey.Length);

                Credential credential = new Credential
                {
                    Type = Credential_GenericType,
                    TargetName = KeyName,
                    CredentialBlobSize = EncryptionKey.Length,
                    CredentialBlob_Ptr = CredentialBlob_Ptr,
                    CredentialPersistLocation = CredentialLocation_LocalMachine
                };

                if (!CredWrite(ref credential, 0))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                Marshal.FreeCoTaskMem(CredentialBlob_Ptr);
            }

            public static bool GetEncryptionKey(string KeyName, out byte[] EncryptionKey)
            {
                IntPtr credentialPointer;

                if (!CredRead(KeyName, Credential_GenericType, 0, out credentialPointer))
                {
                    EncryptionKey = new byte[] { };
                    return false;
                }

                Credential credential = Marshal.PtrToStructure<Credential>(credentialPointer);

                EncryptionKey = new byte[credential.CredentialBlobSize];

                Marshal.Copy(credential.CredentialBlob_Ptr, EncryptionKey, 0, credential.CredentialBlobSize);
                
                CredFree(credentialPointer);

                return true;
            }

        }

        public static string EncryptString(byte[] EncryptionKey, string StringToEncrypt, out byte[] IV)
        {
            byte[] SendedStringInBytes = Encoding.UTF8.GetBytes(StringToEncrypt);

            Aes aes = Aes.Create();
            aes.Key = EncryptionKey;

            IV = GenerateNewInitializationVector();
            aes.IV = IV;

            ICryptoTransform Encryptor = aes.CreateEncryptor();
            aes.Dispose();

            byte[] EncryptedString = Encryptor.TransformFinalBlock(SendedStringInBytes, 0, SendedStringInBytes.Length);
            Encryptor.Dispose();

            return Convert.ToBase64String(EncryptedString);
        }

        public static string EncryptString(byte[] EncryptionKey, string StringToEncrypt)
        {
            return EncryptString(EncryptionKey, StringToEncrypt, out byte[] IV);
        }

        public static string DecryptString(byte[] EncryptionKey, string StringToDecrypt, string IV)
        {
            byte[] EncryptedString = Convert.FromBase64String(StringToDecrypt);

            Aes aes = Aes.Create();
            aes.Key = EncryptionKey;

            byte[] OriginalIV = Convert.FromBase64String(IV);
            aes.IV = OriginalIV;

            ICryptoTransform Decryptor = aes.CreateDecryptor();
            aes.Dispose();

            byte[] DecryptedString = Decryptor.TransformFinalBlock(EncryptedString, 0, EncryptedString.Length);
            Decryptor.Dispose();

            return Encoding.UTF8.GetString(DecryptedString);
        }

        public static void EnableErrorProvider(ErrorProvider erControl, Control control, string ErrorMessage, CancelEventArgs CancelArgs = null)
        {
            erControl.SetError(control, ErrorMessage);

            if (CancelArgs != null)
                CancelArgs.Cancel = true;
        }

        public static void DrawComboBoxItems(object sender, DrawItemEventArgs e, string ColumnName = null)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            string ItemText;

            if (ColumnName != null)
            {
                DataRowView RowView = (DataRowView)((ComboBox)sender).Items[e.Index];
                ItemText = RowView[ColumnName].ToString();
            }
            else
                ItemText = ((ComboBox)sender).Items[e.Index].ToString();

            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(ItemText, e.Font, brush, e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        public static void CenterControlHorizontally(Control ContainerControl, Control control)
        {
            control.Location = new Point(ContainerControl.Width / 2 - control.Width / 2, control.Location.Y);
        }

        private static string _GetNumberFormat(enCustomNumberFormat Format)
        {
            if (Format == enCustomNumberFormat.NoJustZerosAfterFraction)
                return "G29";

            else
                return "F4";
        }

        /// <summary>
        /// Takes decimal number and wanted format and returns a string contains the number and the format is :
        /// if enCustomNumberFormat.NoJustZerosAfterFraction then if there was only zeros after the fraction , it shows only the number with out the fraction and zeros after the fraction.
        /// if enCustomNumberFormat.With4ZerosAfterFraction then it will return string contain fees value with fraction and after it 4 zeros even if numbers after fraction was zeros and even if there was no fraction.
        /// </summary>
        public static string GetCustomNumberFormat(decimal Number,enCustomNumberFormat Format)
        {
            return Number.ToString(_GetNumberFormat(Format));
        }

        /// <summary>
        /// Takes float number and wanted format and returns a string contains the number and the format is :
        /// if enCustomNumberFormat.NoJustZerosAfterFraction then if there was only zeros after the fraction , it shows only the number with out the fraction and zeros after the fraction.
        /// if enCustomNumberFormat.With4ZerosAfterFraction then it will return string contain fees value with fraction and after it 4 zeros even if numbers after fraction was zeros and even if there was no fraction.
        /// </summary>
        public static string GetCustomNumberFormat(float Number, enCustomNumberFormat Format)
        {
            return Number.ToString(_GetNumberFormat(Format));
        }


        /// <summary>
        /// The enCustomDateFormat.NumericFormat returns format "dd/MM/yyyy",
        /// The enCustomDateFormat.DateAppreviatedMonthName returns format "d/MMM/yyyy";
        /// The enCustomDateFormat.DateTimeCustomFormat returns format "dd/MM/yyyy h:mm tt";
        /// </summary>
        public static string GetCustomDateFormat(enCustomDateFormat CustomFormat)
        {
            switch (CustomFormat)
            {
                case enCustomDateFormat.NumericFormat:
                    return "dd/MM/yyyy";

                case enCustomDateFormat.DateAppreviatedMonthName:
                    return "d/MMM/yyyy";

                case enCustomDateFormat.DateTimeCustomFormat:
                    return "dd/MM/yyyy h:mm tt";
            }

            return null;
        }

        /// <summary>
        /// Returns a datatable contains sorted info based on your query and sended arguments, the query must be designed to bring the sorted info , this method is for structure only in order to avoid repeating code and the sorted query must be sended from you.
        /// </summary>
        public static DataTable GetSortedInfoFromYourQueryAndArgs(string ConnectionString, string Query, byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
           string ColumnNameToFilterBy, string ValueToFilterBy, char? WildChar = null, SqlConnection CustomConnection = null, SqlCommand CustomCommand = null)
        {
            DataTable dtSortedData = null;

            SqlConnection Connection;
            SqlCommand Command = null;

            if (CustomCommand == null)
            {
                Connection = new SqlConnection(ConnectionString);

                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

                if (ValueToFilterBy != null)
                    Command.Parameters.AddWithValue("@Value", ValueToFilterBy);

                if (WildChar != null)
                    Command.Parameters.AddWithValue("@WildChar", WildChar);
            }

            else
                Connection = CustomConnection;

                try
                {
                    Connection.Open();

                    SqlDataReader reader = (CustomCommand == null) ? Command.ExecuteReader() : CustomCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dtSortedData = new DataTable();
                        dtSortedData.Load(reader);
                    }

                    reader.Close();
                }

                catch { }

                finally
                {
                    Connection.Close();
                }

            return dtSortedData;
        }

        /// <summary>
        /// Returns a datatable contains sorted info based on your query and sended arguments, the query must be designed to bring the sorted info , this method is for structure only in order to avoid repeating code and the sorted query must be sended from you.
        /// </summary>
        public static DataTable GetSortedInfoFromYourQueryAndArgs(SqlConnection CustomConnection, SqlCommand CustomCommand)
        {
            return GetSortedInfoFromYourQueryAndArgs(null, null, 0, null, null, null, null, null, CustomConnection, CustomCommand);
        }

        /// <summary>
        /// Returns a datatable contains sorted info based on your query and sended arguments, the query must be designed to bring the sorted info , this method is for structure only in order to avoid repeating code and the sorted query must be sended from you.
        /// </summary>
        public static DataTable GetSortedInfoFromYourQueryAndArgs(string ConnectionString, string Query, byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,string ValueToFilterBy = null)
        {
            return GetSortedInfoFromYourQueryAndArgs(ConnectionString, Query, WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, null, ValueToFilterBy, null); 
        }

        private static string _GetLastQueryPartWithOrderByPart(string PrimaryKeyToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                bool PreviousConditionMayExists, int LastBroughtID, bool HasOrderColumnNameWhiteSpaces,char ComparisonOperator, bool HasPrimaryKeyColumnWhiteSpaces)
        {
            if(HasPrimaryKeyColumnWhiteSpaces)
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}
                                  ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }

                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}
                                 ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}
                     ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                  ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                }
            }

            else
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                  ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }

                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                 ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                     ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                  ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                }
            }
        }

        private static string _GetQueryPartWithWhereClauseOnly(string PrimaryKeyToFilterBy, string ColumnNameToOrderBy, string SortDirection,
             bool PreviousConditionMayExists, int LastBroughtID, bool HasOrderColumnNameWhiteSpaces, char ComparisonOperator, bool HasPrimaryKeyColumnWhiteSpaces)
        {
            if (HasPrimaryKeyColumnWhiteSpaces)
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                }
            }

            else
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns last query part with condition if there was brought id then order by sended column or directly if there was no last brought id then send -1 instead and by that it returns the last query part order by sended column
        /// </summary>
        public static string GetLastQueryPart(string PrimaryKeyToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                bool PreviousConditionMayExists, int LastBroughtID = -1 , bool HasOrderColumnNameWhiteSpaces = true,bool ResultWithOrderByClause = false,bool HasPrimaryKeyColumnWhiteSpaces = false)
        {
            char ComparisonOperator = (SortDirection == "Desc") ? '<' : '>';

            if (ResultWithOrderByClause)
            {
                return _GetLastQueryPartWithOrderByPart(PrimaryKeyToFilterBy, ColumnNameToOrderBy, SortDirection, PreviousConditionMayExists,
                    LastBroughtID, HasOrderColumnNameWhiteSpaces, ComparisonOperator,HasOrderColumnNameWhiteSpaces);
            }

            else
            {
                return _GetQueryPartWithWhereClauseOnly(PrimaryKeyToFilterBy, ColumnNameToOrderBy, SortDirection, PreviousConditionMayExists,
                     LastBroughtID, HasOrderColumnNameWhiteSpaces, ComparisonOperator, HasOrderColumnNameWhiteSpaces);
            }
        }

        /// <summary>
        /// Returns last query part with condition if there was brought id then order by sended column or directly if there was no last brought id then send -1 instead and by that it returns the last query part order by sended column
        /// </summary
        public static string GetLastSortQueryPart(string ColumnNameToOrderBy, string SortDirection,bool HasOrderColumnNameWhiteSpaces = true)
        {
            return GetLastQueryPart(null, ColumnNameToOrderBy, SortDirection, false, -1,HasOrderColumnNameWhiteSpaces,true);
        }

        /// <summary>
        /// Returns last part of the query that is used to bring table data and that part is the order by part.
        /// </summary
        public static string GetOrderByQueryPart(string LastColumnNameDataOrderedBy, string SortDirection,bool HasOrderColumnNameWhiteSpaces = true)
        {
            return GetLastQueryPart(null, LastColumnNameDataOrderedBy, SortDirection, false, -1, HasOrderColumnNameWhiteSpaces, true);
        }

        /// <summary>
        /// Returns last query part which is the where clause part for filteration based on a value , the WildChar if sended it will be used after the value to bring specified pattern.
        /// </summary
        public static string GetFilterQueryPart_ValueCondition(string ColumnNameToFilterBy, char? WildChar = null, bool HasColumnWhiteSpaces = false)
        {
            if(HasColumnWhiteSpaces)
            {
                if (WildChar == null)
                    return $" WHERE [{ColumnNameToFilterBy}] = @Value";
                else
                    return $" WHERE [{ColumnNameToFilterBy}] Like @Value + @WildChar";
            }

            else
            {
                if (WildChar == null)
                    return $" WHERE {ColumnNameToFilterBy} = @Value";
                else
                    return $" WHERE {ColumnNameToFilterBy} Like @Value + @WildChar";
            }
        }

        public static string GetYesNoValueAsNumericString(string FilterValue)
        {
            return (FilterValue == "Yes") ? "1" : "0";
        }

        /// <summary>
        /// Returns appropriate hour number to give to datetime object to show the time correctly if it entered hour in AM or PM.
        /// </summary
        public static byte GetAppropriatetHourNumForAmOrPm(byte EnteredHour,bool IsTime_PM)
        {
            if (IsTime_PM)
            {
                switch (EnteredHour)
                {
                    case 1:
                        EnteredHour = 13;
                        break;

                    case 2:
                        EnteredHour = 14;
                        break;

                    case 3:
                        EnteredHour = 15;
                        break;

                    case 4:
                        EnteredHour = 16;
                        break;

                    case 5:
                        EnteredHour = 17;
                        break;

                    case 6:
                        EnteredHour = 18;
                        break;

                    case 7:
                        EnteredHour = 19;
                        break;

                    case 8:
                        EnteredHour = 20;
                        break;

                    case 9:
                        EnteredHour = 21;
                        break;

                    case 10:
                        EnteredHour = 22;
                        break;

                    case 11:
                        EnteredHour = 23;
                        break;
                }
            }
            else
            {
                if (EnteredHour == 12)
                    EnteredHour = 0;
            }

            return EnteredHour;
        }

        /// <summary>
        /// Returns offset pagination query part to add to your main query , the returned part is : " OFFSET @NumberOfRowsToOffset ROWS FETCH NEXT @WantedNumOfRecords ROWS ONLY"
        /// </summary
        public static string GetOffsetPaginationQueryPart()
        {
            return " OFFSET @NumberOfRowsToOffset ROWS FETCH NEXT @WantedNumOfRecords ROWS ONLY";
        }

        /// <summary>
        /// Validate text in the text box control that is for first name, second name, third name and last name.
        /// Third name is optional.
        /// The tag of each text box must have the role of what it, like the first name text box tag must have value : First name.
        /// If entered value not valid then the error provider will be turned on and if you send the cancel event args argument then it will prevent changing focus of that control until user enters valid input.
        /// </summary
        public static bool ValidateName(TextBox txtBox,ErrorProvider errorProvider, CancelEventArgs e)
        {
            if ((txtBox.Text == "" || string.IsNullOrWhiteSpace(txtBox.Text)) && (txtBox.Tag.ToString().ToLower() != "third name"))
            {
                EnableErrorProvider(errorProvider, txtBox, $"It is required to enter your {txtBox.Tag.ToString()}!", e);
                return false;
            }

            else if (!(txtBox.Text.All(Char.IsLetter) || txtBox.Text.Contains("-") || txtBox.Text.Contains("_")))
            {
                EnableErrorProvider(errorProvider, txtBox, $"{txtBox.Tag.ToString()} must contain only letters!", e);
                return false;
            }

            else
                errorProvider.Dispose();

            return true;
        }

        /// <summary>
        /// Validate text box that is for address.
        /// If entered value not valid then the error provider will be turned on and if you send the cancel event args argument then it will prevent changing focus of that control until user enters valid input.
        /// </summary
        public static bool ValidateAddress(TextBox txtBox, ErrorProvider errorProvider,CancelEventArgs e)
        {
            if (txtBox.Text == "" || string.IsNullOrWhiteSpace(txtBox.Text))
            {
                EnableErrorProvider(errorProvider, txtBox, $"It is required to enter your Address!", e);
                return false;
            }
            else
                errorProvider.Dispose();

            return true;
        }

        /// <summary>
        /// Validate text in the text box control that is for national number.
        /// If entered value not valid then the error provider will be turned on and if you send the cancel event args argument then it will prevent changing focus of that control until user enters valid input.
        /// </summary
        public static bool ValidateNationalNo(TextBox txtBox, ErrorProvider errorProvider,CancelEventArgs e,bool IsNationalNoAlreadyExists)
        {
            if (txtBox.Text == "" || string.IsNullOrWhiteSpace(txtBox.Text))
            {
                EnableErrorProvider(errorProvider, txtBox, "It is required to enter your National No!", e);
                return false;
            }

            if (IsNationalNoAlreadyExists)
            {
                EnableErrorProvider(errorProvider, txtBox, $"National Number is used for another person!", e);
                return false;
            }

            else
                errorProvider.Dispose();

            return true;
        }

        /// <summary>
        /// Validate text in the text box control that is for phone number.
        /// If entered value not valid then the error provider will be turned on and if you send the cancel event args argument then it will prevent changing focus of that control until user enters valid input.
        /// </summary
        public static bool ValidatePhone(TextBox txtBox, ErrorProvider errorProvider, CancelEventArgs e)
        {
            if (txtBox.Text == "" || string.IsNullOrWhiteSpace(txtBox.Text))
            {
                clsGeneralUtility.EnableErrorProvider(errorProvider, txtBox, "It is required to enter your Phone Number!", e);
                return false;
            }

            else if (!txtBox.Text.All(Char.IsDigit))
            {
                clsGeneralUtility.EnableErrorProvider(errorProvider, txtBox, "Phone Number must contains only digits!", e);
                return false;
            }
            else
                errorProvider.Dispose();

            return true;
        }

        private static bool _ValidateEmailStart(TextBox txtBox)
        {
            if (!(txtBox.Text.Contains("@") && txtBox.Text.Contains("."))
                || txtBox.Text.StartsWith(".") || txtBox.Text.StartsWith("-")
                || txtBox.Text.StartsWith("_") || txtBox.Text.StartsWith("+"))
                return false;

            else
                return true;
        }

        private static bool _ValdiateEmailMiddle(TextBox txtBox)
        {
            if (txtBox.Text.Contains("@.") || txtBox.Text.Contains("@-")
               || txtBox.Text.Contains(".@") || txtBox.Text.Contains("-@")
               || txtBox.Text.Contains("@+") || txtBox.Text.Contains("+@")
               || txtBox.Text.Contains("@_") || txtBox.Text.Contains("_@")
               || txtBox.Text.Substring(txtBox.Text.IndexOf("@") + 1, (txtBox.Text.IndexOf(".")) - (txtBox.Text.IndexOf("@") + 1)).Contains("_"))
                return false;

            else
                return true;
        }

        private static bool _ValidateEmailEnd(TextBox txtBox)
        {
            if (txtBox.Text.EndsWith(".") || txtBox.Text.EndsWith("-")
                || txtBox.Text.EndsWith("_") || txtBox.Text.EndsWith("+")
                || txtBox.Text.EndsWith("@"))
                return false;

            else
                return true;
        }

        /// <summary>
        /// Validate text in the text box control that is for email.
        /// If entered value not valid then the error provider will be turned on and if you send the cancel event args argument then it will prevent changing focus of that control until user enters valid input.
        /// </summary
        public static bool ValidateEmail(TextBox txtBox, ErrorProvider errorProvider, CancelEventArgs e)
        {
            if (txtBox.Text.Contains(" ") || txtBox.Text.Contains(",") || !(_ValidateEmailStart(txtBox)
            && _ValdiateEmailMiddle(txtBox) && _ValidateEmailEnd(txtBox)))
            {
                EnableErrorProvider(errorProvider, txtBox, "Invalid Email Address Format!", e);
                return false;
            }

            else
                errorProvider.Dispose();

            return true;

        }

        /// <summary>
        /// Set constraint on date by age, like entering the value 18 for MinAge parameter, then here the minimum age is 18 therefore the max date that the user will be allowed to choose in the DateTimePicker control is a date of a person its age at least 18.
        /// </summary
        public static void SetDateConstraintForAge(DateTimePicker dtpControl, byte MinAge, DateTime? MinDate = null)
        {
            DateTime MindateOfBirth = DateTime.Now.AddYears(- MinAge);

            dtpControl.Format = DateTimePickerFormat.Custom;
            dtpControl.CustomFormat = clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.NumericFormat);
            dtpControl.Value = MindateOfBirth;
            dtpControl.MaxDate = MindateOfBirth;

            if (MinDate == null)
                dtpControl.MinDate = DateTime.Now.AddYears(-100);

            else
            {
                dtpControl.MinDate = Convert.ToDateTime(MinDate);
            }
        }

    }
}