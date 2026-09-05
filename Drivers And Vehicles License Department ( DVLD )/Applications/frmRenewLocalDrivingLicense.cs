using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        int _RenewedLicenseID,_SelectedLocalLicenseID, _DriverID;
        clsLocalLicense _SelectedLicenseInfo;
        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);

            _ShowDefaultInfo();

            _RenewedLicenseID = -1;
            _SelectedLocalLicenseID = -1;
            _DriverID = -1;
        }

        private void _ShowDefaultInfo()
        {
            lblApplicationFees.Text = clsUtility.GetCustomFeesFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.RenewLicense));
            lblApplicationDate.Text = DateTime.Now.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblIssueDate.Text = lblApplicationDate.Text;
            lblUserName.Text = clsUtility.DecryptUserName(clsUser.GetUserName(clsGlobalSettings.CurrentUserID));
        }

        private void _ShowApplicationInfo()
        {
            lblOldLocalLicenseID.Text = _SelectedLocalLicenseID.ToString();
            lblExpirationDate.Text = DateTime.Now.AddYears(clsLicenseClass.GetLicenseValidityLength(_SelectedLicenseInfo.LicenseClassID)).ToString(
                clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblLicenseFees.Text = clsUtility.GetCustomFeesFormat(
                clsLicenseClass.GetLicenseClassFees(_SelectedLicenseInfo.LicenseClassID));
            lblTotalFees.Text = clsUtility.GetCustomFeesFormat(
                clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.RenewLicense) + clsLicenseClass.GetLicenseClassFees(_SelectedLicenseInfo.LicenseClassID));
        }
        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(_DriverID));
            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(_RenewedLicenseID);
            frm.ShowDialog();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(clsLocalLicense LocalLicenseInfo)
        {
            _SelectedLicenseInfo = LocalLicenseInfo;
            _SelectedLocalLicenseID = _SelectedLicenseInfo.LicenseID;
            _DriverID = _SelectedLicenseInfo.DriverID;
            _ShowApplicationInfo();

            if (!LocalLicenseInfo.IsExpired())
            {
                MessageBox.Show($"Selected license is not yet expired , it will expire on :\n{LocalLicenseInfo.ExpirationDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName))}",
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
                ApplicantPersonID:clsDriver.GetDriverPersonID(_DriverID),
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
                DriverID: _DriverID,
                LicenseClassID: _SelectedLicenseInfo.LicenseClassID,
                IssueDate: DateTime.Now,
                ExpirationDate: DateTime.Now.AddYears(clsLicenseClass.GetLicenseValidityLength(_SelectedLicenseInfo.LicenseClassID)),
                Notes: (txtNotes.Text != "") ? txtNotes.Text : null,
                PaidFees: clsLicenseClass.GetLicenseClassFees(_SelectedLicenseInfo.LicenseClassID),
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
            if(_SelectedLocalLicenseID!=-1)
            { 
            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to renew license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ConfirmationQuestion == DialogResult.Yes)
                {
                    if (clsLocalLicense.DeactivateLicense(_SelectedLocalLicenseID))
                    {
                        int NewApplicationID = -1;
                        if (_AddNewApplication(ref NewApplicationID))
                        {
                            int RenewedLicenseID = -1;
                            if (_IssueRenewedLocalLicense(NewApplicationID, ref RenewedLicenseID))
                            {
                                lblRenewLicenseAppID.Text = NewApplicationID.ToString();
                                lblRenewedLicenseID.Text = RenewedLicenseID.ToString();
                                _RenewedLicenseID = RenewedLicenseID;

                                MessageBox.Show($"License Renewed Successfully With ID : {RenewedLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                lnlblShowNewLicenseInfo.Enabled = true;
                                btnRenewLicense.Enabled = false;
                            }
                            else
                                MessageBox.Show("Failed to renew license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Failed to save application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Failed to deactivate old local license!\nLicense renewal failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Enter local license ID First in order to renew license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
