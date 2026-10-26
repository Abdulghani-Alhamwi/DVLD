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

        private int _AppointmentsDGVRowIndex;
        private clsTest _Test;

        public frmTakeTest(int TestAppointmentID, clsTestType.enTestType TestType,int AppointmentsDGVRowIndex)
        {
            InitializeComponent();

            uctrlScheduluedTest.LoadInfo(TestAppointmentID, TestType);
            _AppointmentsDGVRowIndex = AppointmentsDGVRowIndex;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Test != null)
                return;

            if (MessageBox.Show("Are you sure you want to save?\nAfter that you cannot change the Pass/Fail result.","Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;

            _Test = new clsTest
            (
                TestAppointmentID: uctrlScheduluedTest.TestAppointment.TestAppointmentID,
                TestResult: (rbPass.Checked) ? true : false,
                Notes: (txtNotes.Text == "") ? null : txtNotes.Text.Trim(),
                CreatedByUserID: clsGlobalSettings.CurrentUserID
            );

            if (_Test.Save())
            {
                uctrlScheduluedTest.TestID = _Test.TestID;

                AfterTestTaken?.Invoke(_AppointmentsDGVRowIndex);
                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_Test.TestResult == true)
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

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            txtNotes.MaxLength = 500;

            _Test = clsTest.Find(uctrlScheduluedTest.TestAppointment.TestID);

            if (_Test != null)
            {
                if (_Test.TestResult)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                txtNotes.Text = _Test.Notes;

                rbPass.Enabled = false;
                rbFail.Enabled = false;
                txtNotes.ReadOnly = true;

                btnSave.Enabled = false;
            }
        }
    }
}