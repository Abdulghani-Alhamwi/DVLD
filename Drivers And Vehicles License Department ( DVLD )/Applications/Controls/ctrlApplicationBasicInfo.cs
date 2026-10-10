using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsGeneralUtility;

namespace DVLDPresentationLayer
{

    public partial class ctrlApplicationBasicInfo : UserControl
    {
        public event ctrlPersonDetails.PersonEditedInfo OnPersonEditedInfo;

        private int _PersonID;

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void _EditPersonName(clsPerson UpdatedPersonInfo)
        {
            lblApplicantFullName.Text = UpdatedPersonInfo.FullName;
            OnPersonEditedInfo?.Invoke(UpdatedPersonInfo);
        }

        public void LoadInfo(clsLocalDrivingLicenseApp LDLApplication)
        {
            lblApplicationID.Text = LDLApplication.ApplicationID.ToString();
            lblStatus.Text = LDLApplication.GetApplicationStatus();
            lblPaidFees.Text = clsGeneralUtility.GetCustomNumberFormat(LDLApplication.PaidApplicationFees, enCustomNumberFormat.NoJustZerosAfterFraction);
            lblApplicationType.Text = clsApplicationType.GetApplicationTypeTitle(LDLApplication.ApplicationTypeID);
            lblApplicantFullName.Text = clsPerson.GetFullName(LDLApplication.ApplicantPersonID);
            lblApplicationDate.Text = LDLApplication.ApplicationDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblLastStatusDate.Text = LDLApplication.LastStatusDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(LDLApplication.CreatedByUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);

            _PersonID = LDLApplication.ApplicantPersonID;
        }

        private void lnlblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID != -1)
            {
                frmPersonDetails frm = new frmPersonDetails(_PersonID);
                frm.OnUpdatedPersonInfo += _EditPersonName;
                frm.ShowDialog();
            }
        }
    }
}
