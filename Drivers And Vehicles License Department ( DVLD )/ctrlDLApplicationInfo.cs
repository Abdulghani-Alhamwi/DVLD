using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class ctrlDLApplicationInfo : UserControl
    {
        public ctrlDLApplicationInfo()
        {
            InitializeComponent();
        }

        int _PersonID = -1;
        public void LoadInfo(int LDLApplicationID)
        {
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.Find(LDLApplicationID);

            if (LDLApplication != null)
            {
                lblLDLApplicationID.Text = LDLApplication.LDLApplicationID.ToString();
                lblLicenseClassName.Text = LDLApplication.LicenseClass.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetPassedTests(LDLApplication.LDLApplicationID).ToString();

                lblApplicationID.Text = LDLApplication.ApplicationID.ToString();
                lblStatus.Text = LDLApplication.GetApplicationStatus();
                lblPaidFees.Text = LDLApplication.PaidApplicationFees.ToString();
                lblApplicationType.Text = clsApplicationTypes.GetApplicationTypeTitle(LDLApplication.ApplicationTypeID);
                lblApplicantFullName.Text = clsPerson.GetFullName(LDLApplication.ApplicantPersonID);
                lblApplicationDate.Text = LDLApplication.ApplicationDate.ToShortDateString();
                lblLastStatusDate.Text = LDLApplication.LastStatusDate.ToShortDateString();
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
