using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrLDLApplicationDetails : UserControl
    {
        public ctrLDLApplicationDetails()
        {
            InitializeComponent();
        }

        int _PersonID = -1;
        int _LocalLicenseID = -1;

        public clsLocalDrivingLicenseApp LDLApplication;
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
                lblPaidFees.Text = clsUtility.SetFeesToCustomFormat(LDLApplication.PaidApplicationFees);
                lblApplicationType.Text = clsApplicationType.GetApplicationTypeTitle(LDLApplication.ApplicationTypeID);
                lblApplicantFullName.Text = clsPerson.GetFullName(LDLApplication.ApplicantPersonID);
                lblApplicationDate.Text = LDLApplication.ApplicationDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblLastStatusDate.Text = LDLApplication.LastStatusDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblUserName.Text = clsUtility.DecryptUserName(clsUser.GetUserName(LDLApplication.CreatedByUserID));
                _PersonID = LDLApplication.ApplicantPersonID;
            }
        }

        public void ShowLicenseInfoLabel(int LocalLicenseID)
        {
            _LocalLicenseID = LocalLicenseID;
            lnlblShowLicenseInfo.Visible = true;
        }

        private void lnlblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID != -1)
            {
                frmPersonDetails frm = new frmPersonDetails(_PersonID);
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