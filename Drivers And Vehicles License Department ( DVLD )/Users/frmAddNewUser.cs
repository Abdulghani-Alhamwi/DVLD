using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmAddNewUser : Form
    {
       public delegate void AddedUserEventHandler();

       public event AddedUserEventHandler AddedUser;
        public frmAddNewUser()
        {
            InitializeComponent();
        }

        clsPerson _Person;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool _MoveToNextTab()
        {
            if (_Person != null)
            {
                if (!clsUser.IsUserExists(_Person.PersonID))
                {
                    tcAddNewUser.SelectedTab = tpLoginInfo;
                    return true;
                }

                else
                    MessageBox.Show("The selected person already has a user. Choose another one.", "Select Another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Select a person or add new person first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return false;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            _MoveToNextTab();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tcAddNewUser_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tcAddNewUser.SelectedTab == tpLoginInfo)
            {
                if (!_MoveToNextTab())
                    tcAddNewUser.SelectedTab = tpPersonalInfo;
            }
        }

        private void txtUserName_Validating(object sender,CancelEventArgs e)
        {
            if (txtUserName.Text == "")
                clsUtility.EnableErrorProvider(ertxtBox, txtUserName, "Username cannot be blank.", e);

            else if (clsUser.IsUserAlreadyExists(txtUserName.Text))
                clsUtility.EnableErrorProvider(ertxtBox, txtUserName, "Username is already taken by another user. Please choose another username.", e);

            else
                ertxtBox.Dispose();
        }
        private void txtPassword_Validating(object sender,CancelEventArgs e)
        {
            if (txtPassword.Text == "")
                clsUtility.EnableErrorProvider(ertxtBox, txtPassword, "Password cannot be blank.", null);

            else if (txtPassword.Text == txtPasswordConfirmation.Text)
                ertxtBox.Dispose();
            
            else
                ertxtBox.Dispose();
                
        }
        private void txtPasswordConfirmation_Validating(object sender,CancelEventArgs e)
        {
            if (txtPassword.Text == "")
                clsUtility.EnableErrorProvider(ertxtBox, txtPasswordConfirmation, "Password confirmation cannot be blank.", null);

            else if (txtPasswordConfirmation.Text != txtPassword.Text)
                clsUtility.EnableErrorProvider(ertxtBox, txtPasswordConfirmation, "Password confirmation does not match password!", null);
            
            else
                ertxtBox.Dispose();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            btnClose.CausesValidation = false;
            btnExit.CausesValidation = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsUser User = new clsUser();

            User.PersonID = _Person.PersonID;

            User.UserName = clsUtility.EncryptUserName(txtUserName.Text);

            byte[] Salt = null;
            User.Password = clsUtility.HashWithSaltPassword(txtPassword.Text,Salt);
            User.Salt = Convert.ToBase64String(Salt);

            User.IsActive = chbIsActive.Checked;

            if(User.Save())
            {                
                lblUserID.Text = User.UserID.ToString();
                MessageBox.Show("Saved successfully!", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;

                AddedUser?.Invoke();
            }
            else
                MessageBox.Show("Saving failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPassword.Text != "" && txtPassword.Text == txtPasswordConfirmation.Text)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;
        }

        private void txtPasswordConfirmation_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPasswordConfirmation.Text != "" &&  txtPasswordConfirmation.Text == txtPassword.Text)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;
        }

        private void uctrlpersonInfoByFilter_OnPersonSelected(clsPerson Person)
        {
            _Person = Person;
        }
    }
}
