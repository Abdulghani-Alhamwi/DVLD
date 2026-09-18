using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Utility_Library
{
    public class clsDgvUtilityLib
    {
        public enum enDataGridViewSortDirection : byte { Ascending = 0, Descending = 1 }

        public static byte WantedNumOfRowsFromDB = 10;
        private int _NumberOfDgvAddedRows;

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
        public static void AddNewRowsToDgv(DataGridView dgv, DataRow[] NewDataRows, string[] ColumnsNamesInOrder)
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
        public void AddNewRowToDGV(DataGridView Dgv, object[] NewValues, string dgvFirstColumnName)
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
        private int _GetActualRowIndexOfDgvRow(DataTable DataSource, int DgvRowIndex)
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
        public void EditFullDataRowInDgv(DataGridView dgv, object[] NewValues, int RowIndex)
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

        public void SetNewDataSourceForOrderedColumn(DataGridView Dgv, DataTable NewSortedDataSource, DataGridViewCellMouseEventArgs e, ref string LastColumnNameDgvSortedBy)
        {
            _NumberOfDgvAddedRows = 0;

            if (LastColumnNameDgvSortedBy != Dgv.Columns[e.ColumnIndex].HeaderText)
            {
                LastColumnNameDgvSortedBy = Dgv.Columns[e.ColumnIndex].HeaderText;
            }

            Dgv.DataSource = NewSortedDataSource;
        }

        public static bool WasDgvColumnHeaderClicked(DataGridView Dgv, MouseEventArgs e)
        {
            return (e.Location.Y <= Dgv.ColumnHeadersHeight);
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

        public static bool _IsRepeatedDataLoadToDgv(TextBox FilterationTextBox)
        {
            if (FilterationTextBox.Text == "")
            {
                return true;
            }

            else
                return false;
        }
    }

}
