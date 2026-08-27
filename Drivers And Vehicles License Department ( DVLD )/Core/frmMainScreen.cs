using System;
using System.Drawing;
using System.Windows.Forms;
using DVLDPresentationLayer.Core;
using MyLib;

namespace DVLDPresentationLayer
{
    public partial class frmMainScreen : Form
    {  
        private frmLoginScreen _frmLogin;
        private bool _SignOut = false;
        public frmMainScreen(frmLoginScreen frmLogin)
        {
            InitializeComponent();
            clsUtility.RemoveMdiClientBorder(this);

            _frmLogin = frmLogin;
        }

        private Size SetFormsSize(int width = 200 , int height = 300)
        {
            return new Size(this.Width - width, this.Height - height);
        }
        private void tsmiPeople_Click(object sender, EventArgs e)
        {
            frmPeopleManagement frm = new frmPeopleManagement();
            frm.Size = SetFormsSize();
            frm.ShowDialog();            
        }
        private void tsmiUsers_Click (object sender, EventArgs e)
        {
            frmUsersManagement frm = new frmUsersManagement();                
            frm.ShowDialog();
        }

        private void frmMainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clsGlobalSettings.LoginInfoChanged)
            {
                _frmLogin.UpdateSavedUserInfo();
                clsGlobalSettings.LoginInfoChanged = false;
            }

            clsGlobalSettings.CurrentUserID = -1;

            if (!_SignOut)
                _frmLogin.Close();
            else
                _frmLogin.Show();
        }

        private void tsmiCurrentUserInfo_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails(clsGlobalSettings.CurrentUserID);
            frm.ShowDialog();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobalSettings.CurrentUserID);
            frm.ShowDialog();
        }

        private void tsmiSignOut_Click(object sender, EventArgs e)
        {
            _SignOut = true;
            this.Close();
        }

        private void tsmiManageApplicationTypes_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void tsmiManageTestTypes_Click(object sender, EventArgs e)
        {
            frmManageTestTypes frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void tsmiLocalLicense_Click(object sender, EventArgs e)
        {
            frmNewLDLApplication frm = new frmNewLDLApplication();
            frm.ShowDialog();
        }

        private void tsmiLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmLDLApplications frm = new frmLDLApplications();
            frm.ShowDialog();
        }

        private void tsmiDrivers_Click(object sender, EventArgs e)
        {
             
        }
    }
}
