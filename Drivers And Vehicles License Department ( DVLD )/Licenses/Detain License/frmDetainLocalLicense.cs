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
            lblDetainedDate.Text = clsFormat.DateToShort(DateTime.Now);

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Fees field has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.No)
            {
                return;
            }

            int DetainID = uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.Detain(Convert.ToSingle(txtFineFees.Text.Trim()), clsGlobalSettings.CurrentUserID);

            if (DetainID == -1)
            {
                MessageBox.Show("Failed To detain license!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text = DetainID.ToString();
            MessageBox.Show($"License Detained Successfully With ID = {DetainID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            object[] DetainedLicenseInfo = new object[] { uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.DetainInfo.DetainID , uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.DetainInfo.LocalLicenseID, uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.DetainInfo.DetainDate.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateTimeCustomFormat))
                 , uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.DetainInfo.IsReleased,clsFormat.GetCustomNumberFormat(uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.DetainInfo.FineFees, clsFormat.enCustomNumberFormat.With4ZerosAfterFraction),null,uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.DriverInfo.PersonalInfo.NationalNo
                 ,uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.DriverInfo.PersonalInfo.FullName, null};

            AfterDetainingLicense?.Invoke(DetainedLicenseInfo);

            btnDetain.Enabled = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LocalLicenseID)
        {
            lblLicenseID.Text = LocalLicenseID.ToString();

            if (LocalLicenseID == -1)
            {
                btnDetain.Enabled = false;
                return;
            }

            if (uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected license is already detained , choose another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
            }

            else if (!uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active , choose an active local license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDetain.Enabled = false;
            }

            else
            {
                btnDetain.Enabled = true;
            }

            txtFineFees.Focus();
            llShowLicenseHistory.Enabled = true;
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLiecnseInfo frm = new frmShowDriverLiecnseInfo(uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo.LicenseID);
            frm.ShowDialog();
        }
    
        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLocalLicenseDetailsWithFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo, int DGVRowIndex)
        {
            _EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
            OnUpdatedDriverPersonalInfo?.Invoke(UpdatedDriverPersonalInfo, DGVRowIndex);
        }

        public static frmShowPersonLicenseHistory GetfrmDriverLicensesHistoryObj(DataGridView dgvDetainedLicenses,clsLocalLicense LocalLicenesInfo,string DGVLocalLicenesColumnName)
        {
            if (dgvDetainedLicenses != null)
            {
                return new frmShowPersonLicenseHistory(clsDriver.GetDriverPersonID(LocalLicenesInfo.DriverID)
                       , clsDataGridViewUtilityLib.GetDGVRowIndexByColumnValue(dgvDetainedLicenses, DGVLocalLicenesColumnName, LocalLicenesInfo.LicenseID));
            }

            else
            {
                return new frmShowPersonLicenseHistory(clsDriver.GetDriverPersonID(LocalLicenesInfo.DriverID));
            }
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = GetfrmDriverLicensesHistoryObj(_DGVDetainedLicenses, uctrlLocalLicenseDetailsWithFilter.SelectedLicenseInfo, _DGVLocalLicenesColumnName);

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

        private void txtFineFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtFineFees.Text.Trim() == "")
            {
                clsValidation.EnableErrorProvider(ertxtFineFees, txtFineFees, "You must enter the fine fees!", e);
            }

            else if (!clsValidation.IsNumber(txtFineFees.Text.Trim()))
            {
                clsValidation.EnableErrorProvider(ertxtFineFees, txtFineFees, "Invalid Number!", e);
            }

            else
                clsValidation.DisableErrorProvider(ertxtFineFees, txtFineFees);
        }

        private void frmDetainLocalLicense_Activated(object sender, EventArgs e)
        {
            uctrlLocalLicenseDetailsWithFilter.txtLicenseIDFocus();
        }
    }
}
