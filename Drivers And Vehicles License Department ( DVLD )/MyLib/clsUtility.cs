using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    internal class clsUtility
    {
        public static Color ComboBoxBackColor = Color.FromArgb(228,228,228);
        public static Color ComboBoxItemsBackColor = Color.FromArgb(245,245,245);
        public static Color ComboBoxHighlightedBackColor = Color.FromArgb(221, 232, 240);

        public static string DateCustomFormat = "dd/MM/yyyy";

        public static byte WantedNumOfRowsFromDB = 10;
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
        public static void AddNewRowsToDGV(DataGridView dgv, DataTable CurrentDataSource, DataRow[] NewDataRows, string[] ColumnsNamesInOrder)
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
        public static string[] GetdgvColumnsNames(DataGridView dgv)
        {
            string[] dgvColumnsNames = new string[dgv.Columns.Count];

            for (byte i = 0; i < dgv.Columns.Count; i++)
            {
                dgvColumnsNames[i] = dgv.Columns[i].Name;
            }
            
            return dgvColumnsNames;
        }

        public static List<string> GetdgvColumnsNames(DataGridView dgv,string UnWantedColumnName)
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
        public static void AddNewRowToDGV(DataGridView dgv, DataTable DataSource,ref object[] NewValues, string dgvFirstColumnName)
        {
            DataSource.Rows.Add(NewValues);

            dgv.Sort(dgv.Columns[dgvFirstColumnName], ListSortDirection.Descending);
        }

        /// <summary>
        /// Edit row in data grid view , new values array length must match the number of data grid view columns and row index is the index of the row that the user want to edit. 
        /// </summary>
        public static void EditFullDataRowInDGV(DataGridView dgv, DataTable DataSource,ref object[] NewValues, int RowIndex)
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
        public static void EditOneColumnValueInDGV(DataGridView dgv, DataTable DataSource, string ColumnName, object NewValue, int RowIndex)
        {
            DataSource.Columns[ColumnName].ReadOnly = false;
            DataSource.Rows[RowIndex].SetField<object>(dgv.Columns[ColumnName].HeaderText, NewValue);
        }

        public static void CenterControlHorizontally(Control ContainerControl , Control control)
        {
            control.Location = new Point(ContainerControl.Width / 2 - control.Width / 2, control.Location.Y);
        }
    }
}

