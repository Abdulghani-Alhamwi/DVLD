using System;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Applications;
using DVLDPresentationLayer.Controls;
using DVLDPresentationLayer.Core;
using DVLDPresentationLayer.Licenses;
using DVLDPresentationLayer.TestTypes;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmMainScreen : Form
    {
        private frmLoginScreen _frmLogin;
        private bool _SignOut = false;

        public frmMainScreen(frmLoginScreen frmLogin)
        {
            InitializeComponent();
            clsGeneralUtility.RemoveMdiClientBorder(this);

            _frmLogin = frmLogin;
        }

        private bool _IsAuthorizedUser(clsUser.enUserPermissions Permission)
        {
            return (clsUser.IsAdminUser(clsGlobalSettings.CurrentUserPermissions) || clsUser.HasUserPermission(Permission, clsGlobalSettings.CurrentUserPermissions));
        }

        private void _GetAccessDeniedMessage()
        {
            MessageBox.Show("You don't have permission, contact your admin for more details.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsmiPeople_Click(object sender, EventArgs e)
        {
            if (_IsAuthorizedUser(clsUser.enUserPermissions.PeopleManagement))
            {
                frmPeopleManagement frm = new frmPeopleManagement();
                frm.Size = new Size(this.Width - 200, this.Height - 300);
                frm.ShowDialog();
            }

            else
                _GetAccessDeniedMessage();
        }
        private void tsmiUsers_Click(object sender, EventArgs e)
        {
            if (_IsAuthorizedUser(clsUser.enUserPermissions.UsersManagement))
            {
                frmUsersManagement frm = new frmUsersManagement();
                frm.ShowDialog();
            }

            else
                _GetAccessDeniedMessage();
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
            frmApplicationTypesManagement frm = new frmApplicationTypesManagement();
            frm.ShowDialog();
        }

        private void tsmiManageTestTypes_Click(object sender, EventArgs e)
        {
            frmTestTypesManagement frm = new frmTestTypesManagement();
            frm.ShowDialog();
        }

        private void tsmiLocalLicense_Click(object sender, EventArgs e)
        {
            frmNewLDLApplication frm = new frmNewLDLApplication();
            frm.ShowDialog();
        }

        private void tsmiLocalDrivingLicenseApplications_Click(object sender, EventArgs e)
        {
            frmLDLApplicationsManagement frm = new frmLDLApplicationsManagement();
            frm.ShowDialog();
        }

        private void tsmiDrivers_Click(object sender, EventArgs e)
        {
            if (_IsAuthorizedUser(clsUser.enUserPermissions.DriversView))
            {
                frmDriversList frm = new frmDriversList();
                frm.ShowDialog();
            }

            else
                _GetAccessDeniedMessage();
        }

        private void tsmiInternationalLicense_Click(object sender, EventArgs e)
        {
            frmNewIntLicenseApplication frm = new frmNewIntLicenseApplication();
            frm.ShowDialog();
        }

        private void tsmiInternationalLicenseApplications_Click(object sender, EventArgs e)
        {
            frmIntLicenseApplications frm = new frmIntLicenseApplications();
            frm.ShowDialog();
        }

        private void tsmiRenewDrivingLicense_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicense frm = new frmRenewLocalDrivingLicense();
            frm.ShowDialog();
        }

        private void tsmiReplacementForLostOrDamaged_Click(object sender, EventArgs e)
        {
            frmReplacementForLostOrDamaged frm = new frmReplacementForLostOrDamaged();
            frm.ShowDialog();
        }

        private void tsmiReleaseDetainedDrivingLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void tsmiRetakeTest_Click(object sender, EventArgs e)
        {
            frmLDLApplicationsManagement frm = new frmLDLApplicationsManagement();
            frm.ShowDialog();
        }

        private void tsmiDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLocalLicense frm = new frmDetainLocalLicense();
            frm.ShowDialog();
        }

        private void tsmiReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.ShowDialog();
        }

        private void tsmiManageDetaiendLicenses_Click(object sender, EventArgs e)
        {
            frmDetainedLicensesManagement frm = new frmDetainedLicensesManagement();
            frm.ShowDialog();
        }

        private void tsmiApplicationsManagement_Paint(object sender, PaintEventArgs e)
        {
    
        }

        private void tsmiApplicationsManagement_DropDownOpening(object sender, EventArgs e)
        {
            if (tsmiApplicationsManagement.DropDown.IsDisposed)
                _GetAccessDeniedMessage();

            else if (!_IsAuthorizedUser(clsUser.enUserPermissions.ApplicationsManagement))
            {
                _GetAccessDeniedMessage();
                ((ToolStripDropDownItem)sender).DropDown.Dispose();
            }
        }
    }
}
