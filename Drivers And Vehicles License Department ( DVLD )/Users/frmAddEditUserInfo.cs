using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmAddEditUserInfo : Form
    {
       public delegate void AddEditUserEventHandler();

       public event AddEditUserEventHandler OnAddedOrEditedUser;

        clsPerson _Person;
        clsUser _User;

        string _DefaultPasswordValue = "Not Real Password";
        bool _WantTochangePassword = true;
        public frmAddEditUserInfo(clsUser User = null)
        {
            InitializeComponent();

            if(User != null)
            {
                _SetTitlesWhenEdit();
                _User = User;
                _ShowUserDetails();
                btnSave.Enabled = true;
            }
            
            lblAddEditUserBigTitle.Location = new Point((this.Width / 2) - (lblAddEditUserBigTitle.Width / 2), lblAddEditUserBigTitle.Location.Y);
        }

        private void _SetTitlesWhenEdit()
        {
            lblAddEditUserTitle.Text = "Update User";
            lblAddEditUserBigTitle.Text = "Update User";
        }
        private void _ShowUserDetails()
        {
            uctrlpersonInfoByFilter.LoadPersonDetails(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = clsUtility.DecryptUserName(_User.UserName);
            txtPassword.Text = _DefaultPasswordValue;
            txtPasswordConfirmation.Text = _DefaultPasswordValue;
            chbIsActive.Checked = _User.IsActive;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool _MoveToNextTab()
        {
            if (_User == null)
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
            else
            {
                tcAddNewUser.SelectedTab = tpLoginInfo;
                return true;
            }
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
            if (_User !=null)
            {
                if (txtUserName.Text == clsUtility.DecryptUserName(_User.UserName))
                    return;
            }

            if (txtUserName.Text == "")
                clsUtility.EnableErrorProvider(erTextBox, txtUserName, "Username cannot be blank.", e);

            else if (clsUser.IsUserAlreadyExists(txtUserName.Text))
                clsUtility.EnableErrorProvider(erTextBox, txtUserName, "Username is already taken by another user. Please choose another username.", e);

            else
                erTextBox.Dispose();
        }
        private void txtPasswordAndConfirmation_Validating(object sender,CancelEventArgs e)
        {
            if (_User != null)
            {
                if (((TextBox)sender).Text == "")
                    return;
            }

            if (((TextBox)sender).Text == "")
                clsUtility.EnableErrorProvider(erTextBox, ((TextBox)sender), "Password confirmation cannot be blank.", null);

            else if (((TextBox)sender).Text != ((TextBox)sender).Text)
                clsUtility.EnableErrorProvider(erTextBox, ((TextBox)sender), "Password confirmation does not match password!", null);
            
            else
                erTextBox.Dispose();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            btnClose.CausesValidation = false;
            btnExit.CausesValidation = false;
        }

        private void _SetPasswordAndSalt(clsUser User)
        {
            byte[] Salt = null;
            User.Password = clsUtility.HashWithSaltPassword(txtPassword.Text, ref Salt);
            User.Salt = Convert.ToBase64String(Salt);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            clsUser User;
            if (_User == null)
            {
                User = new clsUser();
                User.PersonID = _Person.PersonID;
            }
            else
            {
                User = _User;
                User.PersonID = _User.PersonID;
            }

            User.UserName = clsUtility.EncryptUserName(txtUserName.Text);

            if (_User == null)
                _SetPasswordAndSalt(User);
            else
            {
                if(txtPassword.Text != "" && txtPassword.Text!= _DefaultPasswordValue)
                    _SetPasswordAndSalt(User);
            }

            User.IsActive = chbIsActive.Checked;

            if(User.Save())
            {                
                lblUserID.Text = User.UserID.ToString();
                MessageBox.Show("Data Saved successfully!", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_User == null)
                {
                    _SetTitlesWhenEdit();
                    _User = User;
                }

                OnAddedOrEditedUser?.Invoke();
            }
            else
                MessageBox.Show("Saving failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtPasswordAndConfirmation_KeyUp(object sender, KeyEventArgs e)
        {
         if (_User != null && txtPassword.Text == "" && txtPasswordConfirmation.Text == "")
             btnSave.Enabled = true;

         else if (txtPassword.Text != "" && txtPassword.Text == txtPasswordConfirmation.Text)
             btnSave.Enabled = true;
         else
             btnSave.Enabled = false;
        }

        private void uctrlpersonInfoByFilter_OnPersonSelected(clsPerson Person)
        {
            _Person = Person;
        }

        private void uctrlpersonInfoByFilter_AfterEditingPerson()
        {
            if(_User != null)
            OnAddedOrEditedUser?.Invoke();
        }

        private void txtPasswordORtxtConfirmation_Enter(object sender, EventArgs e)
        {
            if (_User != null && _WantTochangePassword)
            {
                txtPassword.Text = "";
                txtPasswordConfirmation.Text = "";
                _WantTochangePassword = false;
                btnSave.Enabled = true;
            }          
        }
    }
}
