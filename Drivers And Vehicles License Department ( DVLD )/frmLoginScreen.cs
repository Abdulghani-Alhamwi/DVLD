using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmLoginScreen : Form
    {
        string _SavedInfoFileName = "LoginInfo.txt";
        public frmLoginScreen()
        {
            InitializeComponent();

           if(File.Exists(_SavedInfoFileName))
            {
                clsUtility.LoadDataFromFile(_SavedInfoFileName);
                txtUserName.Text = clsUtility.DecryptUserName(clsUtility.stSavedUserInfo.UserName);
                txtPassword.Text = clsUtility.stSavedUserInfo.Password;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _ValidateLoginPassword(string UserPassword,string EnteredPassword, byte[] Salt)
        {
            if (File.Exists(_SavedInfoFileName))
                return (EnteredPassword == UserPassword);
            else
                return (clsUtility.HashWithSaltPassword(EnteredPassword, ref Salt) == UserPassword);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("UserName cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtPassword.Text == "")
            {
                MessageBox.Show("Password cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int UserID = -1;
            string UserPassword = "";
            byte[] Salt = null;

            if(clsUser.GetLoginInfo(txtUserName.Text,ref UserID,ref UserPassword , ref Salt))
            {
                if (_ValidateLoginPassword(UserPassword, txtPassword.Text, Salt))
                {
                    if (chbRememberMe.Checked)
                        clsUtility.SaveDataToFile(_SavedInfoFileName, clsUtility.EncryptUserName(txtUserName.Text), UserPassword);
                    else
                        clsUtility.DeleteFile(_SavedInfoFileName);

                    frmMainScreen frm = new frmMainScreen(this);
                    Settings.CurrentUserID = UserID;
                    frm.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("Invalid Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Invalid UserName!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
