using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsUtility;

namespace DVLDPresentationLayer.Core
{
    public partial class frmReplacementForLostOrDamaged : Form
    {
        int _ReplacedLicenseID;
        clsLocalLicense _SelectedLicenseInfo;

        public frmReplacementForLostOrDamaged()
        {
            InitializeComponent();
            _SetFormInfo();

            lblApplicationDate.Text = DateTime.Now.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblUserName.Text = clsUtility.DecryptUserName(clsUser.GetUserName(clsGlobalSettings.CurrentUserID));

            _ReplacedLicenseID = -1;
        }

        private void _SetFormInfo()
        {
            if (rbDamagedLicense.Checked)
            {
                lblFormBigTitle.Text = "Replacement For Damaged License";
                lblFormTitle.Text = lblFormBigTitle.Text;
                lblApplicationFees.Text = clsUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReplacementForDamagedLicense),
                    enCustomNumberFormat.NoJustZerosAfterFraction);
            }
            else
            {
                lblFormBigTitle.Text = "Replacement For Lost License";
                lblFormTitle.Text = lblFormBigTitle.Text;
                lblApplicationFees.Text = clsUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReplacementForLostLicense),
                    enCustomNumberFormat.NoJustZerosAfterFraction);
            }
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            _SetFormInfo();
        }
        private bool _AddNewApplication(ref int NewApplicationID)
        {
            byte AppType = (rbDamagedLicense.Checked) ? clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.ReplacementForDamagedLicense) : clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.ReplacementForLostLicense);
            decimal PaidFees = (rbDamagedLicense.Checked) ? clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReplacementForDamagedLicense) : clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReplacementForLostLicense);

            clsApplication LicenseRenewalApplication = new clsApplication
             (
                ApplicantPersonID: clsDriver.GetDriverPersonID(_SelectedLicenseInfo.DriverID),
                ApplicationDate: DateTime.Now,
                ApplicationTypeID: AppType,
                ApplicationStatus: clsApplication.enApplicationStatus.New,
                LastStatusDate: DateTime.Now,
                PaidApplicationFees: PaidFees,
                CreatedByUserID: clsGlobalSettings.CurrentUserID
             );

            if (LicenseRenewalApplication.Save())
            {
                NewApplicationID = LicenseRenewalApplication.ApplicationID;
                return true;
            }

            else
                return false;
        }

        private bool _IssueReplacement(int NewApplicationID, ref int NewLicenseID)
        {
            clsLocalLicense NewLocalLicense = new clsLocalLicense
            (
                ApplicationID: NewApplicationID,
                DriverID: _SelectedLicenseInfo.DriverID,
                LicenseClassID: _SelectedLicenseInfo.LicenseClassID,
                IssueDate: DateTime.Now,
                ExpirationDate: _SelectedLicenseInfo.ExpirationDate,
                Notes: null,
                PaidFees: _SelectedLicenseInfo.PaidFees,
                IsActive: true,
                IssueReason: (rbDamagedLicense.Checked) ? clsLocalLicense.enIssueReason.ReplacementForDamaged : clsLocalLicense.enIssueReason.ReplacementForLost,
                CreatedByUserID: clsGlobalSettings.CurrentUserID
            );

            if (NewLocalLicense.Save())
            {
                NewLicenseID = NewLocalLicense.LicenseID;
                return true;
            }

            else
                return false;
        }
        
        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (_SelectedLicenseInfo != null)
            {
                DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to issue a replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ConfirmationQuestion == DialogResult.Yes)
                {
                    int NewApplicationID = -1;
                    if (_AddNewApplication(ref NewApplicationID))
                    {
                        int ReplacedLicenseID = -1;
                        if (_IssueReplacement(NewApplicationID, ref ReplacedLicenseID))
                        {
                            clsApplication.ChangeApplicationStatus(NewApplicationID, clsApplication.enApplicationStatus.Completed);

                            if (clsLocalLicense.DeactivateLicense(_SelectedLicenseInfo.LicenseID))
                            {
                                lblLicenseReplacementAppID.Text = NewApplicationID.ToString();
                                lblReplacedLicenseID.Text = ReplacedLicenseID.ToString();
                                _ReplacedLicenseID = ReplacedLicenseID;

                                MessageBox.Show($"License Replaced Successfully With ID : {ReplacedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                lnlblShowNewLicenseInfo.Enabled = true;
                                btnIssueReplacement.Enabled = false;
                            }
                            else
                                MessageBox.Show("Failed to deactivate old local license!\nLicense renewal failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Failed to renew license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Failed to save application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Enter local license ID First in order to renew license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(clsLocalLicense LocalLicenseInfo)
        {
            _SelectedLicenseInfo = LocalLicenseInfo;
            lblOldLocalLicenseID.Text = _SelectedLicenseInfo.LicenseID.ToString();

            if (LocalLicenseInfo.IsActive)
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

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(_SelectedLicenseInfo.DriverID));
            frm.ShowDialog();
        }

        private void lnlblShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_ReplacedLicenseID);
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
