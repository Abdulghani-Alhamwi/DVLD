using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class frmScheduleTest : Form
    {
        public event ctrlScheduleTest.ScheduledAppointment AfterSchedulingAppointment;

        public event ctrlScheduleTest.EditedScheduledAppointment AfterEditingAppointment;
        
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
           uctrlScheduleTest.LoadInfo(LocalDrivingLicenseApplicationID, TestType);
        }

        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestType
            ,int TestAppointmentID , int AppointmentsDGVRowIndex)
        {
            InitializeComponent();
            uctrlScheduleTest.LoadInfo(LocalDrivingLicenseApplicationID, TestType, TestAppointmentID, AppointmentsDGVRowIndex);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            btnClose.CausesValidation = false;
            btnExit.CausesValidation = false;
            AcceptButton = uctrlScheduleTest.btnSave;
        }

        private void uctrlScheduleTest_AfterEditingAppointment(string NewAppointmentDate, int DGVRowIndex)
        {
            AfterEditingAppointment?.Invoke(NewAppointmentDate, DGVRowIndex);
        }

        private void uctrlScheduleTest_AfterSchedulingAppointment(object[] NewAppointmentDetails)
        {
            AfterSchedulingAppointment?.Invoke(NewAppointmentDetails);
        }
    }
}