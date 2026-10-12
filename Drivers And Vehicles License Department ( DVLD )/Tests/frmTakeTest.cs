using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmTakeTest : Form
    {
        public event Action<int> AfterTestTaken;
        public event Action AfterPassingTest;

        clsTestAppointment _Appointment;
        int _AppointmentsDGVRowIndex;
        int _LocalDrivingLicenseApplicationID;

        public frmTakeTest(clsTestAppointment Appointment, clsTestType.enTestType TestType,int AppointmentsDGVRowIndex)
        {
            InitializeComponent();

            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(Appointment.LocalDrivingLicenseApplicationID);
            _SetInfo(Appointment,LDLApp);
            
            ctrlScheduleTest.ShowInfoByTestType(gbTestAppointment,pbTestType,lblTestFees,TestType);

            this._AppointmentsDGVRowIndex = AppointmentsDGVRowIndex;

            clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, pbTestType);
            clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);
        }

        private void _SetInfo(clsTestAppointment Appointment,clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication)
        {
            txtNotes.MaxLength = 500;

            _Appointment = Appointment;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;

            lblLDLApplicationID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

            if (LocalDrivingLicenseApplication != null)
            {
                lblLicenseClassName.Text = LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
                lblApplicantFullName.Text = clsPerson.GetFullName(LocalDrivingLicenseApplication.ApplicantPersonID);
            }

            lblDate.Text = Appointment.AppointmentDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.NumericFormat));
            lblTime.Text = Appointment.AppointmentDate.ToShortTimeString();

            lblTrialNumber.Text = clsLocalDrivingLicenseApplication.GetTotalTrialsPerTestType(_LocalDrivingLicenseApplicationID,Appointment.TestTypeID).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTest Test = new clsTest
            (
                TestAppointmentID : _Appointment.TestAppointmentID,
                TestResult : (rbPass.Checked) ? true : false,
                Notes : (txtNotes.Text == "") ? null : txtNotes.Text,
                CreatedByUserID : clsGlobalSettings.CurrentUserID
            );

            if (Test.Save())
            {
                lblTestID.Text = Test.TestID.ToString();

                AfterTestTaken?.Invoke(_AppointmentsDGVRowIndex);
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (Test.TestResult == true)
                    AfterPassingTest?.Invoke();

                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Failed To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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