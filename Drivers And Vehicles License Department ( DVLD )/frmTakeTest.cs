using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static Driver_And_Vehicle_Licenses_Department___DVLD__.frmScheduleTest;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmTakeTest : Form
    {
        int _TestAppointmentID;
        public frmTakeTest(int TestAppointmentID, enTestType TestType)
        {
            InitializeComponent();
            txtNotes.MaxLength = 500;

            clsTestAppointment Appointment = clsTestAppointment.Find(TestAppointmentID);

            if(Appointment != null)
            {
                clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.Find(Appointment.LDLApplicationID);
                _LoadInfo(Appointment,LDLApp, TestType);
                _TestAppointmentID = TestAppointmentID;
            }
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

        }
    }
}
