using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class ctrLDLApplicationInfo : UserControl
    {
        public event ctrlPersonCard.PersonEditedInfo OnPersonEditedInfo;

        private int _LocalLicenseID = -1;

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;

        public ctrLDLApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadInfoByLocalDrivingLicenseAppID(int LocalDrivingLicenseApplicationID)
        {
            this.LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);

            if (this.LocalDrivingLicenseApplication != null)
            {
                uctrlApplicationBasicInfo.LoadInfo(this.LocalDrivingLicenseApplication);
                lblLDLApplicationID.Text = this.LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblLicenseClassName.Text = this.LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetTotalPassedTests(this.LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID).ToString();
            }
        }

        public void LoadInfoByApplicationID(int ApplicationID)
        {
            LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (LocalDrivingLicenseApplication != null)
            {
                uctrlApplicationBasicInfo.LoadInfo(LocalDrivingLicenseApplication);
                lblLDLApplicationID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblLicenseClassName.Text = LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApplication.GetTotalPassedTests(LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID).ToString();
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