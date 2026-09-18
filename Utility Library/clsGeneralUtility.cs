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
            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(Password, Salt, 10000, HashAlgorithmName.SHA256);
            byte[] HashWithSalt = PBKDF2.GetBytes(32);
            PBKDF2.Dispose();

            return Convert.ToBase64String(HashWithSalt);
        }

        private static byte[] _Key = new byte[16];
        private static byte[] _IV = new byte[16];
        public static string EncryptUserName(string UserName)
        {
            byte[] UserNameInBytes = Encoding.UTF8.GetBytes(UserName);

            Aes aes = Aes.Create();
            aes.Key = _Key;
            aes.IV = _IV;

            ICryptoTransform Encryptor = aes.CreateEncryptor();
            aes.Dispose();
            byte[] EncryptedUserName = Encryptor.TransformFinalBlock(UserNameInBytes, 0, UserNameInBytes.Length);
            Encryptor.Dispose();

            return Convert.ToBase64String(EncryptedUserName);
        }

        public static string DecryptUserName(string UserName)
        {
            byte[] EncryptedUserName = Convert.FromBase64String(UserName);

            Aes aes = Aes.Create();
            aes.Key = _Key;
            aes.IV = _IV;

            ICryptoTransform Decryptor = aes.CreateDecryptor();
            aes.Dispose();
            byte[] DecryptedUserName = Decryptor.TransformFinalBlock(EncryptedUserName, 0, EncryptedUserName.Length);
            Decryptor.Dispose();

            return Encoding.UTF8.GetString(DecryptedUserName);
        }

        public static void EnableErrorProvider(ErrorProvider erControl, Control control, string ErrorMessage, CancelEventArgs CancelArgs = null)
        {
            erControl.SetError(control, ErrorMessage);

            if (CancelArgs != null)
                CancelArgs.Cancel = true;
            else
                CancelArgs.Cancel = false;
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
           string ColumnNameToFilterBy, string ValueToFilterBy, char? WildChar = null)
        {
            DataTable dtSortedData = null;
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (ValueToFilterBy != null)
                command.Parameters.AddWithValue("@Value", ValueToFilterBy);

            if (WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

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
                connection.Close();
            }

            return dtSortedData;
        }

        /// <summary>
        /// Returns a datatable contains sorted info based on your query and sended arguments, the query must be designed to bring the sorted info , this method is for structure only in order to avoid repeating code and the sorted query must be sended from you.
        /// </summary>
        public static DataTable GetSortedInfoFromYourQueryAndArgs(string ConnectionString, string Query, byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,string ValueToFilterBy = null)
        {
            return GetSortedInfoFromYourQueryAndArgs(ConnectionString, Query, WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, null, ValueToFilterBy, null); 
        }

        /// <summary>
        /// Returns last query part with condition if there was brought id then order by sended column or directly if there was no last brought id then send -1 instead and by that it returns the last query part order by sended column
        /// </summary>
        public static string GetLastFilterQueryPart(string PrimaryKeyToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                bool PreviousConditionMayExists, int LastLowestbroughtID = -1 , bool HasColumnNameWhiteSpaces = true)
        {
            if (HasColumnNameWhiteSpaces)
            {
                if (!PreviousConditionMayExists)
                {
                    if (LastLowestbroughtID != -1)
                        return $@" WHERE {PrimaryKeyToFilterBy} < {LastLowestbroughtID}
                     ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                    else
                        return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                }
                else
                {
                    if (LastLowestbroughtID != -1)
                        return $@" AND {PrimaryKeyToFilterBy} < {LastLowestbroughtID}
                     ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                    else
                        return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                }
            }
            else
            {
                if (!PreviousConditionMayExists)
                {
                    if (LastLowestbroughtID != -1)
                        return $@" WHERE {PrimaryKeyToFilterBy} < {LastLowestbroughtID}
                     ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                    else
                        return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                }
                else
                {
                    if (LastLowestbroughtID != -1)
                        return $@" AND {PrimaryKeyToFilterBy} < {LastLowestbroughtID}
                     ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                    else
                        return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                }
            }
        }

        /// <summary>
        /// Returns last query part with condition if there was brought id then order by sended column or directly if there was no last brought id then send -1 instead and by that it returns the last query part order by sended column
        /// </summary
        public static string GetLastSortQueryPart(string ColumnNameToOrderBy, string SortDirection,bool HasColumnNameWhiteSpaces = true)
        {
            return GetLastFilterQueryPart(null, ColumnNameToOrderBy, SortDirection, false, -1,HasColumnNameWhiteSpaces);
        }

        /// <summary>
        /// Returns last query part which is the where clause part for filteration based on a value , the WildChar if sended it will be used after the value to bring specified pattern.
        /// </summary
        public static string GetFilterQueryPart_ValueCondition(string ColumnNameToFilterBy, char? WildChar = null)
        {
            if (WildChar == null)
                return $" WHERE {ColumnNameToFilterBy} = @Value";
            else
                return $" WHERE {ColumnNameToFilterBy} Like @Value + @WildChar";
        }

        public static string GetYesNoValueAsNumericString(string FilterValue)
        {
            return (FilterValue == "Yes") ? "1" : "0";
        }
    }
}