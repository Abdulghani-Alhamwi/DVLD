using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsUtility;

namespace DVLDPresentationLayer
{
    public partial class ctrlDLApplicationInfo : UserControl
    {
        public ctrlDLApplicationInfo()
        {
            InitializeComponent();
        }

        int _PersonID = -1;

        public clsLDLApplication LDLApplication;
        public void LoadLDLAppInfo(int LDLAppID)
        {
            LDLApplication = clsLDLApplication.Find(LDLAppID);

            if (LDLApplication != null)
            {
                lblLDLApplicationID.Text = LDLApplication.LDLAppID.ToString();
                lblLicenseClassName.Text = LDLApplication.LicenseClass.ClassName;
                lblPassedTests.Text = clsLDLApplication.GetPassedTests(LDLApplication.LDLAppID).ToString();

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

        private void lnlblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID != -1)
            {
                frmPersonDetails frm = new frmPersonDetails(_PersonID);
                frm.ShowDialog();
            }
        }
    }
}
