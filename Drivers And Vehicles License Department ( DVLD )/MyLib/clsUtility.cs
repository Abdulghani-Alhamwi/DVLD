using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace MyLib
{
    internal class clsUtility
    {

        public static string HashWithSaltPassword(string Password, out byte[] Salt)
        {
            Salt = new byte[32];

            RandomNumberGenerator rn = RandomNumberGenerator.Create();
            rn.GetBytes(Salt);
            rn.Dispose();

            Rfc2898DeriveBytes PBKDF2 = new Rfc2898DeriveBytes(Password, Salt, 10000);
            byte[] HashWithSalt = PBKDF2.GetBytes(32);

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


        public static void EnableErrorProvider(ErrorProvider erControl,Control control, string ErrorMessage, CancelEventArgs CancelEvent = null)
        {
            erControl.SetError(control, ErrorMessage);

            if (CancelEvent != null)
            CancelEvent.Cancel = true;
        }

        internal static void DrawComboBoxItems(object sender, DrawItemEventArgs e, string ColumnName = null)
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
        internal static void _FilterDataView(DataView dataview, string ColumnName, string FilterOnValue , KeyEventArgs e)
        {
            if (dataview.Table.Rows.Count == 0)
                return;

            if (FilterOnValue == "")
            {
                dataview.RowFilter = null;
                return;
            }


            if (FilterOnValue.Length > 9)
            {
                if (e.KeyData != Keys.Back)
                    MessageBox.Show("Invalid Input !", "Details", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                return;
            }

            if (FilterOnValue.All(Char.IsLetter))
                dataview.RowFilter = $"[{ColumnName}] LIKE '{FilterOnValue}%'";
            else
                dataview.RowFilter = $"[{ColumnName}] = '{FilterOnValue}'";

        }
    }
}

