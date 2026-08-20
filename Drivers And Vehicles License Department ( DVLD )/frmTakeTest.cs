using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static Driver_And_Vehicle_Licenses_Department___DVLD__.frmScheduleTest;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmTakeTest : Form
    {
        public Action<short> OnTestTaken;
        clsTestAppointment _Appointment;
        short _DGVRowIndex;
        public frmTakeTest(clsTestAppointment Appointment, enTestType TestType,short DGVRowIndex)
        {
            InitializeComponent();
            txtNotes.MaxLength = 500;

            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.Find(Appointment.LDLApplicationID);
            _LoadInfo(Appointment,LDLApp, TestType);
            _Appointment = Appointment;
            _DGVRowIndex = DGVRowIndex;
        }
        private void _LoadInfo(clsTestAppointment Appointment,clsLocalDrivingLicenseApplication LDLApp, enTestType TestType)
        {
            _ShowInfoByTestType(gbTestAppointment,pbTestType,lblTestFees,TestType);

            lblLDLApplicationID.Text = LDLApp.LDLApplicationID.ToString();

            if (LDLApp != null)
            {
                lblLicenseClassName.Text = LDLApp.LicenseClass.ClassName;
                lblApplicantFullName.Text = clsPerson.GetFullName(LDLApp.ApplicantPersonID);
            }

            lblDate.Text = Appointment.AppointmentDate.ToShortDateString();
            lblTime.Text = Appointment.AppointmentDate.ToShortTimeString();

            lblTrialNumber.Text = Trial.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTest Test = new clsTest
            {
                TestAppointmentID = _Appointment.TestAppointmentID,
                TestResult = (rbPass.Checked) ? true : false,
                Notes = (txtNotes.Text == "") ? null : txtNotes.Text,
                CreatedByUserID = clsGlobalSettings.CurrentUserID
            };

            if (Test.Save())
            {
                lblTestID.Text = Test.TestID.ToString();
                _Appointment.IsLocked = true;
                if (_Appointment.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnTestTaken?.Invoke(_DGVRowIndex);
                }
                btnSave.Enabled = false; 
            }
            else
                MessageBox.Show("Failed To Save Data", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
