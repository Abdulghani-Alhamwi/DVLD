using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer.Controls
{
    public partial class frmDetainLocalLicense : Form
    {
        public event Action<object[]> AfterDetainingLicense;
        public event Action<clsPerson, int> OnUpdatedDriverPersonalInfo;

        private DataGridView _DGVDetainedLicenses;
        private string _DGVLocalLicenesColumnName;

        public frmDetainLocalLicense()
        {
            InitializeComponent();
            _ShowBasicInfo();
        }

        public frmDetainLocalLicense(DataGridView DGVDetainedLicenses, string DGVLocalLicenesColumnName)
        {
            InitializeComponent();
            _ShowBasicInfo();
            _DGVDetainedLicenses = DGVDetainedLicenses;
            _DGVLocalLicenesColumnName = DGVLocalLicenesColumnName;
        }

        private void _ShowBasicInfo()
        {
            lblDetainedDate.Text = DateTime.Now.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName));

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void _DetainLicense()
        {
            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.No)
            {
                return;
            }

            int DetainID = uctrlLDLDetailsByFilter.SelectedLicenseInfo.Detain(Convert.ToDecimal(txtFineFees.Text.Trim()), clsGlobalSettings.CurrentUserID);

                if (DetainID != -1)
                {
                    lblDetainID.Text = DetainID.ToString();
                    MessageBox.Show($"License Detained Successfully With ID = {DetainID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    object[] DetainedLicenseInfo = new object[] { uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.DetainID , uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.LocalLicenseID, uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.DetainDate.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateTimeCustomFormat))
                        , uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.IsReleased,clsFormat.GetCustomNumberFormat(uctrlLDLDetailsByFilter.SelectedLicenseInfo.DetainInfo.FineFees, clsFormat.enCustomNumberFormat.With4ZerosAfterFraction),null,uctrlLDLDetailsByFilter.SelectedLicenseInfo.DriverInfo.PersonalInfo.NationalNo
                        ,uctrlLDLDetailsByFilter.SelectedLicenseInfo.DriverInfo.PersonalInfo.FullName, null};

                    AfterDetainingLicense?.Invoke(DetainedLicenseInfo);

                    btnDetain.Enabled = false;
                    lnlblShowLicenseInfo.Enabled = true;
                }
                else
                    MessageBox.Show("Failed To detaon license!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(txtFineFees.Text == "")
            {
                MessageBox.Show("You must enter the fine fees!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(Convert.ToDecimal(txtFineFees.Text) == 0)
            {
                if (MessageBox.Show("Are you sure that fine fees is = 0 ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            _DetainLicense();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LocalLicenseID)
        {
            if (LocalLicenseID != -1)
            {
                if (clsDetainedLicense.IsDetainedLicense(LocalLicenseID))
                {
                    MessageBox.Show("Selected license is already detained , choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDetain.Enabled = false;
                }

                else if (!uctrlLDLDetailsByFilter.SelectedLicenseInfo.IsActive)
                {
                    MessageBox.Show("Selected license is not active , choose an active local license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDetain.Enabled = false;
                }

                else
                {
                    btnDetain.Enabled = true;
                }
                lblLicenseID.Text = uctrlLDLDetailsByFilter.SelectedLicenseInfo.LicenseID.ToString();

                lnlblShowLicenseHistory.Enabled = true;
            }
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLiecnseInfo frm = new frmShowDriverLiecnseInfo(uctrlLDLDetailsByFilter.SelectedLicenseInfo.LicenseID);
            frm.ShowDialog();
        }
    
        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLDLDetailsByFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo, int DGVRowIndex)
        {
            _EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
            OnUpdatedDriverPersonalInfo?.Invoke(UpdatedDriverPersonalInfo, DGVRowIndex);
        }

        public static frmDriverLicenseHistory GetfrmDriverLicensesHistoryObj(DataGridView dgvDetainedLicenses,clsLocalLicense LocalLicenesInfo,string DGVLocalLicenesColumnName)
        {
            if (dgvDetainedLicenses != null)
            {
                return new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(LocalLicenesInfo.DriverID)
                       , clsDataGridViewUtilityLib.GetDGVRowIndexByColumnValue(dgvDetainedLicenses, DGVLocalLicenesColumnName, LocalLicenesInfo.LicenseID));
            }

            else
            {
                return new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(LocalLicenesInfo.DriverID));
            }
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = GetfrmDriverLicensesHistoryObj(_DGVDetainedLicenses, uctrlLDLDetailsByFilter.SelectedLicenseInfo, _DGVLocalLicenesColumnName);

            if (_DGVDetainedLicenses != null)
                frm.OnEditedDriverPersonalInfo += _EditDriverPersonalInfo;

            else
                frm.OnUpdatedDriverInfo += _EditDriverPersonalInfo;

            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
