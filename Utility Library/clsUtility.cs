using System;
using System.Collections.Generic;
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
    public class clsUtility
    {
            public enum enCustomDateFormat : byte { NumericFormat = 0, DateAppreviatedMonthName = 1, DateTimeCustomFormat = 2 }

            public enum enCustomNumberFormat : byte { With4ZerosAfterFraction = 0 ,NoJustZerosAfterFraction = 1}

            public enum enDataGridViewSortDirection:byte { Ascending = 0 , Descending = 1 }

            public static byte WantedNumOfRowsFromDB = 10;
            private int _NumberOfDgvAddedRows;

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
            /// Change Win32 style to remove the MDI client 3d border (sunken)
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
            public static void AddNewRowsToDgv(DataGridView dgv,DataRow[] NewDataRows, string[] ColumnsNamesInOrder)
            {
                object[] RowsValues;
                for (short i = 0; i < NewDataRows.Length; i++)
                {
                    RowsValues = new object[ColumnsNamesInOrder.Length];
                    for (short j = 0; j < ColumnsNamesInOrder.Length; j++)
                    {
                        RowsValues[j] = NewDataRows[i][ColumnsNamesInOrder[j]];
                    }
                ((DataTable)dgv.DataSource).Rows.Add(RowsValues);
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
            /// Remove [1 - 255] rows from data grid view that its index in the provided array , if the record cannot be deleted from then the record index in the provided array must be -1.
            /// </summary>
            public void DeleteSelectedDgvRows(DataGridView dgv, int[] SelectedRowsIndex)
            {
                for (byte i = 0; i < SelectedRowsIndex.Length; i++)
                {
                if (SelectedRowsIndex[i] != -1)
                {
                    SelectedRowsIndex[i] = _GetActualRowIndexOfDgvRow((DataTable)dgv.DataSource, SelectedRowsIndex[i]);
                    ((DataTable)dgv.DataSource).Rows.RemoveAt(SelectedRowsIndex[i]);
                }
                }
        }

            /// <summary>
            /// Add new row to data grid view , the new values array length must match the number of data grid view columns and the dgv first column name is to sort that column in order to display the new row as first row when the dgv has a lot of records in order to avoid user to scroll down to reach the new row.
            /// </summary>
            public void AddNewRowToDGV(DataGridView Dgv,object[] NewValues,string dgvFirstColumnName)
            {
                DataTable DataSource = (DataTable)Dgv.DataSource;
                DataSource.Rows.Add(NewValues);
                DataSource.AcceptChanges();
                DataSource.DefaultView.Sort = $"{dgvFirstColumnName} DESC";

                _NumberOfDgvAddedRows++;
            }

        /// <summary>
        /// return the the data source row index for a dgv row.
        /// </summary>
        private int _GetActualRowIndexOfDgvRow(DataTable DataSource,int DgvRowIndex)
         {
            int DataSourceRowIndex;
            if (DgvRowIndex < _NumberOfDgvAddedRows)
                DataSourceRowIndex = (DataSource.Rows.Count - 1) - DgvRowIndex;

            else
                DataSourceRowIndex = DgvRowIndex - _NumberOfDgvAddedRows;

            return DataSourceRowIndex;
         }

            /// <summary>
            /// Edit row in data grid view , new values array length must match the number of data grid view columns and row index is the index of the row that the user want to edit. 
            /// </summary>
            public void EditFullDataRowInDgv(DataGridView dgv,object[] NewValues, int RowIndex)
            {
            DataTable DataSource = (DataTable)dgv.DataSource;
            if (_NumberOfDgvAddedRows != 0)
            {
                RowIndex = _GetActualRowIndexOfDgvRow(DataSource, RowIndex);
            }

            for (short i = 0; i < dgv.Columns.Count; i++)
            {
                DataSource.Columns[i].ReadOnly = false;
                DataSource.Rows[RowIndex].SetField<object>(DataSource.Columns[i], NewValues[i]);
            }
            DataSource.Rows[RowIndex].AcceptChanges();
        }


        /// <summary>
        /// Edit one column value in a row in data grid view .
        /// </summary>
        public void EditOneColumnValueInDgv<T>(DataGridView dgv, string ColumnName, T NewValue, int RowIndex)
        {
            DataTable DataSource = (DataTable)dgv.DataSource;
            if (_NumberOfDgvAddedRows != 0)
            {
                RowIndex = _GetActualRowIndexOfDgvRow(DataSource, RowIndex);
            }

            DataSource.Columns[ColumnName].ReadOnly = false;
            DataSource.Rows[RowIndex].SetField<T>(DataSource.Columns[ColumnName], NewValue);
            DataSource.Rows[RowIndex].AcceptChanges();
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

        public static bool IsDgvLastRowDisplayed(DataGridView dgv)
        {
            return (dgv.Rows.GetLastRow(DataGridViewElementStates.None) == dgv.Rows.GetLastRow(DataGridViewElementStates.Displayed));
        }

        public static bool IsDgvLastRowSelected(DataGridView dgv)
        {
            return (dgv.Rows.GetLastRow(DataGridViewElementStates.None) == dgv.Rows.GetLastRow(DataGridViewElementStates.Selected));
        }

        public static string GetDataGridViewSortDirection(enDataGridViewSortDirection CurrentDgvSortDirection)
        {
            switch(CurrentDgvSortDirection)
            {
                case enDataGridViewSortDirection.Ascending:
                    return "Asc";

                case enDataGridViewSortDirection.Descending:
                    return "Desc";
            }

            return null;
        }

        public void ModifyDataAfterUserOrdersColumn(DataGridView Dgv,DataTable NewSortedDataSource, DataGridViewCellMouseEventArgs e,ref string LastColumnNameDgvSortedBy)
        {
            _NumberOfDgvAddedRows = 0;

            if (LastColumnNameDgvSortedBy != Dgv.Columns[e.ColumnIndex].HeaderText)
            {
                LastColumnNameDgvSortedBy = Dgv.Columns[e.ColumnIndex].HeaderText;
            }
            
            Dgv.DataSource = NewSortedDataSource;
        }

        public static bool WasDgvColumnHeaderClicked(DataGridView Dgv,MouseEventArgs e)
        {
            return (e.Location.Y <= Dgv.ColumnHeadersHeight);
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
        public static string GetLastFilterQueryPart(string ColumnNameToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                bool PreviousConditionMayExists, int LastLowestbroughtID = -1)
        {
            if (!PreviousConditionMayExists)
            {
                if (LastLowestbroughtID != -1)
                    return $@" WHERE {ColumnNameToFilterBy} < {LastLowestbroughtID}
                     ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                else
                    return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
            }
            else
            {
                if (LastLowestbroughtID != -1)
                    return $@" AND {ColumnNameToFilterBy} < {LastLowestbroughtID}
                     ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                else
                    return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
            }
        }

        /// <summary>
        /// Returns last query part with condition if there was brought id then order by sended column or directly if there was no last brought id then send -1 instead and by that it returns the last query part order by sended column
        /// </summary
        public static string GetLastFilterQueryPart(string ColumnNameToOrderBy, string SortDirection)
        {
            return GetLastFilterQueryPart(null, ColumnNameToOrderBy, SortDirection, false, -1);
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

        /// <summary>
        /// Returns a the new sort direction , if current sort direction was Asc then it returns Desc while if it was Desc then it returns Asc
        /// </summary
        public static enDataGridViewSortDirection ReverseCurrentDgvSortDirection(enDataGridViewSortDirection CurrentSortDirection)
        {
            if (CurrentSortDirection == enDataGridViewSortDirection.Descending)
                return enDataGridViewSortDirection.Ascending;

            else
                return enDataGridViewSortDirection.Descending;
        }

        public static string GetYesNoValueAsNumericString(string FilterValue)
        {
            return (FilterValue == "Yes") ? "1" : "0";
        }

    }
}