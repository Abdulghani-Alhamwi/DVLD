using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsGeneralUtility;

namespace DVLDPresentationLayer
{
    public partial class ctrLDLApplicationDetails : UserControl
    {
        public event ctrlPersonDetails.PersonEditedInfo OnPersonEditedInfo;

        private int _PersonID = -1;
        private int _LocalLicenseID = -1;

        public clsLocalDrivingLicenseApp LDLApplication;

        public ctrLDLApplicationDetails()
        {
            InitializeComponent();
        }

        public void LoadLDLAppInfo(int LDLAppID)
        {
            LDLApplication = clsLocalDrivingLicenseApp.Find(LDLAppID);

            if (LDLApplication != null)
            {
                lblLDLApplicationID.Text = LDLApplication.LDLAppID.ToString();
                lblLicenseClassName.Text = LDLApplication.LicenseClass.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApp.GetPassedTests(LDLApplication.LDLAppID).ToString();

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
        }

        public void ShowLicenseInfoLabel(int LocalLicenseID)
        {
            _LocalLicenseID = LocalLicenseID;
            lnlblShowLicenseInfo.Visible = true;
        }

        private void _EditPersonName(clsPerson UpdatedPersonInfo)
        {
            lblApplicantFullName.Text = UpdatedPersonInfo.FullName;
            OnPersonEditedInfo?.Invoke(clsPerson.Find(LDLApplication.ApplicantPersonID));
        }

        private void lnlblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID != -1)
            {
                frmPersonDetails frm = new frmPersonDetails(_PersonID);
                frm.OnUpdatedPersonName += _EditPersonName;
                frm.ShowDialog();
            }
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_LocalLicenseID);
            frm.ShowDialog();
        }
    }
}