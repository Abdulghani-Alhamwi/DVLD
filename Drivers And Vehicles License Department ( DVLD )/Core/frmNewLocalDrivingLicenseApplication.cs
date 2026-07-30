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
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                clsApplications Application = new clsApplications();
                Application.ApplicantPersonID = _PersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = _NewLocalDrivingLicenseApplicationTypeID;
                Application.ApplicationStatus = clsApplications.enApplicationStatus.New;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidApplicationFees = Convert.ToDouble(lblApplicationFees.Text);
                Application.CreatedByUserID = clsGlobalSettings.CurrentUserID;

                if(Application.Save())
                {
                    clsLocalDrivingLicenseApplications LDLApplication = new clsLocalDrivingLicenseApplications();

                    LDLApplication.ApplicationID = Application.ApplicationID;
                    LDLApplication.LicenseClassID = clsLicenseClasses.GetLicenseClassID(((DataRowView)cbLicenseClass.SelectedItem).Row["ClassName"].ToString());

                    if (LDLApplication.Save())
                    {
                        lblDLApplicationID.Text = LDLApplication.LocalDrivingLicenseApplicationID.ToString();
                        MessageBox.Show("Data Saved successfully", "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
