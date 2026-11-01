using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        private int _RenewedLicenseID;

        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);

            _RenewedLicenseID = -1;
        }

        private void _ShowBasicInfo()
        {
            lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.RenewLicense),
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = lblApplicationDate.Text;

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
        }

        private void _ShowNewApplicationInfo()
        {
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength));
            lblLicenseFees.Text = clsFormat.GetCustomNumberFormat(uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.LicenseClassInfo.ClassFees, clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblTotalFees.Text = clsFormat.GetCustomNumberFormat(
                Convert.ToSingle(lblApplicationFees.Text) + uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.LicenseClassInfo.ClassFees,
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlDriverLicenseInfoByFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(clsDriver.GetDriverPersonID(uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.DriverID));
            frm.OnUpdatedDriverInfo += _EditDriverPersonalInfo;
            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLiecnseInfo frm = new frmShowDriverLiecnseInfo(_RenewedLicenseID);
            frm.ShowDialog();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LicenseID)
        {
            llShowLicenseHistory.Enabled = (LicenseID != -1);

            if (LicenseID == -1)
            {
                return;
            }

            lblOldLocalLicenseID.Text = uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.LicenseID.ToString();
            txtNotes.Text = uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.Notes;
            _ShowNewApplicationInfo();

            if (!uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show($"Selected license is not yet expired , it will expire on :\n{clsFormat.DateToShort(uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.ExpirationDate)}",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenewLicense.Enabled = false;
                return;
            }

            if(!uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show($"Selected license is not active, choose an active license.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to renew license?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLocalLicense NewLicense = uctrlDriverLicenseInfoByFilter.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim()
                , clsGlobalSettings.CurrentUserID);

            if (NewLicense != null)
            {
                lblRenewLicenseAppID.Text = NewLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
                _RenewedLicenseID = NewLicense.LicenseID;

                MessageBox.Show($"License Renewed Successfully With ID : {_RenewedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                uctrlDriverLicenseInfoByFilter.FilterEnabled = false;
                llShowNewLicenseInfo.Enabled = true;
                btnRenewLicense.Enabled = false;
            }
            else
                MessageBox.Show("Failed to renew license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ShowBasicInfo();
        }

        private void frmRenewLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            uctrlDriverLicenseInfoByFilter.txtLicenseIDFocus();
        }
    }
}
