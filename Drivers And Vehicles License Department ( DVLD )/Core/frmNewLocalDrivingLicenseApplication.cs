using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__.Core
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        private int _PersonID = -1;
        private const int _NewLocalDrivingLicenseApplicationTypeID = 1;
        clsLocalDrivingLicenseApplications _LDLApplication;
        clsApplications _Application;

        public event Action AfterAddOrUpdate ;
        public frmNewLocalDrivingLicenseApplication(clsLocalDrivingLicenseApplications LDLApplication = null)
        {
            InitializeComponent();

            if(LDLApplication == null)
                _SetTitles(clsLocalDrivingLicenseApplications.enMode.AddNew);

            else
            {
                _LDLApplication = LDLApplication;
                _SetTitles(clsLocalDrivingLicenseApplications.enMode.Update);
                _Application = clsApplications.Find(_LDLApplication.ApplicationID);
                _PersonID = _Application.ApplicantPersonID;
                _ShowDetailsForUpdateMode();
            }
            lblFormBigTitle.Location = new Point(this.Width/2 - lblFormBigTitle.Width/2, lblFormBigTitle.Location.Y);
        }

        private void _SetTitles(clsLocalDrivingLicenseApplications.enMode Mode)
        {
            if (Mode == clsLocalDrivingLicenseApplications.enMode.AddNew)
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
            uctrlPersonDetailsByFilter.LoadPersonDetails(_Application.ApplicantPersonID);
            lblDLApplicationID.Text = _LDLApplication.ApplicationID.ToString();
            cbLicenseClass.SelectedItem = clsLicenseClasses.GetLicenseClassName(_LDLApplication.LicenseClassID);
            lblApplicationDate.Text = _Application.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = _Application.PaidApplicationFees.ToString();
            lblUserName.Text = clsUtility.DecryptUserName(clsUser.GetUserName(_Application.CreatedByUserID));
        }
        private bool _MoveToNextTab()
        {
            if (_PersonID != -1 || _LDLApplication != null)
            {
                tcNewLocalDrivingLicenseApplication.SelectedTab = tpApplicationInfo;
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
            _PersonID = PersonID;
        }

        private void tcNewLocalDrivingLicenseApplication_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tcNewLocalDrivingLicenseApplication.SelectedTab == tpApplicationInfo)
            {
                if (!_MoveToNextTab())
                    tcNewLocalDrivingLicenseApplication.SelectedTab = tpPersonalInfo;
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

        private void _ShowLocalDrivingApplicationDetails()
        {
            lblApplicationDate.Text = DateTime.Today.ToShortDateString();
            cbLicenseClass.DataSource = clsLicenseClasses.GetLicenseClassesNames();
            cbLicenseClass.SelectedIndex = 2;
            lblApplicationFees.Text = clsApplicationTypes.GetApplicationTypeFees(_NewLocalDrivingLicenseApplicationTypeID).ToString();
            lblUserName.Text = clsGlobalSettings.CurrentUserName;
        }
        private void frmNewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ShowLocalDrivingApplicationDetails();
        }

        private void cbLicenseClass_DropDown(object sender, EventArgs e)
        {
            cbLicenseClass.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void cbLicenseClass_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e,"ClassName");
        }

        private void cbLicenseClass_DropDownClosed(object sender, EventArgs e)
        {
            cbLicenseClass.BackColor = Color.FromArgb(228, 228, 228);
        }

        private bool _IsInfoUnchanged()
        {
            return (_Application.ApplicantPersonID == _PersonID && cbLicenseClass.SelectedItem.ToString() == clsLicenseClasses.GetLicenseClassName(_LDLApplication.LicenseClassID));
        }

        private bool _SaveApplication(out clsApplications Application)
        {
            if (_Application != null)
                Application = _Application;

            else
                Application = new clsApplications();

            Application.ApplicantPersonID = _PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = _NewLocalDrivingLicenseApplicationTypeID;
            Application.ApplicationStatus = clsApplications.enApplicationStatus.New;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidApplicationFees = Convert.ToDouble(lblApplicationFees.Text);
            Application.CreatedByUserID = clsGlobalSettings.CurrentUserID;
            
            return Application.Save();
        }

        private bool _SaveLDLApplication(out clsLocalDrivingLicenseApplications LDLApplication,clsApplications Application)
        {
            if (_LDLApplication != null)
                LDLApplication = _LDLApplication;
            else
                LDLApplication = new clsLocalDrivingLicenseApplications();

            LDLApplication.ApplicationID = Application.ApplicationID;
            LDLApplication.LicenseClassID = clsLicenseClasses.GetLicenseClassID(((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString());

            return LDLApplication.Save();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {

            if(_LDLApplication!=null)
            {
                if(_IsInfoUnchanged())
                {
                    MessageBox.Show("There are no changes", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
                clsApplications Application;
                if (_SaveApplication(out Application))
                {
                    clsLocalDrivingLicenseApplications LDLApplication;

                    if (_SaveLDLApplication(out LDLApplication,Application))
                    {
                        lblDLApplicationID.Text = LDLApplication.LocalDrivingLicenseApplicationID.ToString();
                        MessageBox.Show("Data Saved successfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        if(_LDLApplication == null)
                        {
                            _LDLApplication = LDLApplication;
                            _SetTitles(clsLocalDrivingLicenseApplications.enMode.Update);
                        }
                        AfterAddOrUpdate?.Invoke();
                    }
                    else
                        MessageBox.Show("Saving failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Failed to create application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
                MessageBox.Show("Select a person or add new person first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
