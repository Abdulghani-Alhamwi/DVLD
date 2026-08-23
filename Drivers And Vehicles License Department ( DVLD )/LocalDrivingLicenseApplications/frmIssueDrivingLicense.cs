using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer.LocalDrivingLicenseApplications
{
    public partial class frmIssueDrivingLicense : Form
    {
        internal event Action<int> AfterLicenseIssuance;

        clsLicense _License;

        int _LDLAppDGVRowIndex;
        public frmIssueDrivingLicense(int LDLAppID,int DGVRowIndex)
        {
            InitializeComponent();
            uctrlDLApplicationInfo.LoadInfo(LDLAppID);
            lblLicenseFees.Text = Convert.ToSingle(clsLicenseClass.GetLicenseClassFees(uctrlDLApplicationInfo.LDLApplication.LicenseClass.ID)).ToString();
            _LDLAppDGVRowIndex = DGVRowIndex;
        }

        private bool _SaveLicenseInfo(clsLDLApplication LDLApplication ,int DriverID)
        {
                  _License = new clsLicense(
                  ApplicationID: LDLApplication.ApplicationID,
                  DriverID: DriverID,
                  LicenseClassID: LDLApplication.LicenseClass.ID,
                  IssueDate: DateTime.Now,
                  ExpirationDate: DateTime.Now.AddYears(clsLicenseClass.GetLicenseValidityLength(LDLApplication.LicenseClass.ID)),
                  Notes: (txtNotes.Text != "") ? txtNotes.Text : null,
                  PaidFees: clsLicenseClass.GetLicenseClassFees(LDLApplication.LicenseClass.ID),
                  IsActive: true,
                  IssueReason: clsLicense.enIssueReason.FirstTime,
                  CreatedByUserID: clsGlobalSettings.CurrentUserID
                  );

            return _License.Save();
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
                AfterLicenseIssuance?.Invoke(_LDLAppDGVRowIndex);
                MessageBox.Show($"License Issued Successfully With License ID = {_License.LicenseID}", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
