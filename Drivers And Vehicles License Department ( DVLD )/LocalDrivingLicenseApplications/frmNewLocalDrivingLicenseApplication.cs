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
        clsLocalDrivingLicenseApplication _LDLApplication;
        //clsApplications _Application;

        public event Action AfterAddOrUpdate ;
        public frmNewLocalDrivingLicenseApplication(clsLocalDrivingLicenseApplication LDLApplication = null)
        {
            InitializeComponent();

            if(LDLApplication == null)
                _SetTitles(clsLocalDrivingLicenseApplication.enMode.AddNew);

            else
            {
                _LDLApplication = LDLApplication;
                _SetTitles(clsLocalDrivingLicenseApplication.enMode.Update);
                _PersonID = _LDLApplication.ApplicantPersonID;
                _ShowDetailsForUpdateMode();
            }
            lblFormBigTitle.Location = new Point(this.Width/2 - lblFormBigTitle.Width/2, lblFormBigTitle.Location.Y);
        }

        private void _SetTitles(clsLocalDrivingLicenseApplication.enMode Mode)
        {
            if (Mode == clsLocalDrivingLicenseApplication.enMode.AddNew)
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
            cbLicenseClass.SelectedItem = clsLicenseClasses.GetLicenseClassName(_LDLApplication.LicenseClassID);
            lblApplicationDate.Text = _LDLApplication.ApplicationDate.ToShortDateString();
            lblApplicationFees.Text = _LDLApplication.PaidApplicationFees.ToString();
            lblUserName.Text = clsUtility.DecryptUserName(clsUser.GetUserName(_LDLApplication.CreatedByUserID));
        }
        private bool _MoveToNextTab()
        {
            if (_PersonID != -1)
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
            return (_LDLApplication.ApplicantPersonID == _PersonID && ((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString() == clsLicenseClasses.GetLicenseClassName(_LDLApplication.LicenseClassID));
        }
        private bool _SaveLDLApplication(out clsLocalDrivingLicenseApplication LDLApplication)
        {
            if (_LDLApplication != null)
                LDLApplication = _LDLApplication;
            else
                LDLApplication = new clsLocalDrivingLicenseApplication();

            LDLApplication.LicenseClassID = clsLicenseClasses.GetLicenseClassID(((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString());
            LDLApplication.ApplicantPersonID = _PersonID;
            LDLApplication.ApplicationDate = DateTime.Now;
            LDLApplication.ApplicationTypeID = _NewLocalDrivingLicenseApplicationTypeID;
            LDLApplication.ApplicationStatus = clsApplication.enApplicationStatus.New;
            LDLApplication.LastStatusDate = DateTime.Now;
            LDLApplication.PaidApplicationFees = Convert.ToDouble(lblApplicationFees.Text);
            LDLApplication.CreatedByUserID = clsGlobalSettings.CurrentUserID;

            return LDLApplication.Save();
        }

        private bool _CanPersonApply()
        {
            int LicenseClassID = clsLicenseClasses.GetLicenseClassID(((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString());
            clsApplication.enApplicationStatus? PersonApplicationStatus;

            if (clsLocalDrivingLicenseApplication.CanPersonApply(_PersonID, LicenseClassID, out PersonApplicationStatus))
                return true;

            else
            {
                if (PersonApplicationStatus == clsApplication.enApplicationStatus.New)
                    MessageBox.Show($"Choose another license class,the selected person already an active application for the selected class with ID : {clsLocalDrivingLicenseApplication.GetLDLApplicationID(_PersonID)}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

                else if (PersonApplicationStatus == clsApplication.enApplicationStatus.Completed)
                    MessageBox.Show("Choose another license class,the selected person already has an active license of this selected class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
            if(_LDLApplication != null)
            {
                if(_IsInfoUnchanged())
                {
                    MessageBox.Show("There is'nt any change on the information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

                if (_CanPersonApply())
                {
                    clsLocalDrivingLicenseApplication LDLApplication;
                    if (_SaveLDLApplication(out LDLApplication))
                    {
                        lblDLApplicationID.Text = LDLApplication.LocalDrivingLicenseApplicationID.ToString();
                        MessageBox.Show("Data Saved successfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (_LDLApplication == null)
                        {
                            _LDLApplication = LDLApplication;
                            _SetTitles(clsLocalDrivingLicenseApplication.enMode.Update);
                        }
                        AfterAddOrUpdate?.Invoke();
                    }
                    else
                        MessageBox.Show("Saving failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Select a person or add new person first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
