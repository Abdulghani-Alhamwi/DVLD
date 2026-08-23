using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace DVLDPresentationLayer
{
    public partial class frmTakeTest : Form
    {
        internal Action<int> OnTestTaken;
        internal Action<int> AfterPassingTest;

        clsTestAppointment _Appointment;
        int _AppointmentsDGVRowIndex;
        int  _LDLAppDGVRowIndex;
        int _LDLAppID;
        public frmTakeTest(clsTestAppointment Appointment, clsTestType.enTestType TestType,int AppointmentsDGVRowIndex,int LDLAppDGVRowIndex)
        {
            InitializeComponent();
            txtNotes.MaxLength = 500;

            clsLDLApplication LDLApp = clsLDLApplication.Find(Appointment.LDLApplicationID);
            _LDLAppID = LDLApp.LDLAppID;
            _LoadInfo(Appointment,LDLApp, TestType);
            
            _Appointment = Appointment;
            this._AppointmentsDGVRowIndex = AppointmentsDGVRowIndex;
            _LDLAppDGVRowIndex = LDLAppDGVRowIndex;

            clsUtility.CenterControlHorizontally(gbTestAppointment, pbTestType);
            clsUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);

        }
        private void _LoadInfo(clsTestAppointment Appointment,clsLDLApplication LDLApp, clsTestType.enTestType TestType)
        {
            frmScheduleTest.ShowInfoByTestType(gbTestAppointment,pbTestType,lblTestFees,TestType);

            lblLDLApplicationID.Text = LDLApp.LDLAppID.ToString();

            if (LDLApp != null)
            {
                lblLicenseClassName.Text = LDLApp.LicenseClass.ClassName;
                lblApplicantFullName.Text = clsPerson.GetFullName(LDLApp.ApplicantPersonID);
            }

            lblDate.Text = Appointment.AppointmentDate.ToString(clsUtility.DateCustomFormat);
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
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnTestTaken?.Invoke(_AppointmentsDGVRowIndex);

                    if (Test.TestResult == true)
                        AfterPassingTest?.Invoke(_LDLAppDGVRowIndex);
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
