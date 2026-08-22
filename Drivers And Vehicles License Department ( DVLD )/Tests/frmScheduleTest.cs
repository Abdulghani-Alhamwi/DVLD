using System;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;
using DVLDBusinessLayer;
using MyLib;
using static DVLDBusinessLayer.clsApplicationTypes;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmScheduleTest : Form
    {
        internal delegate void ScheduledAppointment(ref object[] NewValues);
        internal event ScheduledAppointment AfterSchedulingAppointment;

        internal delegate void EditedScheduledAppointment(ref object[] NewValues,int DGVRowIndex);
        internal event EditedScheduledAppointment AfterEditingAppointment;
        public enum enTestTrial {FirstTime = 0 , ReTake = 1, Taken = 2}

        private clsLDLApplication _LDLApp;

        private clsTestAppointment _Appointment;

        private clsTestTypes.enTestType _TestType;

        private enTestTrial _TestTrial;

        private int _AppointmentsDGVRowIndex = -1;

        private bool _IsLockedMode;
        public frmScheduleTest(int LDLAppID, clsTestTypes.enTestType TestType, enTestTrial TestTrial)
        {
            _InitializeFormData(LDLAppID, TestType, TestTrial);
        }
        public frmScheduleTest(int LDLAppID , clsTestTypes.enTestType TestType , enTestTrial TestTrial, clsTestAppointment Appointment)
        {
            _IsLockedMode = true;
            _InitializeFormData(LDLAppID, TestType, TestTrial,Appointment);
        }
        public frmScheduleTest(int LDLAppID, clsTestTypes.enTestType TestType, enTestTrial TestTrial, clsTestAppointment Appointment , int AppointmentsDGVRowIndex)
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

        private void _InitializeFormData(int LDLAppID, clsTestTypes.enTestType TestType, enTestTrial TestTrial, clsTestAppointment Appointment = null)
        {
            InitializeComponent();
            dtpTestAppointmentDate.Format = DateTimePickerFormat.Custom;
            dtpTestAppointmentDate.CustomFormat = clsUtility.DateCustomFormat;

            _TestType = TestType;
            _TestTrial = TestTrial;
            clsLDLApplication LDLApp = clsLDLApplication.Find(LDLAppID);

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
            Application = new clsApplication()
            {
                ApplicantPersonID = _LDLApp.ApplicantPersonID,
                ApplicationDate = DateTime.Now,
                ApplicationStatus = clsApplication.enApplicationStatus.New,
                LastStatusDate = DateTime.Now,
                PaidApplicationFees = Convert.ToDecimal(lblRAppFees.Text),
                ApplicationTypeID = clsApplicationTypes.GetApplicationTypeID(clsApplicationTypes.enApplicationType.ReTakeTest),
                CreatedByUserID = clsGlobalSettings.CurrentUserID
            };

            return Application.Save();
        }

        private void _LoadInfo(clsTestTypes.enTestType TestType,enTestTrial TestTrial)
        {
            ShowInfoByTestType(gbTestAppointment,pbTestType,lblTestFees,TestType);

            clsUtility.CenterControlHorizontally(gbTestAppointment, pbTestType);

            lblLDLApplicationID.Text = _LDLApp.LDLApplicationID.ToString();

            lblLicenseClassName.Text = _LDLApp.LicenseClass.ClassName;
            lblApplicantFullName.Text = clsPerson.GetFullName(_LDLApp.ApplicantPersonID);

            if (TestTrial == enTestTrial.ReTake)
            {
                gbReTakeTestInfo.Enabled = true;
                lblFormBigTitle.Text = "Schedule Retake Test";
                lblRAppFees.Text = Convert.ToSingle(clsApplicationTypes.GetApplicationTypeFees(clsApplicationTypes.GetApplicationTypeID(clsApplicationTypes.enApplicationType.ReTakeTest))).ToString();
                lblTotalFees.Text = Convert.ToSingle(Convert.ToDecimal(lblTestFees.Text) + Convert.ToDecimal(lblRAppFees.Text)).ToString();
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
            lblTrialNumber.Text = clsTestAppointment.GetTotalAppointmentsCount(_LDLApp.LDLApplicationID,1).ToString();
        }

        public static void ShowInfoByTestType(GroupBox gbContentContainer, PictureBox pbTestType, Label lblFees , clsTestTypes.enTestType TestType)
        {
            switch(TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    gbContentContainer.Text = "Vision Test";
                    pbTestType.Image = Resources.Vision_512;
                    lblFees.Text = Convert.ToSingle(clsTestTypes.GetTestTypeFees(1)).ToString();
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    gbContentContainer.Text = "Written Test";
                    pbTestType.Image =Resources.Written_Test_512;
                    lblFees.Text = Convert.ToSingle(clsTestTypes.GetTestTypeFees(2)).ToString();
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    gbContentContainer.Text = "Street Test";
                    pbTestType.Image = Resources.driving_test_512;
                    lblFees.Text = Convert.ToSingle(clsTestTypes.GetTestTypeFees(3)).ToString();
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
              DateTime AppointmentDate = new DateTime(dtpTestAppointmentDate.Value.Year, dtpTestAppointmentDate.Value.Month, dtpTestAppointmentDate.Value.Day, EnteredHour, Convert.ToInt32(mtxtAppointmentTime.Text.Substring(3, 2)), 0, DateTimeKind.Local);
            
            return AppointmentDate;
        }

        private void _SaveAppointment(clsTestAppointment Appointment)
        {
            if (Appointment.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_Appointment == null)
                    _Appointment = Appointment;

                object[] NewValues = new object[] { _Appointment.TestAppointmentID, _Appointment.AppointmentDate.ToString(clsUtility.DateCustomFormat), _Appointment.PaidFees, _Appointment.IsLocked };

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
                Appointment = new clsTestAppointment();
                Appointment.TestTypeID = clsTestTypes.GetTestTypeID(_TestType);
                Appointment.LDLApplicationID = _LDLApp.LDLApplicationID;
                Appointment.PaidFees = clsTestTypes.GetTestTypeFees(1);
                Appointment.CreatedByUserID = clsGlobalSettings.CurrentUserID;
                Appointment.IsLocked = false;
            }
            else
                Appointment = _Appointment;
            
            Appointment.AppointmentDate = _GetAppointmentDate();

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
