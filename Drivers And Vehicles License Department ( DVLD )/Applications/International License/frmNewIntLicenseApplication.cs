using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Licenses;
using Utility_Library;
using static Utility_Library.clsGeneralUtility;

namespace DVLDPresentationLayer.Applications
{
    public partial class frmNewIntLicenseApplication : Form
    {
        public delegate void IssuedLicense(object[] NewInternationalLicenseInfo);

        public event IssuedLicense OnIssuedLicense;

        public event Action<clsPerson, int> OnUpdatedDriverPersonalInfo;
        public event Action<int> AfterIssueingLicense;

        private DateTime _InternationalLicenseExpDate = DateTime.Now.AddYears(1);
        private int _SelectedLocalLicenseID, _DriverID;
        private clsInternationalLicense _NewInternationalLicense;
        private clsLocalLicense _DriverOrdinaryLocalLicense;

        private int _DGVRowIndex;

        public frmNewIntLicenseApplication()
        {
            InitializeComponent();
            _InitializeFormData();
            _DGVRowIndex = -1;
        }

        public frmNewIntLicenseApplication(int DriverID,clsLocalLicense OrdinaryLocalLicense,int DGVRowIndex)
        {
            InitializeComponent();
            _InitializeFormData();

            if (OrdinaryLocalLicense != null)
            {
                _DriverOrdinaryLocalLicense = OrdinaryLocalLicense;
                uctrlLDLDetailsByFilter.txtLicenseID.Text = OrdinaryLocalLicense.LicenseID.ToString();
                uctrlLDLDetailsByFilter.uctrlLDLDetails.LoadDriverLicenseInfo(OrdinaryLocalLicense.LicenseID);                
            }

            _DGVRowIndex = DGVRowIndex;
            uctrlLDLDetailsByFilter.gbFilter.Enabled = false;
        }

        private void _InitializeFormData()
        {
            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
            _ShowNewApplicationInfo();

            _SelectedLocalLicenseID = -1;
            _DriverID = -1;
        }

        private void _ShowNewApplicationInfo()
        {
            lblApplicationDate.Text = DateTime.Now.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblIssueDate.Text = DateTime.Now.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblExpirationDate.Text = _InternationalLicenseExpDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
            lblApplicationFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.NewInternationlLicense),
                enCustomNumberFormat.NoJustZerosAfterFraction);

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
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

        private void _EditDriverPersonalInfo(clsPerson UpdatedPersonInfo)
        {
            uctrlLDLDetailsByFilter.uctrlLDLDetails.EditDriverPersonalInfo(UpdatedPersonInfo);
        }

