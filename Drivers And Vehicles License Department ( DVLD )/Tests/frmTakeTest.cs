using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmTakeTest : Form
    {
        internal event Action<int> AfterTestTaken;
        internal event Action AfterPassingTest;

        clsTestAppointment _Appointment;
        int _AppointmentsDGVRowIndex;
        int _LDLAppID;
        public frmTakeTest(clsTestAppointment Appointment, clsTestType.enTestType TestType,int AppointmentsDGVRowIndex)
        {
            InitializeComponent();

            clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.Find(Appointment.LDLApplicationID);
            _SetInfo(Appointment,LDLApp);
            
            frmScheduleTest.ShowInfoByTestType(gbTestAppointment,pbTestType,lblTestFees,TestType);

            this._AppointmentsDGVRowIndex = AppointmentsDGVRowIndex;

            clsUtility.CenterControlHorizontally(gbTestAppointment, pbTestType);
            clsUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);
        }
        private void _SetInfo(clsTestAppointment Appointment,clsLocalDrivingLicenseApp LDLApp)
        {
            txtNotes.MaxLength = 500;

            _Appointment = Appointment;
            _LDLAppID = LDLApp.LDLAppID;

            lblLDLApplicationID.Text = LDLApp.LDLAppID.ToString();

            if (LDLApp != null)
            {
                lblLicenseClassName.Text = LDLApp.LicenseClass.ClassName;
                lblApplicantFullName.Text = clsPerson.GetFullName(LDLApp.ApplicantPersonID);
            }

            lblDate.Text = Appointment.AppointmentDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.NumericFormat));
            lblTime.Text = Appointment.AppointmentDate.ToShortTimeString();

            lblTrialNumber.Text = clsTestAppointment.GetTotalAppointmentsCount(_LDLAppID,1).ToString();
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
                _Appointment.IsLocked = true;
                if (_Appointment.Save())
                {
                    AfterTestTaken?.Invoke(_AppointmentsDGVRowIndex);
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Test.TestResult == true)
                        AfterPassingTest?.Invoke();
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