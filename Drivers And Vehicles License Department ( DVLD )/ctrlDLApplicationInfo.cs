using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class ctrlDLApplicationInfo : UserControl
    {
        public ctrlDLApplicationInfo()
        {
            InitializeComponent();
        }


        private void _LoadInfo(int LDLApplicationID)
        {
            clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.Find(LDLApplicationID);

            if (LDLApplication != null)
            {
                lblApplicationID.Text = LDLApplication.LDLApplicationID.ToString();
                lblLicenseClassName.Text = LDLApplication.LicenseClass.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetPassedTests(LDLApplication.LDLApplicationID).ToString();

                lblApplicationID.Text = LDLApplication.ApplicationID.ToString();
                lblStatus.Text = LDLApplication.GetApplicationStatus();
                lblFees.Text = LDLApplication.PaidApplicationFees.ToString();
                
            }
        }

    }
}
