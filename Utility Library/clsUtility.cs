using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Utility_Library
{
    public class clsUtility
    {
            public enum enCustomDateFormat : byte { NumericFormat = 1, DateAppreviatedMonthName = 2, DateTimeCustomFormat = 3 }

            public static Color ComboBoxBackColor = Color.FromArgb(228, 228, 228);
            public static Color ComboBoxItemsBackColor = Color.FromArgb(245, 245, 245);
            public static Color ComboBoxHighlightedBackColor = Color.FromArgb(221, 232, 240);

            private static string _FeesCustomFormat = "G29";

            public static byte WantedNumOfRowsFromDB = 10;

            // Change Win32 style to remove the MDI client 3d border (sunken) .
            [DllImport("user32.dll")]
            private static extern int GetWindowLong(IntPtr windowHandle, int index);

            [DllImport("user32.dll")]
            private static extern int SetWindowLong(IntPtr windowHandle, int index, int newStyle);

            [DllImport("user32.dll")]
            private static extern bool SetWindowPos(IntPtr windowHandle, IntPtr insertAfterHandle,
                                  int x, int y, int width, int height, int flags);

            private const int ExtendedStyleIndex = -20;
            private const int ClientEdgeExtendedStyle = 0x00000200;
            private const int NoSizeFlag = 0x0001;
            private const int NoMoveFlag = 0x0002;
            private const int NoZOrderFlag = 0x0004;
            private const int FrameChangedFlag = 0x0020;

            /// <summary>
            /// Change Win32 style to remove the MDI client 3d border
            /// </summary>
            public static void RemoveMdiClientBorder(Form frm)
            {

                MdiClient mdiClient = frm.Controls.OfType<MdiClient>().FirstOrDefault();
                if (mdiClient == null)
                {
                    return;
                }

                int currentExtendedStyle = GetWindowLong(mdiClient.Handle, ExtendedStyleIndex);
                int updatedExtendedStyle = currentExtendedStyle & ~ClientEdgeExtendedStyle;

                SetWindowLong(mdiClient.Handle, ExtendedStyleIndex, updatedExtendedStyle);

                SetWindowPos(mdiClient.Handle, IntPtr.Zero, 0, 0, 0, 0, NoSizeFlag | NoMoveFlag | NoZOrderFlag | FrameChangedFlag);

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
            public static void EnableErrorProvider(ErrorProvider erControl, Control control, string ErrorMessage, CancelEventArgs CancelEvent = null)
            {
                erControl.SetError(control, ErrorMessage);

                if (CancelEvent != null)
                    CancelEvent.Cancel = true;
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
            public static void FilterDataView(DataView dataview, string ColumnName, string FilterOnValue, KeyEventArgs e)
            {
                if (dataview.Table.Rows.Count == 0)
                    return;

                if (FilterOnValue == "")
                {
                    dataview.RowFilter = null;
                    return;
                }

                if (FilterOnValue.All(Char.IsLetter))
                    dataview.RowFilter = $"[{ColumnName}] LIKE '{FilterOnValue}%'";
                else
                    dataview.RowFilter = $"[{ColumnName}] = '{FilterOnValue}'";

            }
            /// <summary>
            /// Add new rows to data grid view.
            /// </summary>
            public static void AddNewRowsToDgv(DataGridView dgv, DataTable CurrentDataSource, DataRow[] NewDataRows, string[] ColumnsNamesInOrder)
            {
                object[] RowsValues;
                for (short i = 0; i < NewDataRows.Length; i++)
                {
                    RowsValues = new object[ColumnsNamesInOrder.Length];
                    for (short j = 0; j < ColumnsNamesInOrder.Length; j++)
                    {
                        RowsValues[j] = NewDataRows[i][ColumnsNamesInOrder[j]];
                    }
                    CurrentDataSource.Rows.Add(RowsValues);
                }
            }

            /// <summary>
            /// Return's a string array filled with the data grid view columns names in order.
            /// </summary>
            public static string[] GetDgvColumnsNames(DataGridView dgv)
            {
                string[] dgvColumnsNames = new string[dgv.Columns.Count];

                for (byte i = 0; i < dgv.Columns.Count; i++)
                {
                    dgvColumnsNames[i] = dgv.Columns[i].Name;
                }

                return dgvColumnsNames;
            }

            public static List<string> GetDgvColumnsNames(DataGridView dgv, string UnWantedColumnName)
            {
                List<string> ldgvColumnsNames = new List<string>();

                for (byte i = 0; i < dgv.Columns.Count; i++)
                {
                    ldgvColumnsNames.Add(dgv.Columns[i].Name);
                }

                if (UnWantedColumnName != null)
                    ldgvColumnsNames.Remove(UnWantedColumnName);

                return ldgvColumnsNames;
            }
            public static List<string> GetDgvColumnsNames(DataGridView dgv, string[] UnWantedColumnNames)
            {
                List<string> ldgvColumnsNames = new List<string>();

                for (byte i = 0; i < dgv.Columns.Count; i++)
                {
                    ldgvColumnsNames.Add(dgv.Columns[i].Name);
                }

                if (UnWantedColumnNames != null)
                {
                    for (byte i = 0; i < UnWantedColumnNames.Length; i++)
                        ldgvColumnsNames.Remove(UnWantedColumnNames[i]);
                }

                return ldgvColumnsNames;
            }

            /// <summary>
            /// Remove rows from data grid view that its index in the provided array , if the record cannot be deleted from then the record index in the provided array must be -1.
            /// </summary>
            public static void DeleteSelectedRowsFromView(DataGridView dgv, int[] SelectedRowsIndex)
            {
                for (short i = 0; i < SelectedRowsIndex.Length; i++)
                {
                    if (SelectedRowsIndex[i] != -1)
                        dgv.Rows.RemoveAt(SelectedRowsIndex[i]);
                }
            }

            /// <summary>
            /// Add new row to data grid view , the new values array length must match the number of data grid view columns and the dgv first column name is to sort that column in order to display the new row as first row when the dgv has a lot of records in order to avoid user to scroll down to reach the new row.
            /// </summary>
            public static void AddNewRowToDGV(DataGridView dgv, DataTable DataSource, ref object[] NewValues, string dgvFirstColumnName)
            {
                DataSource.Rows.Add(NewValues);

                dgv.Sort(dgv.Columns[dgvFirstColumnName], ListSortDirection.Descending);
            }

            /// <summary>
            /// Edit row in data grid view , new values array length must match the number of data grid view columns and row index is the index of the row that the user want to edit. 
            /// </summary>
            public static void EditFullDataRowInDgv(DataGridView dgv, DataTable DataSource, ref object[] NewValues, int RowIndex)
            {
                for (short i = 0; i < dgv.Columns.Count; i++)
                {
                    DataSource.Columns[i].ReadOnly = false;
                    DataSource.Rows[RowIndex].SetField<object>(dgv.Columns[i].HeaderText, NewValues[i]);
                }
            }

            /// <summary>
            /// Edit one column value in a row in data grid view .
            /// </summary>
            public static void EditOneColumnValueInDgv(DataGridView dgv, DataTable DataSource, string ColumnName, object NewValue, int RowIndex)
            {
                DataSource.Columns[ColumnName].ReadOnly = false;
                DataSource.Rows[RowIndex].SetField<object>(dgv.Columns[ColumnName].HeaderText, NewValue);
            }

            public static void CenterControlHorizontally(Control ContainerControl, Control control)
            {
                control.Location = new Point(ContainerControl.Width / 2 - control.Width / 2, control.Location.Y);
            }

        /// <summary>
        /// Return a string contains the fees and the format is if there was only zeros after the fraction , it shows only the number with out the fraction and zeros after the fraction.
        /// </summary>
        public static string SetFeesToCustomFormat(decimal Fees)
            {
                return Fees.ToString(_FeesCustomFormat);
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

        public static bool IsDgvLastRowDisplayed(DataGridView dgv)
        {
            return (dgv.Rows.GetLastRow(DataGridViewElementStates.None) == dgv.Rows.GetLastRow(DataGridViewElementStates.Displayed));
        }
        public static bool IsDgvLastRowSelected(DataGridView dgv)
        {
            return (dgv.Rows.GetLastRow(DataGridViewElementStates.None) == dgv.Rows.GetLastRow(DataGridViewElementStates.Selected));
        }
    }
    }