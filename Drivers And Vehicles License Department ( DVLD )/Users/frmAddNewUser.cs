using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmAddNewUser : Form
    {
        public frmAddNewUser()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(uctrlpersonInfoByFilter.FindPerson())
            tcAddNewUser.SelectedTab = tpLoginInfo;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tcAddNewUser_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tcAddNewUser.SelectedTab == tpLoginInfo)
                if (!uctrlpersonInfoByFilter.FindPerson())
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
    }
}