        private void lnlblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDriverLicenseHistory frm;
            if (_DGVRowIndex == -1)
            {
                frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(_DriverID));
            }
            else
            {
                frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID(_DriverID),_DGVRowIndex);
                frm.OnEditedDriverPersonalInfo += OnUpdatedDriverPersonalInfo;
            }
                frm.OnUpdatedDriverInfo += _EditDriverPersonalInfo;

            frm.ShowDialog();
        }

        private void lnlblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseDetails frm = new frmInternationalLicenseDetails(_NewInternationalLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private bool _IsOrdinaryLocalDrivingLicense(int LicenseClassID)
        {
            if (clsLicenseClass.GetLicenseClassEnumMember(LicenseClassID) == clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass)
                return true;
            else
                return false;
        }

        private bool _CanDriverApply(clsLocalLicense LocalLicense)
        {
            if(LocalLicense == null)
            {
                MessageBox.Show("Driver does not has an active local license from the ordinary license class, issue local license from the ordinary license class in order to be able to issue the internationa license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueLicense.Enabled = false;
                return false;
            }

            else if (clsInternationalLicense.HasDriverActiveInternationalLicense(LocalLicense.LicenseID, out int InternationalLicenseID))
            {
                MessageBox.Show($"Person already has an active international license with ID : {InternationalLicenseID}"
             , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else if (!_IsOrdinaryLocalDrivingLicense(LocalLicense.LicenseClassID))
            {
                MessageBox.Show("The entered ID is for a local license but it is not from the ordinary driving license class!\nYou can issue international license only if you have local license from the ordinary driving license class."
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else if (LocalLicense.IsExpired())
            {
                if(clsLocalLicense.HasDriverRenewedLicense(LocalLicense.DriverID,LocalLicense.LicenseClassID,out int RenewedLicenseID))
                    MessageBox.Show($"The entered ID is for a local license but it has been expired!\nThis driver has a renewed local license from the ordinary driving class with ID : {RenewedLicenseID}"
                    , "Expired Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                    MessageBox.Show("The entered ID is for a local license but it has been expired!, re-new it first then re-apply for international license."
                    , "Expired Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            else if (!LocalLicense.IsActive)
            {
                if (clsDetainedLicense.IsDetainedLicense(LocalLicense.LicenseID))
                {
                    MessageBox.Show("The entered ID is for a local license but it is not active because it has been detained,\nrelease it from detain first then re-apply for international license."
                        , "Detained Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (clsDriver.HasActiveLicenseFromClass(LocalLicense.DriverID, clsLicenseClass.GetLicenseClassID(clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass), out int ActiveLicenseID))
                {
                    MessageBox.Show($"The entered ID is for a local license that is no longer active,\nThe driver has already active local licenseID from ordinary driving class which has been issued as {clsLocalLicense.GetLicenseNotes(ActiveLicenseID)} and its ID is : {ActiveLicenseID}"
                        , "This License Is No Longer Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }

            return true;
        }

        private void uctrlLDLDetailsByFilter_OnSelectedLocalLicense(clsLocalLicense LicenseInfo)
        {
            if (LicenseInfo != null)
            {
                lblLocalLicenseID.Text = LicenseInfo.LicenseID.ToString();
                _DriverID = LicenseInfo.DriverID;

                if (_CanDriverApply(LicenseInfo))
                {
                    _SelectedLocalLicenseID = LicenseInfo.LicenseID;
                    btnIssueLicense.Enabled = true;
                }

                else
                    btnIssueLicense.Enabled = false;
            }
            lnlblShowLicenseHistory.Enabled = true;
        
        }

        private void frmNewIntLicenseApplication_Shown(object sender, EventArgs e)
        {
            if (_DGVRowIndex != -1)
            {
                if(_CanDriverApply(_DriverOrdinaryLocalLicense))
                uctrlLDLDetailsByFilter_OnSelectedLocalLicense(_DriverOrdinaryLocalLicense);
            }
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (_SelectedLocalLicenseID == -1)
            {
                MessageBox.Show("Enter local license ID First in order to issue international license", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

                DialogResult ConfirmationQuestion = MessageBox.Show("Are you sure you want to issue license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ConfirmationQuestion == DialogResult.Yes)
            {
                if (_AddNewApplication(_DriverID, out int NewApplicationID))
                {
                    if (_IssueInternationalLicense(_SelectedLocalLicenseID, _DriverID, NewApplicationID))
                    {
                        if (clsApplication.ChangeApplicationStatus(NewApplicationID, clsApplication.enApplicationStatus.Completed))
                        {
                            object[] NewInternationalLicenseInfo = new object[] { _NewInternationalLicense.InternationalLicenseID,_NewInternationalLicense.ApplicationID,_NewInternationalLicense.DriverID,
                        _NewInternationalLicense.IssuedUsingLocalLicenseID,_NewInternationalLicense.IssueDate,_NewInternationalLicense.ExpirationDate,_NewInternationalLicense.IsActive};

                            OnIssuedLicense?.Invoke(NewInternationalLicenseInfo);
                            AfterIssueingLicense?.Invoke(_DGVRowIndex);

                            lblInternationalLicenseAppID.Text = NewApplicationID.ToString();
                            lblInternationalLicenseID.Text = _NewInternationalLicense.InternationalLicenseID.ToString();

                            MessageBox.Show($"International License Issued Successfully With ID : {_NewInternationalLicense.InternationalLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            uctrlLDLDetailsByFilter.gbFilter.Enabled = false;
                            btnIssueLicense.Enabled = false;
                            lnlblShowLicenseInfo.Enabled = true;
                        }
                        else
                            MessageBox.Show("International license issued successfully but failed to change application status to completed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Failed to issue license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Failed to save application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }               
        }

    }
}