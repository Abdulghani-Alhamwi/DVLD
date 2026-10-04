using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Controls;
using Utility_Library;
using static Utility_Library.clsGeneralUtility;

namespace DVLDPresentationLayer.Core
{
    public partial class frmNewLDLApplication : Form
    {
        internal delegate void AddedLDLApplication(object[] NewAppDetails);
        internal event AddedLDLApplication OnAddedLDLApplication;

        internal delegate void EditedLDLApplication(object[] ModifiedAppDetails,int DGVRowIndex);
        internal event EditedLDLApplication OnEditedLDLApplication;

        public event Action<clsPerson,int> OnEditedApplicantPersonalInfo;

        private int _ApplicantPersonID = -1;
        clsLocalDrivingLicenseApp _LDLApplication;

        private int _DGVRowIndex = -1;
        private DataView _LicenseClassesDataView;

        public frmNewLDLApplication()
        {
            InitializeComponent();
            _InitializeFormData();
        }

        public frmNewLDLApplication(clsLocalDrivingLicenseApp LDLApplication ,int DGVRowIndex)
        {
            InitializeComponent();
            _InitilizeFormData(LDLApplication, DGVRowIndex);
        }

        private void _InitilizeFormData(clsLocalDrivingLicenseApp LDLApplication, int DGVRowIndex)
        {
            if(LDLApplication == null)
                _SetTitles(clsLocalDrivingLicenseApp.enMode.AddNew);
            else
            {
                _LDLApplication = LDLApplication;
                _SetTitles(clsLocalDrivingLicenseApp.enMode.Update);
                _ApplicantPersonID = _LDLApplication.ApplicantPersonID;
            }
            _DGVRowIndex = DGVRowIndex;
            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void _InitializeFormData()
        {
            _InitilizeFormData(null,0);
        }

        private void _SetTitles(clsLocalDrivingLicenseApp.enMode Mode)
        {
            if (Mode == clsLocalDrivingLicenseApp.enMode.AddNew)
            {
                lblFormTitle.Text = "Add New Local Driving License Application";
                lblFormBigTitle.Text = "Add New Local Driving License Application";
            }
            else
            {
                lblFormTitle.Text = "Update Local Driving License Application";
                lblFormBigTitle.Text = "Update Local Driving License Application";
            }
        }

        private void _ShowDetailsForUpdateMode()
        {
            uctrlPersonDetailsByFilter.LoadPersonDetails(_LDLApplication.ApplicantPersonID);
            lblDLApplicationID.Text = _LDLApplication.ApplicationID.ToString();
            cbLicenseClass.SelectedIndex = _LicenseClassesDataView.Find(_LDLApplication.LicenseClass.ClassName);
            lblApplicationDate.Text = _LDLApplication.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = clsGeneralUtility.GetCustomNumberFormat(_LDLApplication.PaidApplicationFees, enCustomNumberFormat.NoJustZerosAfterFraction);

            string EncryptedUsername = "", UsernameIV = "";
            clsUser.GetUserName(_LDLApplication.CreatedByUserID, ref EncryptedUsername, ref UsernameIV);

            lblUserName.Text = clsGeneralUtility.DecryptString(clsGlobalSettings.EncryptionKey, EncryptedUsername, UsernameIV);
        }

        private bool _MoveToNextTab()
        {
            if (_ApplicantPersonID != -1)
            {
                tcNewLDLApplication.SelectedTab = tpApplicationInfo;
                return true;
            }

            else
                MessageBox.Show("Select a person or add new person first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _MoveToNextTab();
        }

        private void uctrlPersonDetailsByFilter_OnPersonSelected(int PersonID)
        {
            _ApplicantPersonID = PersonID;
        }

        private void tcNewLocalDrivingLicenseApplication_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tcNewLDLApplication.SelectedTab == tpApplicationInfo)
            {
                if (!_MoveToNextTab())
                    tcNewLDLApplication.SelectedTab = tpPersonalInfo;
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

        private void _ShowLDLAppDetails()
        {
            lblApplicationDate.Text = DateTime.Today.ToShortDateString();

            cbLicenseClass.SelectedIndex = _LicenseClassesDataView.Find(clsLicenseClass.GetLicenseClassName(clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass));

            lblApplicationFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.NewLocalDrivingLicense),
                enCustomNumberFormat.NoJustZerosAfterFraction);
            lblUserName.Text = clsGlobalSettings.CurrentUsername;
        }

        private void _AddLicenseClassesToComboBox()
        {
            _LicenseClassesDataView = clsLicenseClass.GetLicenseClassesNames().DefaultView;
            _LicenseClassesDataView.Sort = "ClassName ASC";
            cbLicenseClass.DataSource = _LicenseClassesDataView;
        }

        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _AddLicenseClassesToComboBox();

            if (_LDLApplication == null)
                _ShowLDLAppDetails();

            else
                _ShowDetailsForUpdateMode();
        }

