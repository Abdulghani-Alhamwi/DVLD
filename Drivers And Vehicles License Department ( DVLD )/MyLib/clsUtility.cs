using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyLib
{
    internal class clsUtility
    {
        public static string HashWithSaltPassword(string Password,ref byte[] Salt)
        {
            if (Salt == null)
            {
                Salt = new byte[32];

                RandomNumberGenerator rn = RandomNumberGenerator.Create();
                rn.GetBytes(Salt);
                rn.Dispose();
            }
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
        public static void _FilterDataView(DataView dataview, string ColumnName, string FilterOnValue , KeyEventArgs e)
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

        public static void SaveDataToFile(string FileName,string UserName,string Password,string Separator = "#//#")
        {
            if (!File.Exists(FileName))
                File.Create(FileName);

            using(StreamWriter writer = new StreamWriter(FileName))
            {
                string DataLine = UserName + Separator + Password;
                writer.WriteLine(DataLine + "\n");
            }
        }
        internal struct stSavedUserInfo
        {
            public static string UserName;
            public static string Password;
        }
        public static void LoadDataFromFile(string FileName,string Separator = "#//#")
        {
            if (File.Exists(FileName))
            {
            using (StreamReader reader = new StreamReader(FileName))
            {
               string [] Data = Regex.Split(reader.ReadLine(),Separator);
               stSavedUserInfo.UserName = Data[0];
               stSavedUserInfo.Password = Data[1];
            }
            }
        }

        public static void DeleteFile(string FileName)
        {
            File.Delete(FileName);
            stSavedUserInfo.UserName = "";
            stSavedUserInfo.Password = "";
        }
    }
}

