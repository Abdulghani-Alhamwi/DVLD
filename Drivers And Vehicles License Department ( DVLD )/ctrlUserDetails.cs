using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class ctrlUserDetails : UserControl
    {
        public ctrlUserDetails()
        {
            InitializeComponent();
        }

        private void _ShowLoginInformation(clsUser User)
        {
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = clsUtility.DecryptUserName(User.UserName);
            lblIsActive.Text = (User.IsActive) ? "Yes" : "No";
        }

        public bool LoadUserInformation(int UserID)
        {
            clsUser User = clsUser.Find(UserID);

            if (User != null)
            {
                uctrlPersonDetails.LoadPersonDetails(User.PersonID);
                _ShowLoginInformation(User);
                return true;
            }
            else
                MessageBox.Show("User is not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }
    }
}
