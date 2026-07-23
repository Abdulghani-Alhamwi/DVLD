using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmChangePassword : Form
    {
        int _UserID = -1;
        string _Password = "";
        byte[] _Salt = null;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

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

        private bool _ValidateCurrentPassword()
        {
            
                if (clsUtility.HashWithSaltPassword(txtCurrentPassword.Text, ref _Salt) == _Password)
                {
                    erTextBox.Dispose();
                    return true;
                }

                else
                    clsUtility.EnableErrorProvider(erTextBox, txtCurrentPassword, "Current password is wrong!",null);

            return false;
        }

        private bool _ValidateNewPassword()
        {
            if (txtNewPassword.Text == "")
            {
                clsUtility.EnableErrorProvider(erTextBox, txtNewPassword, "New password cannot be empty!", null);
                return false;
            }            
            else
                erTextBox.Dispose();

            return true;
        }

        private bool _ValidatePasswordConfirmation()
        {
            if (txtNewPassword.Text == "")
                return false;

            if (txtPasswordConfirmation.Text == "")
            {
                clsUtility.EnableErrorProvider(erTextBox, txtPasswordConfirmation, "Password confirmation cannot be empty!", null);
                return false;
            }

            else if (txtPasswordConfirmation.Text != txtNewPassword.Text)
            {
                clsUtility.EnableErrorProvider(erTextBox, txtPasswordConfirmation, "Password confirmation does not match password!", null);
                return false;
            }
            else
                erTextBox.Dispose();

            return true;
        }
        
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            _ValidateCurrentPassword();
        }

        private void txtNewPassword_Validating(object sender , CancelEventArgs e)
        {
            _ValidateNewPassword();
        }

        private void txtPasswordConfirmation_Validating(object sender, CancelEventArgs e)
        {
            _ValidatePasswordConfirmation();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_ValidateCurrentPassword() && _ValidateNewPassword() && _ValidatePasswordConfirmation())
            {
                byte[] Salt = null;
                string NewPassword = clsUtility.HashWithSaltPassword(txtNewPassword.Text, ref Salt);

                if (clsUser.ChangePassword(_UserID, NewPassword, Convert.ToBase64String(Salt)))
                    MessageBox.Show("Password Changed Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Changing password failed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
