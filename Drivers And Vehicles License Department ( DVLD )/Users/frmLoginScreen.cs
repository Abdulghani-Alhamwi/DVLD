using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmLoginScreen : Form
    {
        private static string _SavedInfoDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DVLD";
        private static string _SavedInfoFilePath = _SavedInfoDirectoryPath + "\\" + "LoginInfo.txt";
        private bool _HasSavedInfo = false;
        string _OldUserName;

        internal struct stSavedUserInfo
        {
            internal static string Username = "";
            internal static string UsernameIV = "";
            internal static string Password = "";
        }

        public frmLoginScreen()
        {
            InitializeComponent();

            if (File.Exists(_SavedInfoFilePath))
            {
                _LoadLoginDataFromFile(_SavedInfoFilePath);
                txtUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey,stSavedUserInfo.Username, stSavedUserInfo.UsernameIV);
                txtPassword.Text = stSavedUserInfo.Password;
                _OldUserName = txtUserName.Text;
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
            
            return (clsGeneralUtility.HashWithSaltPassword(EnteredPassword, ref Salt) == UserPassword);
        }

        private void _SaveLoginDataToFile(string DirectoryPath, string FilePath, string Username,string UsernameVI, string Password, string Separator = "#//#")
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            if (!File.Exists(FilePath))
                File.Create(FilePath).Dispose();

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                string DataLine = Username + Separator + UsernameVI + Separator + Password;
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
                    stSavedUserInfo.Username = Data[0];
                    stSavedUserInfo.UsernameIV = Data[1];
                    stSavedUserInfo.Password = Data[2];
                }
            }
        }

        private void _DeleteLoginFile(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
                stSavedUserInfo.Username = "";
                stSavedUserInfo.Password = "";
                _HasSavedInfo = false;
            }
        }

        private void _SaveLoginInfoInFile(string UserPassword)
        {
            if (chbRememberMe.Checked && File.Exists(_SavedInfoFilePath))
                return;

            else if (chbRememberMe.Checked)
                _SaveLoginDataToFile(_SavedInfoDirectoryPath, _SavedInfoFilePath, clsGeneralUtility.EncryptString(clsGlobalSettings.EncryptionKey, txtUserName.Text,out byte[] UsernameIV), Convert.ToBase64String(UsernameIV), UserPassword);

            else if (!chbRememberMe.Checked)
                _DeleteLoginFile(_SavedInfoFilePath);
        }

        internal void UpdateSavedUserInfo()
        {
            if (_HasSavedInfo)
            {
                _DeleteLoginFile(_SavedInfoFilePath);
                clsUser.GetLoginInfo(clsGlobalSettings.CurrentUserID, ref stSavedUserInfo.Username, ref stSavedUserInfo.UsernameIV, ref stSavedUserInfo.Password);
                _SaveLoginDataToFile(_SavedInfoDirectoryPath, _SavedInfoFilePath, stSavedUserInfo.Username, stSavedUserInfo.UsernameIV, stSavedUserInfo.Password);

                txtUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, stSavedUserInfo.Username, stSavedUserInfo.UsernameIV);
                txtPassword.Text = stSavedUserInfo.Password;
                _OldUserName = txtUserName.Text;
            }
        }

        private void _Login(int UserID, string Username, string UsernameIV, string UserPassword, sbyte UserPermissions)
        {
            _SaveLoginInfoInFile(UserPassword);

            frmMainScreen frmMain = new frmMainScreen(this);
            clsGlobalSettings.CurrentUserID = UserID;
            clsGlobalSettings.CurrentUsername = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, Username, UsernameIV);
            clsGlobalSettings.CurrentUserPermissions = UserPermissions;
            _OldUserName = clsGlobalSettings.CurrentUsername;

            frmMain.Show();
            this.Hide();

            if (!chbRememberMe.Checked)
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                txtPassword.Text = UserPassword;
                _HasSavedInfo = true;
            }
        }

        private bool _HasSavedUserChanged()
        {
            if (File.Exists(_SavedInfoFilePath))
                return (txtUserName.Text != _OldUserName);

            else
                return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!_ValidateTextBoxes())
                return;

            int UserID = -1;
            string Username = "";
            string UsernameIV = "";
            string UserPassword = "";
            bool IsActive = false;
            byte[] Salt = null;
            sbyte UserPermissions = 0;

            if (clsUser.GetLoginInfo(clsGeneralUtility.HashUsernameForLookUp(txtUserName.Text),ref UserID,ref Username, ref UsernameIV, ref UserPassword, ref Salt, ref IsActive, ref UserPermissions))
            {
                if(chbRememberMe.Checked)
                {
                    if(_HasSavedUserChanged())
                     {
                        _DeleteLoginFile(_SavedInfoFilePath);
                     }
                }

                if (_ValidateLoginPassword(UserPassword, txtPassword.Text, Salt))
                {
                    if (IsActive)
                    {
                        _Login(UserID, Username, UsernameIV, UserPassword, UserPermissions);
                    }
                    
                    else
                        MessageBox.Show("User is not active! , contact your admin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Invalid Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Invalid UserName!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
