using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        bool _CanMoveToNextTab;
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_CanMoveToNextTab)
            tcAddNewUser.SelectedTab = tpLoginInfo;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tcAddNewUser_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tcAddNewUser.SelectedTab == tpLoginInfo && _CanMoveToNextTab)
                    tcAddNewUser.SelectedTab = tpPersonalInfo;
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

            if (uctrlpersonInfoByFilter.NationalNo != "")
                User.PersonID = clsPerson.GetPersonIDByNationalNo(uctrlpersonInfoByFilter.NationalNo);

            else
                User.PersonID = uctrlpersonInfoByFilter.PersonID;

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

        private void uctrlpersonInfoByFilter_OnPersonSelected()
        {
            _CanMoveToNextTab = true;
        }
    }
}
