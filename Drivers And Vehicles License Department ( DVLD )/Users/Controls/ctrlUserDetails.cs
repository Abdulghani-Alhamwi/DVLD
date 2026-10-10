using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlUserDetails : UserControl
    {
        public event ctrlPersonDetails.PersonEditedInfo OnPersonEditedInfo;

        public ctrlUserDetails()
        {
            InitializeComponent();
        }

        private void _ShowLoginInformation(clsUser User)
        {
            lblUserID.Text = User.UserID.ToString();

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(User.UserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, User.Username, User.UsernameIV);

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

        private void uctrlPersonDetails_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnPersonEditedInfo?.Invoke(UpdatedPersonInfo);
        }
    }
}
