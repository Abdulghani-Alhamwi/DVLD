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

            uctrlDLApplicationInfo.LoadLDLAppInfo(LDLAppID);
            lblLicenseFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsLicenseClass.GetLicenseClassFees(uctrlDLApplicationInfo.LDLApplication.LicenseClass.ID),
                enCustomNumberFormat.NoJustZerosAfterFraction);
            this._DGVRowIndex = _DGVRowIndex;
        }

        private bool _SaveLicenseInfo(clsLocalDrivingLicenseApp LDLApplication ,int DriverID)
        {
            _LocalLicense = new clsLocalLicense(
            ApplicationID: LDLApplication.ApplicationID,
            DriverID: DriverID,
            LicenseClassID: LDLApplication.LicenseClass.ID,
            IssueDate: DateTime.Now,
            ExpirationDate: DateTime.Now.AddYears(clsLicenseClass.GetLicenseValidityLength(LDLApplication.LicenseClass.ID)),
            Notes: (txtNotes.Text != "") ? txtNotes.Text : null,
            PaidFees: clsLicenseClass.GetLicenseClassFees(LDLApplication.LicenseClass.ID),
            IsActive: true,
            IssueReason: clsLocalLicense.enIssueReason.FirstTime,
            CreatedByUserID: clsGlobalSettings.CurrentUserID
            );

            return _LocalLicense.Save();
        }

        private bool _IssueLicense()
        {
            if (!clsDriver.IsPersonAlreadyADriver(uctrlDLApplicationInfo.LDLApplication.ApplicantPersonID))
            { 
            clsDriver Driver = new clsDriver(uctrlDLApplicationInfo.LDLApplication.ApplicantPersonID, clsGlobalSettings.CurrentUserID, DateTime.Now);
            if(Driver.Save())
            {
                    return _SaveLicenseInfo(uctrlDLApplicationInfo.LDLApplication,Driver.DriverID);
            }
            else
            {
                MessageBox.Show("Failed to add driver!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            }
            else
                return _SaveLicenseInfo(uctrlDLApplicationInfo.LDLApplication,clsDriver.GetDriverID(uctrlDLApplicationInfo.LDLApplication.ApplicantPersonID));
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (_IssueLicense())
            {
                AfterLicenseIssuance?.Invoke(_DGVRowIndex);
                MessageBox.Show($"License Issued Successfully With License ID = {_LocalLicense.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                uctrlDLApplicationInfo.ShowLicenseInfoLabel(_LocalLicense.LicenseID);
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
