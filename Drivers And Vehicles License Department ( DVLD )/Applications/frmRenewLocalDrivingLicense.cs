using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;
using static Utility_Library.clsGeneralUtility;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        int _RenewedLicenseID;
        clsLocalLicense _SelectedLicenseInfo;
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
            _ShowBasicInfo();
            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);

            _RenewedLicenseID = -1;
        }

        private void _ShowBasicInfo()
        {
            lblApplicationFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.RenewLicense),
                enCustomNumberFormat.NoJustZerosAfterFraction);
            lblApplicationDate.Text = DateTime.Now.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblIssueDate.Text = lblApplicationDate.Text;
            lblUserName.Text = clsGeneralUtility.DecryptUserName(clsUser.GetUserName(clsGlobalSettings.CurrentUserID));
        }

        private void _ShowApplicationInfo()
        {
            lblOldLocalLicenseID.Text = _SelectedLicenseInfo.LicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(clsLicenseClass.GetLicenseValidityLength(_SelectedLicenseInfo.LicenseClassID)).ToString(
                clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblLicenseFees.Text = clsGeneralUtility.GetCustomNumberFormat(_SelectedLicenseInfo.PaidFees, enCustomNumberFormat.NoJustZerosAfterFraction);
            lblTotalFees.Text = clsGeneralUtility.GetCustomNumberFormat(
                Convert.ToDecimal(lblApplicationFees.Text) + _SelectedLicenseInfo.PaidFees,
                enCustomNumberFormat.NoJustZerosAfterFraction);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(_SelectedLicenseInfo.DriverID));
            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_RenewedLicenseID);
            frm.ShowDialog();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(clsLocalLicense LicenseInfo)
        {
            _SelectedLicenseInfo = LicenseInfo;
            _ShowApplicationInfo();

            if (!LicenseInfo.IsExpired())
            {
                MessageBox.Show($"Selected license is not yet expired , it will expire on :\n{LicenseInfo.ExpirationDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName))}",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenewLicense.Enabled = false;
            }
            else
            {
                btnRenewLicense.Enabled = true;
            }

            lnlblShowLicenseHistory.Enabled = true;
        }

        private bool _AddNewApplication(ref int NewApplicationID)
        {
            clsApplication LicenseRenewalApplication = new clsApplication
             (
                ApplicantPersonID:clsDriver.GetDriverPersonID(_SelectedLicenseInfo.DriverID),
                ApplicationDate:DateTime.Now,
                ApplicationTypeID:clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.RenewLicense),
                ApplicationStatus:clsApplication.enApplicationStatus.New,
                LastStatusDate:DateTime.Now,
                PaidApplicationFees: clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.RenewLicense),
                CreatedByUserID:clsGlobalSettings.CurrentUserID
             );

            if (LicenseRenewalApplication.Save())
            {
                NewApplicationID = LicenseRenewalApplication.ApplicationID;
                return true;
            }

            else
                return false;
        }

        private bool _IssueRenewedLocalLicense(int NewApplicationID,ref int NewLicenseID)
        {
            clsLocalLicense NewLocalLicense = new clsLocalLicense
            (
                ApplicationID: NewApplicationID,
                DriverID: _SelectedLicenseInfo.DriverID,
                LicenseClassID: _SelectedLicenseInfo.LicenseClassID,
                IssueDate: DateTime.Now,
                ExpirationDate: DateTime.Now.AddYears(clsLicenseClass.GetLicenseValidityLength(_SelectedLicenseInfo.LicenseClassID)),
                Notes: (txtNotes.Text != "") ? txtNotes.Text : null,
                PaidFees: _SelectedLicenseInfo.PaidFees,
                IsActive:true,
                IssueReason: clsLocalLicense.enIssueReason.Renew,
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

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (_SelectedLicenseInfo != null)
            {
                MessageBox.Show("Enter local license ID First in order to renew license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to renew license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ConfirmationQuestion == DialogResult.Yes)
                {
                        int NewApplicationID = -1;
                        if (_AddNewApplication(ref NewApplicationID))
                        {
                            int RenewedLicenseID = -1;
                        if (_IssueRenewedLocalLicense(NewApplicationID, ref RenewedLicenseID))
                        {
                        if (clsLocalLicense.DeactivateLicense(_SelectedLicenseInfo.LicenseID))
                        {
                            if (clsApplication.ChangeApplicationStatus(NewApplicationID, clsApplication.enApplicationStatus.Completed))
                            {
                                lblRenewLicenseAppID.Text = NewApplicationID.ToString();
                                lblRenewedLicenseID.Text = RenewedLicenseID.ToString();
                                _RenewedLicenseID = RenewedLicenseID;

                                MessageBox.Show($"License Renewed Successfully With ID : {RenewedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                uctrlLDLDetailsByFilter.gbFilter.Enabled = false;
                                lnlblShowNewLicenseInfo.Enabled = true;
                                btnRenewLicense.Enabled = false;
                            }
                            else
                                MessageBox.Show("License renewed successfully but failed to change application status to completed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
