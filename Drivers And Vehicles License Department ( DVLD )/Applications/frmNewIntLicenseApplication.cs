using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Licenses;
using Utility_Library;

namespace DVLDPresentationLayer.Applications
{
    public partial class frmNewIntLicenseApplication : Form
    {
        public delegate void IssuedLicense(ref object[] NewInternationalLicenseInfo);

        public event IssuedLicense OnIssuedLicense;

        private DateTime _InternationalLicenseExpDate = DateTime.Now.AddYears(1);

        private int _LocalLicenseID, _DriverID;

        private clsInternationalLicense _NewInternationalLicense;
        public frmNewIntLicenseApplication()
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            _ShowNewApplicationInfo();
        }
        private void _ShowNewApplicationInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblIssueDate.Text = DateTime.Now.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblExpirationDate.Text = _InternationalLicenseExpDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblApplicationFees.Text = clsUtility.SetFeesToCustomFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.NewInternationlLicense));
            lblUserName.Text = clsUtility.DecryptUserName(clsUser.GetUserName(clsGlobalSettings.CurrentUserID));
        }
        private bool _AddNewApplication(int DriverID, out int NewApplicationID)
        {
            clsApplication InternationalLicenseApp = new clsApplication
            (
                ApplicantPersonID: clsDriver.GetDriverPersonID(DriverID),
                ApplicationDate: DateTime.Now,
                ApplicationTypeID: clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.NewInternationlLicense),
                ApplicationStatus: clsApplication.enApplicationStatus.New,
                LastStatusDate: DateTime.Now,
                PaidApplicationFees: clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.NewInternationlLicense),
                CreatedByUserID: clsGlobalSettings.CurrentUserID
            );

            InternationalLicenseApp.Save();
            NewApplicationID = InternationalLicenseApp.ApplicationID;

            return (NewApplicationID != -1);
        }

        private bool _IssueInternationalLicense(int LocalLicenseID, int DriverID, int NewApplicationID)
        {
            _NewInternationalLicense = new clsInternationalLicense
            (
            ApplicationID: NewApplicationID,
            DriverID: DriverID,
            LocalLicenseID: LocalLicenseID,
            IssueDate: DateTime.Now,
            ExpirationDate: _InternationalLicenseExpDate,
            IsActive: true, CreatedByUserID: clsGlobalSettings.CurrentUserID
            );

            return (_NewInternationalLicense.Save());
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to issue license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.Yes)
            {
                if (_AddNewApplication(_DriverID, out int NewApplicationID))
                {
                    if (_IssueInternationalLicense(_LocalLicenseID, _DriverID, NewApplicationID))
                    {
                        clsApplication.ChangeApplicationStatus(NewApplicationID, clsApplication.enApplicationStatus.Completed);

                        object[] NewInternationalLicenseInfo = new object[] { _NewInternationalLicense.InternationalLicenseID,_NewInternationalLicense.ApplicationID,_NewInternationalLicense.DriverID,
                        _NewInternationalLicense.IssuedUsingLocalLicenseID,_NewInternationalLicense.IssueDate,_NewInternationalLicense.ExpirationDate,_NewInternationalLicense.IsActive};

                        OnIssuedLicense?.Invoke(ref NewInternationalLicenseInfo);
                        lblInternationalLicenseAppID.Text = NewApplicationID.ToString();
                        lblInternationalLicenseID.Text = _NewInternationalLicense.InternationalLicenseID.ToString();
                        MessageBox.Show($"International License Issued Successfully With ID : {_NewInternationalLicense.InternationalLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnIssueLicense.Enabled = false;
                        lnlblShowLicenseInfo.Enabled = true;
                    }
                    else
                        MessageBox.Show("Failed to issue license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Failed to save application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(_DriverID));
            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseDetails frm = new frmInternationalLicenseDetails(_NewInternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(int LocalLicenseID, int DriverID)
        {
            _LocalLicenseID = LocalLicenseID;
            lblLocalLicenseID.Text = LocalLicenseID.ToString();

            _DriverID = DriverID;
            lnlblShowLicenseHistory.Enabled = true;
        }
    }
}