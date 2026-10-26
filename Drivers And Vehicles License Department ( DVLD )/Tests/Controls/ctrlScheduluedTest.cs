using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlScheduluedTest : UserControl
    {
        private clsTestType.enTestType _TestTypeID;
        public clsTestType.enTestType TestTypeID
        {
            get { return _TestTypeID; }

            set
            {
                _TestTypeID = value;

                ctrlScheduleTest.ShowInfoByTestType(gbTestAppointment, pbTestType, lblTestFees, _TestTypeID);
            }
        }

        public clsTestAppointment TestAppointment;

        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;

        public int TestID 
        {
            get { return (TestAppointment != null) ? TestAppointment.TestID : -1; }
        
            set
            {
                lblTestID.Text = value.ToString();
            }
        }

        public ctrlScheduluedTest()
        {
            InitializeComponent();
        }

        public void LoadInfo(int TestAppointmentID, clsTestType.enTestType TestType)
        {
            this.TestTypeID = TestType;

            TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if(TestAppointment == null)
            {
                MessageBox.Show("Test Appointment with ID : " + TestAppointmentID.ToString() + " Is Not Found", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID(TestAppointment.LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Local Driving License Application with ID : " + LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString() + " Is Not Found", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblLDLApplicationID.Text = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();

            lblLicenseClassName.Text = LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblApplicantFullName.Text = LocalDrivingLicenseApplication.PersonInfo.FullName;
            lblTrialNumber.Text = LocalDrivingLicenseApplication.GetTotalTrialsPerTestType(TestAppointment.TestTypeID).ToString();

            lblDate.Text = TestAppointment.AppointmentDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.NumericFormat));
            lblTime.Text = TestAppointment.AppointmentDate.ToShortTimeString();

            lblTestID.Text = (TestAppointment.TestID == -1) ? "No Taken Yet" : TestAppointment.TestID.ToString();
        }

        private void ctrlScheduluedTest_Load(object sender, EventArgs e)
        {
            clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, pbTestType);
            clsGeneralUtility.CenterControlHorizontally(gbTestAppointment, lblFormBigTitle);
        }
    }
}
