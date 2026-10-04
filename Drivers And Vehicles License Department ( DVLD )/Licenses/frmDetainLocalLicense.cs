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

        private clsLocalLicense _LocalLicenseInfo;
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
            lblDetainedDate.Text = DateTime.Now.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void _DetainLicense()
        {
            clsDetainedLicense DetainedLicense = new clsDetainedLicense(_LocalLicenseInfo.LicenseID, DateTime.Now, Convert.ToDecimal(txtFineFees.Text), clsGlobalSettings.CurrentUserID);

            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.Yes)
            {
                if (DetainedLicense.Save())
                {
                    lblDetainID.Text = DetainedLicense.DetainID.ToString();
                    MessageBox.Show($"License Detained Successfully With ID = {DetainedLicense.DetainID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    object[] DetainedLicenseInfo = new object[] { DetainedLicense.DetainID , DetainedLicense.LocalLicenseID, DetainedLicense.DetainDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat))
                        , DetainedLicense.IsReleased,DetainedLicense.FineFees,null,clsPerson.GetNationalNumber(clsDriver.GetDriverPersonID(clsLocalLicense.GetDriverID(DetainedLicense.LocalLicenseID)))
                        ,clsPerson.GetFullName(clsDriver.GetDriverPersonID(clsLocalLicense.GetDriverID(DetainedLicense.LocalLicenseID))), null};

                    AfterDetainingLicense?.Invoke(DetainedLicenseInfo);

                    btnDetain.Enabled = false;
                    lnlblShowLicenseInfo.Enabled = true;
                }
                else
                    MessageBox.Show("Failed To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(clsLocalLicense LocalLicenseInfo)
        {
            if (clsDetainedLicense.IsDetainedLicense(LocalLicenseInfo.LicenseID))
            {
                MessageBox.Show("Selected license is already detained , choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
            }

            else if (!LocalLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active , choose an active local license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
            }

            else
            {
                btnDetain.Enabled = true;
                _LocalLicenseInfo = LocalLicenseInfo;
            }
            lblLicenseID.Text = LocalLicenseInfo.LicenseID.ToString();

            lnlblShowLicenseHistory.Enabled = true;
        }

        private void txtFineFees_KeyDown(object sender, KeyEventArgs e)
        {
            frmUpdateApplicationType.ValidateFeesTextBox_KeyDown(ertxtFineFees, txtFineFees, e);
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_LocalLicenseInfo.LicenseID);
            frm.ShowDialog();
        }
    
        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLDLDetailsByFilter.uctrlLDLDetails.EditDriverNameAndNationalNo(UpdatedDriverPersonalInfo.FullName, UpdatedDriverPersonalInfo.NationalNo);
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
            frmDriverLicenseHistory frm = GetfrmDriverLicensesHistoryObj(_DGVDetainedLicenses, _LocalLicenseInfo, _DGVLocalLicenesColumnName);

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
