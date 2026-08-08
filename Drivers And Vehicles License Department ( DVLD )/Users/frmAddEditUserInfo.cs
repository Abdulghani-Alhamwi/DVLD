using System;
using System.ComponentModel;
using System.Data;
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

        internal delegate void AfterSavingNewInfo(ref object[] NewValues);
        internal delegate void AfterSavingEditedInfo(ref object[] NewValues, int RowIndex);
        internal event AfterSavingNewInfo AfterSavingNewUserInfo;
        internal event AfterSavingEditedInfo AfterSavingEditedUserInfo;

        private int _PersonID = -1;
        private clsUser _User;
        private string _DefaultPasswordValue = "Not Real Password";
        private bool _WantTochangePassword = true;
        int _IndexOfWantedDataRowToEdit = -1;

        public frmAddEditUserInfo(clsUser User = null)
        {
            _InitializeForm(User);
        }

        public frmAddEditUserInfo(clsUser User, int IndexOfWantedDataRowToEdit = -1)
        {
            _InitializeForm(User);
            _IndexOfWantedDataRowToEdit = IndexOfWantedDataRowToEdit;
        }

        private void _InitializeForm(clsUser User)
        {
            InitializeComponent();

            if (User == null)
                _SetTitles(clsUser.enMode.AddNew);

            else
            {
                _User = User;
                _SetTitles(clsUser.enMode.Update);
                _ShowUserDetails();
                btnSave.Enabled = true;
            }

            lblFormBigTitle.Location = new Point((this.Width / 2) - (lblFormBigTitle.Width / 2), lblFormBigTitle.Location.Y);
        }

        private void _SetTitles(clsUser.enMode Mode)
        {
            if (Mode == clsUser.enMode.AddNew)
            {
                lblFormTitle.Text = "Add New User";
                lblFormBigTitle.Text = "Add New User";
            }
            else
            {
                lblFormTitle.Text = "Update User";
                lblFormBigTitle.Text = "Update User";
            }
        }
        private void _ShowUserDetails()
        {
            uctrlpersonInfoByFilter.LoadPersonDetails(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = clsUtility.DecryptUserName(_User.UserName);
            txtPassword.Text = _DefaultPasswordValue;
            txtPasswordConfirmation.Text = _DefaultPasswordValue;
            chkIsActive.Checked = _User.IsActive;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool _MoveToNextTab()
        {
            if (_User == null)
            {
                if (_PersonID != -1)
                {
                    if (!clsUser.IsUserExists(_PersonID))
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

            if (txtUserName.Text == "" || string.IsNullOrWhiteSpace(txtUserName.Text))
                clsUtility.EnableErrorProvider(erTextBox, txtUserName, "Username cannot be blank.", e);

            else if (clsUser.IsUserAlreadyExists(txtUserName.Text))
                clsUtility.EnableErrorProvider(erTextBox, txtUserName, "Username is already taken by another user. Please choose another username.", e);

            else
                erTextBox.Dispose();
        }
        private void txtPasswordConfirmation_Validating(object sender,CancelEventArgs e)
        {
            if (txtPasswordConfirmation.Text == "" || string.IsNullOrWhiteSpace(txtPasswordConfirmation.Text)) 
                clsUtility.EnableErrorProvider(erTextBox, txtPasswordConfirmation, "Password confirmation cannot be blank.", null);

            else if (txtPasswordConfirmation.Text != txtPasswordConfirmation.Text)
                clsUtility.EnableErrorProvider(erTextBox, txtPasswordConfirmation, "Password confirmation does not match password!", null);
            
            else
                erTextBox.Dispose();
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text == "" || string.IsNullOrWhiteSpace(txtPassword.Text))
                clsUtility.EnableErrorProvider(erTextBox, ((TextBox)sender), "Password cannot be blank.", null);

            else
                erTextBox.Dispose();
        }

        private void frmAddNewUser_Load(object sender, EventArgs e)
        {
            btnClose.CausesValidation = false;
            btnExit.CausesValidation = false;
        }

        private object[] _GetCurrentValuesInArray()
        {
            object[] Values = new object[] {lblUserID.Text,_PersonID, clsPerson.GetFullName(_PersonID),txtUserName.Text,chkIsActive};

            return Values;
        }

        private void _SetPasswordAndSalt(clsUser User)
        {
            byte[] Salt = null;
            User.Password = clsUtility.HashWithSaltPassword(txtPassword.Text, ref Salt);
            User.Salt = Convert.ToBase64String(Salt);
        }

        private bool _IsInfoUnchanged()
        {
            return (txtUserName.Text == clsUtility.DecryptUserName(_User.UserName)
                 && txtPassword.Text == _DefaultPasswordValue
                 && txtPasswordConfirmation.Text == _DefaultPasswordValue
                 && chkIsActive.Checked == _User.IsActive);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsUser User;
            if (_User == null)
            {
                User = new clsUser();
                User.PersonID = _PersonID;
            }
            else
            {
                if(_IsInfoUnchanged())
                {
                    MessageBox.Show("There is'nt any change on the information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }    

                User = _User;
                User.PersonID = _User.PersonID;
            }

            User.UserName = clsUtility.EncryptUserName(txtUserName.Text);

            if (_User == null)
                _SetPasswordAndSalt(User);
            else
            {
                if(txtPassword.Text != _DefaultPasswordValue)
                    _SetPasswordAndSalt(User);
            }
            
            User.IsActive = chkIsActive.Checked;

            if(User.Save())
            {                
                lblUserID.Text = User.UserID.ToString();
                MessageBox.Show("Data Saved successfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_User == null)
                {
                    _SetTitles(clsUser.enMode.Update);
                    _User = User;
                }

                OnAddedOrEditedUser?.Invoke();

                object[] NewValues = _GetCurrentValuesInArray();
                AfterSavingNewUserInfo?.Invoke(ref NewValues);
                AfterSavingEditedUserInfo?.Invoke(ref NewValues,_IndexOfWantedDataRowToEdit);

                clsGlobalSettings.LoginInfoChanged = true;
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

        private void uctrlpersonInfoByFilter_OnPersonSelected(int PersonID)
        {
            _PersonID = PersonID;
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
