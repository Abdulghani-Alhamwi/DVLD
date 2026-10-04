using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmAddEditUserInfo : Form
    {
        public delegate void AddEditUserEventHandler();
        public event AddEditUserEventHandler OnAddedOrEditedUserInfo;

        public delegate void SavedNewInfo(object[] NewUserDetails, int NewUserID, string UsernameIV);
        public delegate void SavedEditedInfo(object[] ModifiedUserDetails, int RowIndex, int UserID, string UsernameIV, string NewUserFullName);
        public event SavedNewInfo AfterSavingNewInfo;
        public event SavedEditedInfo AfterSavingEditedInfo;

        public event frmUserDetails.UserPersonalEditedInfo OnEditedUserPersonalInfo;

        private int _PersonID = -1;
        private clsUser _User;
        private string _DefaultPasswordValue = "Not Real Password";
        private bool _WantTochangePassword = true;
        bool _IsUpdateDirectlyAfterAddition;
        int _DGVRowIndex = -1;
        string _CurrentUserFullName;

        public frmAddEditUserInfo()
        {
            InitializeComponent();
            _InitializeInfo(null);
        }

        public frmAddEditUserInfo(int UsersDGVRowIndex)
        {
            InitializeComponent();
            _InitializeInfo(null);
            _DGVRowIndex = UsersDGVRowIndex;
        }

        public frmAddEditUserInfo(clsUser User, int UsersDGVRowIndex,string CurrentUserFullName)
        {
            InitializeComponent();
            _InitializeInfo(User);
            _DGVRowIndex = UsersDGVRowIndex;
            _CurrentUserFullName = CurrentUserFullName;
        }

        private void _InitializeInfo(clsUser User)
        {
            if (User == null)
                _SetTitles(clsUser.enMode.AddNew);

            else
            {
                _User = User;
                _PersonID = User.PersonID;
                _SetTitles(clsUser.enMode.Update);
                _ShowUserDetails();
                btnSave.Enabled = true;
            }

            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
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
        
        private void _EnableAllPermissionsCheckBoxes()
        {
            chkManageUsers.Checked = true;
            chkManagePeople.Checked = true;
            chkManageApplications.Checked = true;
            chkViewDrivers.Checked = true;
        }

        private void _ShowUserDetails()
        {
            uctrlpersonInfoByFilter.LoadPersonDetails(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey,_User.Username,_User.UsernameIV);
            txtPassword.Text = _DefaultPasswordValue;
            txtPasswordConfirmation.Text = _DefaultPasswordValue;
            chkIsActive.Checked = _User.IsActive;
            
            if (_User.IsAdminUser())
                _EnableAllPermissionsCheckBoxes();

            if (_User.HasUserPermission(clsUser.enUserPermissions.UsersManagement))
                chkManageUsers.Checked = true;

            if(_User.HasUserPermission(clsUser.enUserPermissions.PeopleManagement))
                 chkManagePeople.Checked = true;

            if (_User.HasUserPermission(clsUser.enUserPermissions.ApplicationsManagement))
                chkManageApplications.Checked = true;

            if(_User.HasUserPermission(clsUser.enUserPermissions.DriversView))
                chkViewDrivers.Checked = true;
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

        private void btnClose_Click(object sender, EventArgs e)
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
                if (txtUserName.Text == clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, _User.Username, _User.UsernameIV))
                    return;
            }

            if (txtUserName.Text == "" || string.IsNullOrWhiteSpace(txtUserName.Text))
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtUserName, "Username cannot be blank.", e);

            else if (clsUser.IsUserAlreadyExists(clsGeneralUtility.EncryptString(clsGlobalSettings.EncryptionKey,txtUserName.Text)))
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtUserName, "Username is already taken by another user. Please choose another username.", e);

            else
                erTextBox.Dispose();
        }

        private void txtPasswordConfirmation_Validating(object sender,CancelEventArgs e)
        {
            if (txtPasswordConfirmation.Text == "" || string.IsNullOrWhiteSpace(txtPasswordConfirmation.Text)) 
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtPasswordConfirmation, "Password confirmation cannot be blank.", null);

            else if (txtPasswordConfirmation.Text != txtPasswordConfirmation.Text)
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtPasswordConfirmation, "Password confirmation does not match password!", null);
            
            else
                erTextBox.Dispose();
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text == "" || string.IsNullOrWhiteSpace(txtPassword.Text))
                clsGeneralUtility.EnableErrorProvider(erTextBox, ((TextBox)sender), "Password cannot be blank.", null);

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
            string Password = "", Salt = "";
            _SetPasswordAndSalt(ref Password,ref Salt);

            User.Password = Password;
            User.Salt = Salt;
        }

        private void _SetPasswordAndSalt(ref string Password,ref string Salt)
        {
            byte[] SaltArray = null;
            Password = clsGeneralUtility.HashWithSaltPassword(txtPassword.Text, ref SaltArray);
            Salt = Convert.ToBase64String(SaltArray);
        }

        private sbyte _GetNewUserPermissions()
        {
            if (chkManageUsers.Checked && chkManagePeople.Checked
             && chkManageApplications.Checked && chkViewDrivers.Checked)
                return -1;

            else
            {
                sbyte Permissions = 0;

                if (chkManageUsers.Checked)
                    Permissions += 1;

                if (chkManagePeople.Checked)
                    Permissions += 2;

                if (chkManageApplications.Checked)
                    Permissions += 4;

                if (chkViewDrivers.Checked)
                    Permissions += 8;

                return Permissions;
            }
        }

        private void _SetUserInfo(out clsUser User)
        {
            if (_User != null)
            {
                User = _User;
                User.PersonID = _PersonID;
                User.Username = clsGeneralUtility.EncryptString(clsGlobalSettings.EncryptionKey, txtUserName.Text, out byte[] UsernameIV);
                User.UsernameIV = Convert.ToBase64String(UsernameIV);
                User.UsernameHash = clsGeneralUtility.HashUsernameForLookUp(txtUserName.Text);

                if (txtPassword.Text != _DefaultPasswordValue)
                    _SetPasswordAndSalt(User);

                User.IsActive = chkIsActive.Checked;
                User.UserPermissions = _GetNewUserPermissions();
            }
            else
            {
                string _Password = "";
                string _Salt = "";
                _SetPasswordAndSalt(ref _Password,ref _Salt);

                User = new clsUser(
                 PersonID: _PersonID,
                 Username: clsGeneralUtility.EncryptString(clsGlobalSettings.EncryptionKey, txtUserName.Text, out byte[] UsernameIV),
                 UsernameIV: Convert.ToBase64String(UsernameIV),
                 UsernameHash: clsGeneralUtility.HashUsernameForLookUp(txtUserName.Text),
                 Password: _Password, Salt: _Salt,
                 IsActive: chkIsActive.Checked, Permissions: _GetNewUserPermissions()
                 );
            }
        }

        private void _SetInfoAfterAddition(clsUser NewUserInfo, object[] NewDetails)
        {
            lblUserID.Text = NewUserInfo.UserID.ToString();
            _SetTitles(clsUser.enMode.Update);

            AfterSavingNewInfo?.Invoke(NewDetails, NewUserInfo.UserID, NewUserInfo.UsernameIV);
            _User = NewUserInfo;

            _IsUpdateDirectlyAfterAddition = true;
        }

        private void _SetInfoAfterEditingInfo(clsUser NewUserInfo, object[] NewDetails)
        {
            if (_IsUpdateDirectlyAfterAddition)
                _DGVRowIndex = 0;

            if (_User.HasUsernameChanged())
            {
                AfterSavingEditedInfo.Invoke(NewDetails, _DGVRowIndex, NewUserInfo.UserID, NewUserInfo.UsernameIV, null);
            }
            else
                AfterSavingEditedInfo?.Invoke(NewDetails, _DGVRowIndex, NewUserInfo.UserID, null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsUser NewUserInfo;
            _SetUserInfo(out NewUserInfo);

            if (_User != null)
            {
                if (_User.AreAllFieldsOldValuesNotChanged())
                {
                    MessageBox.Show("There is'nt any change on user information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            
            if(NewUserInfo.Save())
            {
                object[] NewDetails = new object[]
                {
                    NewUserInfo.UserID, _PersonID, clsPerson.GetFullName(_PersonID), txtUserName.Text, chkIsActive.Checked, _GetNewUserPermissions()
                };

                if (_User == null)
                {
                    _SetInfoAfterAddition(NewUserInfo, NewDetails);
                }

                else
                {
                    _SetInfoAfterEditingInfo(NewUserInfo, NewDetails);
                }

                MessageBox.Show("Data Saved successfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _CurrentUserFullName = clsPerson.GetFullName(_PersonID);

                OnAddedOrEditedUserInfo?.Invoke();

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

        private void frmAddEditUserInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            string UserFullName = clsPerson.GetFullName(_PersonID);
            
            if (_CurrentUserFullName != UserFullName && _CurrentUserFullName != null)
            {
                object[] ModifiedDetails = null;
                AfterSavingEditedInfo?.Invoke(ModifiedDetails, _DGVRowIndex, _User.UserID, null, UserFullName);
            }
        }

        private void uctrlpersonInfoByFilter_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnEditedUserPersonalInfo?.Invoke(UpdatedPersonInfo, _DGVRowIndex);
        }
    }
}