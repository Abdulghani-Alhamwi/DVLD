using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsGeneralUtility;

namespace DVLDPresentationLayer.LocalDrivingLicenseApplications
{
    public partial class frmIssueDrivingLicense : Form
    {
        internal event Action<int> AfterLicenseIssuance;
        public event Action<clsPerson, int> OnEditedApplicantPersonalInfo;

        clsLocalLicense _LocalLicense;
        int _DGVRowIndex;

        public frmIssueDrivingLicense(int LDLAppID,int _DGVRowIndex)
        {
            InitializeComponent();

            uctrlDLApplicationInfo.LoadInfoByLocalDrivingLicenseAppID(LDLAppID);
            lblLicenseFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsLicenseClass.GetLicenseClassFees(uctrlDLApplicationInfo.LocalDrivingLicenseApplication.LicenseClassInfo.ID),
                enCustomNumberFormat.NoJustZerosAfterFraction);
            this._DGVRowIndex = _DGVRowIndex;
        }

        private bool _IssueLicense()
        {
          _LocalLicense = new clsLocalLicense(
             ApplicationID: uctrlDLApplicationInfo.LocalDrivingLicenseApplication.ApplicationID,
             PersonID: uctrlDLApplicationInfo.LocalDrivingLicenseApplication.ApplicantPersonID,
             LicenseClassID: uctrlDLApplicationInfo.LocalDrivingLicenseApplication.LicenseClassInfo.ID,
             IssueDate: DateTime.Now,
             ExpirationDate: DateTime.Now.AddYears(clsLicenseClass.GetLicenseValidityLength(uctrlDLApplicationInfo.LocalDrivingLicenseApplication.LicenseClassInfo.ID)),
             Notes: (txtNotes.Text != "") ? txtNotes.Text : null,
             PaidFees: clsLicenseClass.GetLicenseClassFees(uctrlDLApplicationInfo.LocalDrivingLicenseApplication.LicenseClassInfo.ID),
             IsActive: true,
             IssueReason: clsLocalLicense.enIssueReason.FirstTime,
             CreatedByUserID: clsGlobalSettings.CurrentUserID
          );

            return _LocalLicense.Save();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (_IssueLicense())
            {
                AfterLicenseIssuance?.Invoke(_DGVRowIndex);
                MessageBox.Show($"License Issued Successfully With License ID = {_LocalLicense.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uctrlDLApplicationInfo.ShowLicenseInfoLabel(_LocalLicense.LicenseID);

                btnIssueLicense.Enabled = false;
            }
            else
                MessageBox.Show("Failed to issue license!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
