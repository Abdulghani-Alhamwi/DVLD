using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class ctrLDLApplicationDetails : UserControl
    {
        public event ctrlPersonDetails.PersonEditedInfo OnPersonEditedInfo;

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
                uctrlApplicationBasicInfo.LoadInfo(LDLApplication);
                lblLDLApplicationID.Text = LDLApplication.LDLAppID.ToString();
                lblLicenseClassName.Text = LDLApplication.LicenseClass.ClassName;
                lblPassedTests.Text = clsLocalDrivingLicenseApp.GetPassedTests(LDLApplication.LDLAppID).ToString();
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