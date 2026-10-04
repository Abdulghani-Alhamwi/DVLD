using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Utility_Library
{
    public class clsDataGridViewUtilityLib
    {
        public enum enDataGridViewSortDirection : byte { Ascending = 0, Descending = 1 }

        public static byte WantedNumOfRowsFromDB = 10;
        private int _NumberOfDGVAddedRows;

        private int _NumberOfRowsToOffset;
        public int NumberOfRowsToOffset { get { return _NumberOfRowsToOffset; } }

        private bool _IsFirstTimeOffset = true;

        private string _LastDGVOrderedColumn;

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

        private void _OrderDataRowsDescending(ref DataRow[] DataRows)
        {
            DataRow[] DataRowAfterOrdering = new DataRow[DataRows.Count()];

            for (byte i = 0; i < DataRows.Count(); i++)
            {
                DataRowAfterOrdering[i] = DataRows[DataRows.Count() - i - 1];
            }
            DataRows = DataRowAfterOrdering;
        }

        private void _OrderDataRowsAscending(ref DataRow[] DataRows)
        {
            DataRow[] DataRowAfterOrdering = new DataRow[DataRows.Count()];
            DataRow TempDataRow = null;

            for (byte i = 0; i < DataRows.Count(); i++)
            {
                if (i < DataRows.Count() - 1)
                {
                    if ((int)DataRows[i][0] < (int)DataRows[i + 1][0])
                        DataRowAfterOrdering[i] = DataRows[i];

                    else
                    {
                        TempDataRow = DataRows[i + 1];
                        DataRows[i + 1] = DataRows[i];
                        DataRows[i] = TempDataRow;
                        DataRowAfterOrdering[i] = DataRows[i];
                    }
                }
                else
                    DataRowAfterOrdering[i] = DataRows[i];
            }
            DataRows = DataRowAfterOrdering;
        }

        /// <summary>
        /// Add new rows to data grid view.
        /// </summary>
        public void AddNewRowsToDgv(DataGridView dgv, DataRow[] NewDataRows, string[] ColumnsNamesInOrder,enDataGridViewSortDirection SortDirection)
        {
            if (SortDirection == enDataGridViewSortDirection.Descending)
                _OrderDataRowsDescending(ref NewDataRows);

            else
                _OrderDataRowsAscending(ref NewDataRows);

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
        /// Add new row to data grid view , the new values array length must match the number of data grid view columns and the dgv first column name is to sort that column in order to display the new row as first row when the dgv has a lot of records in order to avoid user to scroll down to reach the new row.
        /// </summary>
        public void AddNewRowToDGV(DataGridView dgv, object[] NewValues, string dgvPrimaryKeyColumnName)
        {
            DataTable DataSource = (DataTable)dgv.DataSource;
            DataSource.Rows.Add(NewValues);
            DataSource.AcceptChanges();
            DataSource.DefaultView.Sort = $"{dgvPrimaryKeyColumnName} DESC";

            _NumberOfDGVAddedRows++;
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
            DataTable DataSource = (DataTable)dgv.DataSource;
            for (byte i = 0; i < SelectedRowsIndex.Length; i++)
            {
                if (SelectedRowsIndex[i] != -1)
                {
                    SelectedRowsIndex[i] = _GetActualRowIndexOfDgvRow(DataSource, SelectedRowsIndex[i]);
                    ((DataTable)dgv.DataSource).Rows.RemoveAt(SelectedRowsIndex[i]);
                }
            }
        }

        /// <summary>
        /// return the the data source row index for a dgv row.
        /// </summary>
        private int _GetActualRowIndexOfDgvRow(DataTable DataSource, int dgvRowIndex)
        {
            int DataSourceRowIndex;
            if (dgvRowIndex < _NumberOfDGVAddedRows)
                DataSourceRowIndex = (DataSource.Rows.Count - 1) - dgvRowIndex;

            else
                DataSourceRowIndex = dgvRowIndex - _NumberOfDGVAddedRows;

            return DataSourceRowIndex;
        }

        /// <summary>
        /// Edit row in data grid view , new values array length must match the number of data grid view columns and row index is the index of the row that the user want to edit. 
        /// </summary>
        public void EditFullDataRowInDgv(DataGridView dgv, object[] NewValues, int RowIndex)
        {
            DataTable DataSource = (DataTable)dgv.DataSource;
            if (_NumberOfDGVAddedRows != 0)
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
            if (_NumberOfDGVAddedRows != 0)
            {
                RowIndex = _GetActualRowIndexOfDgvRow(DataSource, RowIndex);
            }

            DataSource.Columns[ColumnName].ReadOnly = false;
            DataSource.Rows[RowIndex].SetField<T>(DataSource.Columns[ColumnName], NewValue);
            DataSource.Rows[RowIndex].AcceptChanges();
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
            switch (CurrentDgvSortDirection)
            {
                case enDataGridViewSortDirection.Ascending:
                    return "Asc";

                case enDataGridViewSortDirection.Descending:
                    return "Desc";
            }

            return null;
        }

        public static string GetClickedColumnNameInDGV(DataGridView dgv, DataGridViewCellMouseEventArgs e)
        {
            return dgv.Columns[e.ColumnIndex].HeaderText;
        }

        private static void _SetLastColumnNameDGVSortedBy(DataGridView dgv, DataGridViewCellMouseEventArgs e,ref string LastColumnNameDGVSortedBy)
        {
            if (LastColumnNameDGVSortedBy != dgv.Columns[e.ColumnIndex].HeaderText)
            {
                LastColumnNameDGVSortedBy = dgv.Columns[e.ColumnIndex].HeaderText;
            }
        }

        public void SetDataSourceAfterColumnOrdering(DataGridView dgv, DataTable NewSortedDataSource, DataGridViewCellMouseEventArgs e, ref string LastColumnNameDGVSortedBy)
        {
            _NumberOfDGVAddedRows = 0;
            _SetLastColumnNameDGVSortedBy(dgv, e,ref LastColumnNameDGVSortedBy);

            dgv.DataSource = NewSortedDataSource;
        }

        public static bool WasDgvColumnHeaderClicked(DataGridView dgv, MouseEventArgs e)
        {
            return (e.Location.Y <= dgv.ColumnHeadersHeight);
        }

        /// <summary>
        /// Returns a the new sort direction , if current sort direction was Asc then it returns Desc while if it was Desc then it returns Asc
        /// </summary
        public static void ReverseCurrentDGVSortDirection(DataGridView dgv, ref string LastColumnNameDGVSortedBy, ref enDataGridViewSortDirection CurrentDGVColumnSortDirection, DataGridViewCellMouseEventArgs e)
        {
            if (LastColumnNameDGVSortedBy != GetClickedColumnNameInDGV(dgv, e))
            {
                CurrentDGVColumnSortDirection = enDataGridViewSortDirection.Descending;
            }

            if (CurrentDGVColumnSortDirection == enDataGridViewSortDirection.Descending)
                CurrentDGVColumnSortDirection = enDataGridViewSortDirection.Ascending;

            else
                CurrentDGVColumnSortDirection = enDataGridViewSortDirection.Descending;
        }

        public static bool _IsRepeatedDataLoadToDgv(TextBox FilterationTextBox)
        {
            if (FilterationTextBox.Text == "")
            {
                return true;
            }

            else
                return false;
        }

        private static void _DecryptDGVColumnValues(byte[] EncryptionKey,Dictionary<int,string> IDWithIVPairs, DataTable NewDataSource,string IDColumnName, string EncryptedColumnName)
        {
            if (NewDataSource != null)
            {
                foreach (DataRow datarow in NewDataSource.Rows)
                {
                    IDWithIVPairs.TryGetValue((int)datarow[IDColumnName], out string IV);
                    datarow[EncryptedColumnName] = clsGeneralUtility.DecryptString(EncryptionKey,datarow[EncryptedColumnName].ToString(),IV);
                }
            }
        }

        /// <summary>
        /// Returns DataSource and the Username is descrypted therefore it will be shown in data grid view appropriatly.
        /// </summary
        public static void DecryptDGVColumnValues(byte[] EncryptionKey, Dictionary<int, string> IDWithIVPairs, DataGridView dgv,string IDColumnName, string EncryptedColumnName)
        {
            _DecryptDGVColumnValues(EncryptionKey, IDWithIVPairs, (DataTable)dgv.DataSource, IDColumnName, EncryptedColumnName);
        }

        /// <summary>
        /// Decrypt the Username therefore it will be shown in data grid view appropriatly.
        /// </summary
        public static void DecryptDGVColumnValues(byte[] EncryptionKey, Dictionary<int, string> IDWithIVPairs, DataTable NewDataSource, string IDColumnName, string EncryptedColumnName, out DataRow[] NewRows)
        {
            _DecryptDGVColumnValues(EncryptionKey, IDWithIVPairs, NewDataSource, IDColumnName, EncryptedColumnName);

            NewRows = NewDataSource?.Select();
        }

        /// <summary>
        /// Set number of offset rows for offset pagination and the set is based on the sended columns , the primary key column is not used for offset pagination but for cusor pagination therefore you must handle cursor pagination for the primary key column and offset pagination for other columns therefore when ordering with other column than the primary key column the order be correct instead of using the unique identifier which can lead to rbing the last id and first id and by that the condition to bring more records using cursor pagination won't work correctly..
        /// </summary
        public void SetNumberOfRowsToOffset(string PrimaryKeyViewedColumnName, string LastColumnNameDGVSortedBy)
        {
            if (LastColumnNameDGVSortedBy == null)
                _IsFirstTimeOffset = false;

            else if (_LastDGVOrderedColumn != LastColumnNameDGVSortedBy)
            {
                _IsFirstTimeOffset = true;
            }

            if (_IsFirstTimeOffset)
            {
                _NumberOfRowsToOffset = 0;
                _IsFirstTimeOffset = false;
            }

            if (LastColumnNameDGVSortedBy != null)
                _NumberOfRowsToOffset += 10;

            _LastDGVOrderedColumn = LastColumnNameDGVSortedBy;
        }

        /// <summary>
        /// Returns lowest brought value to data grid view from the primary key column , primary key column here is one column and its type must be int.
        /// </summary
        private static int _GetLowestBroughtValueOfPKColumn(DataGridView dgv, string dgvPrimaryKeyColumnName)
        {
            int ID = -1;
            foreach (DataRow Row in ((DataTable)dgv.DataSource).Rows)
            {
                if (ID == -1)
                    ID = (int)Row[dgvPrimaryKeyColumnName];

                if ((int)Row[dgvPrimaryKeyColumnName] < ID)
                    ID = (int)Row[dgvPrimaryKeyColumnName];
            }
            return ID;
        }

        /// <summary>
        /// Returns highest brought value to data grid view from the primary key column , primary key column here is one column and its type must be int.
        /// </summary
        private static int _GetHighestBroughtValueOfPKColumn(DataGridView dgv, string dgvPrimaryKeyColumnName)
        {
            int ID = -1;
            foreach (DataRow Row in ((DataTable)dgv.DataSource).Rows)
            {
                if (ID == -1)
                    ID = (int)Row[dgvPrimaryKeyColumnName];

                if ((int)Row[dgvPrimaryKeyColumnName] > ID)
                    ID = (int)Row[dgvPrimaryKeyColumnName];
            }
            return ID;
        }

        /// <summary>
        /// Returns Last lowest or highest brought value of the primary key column according to current dgv sort direction.
        /// </summary
        public static int GetLastBroughtValueOfPKColumn(DataGridView dgv, string dgvPrimaryKeyColumnName,enDataGridViewSortDirection SortDirection)
        {
            if (SortDirection == enDataGridViewSortDirection.Descending)
                return _GetLowestBroughtValueOfPKColumn(dgv, dgvPrimaryKeyColumnName);

            else
                return _GetHighestBroughtValueOfPKColumn(dgv, dgvPrimaryKeyColumnName);
        }

        /// <summary>
        /// Reset the last column to order by to null and the sort direction to enDataGridViewSortDirection.Descending
        /// </summary
        public static void ResetSortPropertiesToDefault(ref string LastColumnNameDGVOrderedBy,ref enDataGridViewSortDirection SortDirection)
        {
            LastColumnNameDGVOrderedBy = null;
            SortDirection = enDataGridViewSortDirection.Descending;
        }

        public static int GetDGVRowIndexByColumnValue(DataGridView dgv, string ColumnName, int Value)
        {
            DataTable DataSource = (DataTable)dgv.DataSource;

            foreach (DataRow Row in DataSource.Rows)
            {
                if ((int)Row[ColumnName] == Value)
                {
                 return DataSource.Rows.IndexOf(Row);
                }
            }
            return -1;
        }
    }
}
