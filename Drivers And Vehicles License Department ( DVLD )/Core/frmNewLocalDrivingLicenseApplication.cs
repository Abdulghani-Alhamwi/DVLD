using System;
using System.Windows.Forms;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__.Core
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
    int _PersonID = -1;
        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void _MoveToNextTab()
        {
            if (_PersonID != -1)
                tcNewLocalDrivingLicenseApplication.SelectedTab = tpApplicationInfo;

            else
                MessageBox.Show("Select a person or add new person first!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            _MoveToNextTab();
        }
    }
}
