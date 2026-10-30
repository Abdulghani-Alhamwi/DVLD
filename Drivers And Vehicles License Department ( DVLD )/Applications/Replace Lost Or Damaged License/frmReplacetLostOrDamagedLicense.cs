using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer.Core
{
    public partial class frmReplacetLostOrDamagedLicense : Form
    {
        private int _ReplacedLicenseID;
        public frmReplacetLostOrDamagedLicense()
        {
            InitializeComponent();
            _SetFormInfo();

            lblApplicationDate.Text = DateTime.Now.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName));

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);

            _ReplacedLicenseID = -1;
        }

        private void _SetFormInfo()
        {
            if (rbDamagedLicense.Checked)
            {
                lblFormBigTitle.Text = "Replacement For Damaged License";
                lblFormTitle.Text = lblFormBigTitle.Text;
                lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.ReplacementForDamagedLicense),
                    clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            }
            else
            {
                lblFormBigTitle.Text = "Replacement For Lost License";
                lblFormTitle.Text = lblFormBigTitle.Text;
                lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.ReplacementForLostLicense),
                    clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
            }
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _SetFormInfo();
        }
        
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (uctrlLDLDetailsByFilter.SelectedLicenseInfo == null)
            {
                MessageBox.Show("Enter local license ID First in order to renew license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to issue a replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.No)
            {
                return;
            }

            clsLocalLicense NewLicense = uctrlLDLDetailsByFilter.SelectedLicenseInfo.Replace((rbDamagedLicense.Checked) ? clsLocalLicense.enIssueReason.ReplacementForDamaged : clsLocalLicense.enIssueReason.ReplacementForLost, clsGlobalSettings.CurrentUserID);
            if (NewLicense != null)
            {
                lblLicenseReplacementAppID.Text = NewLicense.ApplicationID.ToString();
                lblReplacedLicenseID.Text = NewLicense.LicenseID.ToString();
                _ReplacedLicenseID = NewLicense.LicenseID;

                MessageBox.Show($"License Replaced Successfully With ID : {_ReplacedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                uctrlLDLDetailsByFilter.FilterEnabled = false;
                lnlblShowNewLicenseInfo.Enabled = true;
                btnIssueReplacement.Enabled = false;
            }
            else
                MessageBox.Show("Failed to replace license!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LicenseID)
        {
            lblOldLocalLicenseID.Text = LicenseID.ToString();

            if (uctrlLDLDetailsByFilter.SelectedLicenseInfo.IsActive)
            {
                btnIssueReplacement.Enabled = true;
            }
            else
            {
                MessageBox.Show("Selected license is not active , choose an active license.",
                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssueReplacement.Enabled = false;
            }
                lnlblShowLicenseHistory.Enabled = true;
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

        private void lnlblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLiecnseInfo frm = new frmShowDriverLiecnseInfo(_ReplacedLicenseID);
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
