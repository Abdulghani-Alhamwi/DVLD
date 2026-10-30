using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsUtility;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        private int _RenewedLicenseID;

        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
            _ShowBasicInfo();
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);

            _RenewedLicenseID = -1;
        }

        private void _ShowBasicInfo()
        {
            lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.RenewLicense),
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblApplicationDate.Text = DateTime.Now.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName));
            lblIssueDate.Text = lblApplicationDate.Text;

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
        }

        private void _ShowApplicationInfo()
        {
            lblOldLocalLicenseID.Text = uctrlLDLDetailsByFilter.SelectedLicenseInfo.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(uctrlLDLDetailsByFilter.SelectedLicenseInfo.LicenseClassInfo.DefaultValidityLength).ToString(
                clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName));
            lblLicenseFees.Text = clsFormat.GetCustomNumberFormat(uctrlLDLDetailsByFilter.SelectedLicenseInfo.PaidFees, clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            lblTotalFees.Text = clsFormat.GetCustomNumberFormat(
                Convert.ToDecimal(lblApplicationFees.Text) + uctrlLDLDetailsByFilter.SelectedLicenseInfo.PaidFees,
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLDLDetailsByFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(uctrlLDLDetailsByFilter.SelectedLicenseInfo.DriverID));
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
            if (LicenseID != -1)
            {
                _ShowApplicationInfo();

                if (!uctrlLDLDetailsByFilter.SelectedLicenseInfo.IsExpired())
                {
                    MessageBox.Show($"Selected license is not yet expired , it will expire on :\n{uctrlLDLDetailsByFilter.SelectedLicenseInfo.ExpirationDate.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName))}",
                        "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    btnRenewLicense.Enabled = false;
                }
                else
                {
                    btnRenewLicense.Enabled = true;
                }

                lnlblShowLicenseHistory.Enabled = true;
            }
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (uctrlLDLDetailsByFilter.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Enter local license ID First in order to renew license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to renew license?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLocalLicense NewLicense = uctrlLDLDetailsByFilter.SelectedLicenseInfo.Renew(txtNotes.Text.Trim(), clsGlobalSettings.CurrentUserID);
            if (NewLicense != null)
            {
                lblRenewLicenseAppID.Text = NewLicense.ApplicationID.ToString();
                lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
                _RenewedLicenseID = NewLicense.LicenseID;

                MessageBox.Show($"License Renewed Successfully With ID : {_RenewedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                uctrlLDLDetailsByFilter.FilterEnabled = false;
                lnlblShowNewLicenseInfo.Enabled = true;
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
    }
}
