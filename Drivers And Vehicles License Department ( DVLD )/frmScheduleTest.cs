using System;
using System.Drawing;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmScheduleTest : Form
    {
        public Action<object[]> OnSchedulingAppointment;
        public Action<object[],short> OnEditingScheduledAppointment;
        public enum enTestTrial {FirstTime = 0 , ReTake = 1}
        public enum enTestType {VisionTest = 0 , WrittenTest = 1 , StreetTest = 2}

        public static short Trial;

        private clsLocalDrivingLicenseApplication _LDLApp;

        private clsTestAppointment _Appointment;

        enTestType _TestType;

        short _RowIndex;
        public frmScheduleTest(int LDLAppID , enTestType TestType , enTestTrial TestTrial)
        {
            InitializeComponent();
            _InitializeFormData(LDLAppID, TestType, TestTrial);
        }

        public frmScheduleTest(int LDLAppID, enTestType TestType, enTestTrial TestTrial, clsTestAppointment Appointment , short RowIndex)
        {
            InitializeComponent();
            _InitializeFormData(LDLAppID, TestType, TestTrial,Appointment);
            _RowIndex = RowIndex;
        }

        private void _InitializeFormData(int LDLAppID, enTestType TestType, enTestTrial TestTrial, clsTestAppointment Appointment = null)
        {
            _TestType = TestType;
            clsLocalDrivingLicenseApplication LDLApp = clsLocalDrivingLicenseApplication.Find(LDLAppID);

            if (Appointment != null)
                _Appointment = Appointment;

                _LDLApp = LDLApp;
                _LoadInfo(_LDLApp, TestType, TestTrial);

            dtpTestAppointmentDate.MinDate = DateTime.Now;
            dtpTestAppointmentDate.MaxDate = dtpTestAppointmentDate.MinDate.AddMonths(3);
        }

        private void _LoadInfo(clsLocalDrivingLicenseApplication LDLApp, enTestType TestType,enTestTrial TestTrial)
        {
            _ShowInfoByTestType(gbTestAppointment,pbTestType,lblTestFees,TestType);

            lblLDLApplicationID.Text = LDLApp.LDLApplicationID.ToString();

            if(LDLApp != null)
            {
                lblLicenseClassName.Text = LDLApp.LicenseClass.ClassName;
                lblApplicantFullName.Text = clsPerson.GetFullName(LDLApp.ApplicantPersonID);
            }

            if (TestTrial == enTestTrial.ReTake)
            {
                gbReTakeTestInfo.Enabled = true;
                Trial++;
                lblRAppFees.Text = LDLApp.PaidApplicationFees.ToString();
                lblTotalFees.Text = (LDLApp.PaidApplicationFees + Convert.ToDecimal(lblTestFees.Text)).ToString();
                lblReTestAppID.Text = LDLApp.ApplicationID.ToString();
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

                lblTrialNumber.Text = Trial.ToString();                
        }

        public static void _ShowInfoByTestType(GroupBox gbContentContainer, PictureBox pbTestType, Label lblFees , enTestType TestType)
        {
            switch(TestType)
            {
                case enTestType.VisionTest:
                    gbContentContainer.Text = "Vision Test";
                    pbTestType.Image = Resources.Vision_512;
                    lblFees.Text = clsTestTypes.GetTestTypeFees(1).ToString();
                    break;

                case enTestType.WrittenTest:
                    gbContentContainer.Text = "Written Test";
                    pbTestType.Image =Resources.Written_Test_512;
                    lblFees.Text = clsTestTypes.GetTestTypeFees(2).ToString();
                    break;

                case enTestType.StreetTest:
                    gbContentContainer.Text = "Street Test";
                    pbTestType.Image = Resources.driving_test_512;
                    lblFees.Text = clsTestTypes.GetTestTypeFees(3).ToString();
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

        private byte _GetTestTypeID(enTestType TestType)
        {
            switch (TestType)
            {
                case enTestType.VisionTest:
                    return 1;

                case enTestType.WrittenTest:
                    return 2;

                case enTestType.StreetTest:
                    return 3;
            }
            return 0;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTestAppointment Appointment;
            if (_Appointment == null)
            {
                Appointment = new clsTestAppointment();
                Appointment.TestTypeID = _GetTestTypeID(_TestType);
                Appointment.LDLApplicationID = _LDLApp.LDLApplicationID;
                Appointment.PaidFees = Convert.ToDecimal(lblTestFees.Text);
                Appointment.CreatedByUserID = clsGlobalSettings.CurrentUserID;
                Appointment.IsLocked = false;
            }
            else
                Appointment = _Appointment;

            
            Appointment.AppointmentDate = _GetAppointmentDate();
            
                if (Appointment.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if(_Appointment == null)
                    _Appointment = Appointment;

                object[] NewValues = new object[] {_Appointment.TestAppointmentID,_Appointment.AppointmentDate,_Appointment.PaidFees , _Appointment.IsLocked};

                OnSchedulingAppointment?.Invoke(NewValues);
                OnEditingScheduledAppointment?.Invoke(NewValues, _RowIndex);
                }
                else
                    MessageBox.Show("Failed To Save Data", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
