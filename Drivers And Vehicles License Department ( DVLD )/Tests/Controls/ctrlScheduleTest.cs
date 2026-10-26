using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;
using static Utility_Library.clsGeneralUtility;

namespace DVLDPresentationLayer
{
    public partial class ctrlScheduleTest : UserControl
    {
        public delegate void ScheduledAppointment(object[] NewAppointmentDetails);
        public event ScheduledAppointment AfterSchedulingAppointment;

        public delegate void EditedScheduledAppointment(string NewAppointmentDate, int DGVRowIndex);
        public event EditedScheduledAppointment AfterEditingAppointment;

        public enum enMode : byte { AddNew  = 0, Update = 1}
        private enMode _Mode;

        public enum enCreationMode { FirstTime = 0, ReTake = 1}
        private enCreationMode _CreationMode;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private clsTestAppointment _TestAppointment;

        private clsTestType.enTestType _TestTypeID;
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }

            set
            {
                _TestTypeID = value;

                ShowInfoByTestType(gbTestAppointment, pbTestType, lblTestFees, _TestTypeID);
            }
        }

        private int _AppointmentsDGVRowIndex = -1;
        private bool _IsLockedMode;

        public ctrlScheduleTest()
        {
            InitializeComponent();
            _TestTypeID = clsTestType.enTestType.VisionTest;
        }

        private void _SetControlsForLockedAppointment(bool DidPassTestType)
        {
            if (!DidPassTestType)
            {
                lblNote.Text = "Person already sat for the test , appointment locked.";
            }
            else
            {
                lblFormBigTitle.Text = "Test Have Been Taken";
                lblNote.Text = "Person already has taken the test , appointment locked.";
            }
            lblNote.Visible = true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode == enMode.AddNew && _LocalDrivingLicenseApplication.HasActiveScheduledAppointment(_TestTypeID))
            {
                MessageBox.Show("Person already has an active appointment for this test, you cannot add new appointment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private bool _HandleLockedAppointmentConstraint()
        {
            if (_Mode == enMode.Update && _TestAppointment.IsLocked)
            {
                if (_LocalDrivingLicenseApplication.DoesPassedTestType(_TestTypeID))
                {
                    _SetControlsForLockedAppointment(true);
                }
                else
                    _SetControlsForLockedAppointment(false);

                _IsLockedMode = true;

                return true;
            }

            return false;
        }

        private bool _HandlePreviousTestPassConstraint()
        {
            switch(_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    if(_LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.VisionTest))
                            return true;
                    break;

                case clsTestType.enTestType.StreetTest:
                    if (_LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.WrittenTest))
                        return true;
                    break;
            }

            return false;
        }

        public void LoadInfo(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID, int TestAppointmentID = -1, int AppointmentsDGVRowIndex = -1)
        {
            dtpTestAppointmentDate.Format = DateTimePickerFormat.Custom;
            dtpTestAppointmentDate.CustomFormat = clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.NumericFormat);

            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            this.TestTypeID = TestTypeID;
            _AppointmentsDGVRowIndex = AppointmentsDGVRowIndex;

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(LocalDrivingLicenseApplicationID);

            if (_Mode == enMode.Update)
            {
                clsTestAppointment TestAppointment = clsTestAppointment.Find(TestAppointmentID);

                if (TestAppointment != null)
                    _TestAppointment = TestAppointment;
                else
                {
                    MessageBox.Show("No test appointment with id : " + TestAppointmentID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            else
            {
                dtpTestAppointmentDate.MinDate = DateTime.Now;
            }

            if (LocalDrivingLicenseApplication != null)
            {
                _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            }
            else
            {
                MessageBox.Show("No local driving license application with id : " + LocalDrivingLicenseApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_LocalDrivingLicenseApplication.DoesAttendTestType(this.TestTypeID))
                _CreationMode = enCreationMode.ReTake;
            else
                _CreationMode = enCreationMode.FirstTime;

            _ShowInfo(TestTypeID, _CreationMode);

            if (_HandleActiveTestAppointmentConstraint() || _HandleLockedAppointmentConstraint() || !_HandlePreviousTestPassConstraint())
            {
                clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);
                clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, lblNote);

                btnSave.Enabled = false;
                dtpTestAppointmentDate.Enabled = false;
                mtxtAppointmentTime.Enabled = false;

                return;
            }

            dtpTestAppointmentDate.MaxDate = dtpTestAppointmentDate.MinDate.AddMonths(3);
        }

        private bool _AddReTakeTestApp(out clsApplication Application)
        {
            Application = new clsApplication(
                ApplicantPersonID: _LocalDrivingLicenseApplication.ApplicantPersonID,
                ApplicationDate: DateTime.Now,
                ApplicationStatus: clsApplication.enApplicationStatus.New,
                LastStatusDate: DateTime.Now,
                PaidApplicationFees: Convert.ToDecimal(lblRAppFees.Text),
                ApplicationTypeID: clsApplicationType.GetApplicationTypeID(clsApplicationType.enApplicationType.ReTakeTest),
                CreatedByUserID: clsGlobalSettings.CurrentUserID
            );

            return Application.Save();
        }

        private bool _IsValidAppoitmentDateInEditMode()
        {
            return !(DateTime.Compare(_TestAppointment.AppointmentDate, dtpTestAppointmentDate.MinDate) < 0);
        }

        private void _SetDate()
        {
            if (_TestAppointment != null)
            {
                mtxtAppointmentTime.Text = _TestAppointment.AppointmentDate.ToString(clsGeneralUtility.GetCustomDateFormat(enCustomDateFormat.TimeFormat));

                if (_IsValidAppoitmentDateInEditMode())
                    dtpTestAppointmentDate.MinDate = _TestAppointment.AppointmentDate;

                if (_TestAppointment.IsLocked)
                {
                    dtpTestAppointmentDate.MinDate = _TestAppointment.AppointmentDate;
                }

            }

            else
                mtxtAppointmentTime.Text = DateTime.Now.ToString(clsGeneralUtility.GetCustomDateFormat(enCustomDateFormat.TimeFormat));
        }

        private void _ShowInfo(clsTestType.enTestType TestTypeID, enCreationMode CreationMode)
        {
            clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, pbTestType);

            lblLDLApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

            lblLicenseClassName.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblApplicantFullName.Text = _LocalDrivingLicenseApplication.PersonInfo.FullName;

            if (CreationMode != enCreationMode.FirstTime)
            {
                lblFormBigTitle.Text = "Schedule Retake Test";

                lblRAppFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsApplicationType.GetApplicationTypeFees(clsApplicationType.enApplicationType.ReTakeTest),
                    enCustomNumberFormat.NoJustZerosAfterFraction);

                lblTotalFees.Text = clsGeneralUtility.GetCustomNumberFormat(Convert.ToDecimal(lblTestFees.Text) + Convert.ToDecimal(lblRAppFees.Text),
                    enCustomNumberFormat.NoJustZerosAfterFraction);

                if (_TestAppointment != null && _TestAppointment?.RetakeTestApplicationID != -1)
                {
                    lblReTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                }

                gbReTakeTestInfo.Enabled = (CreationMode == enCreationMode.ReTake);
            }

            else
            {
                lblRAppFees.Text = "0";
                lblTotalFees.Text = lblTestFees.Text;
                lblReTestAppID.Text = "N/A";
            }

            _SetDate();

            clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);
            lblTrialNumber.Text = clsLocalDrivingLicenseApplication.GetTotalTrialsPerTestType(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID, TestTypeID).ToString();
        }

        public static void ShowInfoByTestType(GroupBox gbContentContainer, PictureBox pbTestType, Label lblFees, clsTestType.enTestType TestType)
        {
            switch (TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    gbContentContainer.Text = "Vision Test";
                    pbTestType.Image = Resources.Vision_512;
                    lblFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsTestType.GetTestTypeFees((byte)clsTestType.enTestType.VisionTest), enCustomNumberFormat.NoJustZerosAfterFraction);
                    break;

                case clsTestType.enTestType.WrittenTest:
                    gbContentContainer.Text = "Written Test";
                    pbTestType.Image = Resources.Written_Test_512;
                    lblFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsTestType.GetTestTypeFees((byte)clsTestType.enTestType.WrittenTest), enCustomNumberFormat.NoJustZerosAfterFraction);
                    break;

                case clsTestType.enTestType.StreetTest:
                    gbContentContainer.Text = "Street Test";
                    pbTestType.Image = Resources.driving_test_512;
                    lblFees.Text = clsGeneralUtility.GetCustomNumberFormat(clsTestType.GetTestTypeFees((byte)clsTestType.enTestType.StreetTest), enCustomNumberFormat.NoJustZerosAfterFraction);
                    break;
            }
        }

        private DateTime _GetAppointmentDate()
        {
            byte EnteredHour = clsGeneralUtility.GetHourFromInputToTextBox(mtxtAppointmentTime);
            byte EnteredMinute = clsGeneralUtility.GetMinuteFromInputToTextBox(mtxtAppointmentTime);

            if (clsGeneralUtility.IsTimeInPmInputToTextBox(mtxtAppointmentTime))
            {
                EnteredHour = clsGeneralUtility.GetAppropriatetHourNumForAmOrPm(EnteredHour, true);
            }
            else
            {
                EnteredHour = clsGeneralUtility.GetAppropriatetHourNumForAmOrPm(EnteredHour, false);
            }

            DateTime AppointmentDate = new DateTime(dtpTestAppointmentDate.Value.Year, dtpTestAppointmentDate.Value.Month, dtpTestAppointmentDate.Value.Day,
                                  EnteredHour, EnteredMinute, 0, DateTimeKind.Local);

            return AppointmentDate;
        }

        private void _FillNewAppointmentData(ref clsTestAppointment Appointment)
        {
            Appointment = new clsTestAppointment(
             TestTypeID: _TestTypeID,
             LDLApplicationID: _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID,
             AppointmentDate: _GetAppointmentDate(),
             PaidFees: clsTestType.GetTestTypeFees((byte)_TestTypeID),
             CreatedByUserID: clsGlobalSettings.CurrentUserID,
             IsLocked: false
             );
        }

        private void _UpdateAppointmentData()
        {
            if (_TestAppointment != null)
            {
                DateTime NewAppointmentDate = _GetAppointmentDate();

                if (_TestAppointment.AppointmentDate == NewAppointmentDate)
                {
                    MessageBox.Show("there is'nt any change on the appointment data", "No change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                else
                    _TestAppointment.AppointmentDate = NewAppointmentDate;
            }

            else
                MessageBox.Show("Appointment is not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void _SaveAppointment()
        {
            if (_TestAppointment != null)
            {
                if (_TestAppointment.Save())
                {
                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (AfterSchedulingAppointment != null)
                    {
                        object[] NewDetails = new object[] { _TestAppointment.TestAppointmentID, _TestAppointment.AppointmentDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)), _TestAppointment.PaidFees, _TestAppointment.IsLocked };
                        AfterSchedulingAppointment?.Invoke(NewDetails);
                    }

                    AfterEditingAppointment?.Invoke(_TestAppointment.AppointmentDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)), _AppointmentsDGVRowIndex);

                    btnSave.Enabled = false;
                }
                else
                    MessageBox.Show("Failed to save data", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
                MessageBox.Show("Appointment is not found", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsGeneralUtility.DisableErrorProvider(erTime, mtxtAppointmentTime);

            if (_TestAppointment == null)
            {
                _FillNewAppointmentData(ref _TestAppointment);
            }

            else
            {
                _UpdateAppointmentData();
            }

            if (_CreationMode == enCreationMode.ReTake)
            {
                clsApplication Application;
                if (_AddReTakeTestApp(out Application))
                {
                    _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;
                    lblReTestAppID.Text = Application.ApplicationID.ToString();

                    _SaveAppointment();
                }
                else
                    MessageBox.Show("Failed to create the retake test application", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                _SaveAppointment();

        }

        private void mtxtAppointmentTime_Click(object sender, EventArgs e)
        {
            mtxtAppointmentTime.Text = "";
        }

        private void mtxtAppointmentTime_Validating(object sender, CancelEventArgs e)
        {
            if (_IsLockedMode)
                return;

            if (mtxtAppointmentTime.Text.Substring(0, 1) == " ")
                clsGeneralUtility.EnableErrorProvider(erTime, mtxtAppointmentTime, "Time cannot be empty!", e);

            else if (!DateTime.TryParse(mtxtAppointmentTime.Text, out DateTime dt))
                clsGeneralUtility.EnableErrorProvider(erTime, mtxtAppointmentTime, "Invalid time format , format must be like : 10:00 AM", e);

            else if (!clsGeneralUtility.IsValidEnteredTimeInTextBox(mtxtAppointmentTime, dtpTestAppointmentDate.Value))
            {
                clsGeneralUtility.EnableErrorProvider(erTime, mtxtAppointmentTime, "Choosed time is earlier than current time!", e);
            }

            else
            {
                clsGeneralUtility.DisableErrorProvider(erTime, mtxtAppointmentTime);
                return;
            }
        }

        private void ctrlScheduleTest_Load(object sender, EventArgs e)
        {
            dtpTestAppointmentDate.MinDate = DateTime.Now;

        }

        private void mtxtAppointmentTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                btnSave.PerformClick();
        }
    }
}
