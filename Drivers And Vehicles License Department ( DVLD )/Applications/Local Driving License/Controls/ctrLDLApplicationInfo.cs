using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class ctrLDLApplicationInfo : UserControl
    {
        public event ctrlPersonCard.PersonEditedInfo OnPersonEditedInfo;

        private int _LocalLicenseID = -1;

        public clsLocalDrivingLicenseApplication LDLApplication;

        public ctrLDLApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadInfoByLocalDrivingLicenseAppID(int LDLApplicationID)
        {
            LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LDLApplicationID);

            if (LDLApplication != null)
            {
                uctrlApplicationBasicInfo.LoadInfo(LDLApplication);
                lblLDLApplicationID.Text = LDLApplication.LocalDrivingLicenseApplicationID.ToString();
                lblLicenseClassName.Text = LDLApplication.LicenseClassInfo.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetTotalPassedTests(LDLApplication.LocalDrivingLicenseApplicationID).ToString();
            }
        }

        public void LoadInfoByApplicationID(int ApplicationID)
        {
            LDLApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (LDLApplication != null)
            {
                uctrlApplicationBasicInfo.LoadInfo(LDLApplication);
                lblLDLApplicationID.Text = LDLApplication.LocalDrivingLicenseApplicationID.ToString();
                lblLicenseClassName.Text = LDLApplication.LicenseClassInfo.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetTotalPassedTests(LDLApplication.LocalDrivingLicenseApplicationID).ToString();
            }
        }

        public void ShowLicenseInfoLabel(int LocalLicenseID)
        {
            _LocalLicenseID = LocalLicenseID;
            lnlblShowLicenseInfo.Visible = true;
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_LocalLicenseID);
            frm.ShowDialog();
        }

        private void ctrlApplicationBasicInfo1_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnPersonEditedInfo?.Invoke(UpdatedPersonInfo);
        }
    }
}