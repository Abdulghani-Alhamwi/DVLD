using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static DVLDBusinessLayer.clsLocalLicense;
using static DVLDBusinessLayer.clsApplication;

namespace DVLDPresentationLayer.Core
{
    public partial class frmReplacetLostOrDamagedLicenseApplication : Form
    {
        private int _ReplacedLicenseID;
        public frmReplacetLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
            _ReplacedLicenseID = -1;
        }
    
        private enApplicationType _GetApplicationTypeID()
        {
            if (rbDamagedLicense.Checked)
                return clsApplication.enApplicationType.ReplacementForDamagedLicense;

            else
                return clsApplication.enApplicationType.ReplacementForLostLicense;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblFormBigTitle.Text = "Replacement For Damaged License";
            lblFormTitle.Text = lblFormBigTitle.Text;
            lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(_GetApplicationTypeID()),
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblFormBigTitle.Text = "Replacement For Lost License";
            lblFormTitle.Text = lblFormBigTitle.Text;
            lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(_GetApplicationTypeID()),
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);
        }

        private enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return clsLocalLicense.enIssueReason.ReplacementForDamaged;

            else
                return clsLocalLicense.enIssueReason.ReplacementForLost;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to issue a replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.No)
            {
                return;
            }

            clsLocalLicense NewLicense = uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.Replace(_GetIssueReason(),clsGlobalSettings.CurrentUserID);
            if (NewLicense != null)
            {
                lblLicenseReplacementAppID.Text = NewLicense.ApplicationID.ToString();
                lblReplacedLicenseID.Text = NewLicense.LicenseID.ToString();
                _ReplacedLicenseID = NewLicense.LicenseID;

                MessageBox.Show($"License Replaced Successfully With ID : {_ReplacedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnIssueReplacement.Enabled = false;
                uctrlLocalDrivingLicenseInfoByFilter.FilterEnabled = false;
                gbReplacementFor.Enabled = false;
                llShowNewLicenseInfo.Enabled = true;
            }
            else
                MessageBox.Show("Failed to replace license!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LicenseID)
        {
            llShowLicenseHistory.Enabled = (LicenseID != -1);

            if (LicenseID == -1)
                return;

            lblOldLocalLicenseID.Text = LicenseID.ToString();
            if (!uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected license is not active , choose an active license.",
                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;

                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedDriverPersonalInfo)
        {
            uctrlLocalDrivingLicenseInfoByFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedDriverPersonalInfo);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(clsDriver.GetDriverPersonID(uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverID));
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

        private void frmReplacetLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);

            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void frmReplacetLostOrDamagedLicenseApplication_Activated(object sender, EventArgs e)
        {
            uctrlLocalDrivingLicenseInfoByFilter.txtLicenseIDFocus();
        }
    }
}
