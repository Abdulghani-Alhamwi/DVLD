using System;
using System.Threading;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Controls;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public event Action<int,DateTime,int> AfterReleasingSentLicense;
        public event Action<int,DateTime,int> AfterReleasingALicense;
        public event Action<clsPerson, int> OnUpdatedDriverPersonalInfo;

        private DataGridView _DGVDetainedLicenses;
        private string _DGVLocalLicenesColumnName;

        private int _DetainedLicensesDgvRowIndex;

        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            _InitilizeFormData();
        }

        public frmReleaseDetainedLicense(DataGridView DGVDetainedLicenses, string DGVLocalLicenesColumnName)
        {
            InitializeComponent();
            _InitilizeFormData();
            _DGVDetainedLicenses = DGVDetainedLicenses;
            _DGVLocalLicenesColumnName = DGVLocalLicenesColumnName;
        }

        public frmReleaseDetainedLicense(int LocalLicenseID , int DetainedLicensesDgvRowIndex)
        {
            InitializeComponent();
            _InitilizeFormData(LocalLicenseID);
            _DetainedLicensesDgvRowIndex = DetainedLicensesDgvRowIndex;
        }

   
        private void _InitilizeFormData(int LocalLicenseID = -1)
          {
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);

            if (LocalLicenseID != -1)
            {
                lblLicenseID.Text = LocalLicenseID.ToString();
                uctrlLDLDetailsByFilter.LoadInfo(LocalLicenseID);

                if (uctrlLDLDetailsByFilter.SelectedLicenseInfo != null)
                {
                    lnlblShowLicenseHistory.Enabled = true;

                    if (uctrlLDLDetailsByFilter.SelectedLicenseInfo.IsDetained)
                    {
                        _ShowDetainInfo();
                        btnRelease.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Detain info is not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnRelease.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Local license info is not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRelease.Enabled = false;
                }

                uctrlLDLDetailsByFilter.FilterEnabled = false;
            }
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLDLDetailsByFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo,int DGVRowIndex)
        {
            _EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
            OnUpdatedDriverPersonalInfo?.Invoke(UpdatedDriverPersonalInfo, DGVRowIndex);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = frmDetainLocalLicense.GetfrmDriverLicensesHistoryObj(_DGVDetainedLicenses, uctrlLDLDetailsByFilter.SelectedLicenseInfo, _DGVLocalLicenesColumnName);

            if (_DGVDetainedLicenses != null)
                frm.OnEditedDriverPersonalInfo += _EditDriverPersonalInfo;

            else
                frm.OnUpdatedDriverInfo += _EditDriverPersonalInfo;

            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLiecnseInfo frm = new frmShowDriverLiecnseInfo(uctrlLDLDetailsByFilter.SelectedLicenseInfo.LicenseID);
            frm.ShowDialog();
        }

        private void _ShowDetainInfo()
        {
            lblDetainID.Text = uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.DetainID.ToString();
            lblDetainedDate.Text = uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.DetainDate.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName));

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.CreatedByUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);

            lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.ReleaseDetainedLicense), clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblFineFees.Text = clsFormat.GetCustomNumberFormat(uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.FineFees, clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblFineFees.Text)).ToString();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LicenseID)
        {
            if (LicenseID != -1)
            {
                if (!clsDetainedLicense.IsDetainedLicense(uctrlLDLDetailsByFilter.SelectedLicenseInfo.LicenseID))
                {
                    MessageBox.Show("Selected license is not detained , choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    if (uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo != null)
                    {
                        _ShowDetainInfo();
                        btnRelease.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Detained license is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lnlblShowLicenseHistory.Enabled = true;
                    }
                }
                lblLicenseID.Text = uctrlLDLDetailsByFilter.SelectedLicenseInfo.LicenseID.ToString();

                lnlblShowLicenseHistory.Enabled = true;
            }
        }

        private void _ReleaseDetainedLicense()
        {
            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to release this detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.No)
            {
                return;
            }

            if (uctrlLDLDetailsByFilter.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobalSettings.CurrentUserID))
            {
                lblReleaseApplicationID.Text = uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.ReleaseApplicationID.ToString();
                MessageBox.Show($"Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AfterReleasingSentLicense?.Invoke(uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.ReleaseApplicationID, uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.ReleaseDate, _DetainedLicensesDgvRowIndex);

                if (AfterReleasingALicense != null)
                {
                    Thread NewThread = new Thread(() => AfterReleasingALicense.Invoke(uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.DetainID, uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.ReleaseDate
                        , uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.ReleaseApplicationID));
                    NewThread.Start();
                }

                uctrlLDLDetailsByFilter.FilterEnabled = false;
                btnRelease.Enabled = false;
                lnlblShowLicenseInfo.Enabled = true;
            }
            else
                MessageBox.Show("Failed To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (uctrlLDLDetailsByFilter.SelectedLicenseInfo == null && _DetainedLicensesDgvRowIndex == -1)
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
    }
}