        private void cbLicenseClass_DropDown(object sender, EventArgs e)
        {
            cbLicenseClass.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void cbLicenseClass_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsGeneralUtility.DrawComboBoxItems(sender, e,"ClassName");
        }

        private void cbLicenseClass_DropDownClosed(object sender, EventArgs e)
        {
            cbLicenseClass.BackColor = Color.FromArgb(228, 228, 228);
        }

        private void _UpdateLDLApplicationInfo()
        {
            _LDLApplication.LicenseClass.ID = clsLicenseClass.GetLicenseClassID(((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString());
            _LDLApplication.LicenseClass.ClassName = ((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString();
            _LDLApplication.ApplicantPersonID = _ApplicantPersonID;
            _LDLApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            _LDLApplication.LastStatusDate = DateTime.Now;
        }

        private clsLocalDrivingLicenseApp _SaveLDLApplication()
        {
            if (_LDLApplication != null)
            {
                if (_LDLApplication.Save())
                    return _LDLApplication;
            }

            else
            {
                clsLocalDrivingLicenseApp LDLApplication = new clsLocalDrivingLicenseApp(
                ApplicantPersonID: _ApplicantPersonID,
                LicenseClassID: clsLicenseClass.GetLicenseClassID(((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString()),
                LicenseClassName: ((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString(),
                ApplicationDate: DateTime.Now,
                ApplicationTypeID: clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.NewLocalDrivingLicense),
                ApplicationStatus: clsApplication.enApplicationStatus.New,
                LastStatusDate: DateTime.Now,
                PaidApplicationFees: Convert.ToDecimal(lblApplicationFees.Text),
                CreatedByUserID: clsGlobalSettings.CurrentUserID
              );

                if (LDLApplication.Save())
                    return LDLApplication;
            }
                    return null;
        }

        private bool _CanPersonApply()
        {
            byte LicenseClassID = clsLicenseClass.GetLicenseClassID(((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString());
            clsApplication.enApplicationStatus PersonApplicationStatus = 0;

            if (!clsLocalDrivingLicenseApp.HasPersonApplied(_ApplicantPersonID, LicenseClassID, ref PersonApplicationStatus))
            {
                if (clsLocalDrivingLicenseApp.IsPersonAgeAppropriate(_ApplicantPersonID, LicenseClassID))
                    return true;

                else
                {
                    MessageBox.Show($"Person is younger than the minimum allowed age to apply for this license class which is : {clsLicenseClass.GetMinimumAllowedAge(LicenseClassID)} years.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }

            else
            {
                switch (PersonApplicationStatus)
                {
                    case clsApplication.enApplicationStatus.New:
                        MessageBox.Show("Choose another license class,the selected person already has an active application of this selected class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsApplication.enApplicationStatus.Completed:
                        MessageBox.Show($"Choose another license class,the selected person already has an active application for the selected class with ID : {clsLocalDrivingLicenseApp.GetLDLApplicationID(_ApplicantPersonID)}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case clsApplication.enApplicationStatus.Canceled:
                        return true;
                }

                return false;
            }
        }

        private void _SaveApplicationData()
        {
            clsLocalDrivingLicenseApp LDLApplication = _SaveLDLApplication();

            if (LDLApplication != null)
            {
                MessageBox.Show("Data Saved successfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                object[] NewDetails = new object[] { LDLApplication.LDLAppID, LDLApplication.LicenseClass.ClassName,
                        clsPerson.GetNationalNumber(LDLApplication.ApplicantPersonID), clsPerson.GetFullName(LDLApplication.ApplicantPersonID),
                        LDLApplication.ApplicationDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)),clsTest.GetTotalPassedTestsCount(LDLApplication.LDLAppID),LDLApplication.GetApplicationStatus()};

                if (_LDLApplication == null)
                {
                    lblDLApplicationID.Text = LDLApplication.LDLAppID.ToString();
                    _LDLApplication = LDLApplication;
                    _SetTitles(clsLocalDrivingLicenseApp.enMode.Update);

                    OnAddedLDLApplication?.Invoke(NewDetails);
                }
                else
                    OnEditedLDLApplication?.Invoke(NewDetails, _DGVRowIndex);
            }
            else
                MessageBox.Show("Saving failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_ApplicantPersonID != -1)
            {
                if (_LDLApplication != null)
                {
                    _UpdateLDLApplicationInfo();

                    if (_LDLApplication.AreAllFieldsOldValuesNotChanged())
                    {
                        MessageBox.Show("There is'nt any change on the information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (_CanPersonApply())
                {
                    _SaveApplicationData();
                    btnSave.Enabled = false;
                }
            }
            else
                MessageBox.Show("Select a person or add new person first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void uctrlPersonDetailsByFilter_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
            OnEditedApplicantPersonalInfo?.Invoke(UpdatedPersonInfo, _DGVRowIndex);
        }
    }
}