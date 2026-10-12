using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmChangePassword : Form
    {
        public event frmUserInfo.UserPersonalEditedInfo OnEditedPersonInfo;

        private int _UserID = -1;
        private string _Password = "";
        private byte[] _Salt = null;
        private int _DGVRowIndex;

        public frmChangePassword(int UserID)
        {
             InitializeComponent();
            _LoadInfo(UserID);
        }

        public frmChangePassword(int UserID,int UsersDGVRowIndex)
        {
            InitializeComponent();
            _LoadInfo(UserID);
            _DGVRowIndex = UsersDGVRowIndex;
        }

        private void _LoadInfo(int UserID)
        {
            if (!uctrlUserDetails.LoadUserInformation(UserID))
                this.Close();

            else
            {
                _UserID = UserID;
                clsUser.GetUserPasswordWithSalt(_UserID, ref _Password, ref _Salt);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtCurrentPassword, "Current password cannot be empty!", e);

            else if (clsGeneralUtility.HashWithSaltPassword(txtCurrentPassword.Text, ref _Salt) == _Password)
            {
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtCurrentPassword);
            }

            else
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtCurrentPassword, "Current password is wrong!", e);
        }

        private void txtNewPassword_Validating(object sender , CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtNewPassword, "New password cannot be empty!", e);
            }

            else
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtNewPassword);
        }

        private void txtPasswordConfirmation_Validating(object sender, CancelEventArgs e)
        {
            if (txtPasswordConfirmation.Text != txtNewPassword.Text)
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtPasswordConfirmation, "Password confirmation does not match password!", e);
            }

            else
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtPasswordConfirmation);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                byte[] Salt = null;
                string NewPassword = clsGeneralUtility.HashWithSaltPassword(txtNewPassword.Text, ref Salt);

                if (clsUser.ChangePassword(_UserID, NewPassword, Convert.ToBase64String(Salt)))
                {
                    MessageBox.Show("Password Changed Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clsGlobalSettings.LoginInfoChanged = true;

                    btnSave.Enabled = false;
                }
                else
                    MessageBox.Show("Changing password failed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        private void uctrlUserDetails_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnEditedPersonInfo?.Invoke(UpdatedPersonInfo,_DGVRowIndex);
        }
    }
}
