using System;
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
        public static void RefreshInformationView(DataGridView dgv, DataTable datatable)
        {
            dgv.DataSource = datatable;
        }

        public static void RefreshInformationView(DataGridView dgv, DataView dataview)
        {
            dgv.DataSource = dataview;
        }

    /// <summary>
    /// Add New Rows To Data Grid View.
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
        /// Return's A String Array Filled With The Data Grid View Columns Names In Order.
        /// </summary>
        public static string[] GetdgvColumnsNames(DataGridView dgv)
        {
            string[] dgvColumnsNames = new string[dgv.Columns.Count];

            for (short i = 0; i < dgv.Columns.Count; i++)
            {
                dgvColumnsNames[i] = dgv.Columns[i].HeaderText;
            }

            return dgvColumnsNames;
        }
    }
}

