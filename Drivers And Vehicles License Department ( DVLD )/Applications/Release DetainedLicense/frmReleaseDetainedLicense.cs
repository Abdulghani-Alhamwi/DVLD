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

        private clsLocalLicense _SelectedLocalLicenseInfo;
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

        private bool _AddLocalLicenseInfo(int LocalLicenseID)
        {
            uctrlLDLDetailsByFilter.txtLicenseID.Text = LocalLicenseID.ToString();
            lblLicenseID.Text = LocalLicenseID.ToString();
            clsLocalLicense LocalLicense = clsLocalLicense.Find(LocalLicenseID);

            if (LocalLicense != null)
            {
                _SelectedLocalLicenseInfo = LocalLicense;
                uctrlLDLDetailsByFilter.uctrlLDLDetails.LoadDriverLicenseInfo(LocalLicenseID);
                return true;
            }

            else
                return false;
        }

        private void _InitilizeFormData(int LocalLicenseID = -1)
          {
            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
            
            if (LocalLicenseID != -1)
               {
                if (_AddLocalLicenseInfo(LocalLicenseID))
               {
                    lnlblShowLicenseHistory.Enabled = true;
                    
                    if (_SelectedLocalLicenseInfo.IsDetained)
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
                
               uctrlLDLDetailsByFilter.gbFilter.Enabled = false; 
           }

        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLDLDetailsByFilter.uctrlLDLDetails.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo,int DGVRowIndex)
        {
            _EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
            OnUpdatedDriverPersonalInfo?.Invoke(UpdatedDriverPersonalInfo, DGVRowIndex);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = frmDetainLocalLicense.GetfrmDriverLicensesHistoryObj(_DGVDetainedLicenses,_SelectedLocalLicenseInfo, _DGVLocalLicenesColumnName);

            if (_DGVDetainedLicenses != null)
                frm.OnEditedDriverPersonalInfo += _EditDriverPersonalInfo;

            else
                frm.OnUpdatedDriverInfo += _EditDriverPersonalInfo;

            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_SelectedLocalLicenseInfo.LicenseID);
            frm.ShowDialog();
        }

        private void _ShowDetainInfo()
        {
            lblDetainID.Text = _SelectedLocalLicenseInfo.DetainInfo.DetainID.ToString();
            lblDetainedDate.Text = _SelectedLocalLicenseInfo.DetainInfo.DetainDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(_SelectedLocalLicenseInfo.DetainInfo.CreatedByUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);

            lblApplicationFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.ReleaseDetainedLicense),clsGeneralUtility.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblFineFees.Text = clsGeneralUtility.GetCustomNumberFormat(_SelectedLocalLicenseInfo.DetainInfo.FineFees, clsGeneralUtility.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblFineFees.Text)).ToString();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(clsLocalLicense LicenseInfo)
        {
            if (!clsDetainedLicense.IsDetainedLicense(LicenseInfo.LicenseID))
            {
                MessageBox.Show("Selected license is not detained , choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (LicenseInfo.DetainInfo != null)
                {
                    _ShowDetainInfo();
                    _SelectedLocalLicenseInfo = LicenseInfo;
                    btnRelease.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Detained license is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lnlblShowLicenseHistory.Enabled = true;
                }
            }
            lblLicenseID.Text = LicenseInfo.LicenseID.ToString();

            lnlblShowLicenseHistory.Enabled = true;
        }

        private void _ReleaseDetainedLicense()
        {
            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to release this detained license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.No)
            {
                return;
            }

            if (_SelectedLocalLicenseInfo.ReleaseDetainedLicense(clsGlobalSettings.CurrentUserID))
            {
                lblReleaseApplicationID.Text = _SelectedLocalLicenseInfo.DetainInfo.ReleaseApplicationID.ToString();
                MessageBox.Show($"Detained License Released Successfully", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                AfterReleasingSentLicense?.Invoke(_SelectedLocalLicenseInfo.DetainInfo.ReleaseApplicationID, _SelectedLocalLicenseInfo.DetainInfo.ReleaseDate, _DetainedLicensesDgvRowIndex);

                if (AfterReleasingALicense != null)
                {
                    Thread NewThread = new Thread(() => AfterReleasingALicense.Invoke(_SelectedLocalLicenseInfo.DetainInfo.DetainID, _SelectedLocalLicenseInfo.DetainInfo.ReleaseDate, _SelectedLocalLicenseInfo.DetainInfo.ReleaseApplicationID));
                    NewThread.Start();
                }

                uctrlLDLDetailsByFilter.gbFilter.Enabled = false;
                btnRelease.Enabled = false;
                lnlblShowLicenseInfo.Enabled = true;
            }
            else
                MessageBox.Show("Failed To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (_SelectedLocalLicenseInfo == null && _DetainedLicensesDgvRowIndex == -1)
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