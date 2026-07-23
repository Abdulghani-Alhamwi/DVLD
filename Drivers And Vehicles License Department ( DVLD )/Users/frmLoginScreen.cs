using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmLoginScreen : Form
    {
        string _SavedInfoDirectoryPath;
        string _SavedInfoFilePath;
        bool _HasSavedInfo = false;
        private struct _stSavedUserInfo
        {
            internal static string UserName;
            internal static string Password;
        }
        public frmLoginScreen()
        {
            InitializeComponent();

            _SavedInfoDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DVLD";
            _SavedInfoFilePath = _SavedInfoDirectoryPath + "\\" + "LoginInfo.txt";

            if (File.Exists(_SavedInfoFilePath))
            {
                _LoadLoginDataFromFile(_SavedInfoFilePath);
                txtUserName.Text = clsUtility.DecryptUserName(_stSavedUserInfo.UserName);
                txtPassword.Text = _stSavedUserInfo.Password;
                chbRememberMe.Checked = true;
                _HasSavedInfo = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _ValidateTextBoxes()
        {
            if (txtUserName.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show("UserName And Password cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtUserName.Text == "")
            {
                MessageBox.Show("UserName cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Password cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool _ValidateLoginPassword(string UserPassword,string EnteredPassword, byte[] Salt)
        {
            if (File.Exists(_SavedInfoFilePath))
                if (EnteredPassword == UserPassword)
                    return true;

              return (clsUtility.HashWithSaltPassword(EnteredPassword, ref Salt) == UserPassword);
        }

        private void _SaveLoginDataToFile(string DirectoryPath, string FilePath, string UserName, string Password, string Separator = "#//#")
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            if (!File.Exists(FilePath))
                File.Create(FilePath).Dispose();

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                string DataLine = UserName + Separator + Password;
                writer.WriteLine(DataLine);
            }
        }
        private void _LoadLoginDataFromFile(string FilePath, string Separator = "#//#")
        {
            if (File.Exists(FilePath))
            {
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    string[] Data = Regex.Split(reader.ReadLine(), Separator);
                    _stSavedUserInfo.UserName = Data[0];
                    _stSavedUserInfo.Password = Data[1];
                }
            }
        }

        private void _DeleteLoginFile(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                _stSavedUserInfo.UserName = "";
                _stSavedUserInfo.Password = "";
            }
        }
        private void _SaveLoginInfoInFile(string UserPassword)
        {
            if (chbRememberMe.Checked && File.Exists(_SavedInfoFilePath))
                return;

           else if (chbRememberMe.Checked &&!_HasSavedInfo)
                _SaveLoginDataToFile(_SavedInfoDirectoryPath,_SavedInfoFilePath, clsUtility.EncryptUserName(txtUserName.Text), UserPassword);

            else if (!chbRememberMe.Checked)
               _DeleteLoginFile(_SavedInfoFilePath);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!_ValidateTextBoxes())
                return;

            int UserID = -1;
            string UserPassword = "";
            byte[] Salt = null;

            if(clsUser.GetLoginInfo(clsUtility.EncryptUserName(txtUserName.Text),ref UserID,ref UserPassword , ref Salt))
            {
                if (_ValidateLoginPassword(UserPassword, txtPassword.Text, Salt))
                {
                    _SaveLoginInfoInFile(UserPassword);

                    frmMainScreen frm = new frmMainScreen(this);
                    clsGlobalSettings.CurrentUserID = UserID;
                    frm.Show();
                    this.Hide();

                    if (!chbRememberMe.Checked)
                    {
                        txtUserName.Text = "";
                        txtPassword.Text = "";
                    }
                    else
                        txtPassword.Text = UserPassword;

                }
                else
                    MessageBox.Show("Invalid Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Invalid UserName!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
