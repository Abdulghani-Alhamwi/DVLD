using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Licenses;
using Utility_Library;

namespace DVLDPresentationLayer.Applications
{
    public partial class frmNewIntLicenseApplication : Form
    {
        public delegate void IssuedLicense(object[] NewInternationalLicenseInfo);

        public event IssuedLicense OnIssuedLicense;

        public event Action<clsPerson, int> OnUpdatedDriverPersonalInfo;
        public event Action<int> AfterIssueingLicense;
        
        private int _SelectedLocalLicenseID, _DriverID, _InternationalLicenseID;
        private clsInternationalLicense _NewInternationalLicense;

        private int _DGVRowIndex;

        public frmNewIntLicenseApplication()
        {
            InitializeComponent();
            _InitializeFormData();

            _DriverID = -1;
            _SelectedLocalLicenseID = -1; ;
            _DGVRowIndex = -1;
        }

        public frmNewIntLicenseApplication(int DriverID,int DGVRowIndex)
        {
            InitializeComponent();
            _InitializeFormData();

            _DriverID = DriverID;
            _DGVRowIndex = DGVRowIndex;
        }

        private void _InitializeFormData()
        {
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            _ShowNewApplicationInfo();
        }

        private void _ShowNewApplicationInfo()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(clsSettings.GetInternationalLicenseValidityLength()));
            lblApplicationFees.Text = clsFormat.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.NewInternationlLicense),
                clsFormat.enCustomNumberFormat.NoJustZerosAfterFraction);

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(clsGlobalSettings.CurrentUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsCryptography.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
        }

        private bool _IssueInternationalLicense(int LocalLicenseID)
        {
            _NewInternationalLicense = new clsInternationalLicense
            (
            DriverID: uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverID,
            LocalLicenseID: LocalLicenseID,
            IssueDate: DateTime.Now,
            ExpirationDate: DateTime.Now.AddYears(clsSettings.GetInternationalLicenseValidityLength()),
            IsActive: true, CreatedByUserID: clsGlobalSettings.CurrentUserID,
            ApplicantPersonID: uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverInfo.PersonID,
            ApplicationDate: DateTime.Now,
            ApplicationTypeID: clsApplication.enApplicationType.NewInternationlLicense,
            ApplicationStatus: clsApplication.enApplicationStatus.New,
            LastStatusDate: DateTime.Now,
            PaidApplicationFees: clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.NewInternationlLicense)
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
            uctrlLocalDrivingLicenseInfoByFilter.uctrlLocalDrivingLicenseInfo.EditDriverPersonalInfo(UpdatedPersonInfo);
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm;
            if (_DGVRowIndex == -1)
            {
                frm = new frmShowPersonLicenseHistory(clsDriver.GetDriverPersonID(_DriverID));
            }
            else
            {
                frm = new frmShowPersonLicenseHistory(uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverInfo.PersonID,_DGVRowIndex);
                frm.OnEditedDriverPersonalInfo += OnUpdatedDriverPersonalInfo;
            }
                frm.OnUpdatedDriverInfo += _EditDriverPersonalInfo;

            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmInternationalLicenseInfo frm = new frmInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }

        private bool _CanDriverApply()
        {
            llShowLicenseInfo.Enabled = false;

            if (uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.LicenseClassID != (byte)clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass)
            {
                MessageBox.Show("The entered ID is for a local license but it is not from the ordinary driving license class!\nYou can issue international license only if you have local license from the ordinary driving license class."
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.IsLicenseExpired())
            {
                if(clsLocalLicense.HasDriverRenewedLicense(uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverID, uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.LicenseClassID,out int RenewedLicenseID))
                    MessageBox.Show($"The entered ID is for a local license but it has been expired!\nThis driver has a renewed local license from the ordinary driving class with ID : {RenewedLicenseID}"
                    , "Expired Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                    MessageBox.Show("The entered ID is for a local license but it has been expired!, re-new it first then re-apply for international license."
                    , "Expired Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (!uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.IsActive)
            {
                if (uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.IsDetained)
                {
                    MessageBox.Show("The entered ID is for a local license but it is not active because it has been detained,\nrelease it from detain first then re-apply for international license."
                        , "Detained Local License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (clsDriver.HasActiveLicenseFromClass(uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverID, clsLicenseClass.GetLicenseClassID(clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass), out int ActiveLicenseID))
                {
                    MessageBox.Show($"The entered ID is for a local license that is no longer active,\nThe driver has already active local licenseID from ordinary driving class which has been issued {((uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.Notes==null)?"": $"as {uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.Notes}")} and its ID is : {ActiveLicenseID}"
                        , "This License Is No Longer Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }

            _InternationalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseID(uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverID);

            if (_InternationalLicenseID != -1)
            {
                MessageBox.Show($"Person already has an active international license with ID : {_InternationalLicenseID}"
             , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                llShowLicenseInfo.Enabled = true;
                return false;
            }


            return true;
        }

        private void uctrlLocalDrivingLicenseInfoWithFilter_OnSelectedLocalLicense(int LicenseID)
        {
            llShowLicenseHistory.Enabled = (LicenseID != -1);
            _SelectedLocalLicenseID = LicenseID;

            if (LicenseID == -1)
            {
                _CanDriverApply();
                return;
            }

            lblLocalLicenseID.Text = _SelectedLocalLicenseID.ToString();

            if (_DriverID == -1)
            _DriverID = uctrlLocalDrivingLicenseInfoByFilter.SelectedLicenseInfo.DriverID;

            if (_CanDriverApply())
            {
                btnIssueLicense.Enabled = true;
            }
            else
                btnIssueLicense.Enabled = false;
        }

        private void frmNewIntLicenseApplication_Activated(object sender, EventArgs e)
        {
            uctrlLocalDrivingLicenseInfoByFilter.txtLicenseIDFocus();
        }

        private void frmNewIntLicenseApplication_Shown(object sender, EventArgs e)
        {
            clsLocalLicense LicenseFromOrdinaryClass = clsLocalLicense.Find(
                clsLocalLicense.GetLocalLicenseIdByLicenseClass(_DriverID, clsLicenseClass.GetLicenseClassID(clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass)));

            if (_DriverID != -1)
            {
                uctrlLocalDrivingLicenseInfoByFilter.FilterEnabled = false;

                if (LicenseFromOrdinaryClass != null)
                {
                    uctrlLocalDrivingLicenseInfoByFilter.LoadInfo(LicenseFromOrdinaryClass.LicenseID, false);
                }

                else
                    MessageBox.Show("This driver does not have an active ordinary driving license","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to issue license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (_IssueInternationalLicense(_SelectedLocalLicenseID))
            {
                _InternationalLicenseID = _NewInternationalLicense.InternationalLicenseID;

                object[] NewInternationalLicenseInfo = new object[] { _NewInternationalLicense.InternationalLicenseID,_NewInternationalLicense.ApplicationID,_NewInternationalLicense.DriverID,
                        _NewInternationalLicense.IssuedUsingLocalLicenseID,_NewInternationalLicense.IssueDate,_NewInternationalLicense.ExpirationDate,_NewInternationalLicense.IsActive};

                OnIssuedLicense?.Invoke(NewInternationalLicenseInfo);
                AfterIssueingLicense?.Invoke(_DGVRowIndex);

                lblInternationalLicenseAppID.Text = _NewInternationalLicense.ApplicationID.ToString();
                lblInternationalLicenseID.Text = _NewInternationalLicense.InternationalLicenseID.ToString();

                MessageBox.Show($"International License Issued Successfully With ID : {_NewInternationalLicense.InternationalLicenseID}", "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

                uctrlLocalDrivingLicenseInfoByFilter.FilterEnabled = false;
                btnIssueLicense.Enabled = false;
                llShowLicenseInfo.Enabled = true;
            }
            else
                MessageBox.Show("Failed to issue license!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}