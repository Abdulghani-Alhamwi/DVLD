using System;
using System.Windows.Forms;
using DVLDPresentationLayer.Properties;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmScheduleTest : Form
    {
        internal delegate void ScheduledAppointment(ref object[] NewValues);
        internal event ScheduledAppointment AfterSchedulingAppointment;

        internal delegate void EditedScheduledAppointment(ref object[] NewValues,int DGVRowIndex);
        internal event EditedScheduledAppointment AfterEditingAppointment;
        public enum enTestTrial {FirstTime = 0 , ReTake = 1, Taken = 2}

        private clsLocalDrivingLicenseApp _LDLApp;

        private clsTestAppointment _Appointment;

        private clsTestType.enTestType _TestType;

        private enTestTrial _TestTrial;

        private int _AppointmentsDGVRowIndex = -1;

        private bool _IsLockedMode;
        public frmScheduleTest(int LDLAppID, clsTestType.enTestType TestType, enTestTrial TestTrial)
        {
            _InitializeFormData(LDLAppID, TestType, TestTrial);
        }
        public frmScheduleTest(int LDLAppID , clsTestType.enTestType TestType , enTestTrial TestTrial, clsTestAppointment Appointment)
        {
            _IsLockedMode = true;
            _InitializeFormData(LDLAppID, TestType, TestTrial,Appointment);
        }
        public frmScheduleTest(int LDLAppID, clsTestType.enTestType TestType, enTestTrial TestTrial, clsTestAppointment Appointment , int AppointmentsDGVRowIndex)
        {
            _InitializeFormData(LDLAppID, TestType, TestTrial,Appointment);
            _AppointmentsDGVRowIndex = AppointmentsDGVRowIndex;
        }
        public void _SetControlsForLockedAppointment(bool IsPassedTest)
        {
            if (!IsPassedTest)
            {
                lblFormBigTitle.Text = "Schedule Retake Test";
                lblNote.Text = "Person already sat for the test , appointment locked.";
                gbReTakeTestInfo.Enabled = true;
            }
            else
            {
                lblFormBigTitle.Text = "Test Have Been Taken";
                lblNote.Text = "Person already has taken the test , appointment locked.";
                gbReTakeTestInfo.Enabled = false;
            }
            lblNote.Visible = true;

            clsUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);
            clsUtility.CenterControlHorizontally(gbTestAppointment, lblNote);

            btnSave.Enabled = false;
            dtpTestAppointmentDate.Enabled = false;
            mtxtAppointmentTime.Enabled = false;
        }

        private void _InitializeFormData(int LDLAppID, clsTestType.enTestType TestType, enTestTrial TestTrial, clsTestAppointment Appointment = null)
        {
            InitializeComponent();
            dtpTestAppointmentDate.Format = DateTimePickerFormat.Custom;
            dtpTestAppointmentDate.CustomFormat = clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.NumericFormat);

            _TestType = TestType;
            _TestTrial = TestTrial;
            clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.Find(LDLAppID);

            if(Appointment != null)
            _Appointment = Appointment;

            _LDLApp = LDLApp;
            _LoadInfo(TestType, TestTrial);

            if (!_IsLockedMode)
            {
                dtpTestAppointmentDate.MinDate = DateTime.Now;
                dtpTestAppointmentDate.MaxDate = dtpTestAppointmentDate.MinDate.AddMonths(3);
            }
        }
        
        private bool _AddReTakeTestApp(out clsApplication Application)
        {
            Application = new clsApplication(
                ApplicantPersonID : _LDLApp.ApplicantPersonID,
                ApplicationDate : DateTime.Now,
                ApplicationStatus : clsApplication.enApplicationStatus.New,
                LastStatusDate : DateTime.Now,
                PaidApplicationFees : Convert.ToDecimal(lblRAppFees.Text),
                ApplicationTypeID : clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.ReTakeTest),
                CreatedByUserID : clsGlobalSettings.CurrentUserID
            );

            return Application.Save();
        }

        private void _LoadInfo(clsTestType.enTestType TestType,enTestTrial TestTrial)
        {
            ShowInfoByTestType(gbTestAppointment,pbTestType,lblTestFees,TestType);

            clsUtility.CenterControlHorizontally(gbTestAppointment, pbTestType);

            lblLDLApplicationID.Text = _LDLApp.LDLAppID.ToString();

            lblLicenseClassName.Text = _LDLApp.LicenseClass.ClassName;
            lblApplicantFullName.Text = clsPerson.GetFullName(_LDLApp.ApplicantPersonID);

            if (TestTrial == enTestTrial.ReTake)
            {
                gbReTakeTestInfo.Enabled = true;
                lblFormBigTitle.Text = "Schedule Retake Test";
                lblRAppFees.Text =  clsUtility.SetFeesToCustomFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReTakeTest));
                lblTotalFees.Text = clsUtility.SetFeesToCustomFormat(Convert.ToDecimal(lblTestFees.Text) + Convert.ToDecimal(lblRAppFees.Text));
            }
            else
            {
                lblRAppFees.Text = "0";
                lblTotalFees.Text = lblTestFees.Text;
                lblReTestAppID.Text = "N/A";
            }

            if(_Appointment != null)
            {
                dtpTestAppointmentDate.Value = _Appointment.AppointmentDate;    
                mtxtAppointmentTime.Text = _Appointment.AppointmentDate.ToString("hh:mm tt");
            }
            clsUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);
            lblTrialNumber.Text = clsTestAppointment.GetTotalAppointmentsCount(_LDLApp.LDLAppID,1).ToString();
        }

        public static void ShowInfoByTestType(GroupBox gbContentContainer, PictureBox pbTestType, Label lblFees , clsTestType.enTestType TestType)
        {
            switch(TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    gbContentContainer.Text = "Vision Test";
                    pbTestType.Image = Resources.Vision_512;
                    lblFees.Text = clsUtility.SetFeesToCustomFormat(clsTestType.GetTestTypeFees(1));
                    break;

                case clsTestType.enTestType.WrittenTest:
                    gbContentContainer.Text = "Written Test";
                    pbTestType.Image =Resources.Written_Test_512;
                    lblFees.Text = clsUtility.SetFeesToCustomFormat(clsTestType.GetTestTypeFees(2));
                    break;

                case clsTestType.enTestType.StreetTest:
                    gbContentContainer.Text = "Street Test";
                    pbTestType.Image = Resources.driving_test_512;
                    lblFees.Text = clsUtility.SetFeesToCustomFormat(clsTestType.GetTestTypeFees(3));
                    break;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DateTime _GetAppointmentDate()
        {
            short EnteredHour = Convert.ToInt16(mtxtAppointmentTime.Text.Substring(0, 2));
            if(mtxtAppointmentTime.Text.Substring(6, 2) == "PM")
            {
                switch (EnteredHour)
                {
                    case 1:
                        EnteredHour = 13;
                        break;

                    case 2:
                        EnteredHour = 14;
                        break;

                    case 3:
                        EnteredHour = 15;
                        break;

                    case 4:
                        EnteredHour = 16;
                        break;

                    case 5:
                        EnteredHour = 17;
                        break;

                    case 6:
                        EnteredHour = 18;
                        break;

                    case 7:
                        EnteredHour = 19;
                        break;

                    case 8:
                        EnteredHour = 20;
                        break;

                    case 9:
                        EnteredHour = 21;
                        break;

                    case 10:
                        EnteredHour = 22;
                        break;

                    case 11:
                        EnteredHour = 23;
                        break;
                }
            }
            else
            {
                if (EnteredHour == 12)
                    EnteredHour = 24;
            }
              DateTime AppointmentDate = new DateTime(dtpTestAppointmentDate.Value.Year, dtpTestAppointmentDate.Value.Month, dtpTestAppointmentDate.Value.Day,
                                        EnteredHour, Convert.ToInt32(mtxtAppointmentTime.Text.Substring(3, 2)), 0, DateTimeKind.Local);
            
            return AppointmentDate;
        }

        private void _SaveAppointment(clsTestAppointment Appointment)
        {
            if (Appointment.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_Appointment == null)
                    _Appointment = Appointment;

                object[] NewValues = new object[] { _Appointment.TestAppointmentID, _Appointment.AppointmentDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)), _Appointment.PaidFees, _Appointment.IsLocked };

                AfterSchedulingAppointment?.Invoke(ref NewValues);
                AfterEditingAppointment?.Invoke(ref NewValues, _AppointmentsDGVRowIndex);

                btnSave.Enabled = false;
            }
            else
                MessageBox.Show("Failed to save data", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTestAppointment Appointment;
            if (_Appointment == null)
            {
                byte _TestTypeID = clsTestType.GetTestTypeID(_TestType);
                Appointment = new clsTestAppointment(
                TestTypeID: _TestTypeID,
                LDLApplicationID: _LDLApp.LDLAppID,
                AppointmentDate: _GetAppointmentDate(),
                PaidFees: clsTestType.GetTestTypeFees(_TestTypeID),
                CreatedByUserID: clsGlobalSettings.CurrentUserID,
                IsLocked: false
                );
            }
            else
            {
                Appointment = _Appointment;
                Appointment.AppointmentDate = _GetAppointmentDate();
            }

            if (_TestTrial == enTestTrial.ReTake)
            {
                clsApplication Application;
                if (_AddReTakeTestApp(out Application))
                {
                    lblReTestAppID.Text = Application.ApplicationID.ToString();
                    _SaveAppointment(Appointment);
                }
                else
                    MessageBox.Show("Failed to save the retake test application", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _SaveAppointment(Appointment);
        }
        private void mtxtAppointmentTime_Click(object sender, EventArgs e)
        {
          mtxtAppointmentTime.Text = "";
        }
        private void mtxtAppointmentTime_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(mtxtAppointmentTime.Text.Substring(0,1) == " ")
                clsUtility.EnableErrorProvider(erTime, mtxtAppointmentTime, "Time cannot be empty!", e);

            else if (!DateTime.TryParse(mtxtAppointmentTime.Text, out DateTime dt))
                clsUtility.EnableErrorProvider(erTime, mtxtAppointmentTime, "Invalid time format , format must be like : 10:00 AM", e);

            else
                erTime.Dispose();
        }
        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            btnClose.CausesValidation = false;
            btnExit.CausesValidation = false;
        }
    }
}
