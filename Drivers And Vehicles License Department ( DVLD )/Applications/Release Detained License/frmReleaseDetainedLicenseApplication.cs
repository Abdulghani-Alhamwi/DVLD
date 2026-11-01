using System;
using System.Threading;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Controls;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        public event Action<int,DateTime,int> AfterReleasingSentLicense;
        public event Action<int,DateTime,int> AfterReleasingALicense;
        public event Action<clsPerson, int> OnUpdatedDriverPersonalInfo;

        private DataGridView _DGVDetainedLicenses;
        private string _DGVLocalLicenesColumnName;

        private int _DetainedLicensesDgvRowIndex;

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicenseApplication(DataGridView DGVDetainedLicenses, string DGVLocalLicenesColumnName)
        {
            InitializeComponent();
            _DGVDetainedLicenses = DGVDetainedLicenses;
            _DGVLocalLicenesColumnName = DGVLocalLicenesColumnName;
        }

        public frmReleaseDetainedLicenseApplication(int LocalLicenseID , int DetainedLicensesDgvRowIndex)
        {
            InitializeComponent();
            uctrlLocalDrivingLicenseInfoWithFilter.LoadInfo(LocalLicenseID);
            uctrlLocalDrivingLicenseInfoWithFilter.FilterEnabled = false;
            _DetainedLicensesDgvRowIndex = DetainedLicensesDgvRowIndex;
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLocalDrivingLicenseInfoWithFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo,int DGVRowIndex)
        {
            _EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
            OnUpdatedDriverPersonalInfo?.Invoke(UpdatedDriverPersonalInfo, DGVRowIndex);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = frmDetainLocalLicense.GetfrmDriverLicensesHistoryObj(_DGVDetainedLicenses, uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo, _DGVLocalLicenesColumnName);

            if (_DGVDetainedLicenses != null)
                frm.OnEditedDriverPersonalInfo += _EditDriverPersonalInfo;

            else
                frm.OnUpdatedDriverInfo += _EditDriverPersonalInfo;

            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLiecnseInfo frm = new frmShowDriverLiecnseInfo(uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.LicenseID);
            frm.ShowDialog();
        }

        private void _ShowDetainInfo()
        {
            lblDetainID.Text = uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.DetainID.ToString();
            lblDetainedDate.Text = uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.DetainDate.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName));

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.CreatedByUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);

            lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.ReleaseDetainedLicense), clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblFineFees.Text = clsFormat.GetCustomNumberFormat(uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.FineFees, clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblFineFees.Text)).ToString();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LicenseID)
        {
            lblLicenseID.Text = LicenseID.ToString();

            lnlblShowLicenseHistory.Enabled = (LicenseID != -1);

            if (LicenseID == -1)
            {
                btnRelease.Enabled = false;
                return;
            }

            if (!uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected license is not detained , choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ShowDetainInfo();
            btnRelease.Enabled = true;
        }

        private void _ReleaseDetainedLicense()
        {
            if (MessageBox.Show("Are you sure you want to release this detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplicationID = -1;
            bool IsReleased = uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobalSettings.CurrentUserID, ref ApplicationID);

            if (!IsReleased)
            {
                MessageBox.Show("Failed To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblReleaseApplicationID.Text = ApplicationID.ToString();
            MessageBox.Show($"Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            AfterReleasingSentLicense?.Invoke(uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.ReleaseApplicationID, uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.ReleaseDate, _DetainedLicensesDgvRowIndex);

            if (AfterReleasingALicense != null)
            {
                AfterReleasingALicense.Invoke(uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.DetainID, uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.ReleaseDate
                     , uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo.DetainInfo.ReleaseApplicationID);
            }

            btnRelease.Enabled = false;
            uctrlLocalDrivingLicenseInfoWithFilter.FilterEnabled = false;
            lnlblShowLicenseInfo.Enabled = true;
        }
        
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (uctrlLocalDrivingLicenseInfoWithFilter.SelectedLicenseInfo == null && _DetainedLicensesDgvRowIndex == -1)
            {
                MessageBox.Show("Enter local license ID First in order to release detained license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _ReleaseDetainedLicense();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }
    }
}