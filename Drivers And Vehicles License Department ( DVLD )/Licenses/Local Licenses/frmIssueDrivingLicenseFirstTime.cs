using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsUtility;

namespace DVLDPresentationLayer.LocalDrivingLicenseApplications
{
    public partial class frmIssueDrivingLicenseFirstTime : Form
    {
        internal event Action<int> AfterLicenseIssuance;
        public event Action<clsPerson, int> OnEditedApplicantPersonalInfo;

        private int _LocalDrivingLicenseApplicationID;
        private int _DGVRowIndex;

        public frmIssueDrivingLicenseFirstTime(int LocalDrivingLicenseApplicationID,int _DGVRowIndex)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this._DGVRowIndex = _DGVRowIndex;
        }

        private void frmIssueDrivingLicenseFirstTime_Load(object sender, EventArgs e)
        {
            uctrlDLApplicationInfo.LoadInfoByLocalDrivingLicenseAppID(_LocalDrivingLicenseApplicationID);

            if (uctrlDLApplicationInfo.LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Local Driving License Application with ID : " + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            if (!uctrlDLApplicationInfo.LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Tests First", "Not Allowed"
                   , MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            int LicenseID = uctrlDLApplicationInfo.LocalDrivingLicenseApplication.GetActiveLicenseID();

            if (LicenseID != -1)
            {
                MessageBox.Show("Person already has license before with license ID : " + LicenseID.ToString(), "Not Allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
                return;
            }

            lblLicenseFees.Text = clsFormat.GetCustomNumberFormat(uctrlDLApplicationInfo.LocalDrivingLicenseApplication.LicenseClassInfo.ClassFees,
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
        }


        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int NewLicenseID = uctrlDLApplicationInfo.LocalDrivingLicenseApplication.IssueDrivingLicenseForTheFirstTime(txtNotes.Text.Trim(), clsGlobalSettings.CurrentUserID);

            if (NewLicenseID != -1)
            {
                AfterLicenseIssuance?.Invoke(_DGVRowIndex);
                MessageBox.Show($"License Issued Successfully With License ID = {NewLicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uctrlDLApplicationInfo.ShowLicenseInfoLabel(NewLicenseID);

                btnIssueLicense.Enabled = false;
            }
            else
                MessageBox.Show("License Was not Issued !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uctrlDLApplicationInfo_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnEditedApplicantPersonalInfo?.Invoke(UpdatedPersonInfo, _DGVRowIndex);
        }

    }
}
