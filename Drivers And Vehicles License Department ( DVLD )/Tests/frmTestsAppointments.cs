using System;
using System.Data;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmTestsAppointments : Form
    {
        internal Action<int> AfterPassingTest;

       private clsTestTypes.enTestType _TestType;

        private int _LDLAppId;
        private int _TestsDGVRowIndex;
        private byte _TestTypeID;
        public frmTestsAppointments(int LDLApplicationID,int TestsDGVRowIndex, clsTestTypes.enTestType TestType)
        {
            InitializeComponent();
            uctrlDLApplicationInfo.LoadInfo(LDLApplicationID);

            _ShowInfoByTestType(TestType);

            _LDLAppId = LDLApplicationID;
            _TestsDGVRowIndex = TestsDGVRowIndex;
            _TestType = TestType;
            _TestTypeID = clsTestTypes.GetTestTypeID(_TestType);
        }

        private void _ShowInfoByTestType(clsTestTypes.enTestType TestType)
        {
            switch(TestType)
            {
                case clsTestTypes.enTestType.VisionTest:
                    lblFormBigTitle.Text = "Vision Test Appointments";
                    lblFormTitle.Text = lblFormBigTitle.Text;
                    pbTestType.Image = Resources.Vision_512;
                    break;

                case clsTestTypes.enTestType.WrittenTest:
                    lblFormBigTitle.Text = "Written Test Appointments";
                    lblFormTitle.Text = lblFormBigTitle.Text;
                    pbTestType.Image = Resources.Written_Test_512;
                    break;

                case clsTestTypes.enTestType.StreetTest:
                    lblFormBigTitle.Text = "Street Test Appointments";
                    lblFormTitle.Text = lblFormBigTitle.Text;
                    pbTestType.Image = Resources.driving_test_512;
                    break;
            }
            clsUtility.CenterControlHorizontally(this,pbTestType);
            clsUtility.CenterControlHorizontally(this,lblFormBigTitle);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _AddNewRowToDGV(ref object[] NewValues)
        {
            if (dgvTestAppointments.DataSource == null)
                dgvTestAppointments.DataSource = clsTestAppointment.GetColumnsNamesForView();

            clsUtility.AddNewRowToDGV(dgvTestAppointments,(DataTable) dgvTestAppointments.DataSource, ref NewValues, "Appointment ID");
            lblRecordsNumber.Text = (Convert.ToInt16(lblRecordsNumber.Text) + 1).ToString();
        }
        private void btnScheduleTest_Click(object sender, EventArgs e)
        {
            if (!clsTest.HasPassedTheTest(_LDLAppId, _TestTypeID))
            {
                if (clsTestAppointment.IsAppointmentSchedulingAvailable(_LDLAppId, _TestTypeID))
                    MessageBox.Show("Person already has an active appointment for this test, you cannot add new appointment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                {
                    frmScheduleTest frm;
                    if (dgvTestAppointments.Rows.Count == 0)
                        frm = new frmScheduleTest(_LDLAppId, _TestType, frmScheduleTest.enTestTrial.FirstTime);
                    
                    else
                        frm = new frmScheduleTest(_LDLAppId, _TestType, frmScheduleTest.enTestTrial.ReTake);

                    frm.AfterSchedulingAppointment += _AddNewRowToDGV;
                    frm.ShowDialog();
                }
            }
            else
                MessageBox.Show("This person already passed this test, you can only retake failed test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
        }
        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            dgvTestAppointments.DataSource = clsTestAppointment.GetTestAppointments(clsUtility.WantedNumOfRowsFromDB, _TestTypeID, _LDLAppId);
            lblRecordsNumber.Text = clsTestAppointment.GetTotalAppointmentsCount(_LDLAppId, _TestTypeID).ToString();
        }

        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows = clsTestAppointment.GetTestAppointments(clsUtility.WantedNumOfRowsFromDB, _TestTypeID, _LDLAppId,(int)dgvTestAppointments.Rows[dgvTestAppointments.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Appointment ID"].Value)?.Select();

            if (NewRows != null)
                clsUtility.AddNewRowsToDGV(dgvTestAppointments, (DataTable)dgvTestAppointments.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvTestAppointments));
        }

        private void dgvVisionTestAppointments_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvTestAppointments.Rows.GetLastRow(DataGridViewElementStates.None) == dgvTestAppointments.Rows.GetLastRow(DataGridViewElementStates.Selected))
                _AppendPartOfRemainingData();
        }

        private void dgvVisionTestAppointments_Scroll(object sender, ScrollEventArgs e)
        {
            if (dgvTestAppointments.Rows.GetLastRow(DataGridViewElementStates.None) == dgvTestAppointments.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                _AppendPartOfRemainingData();
        }
        private void _EditDataRowInDGV(ref object[] NewValues,int RowIndex)
        {
            clsUtility.EditFullDataRowInDGV(dgvTestAppointments, (DataTable)dgvTestAppointments.DataSource, ref NewValues, RowIndex);
        }
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.SelectedRows.Count == 1)
            {
                clsTestAppointment TestAppointment = clsTestAppointment.Find((int)dgvTestAppointments.SelectedRows[0].Cells["Appointment ID"].Value);
                frmScheduleTest frm;
                if (clsTest.HasPassedTheTest(_LDLAppId, _TestTypeID))
                {
                    frm = new frmScheduleTest(_LDLAppId, _TestType, frmScheduleTest.enTestTrial.Taken, TestAppointment);
                    frm._SetControlsForLockedAppointment(true);
                }

                else if ((bool)dgvTestAppointments.SelectedRows[0].Cells["Is Locked"].Value)
                {
                    frm = new frmScheduleTest(_LDLAppId, _TestType, frmScheduleTest.enTestTrial.Taken, TestAppointment);
                    frm._SetControlsForLockedAppointment(false);
                }
                else
                {
                    frm = new frmScheduleTest(_LDLAppId, _TestType, frmScheduleTest.enTestTrial.FirstTime, TestAppointment, (int)dgvTestAppointments.SelectedRows[0].Index);
                    frm.AfterEditingAppointment += _EditDataRowInDGV;
                }
                    frm.ShowDialog();
                
            }
            else
                MessageBox.Show("You can select one appointment to edit!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmsAppointment_Paint(object sender, PaintEventArgs e)
        {
            if (dgvTestAppointments.SelectedRows.Count == 0)
                cmsAppointment.Close();
        }

        private void _LockTestAppoitment(int RowIndex)
        {
            clsUtility.EditOneColumnValueInDGV(dgvTestAppointments, (DataTable)dgvTestAppointments.DataSource, "Is Locked", true, RowIndex);
        }

        private void tsmiTakeTest_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.SelectedRows.Count == 1)
            {
                if((bool)dgvTestAppointments.SelectedRows[0].Cells["Is Locked"].Value)
                {
                    if(clsTest.HasPassedTheTest(_LDLAppId, _TestTypeID))
                        MessageBox.Show("This person already passed this test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    else
                        MessageBox.Show("This person already taken this test and failed in it , schedule new test for the person in order to retake it.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                }

                clsTestAppointment TestAppointment = clsTestAppointment.Find((int)dgvTestAppointments.SelectedRows[0].Cells["Appointment ID"].Value);
                frmTakeTest frm = new frmTakeTest(TestAppointment, _TestType, (int)dgvTestAppointments.SelectedRows[0].Index, _TestsDGVRowIndex);
                frm.AfterPassingTest = AfterPassingTest;
                frm.OnTestTaken += _LockTestAppoitment;

                frm.ShowDialog();
            }
            else
                MessageBox.Show("You can select one appointment to take test!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
    }
}
