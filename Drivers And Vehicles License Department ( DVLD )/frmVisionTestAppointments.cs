using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmVisionTestAppointments : Form
    {
        int _LDLAppId;
        public frmVisionTestAppointments(int LDLApplicationID)
        {
            InitializeComponent();
            uctrlDLApplicationInfo.LoadInfo(LDLApplicationID);
            _LDLAppId = LDLApplicationID;
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
            if (dgvVisionTestAppointments.DataSource == null)
                dgvVisionTestAppointments.DataSource = clsTestAppointment.GetColumnsNamesForView();

            clsUtility.AddNewRowToDGV(dgvVisionTestAppointments,(DataTable) dgvVisionTestAppointments.DataSource, ref NewValues, "Appointment ID");
            lblRecordsNumber.Text = (Convert.ToInt16(lblRecordsNumber.Text) + 1).ToString();
        }
        private void btnScheduleTest_Click(object sender, EventArgs e)
        {
            if (!clsTest.HasPassedTheTest(_LDLAppId, 1))
            {
                if (clsTestAppointment.IsAppointmentSchedulingAvailable(_LDLAppId, 1))
                    MessageBox.Show("Person already has an active appointment for this test, you cannot add new appointment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    frmScheduleTest frm = new frmScheduleTest(_LDLAppId, frmScheduleTest.enTestType.VisionTest, frmScheduleTest.enTestTrial.FirstTime);
                    frm.OnSchedulingAppointment += _AddNewRowToDGV;
                    frm.ShowDialog();
                }
            }
            else
                MessageBox.Show("This person already passed this test, you can only retake failed test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
        }
        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            dgvVisionTestAppointments.DataSource = clsTestAppointment.GetTestAppointments(10,1, _LDLAppId);
            lblRecordsNumber.Text = clsTestAppointment.GetTotalAppointmentsCount(_LDLAppId,1).ToString();
        }

        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows = clsTestAppointment.GetTestAppointments(100,1, _LDLAppId,(int)dgvVisionTestAppointments.Rows[dgvVisionTestAppointments.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Appointment ID"].Value)?.Select();

            if (NewRows != null)
                clsUtility.AddNewRowsToDGV(dgvVisionTestAppointments, (DataTable)dgvVisionTestAppointments.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvVisionTestAppointments));
        }

        private void dgvVisionTestAppointments_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvVisionTestAppointments.Rows.GetLastRow(DataGridViewElementStates.None) == dgvVisionTestAppointments.Rows.GetLastRow(DataGridViewElementStates.Selected))
                _AppendPartOfRemainingData();
        }

        private void dgvVisionTestAppointments_Scroll(object sender, ScrollEventArgs e)
        {
            if (dgvVisionTestAppointments.Rows.GetLastRow(DataGridViewElementStates.None) == dgvVisionTestAppointments.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                _AppendPartOfRemainingData();
        }
        private void _EditDataRowInDGV(ref object[] NewValues,short RowIndex)
        {
            clsUtility.EditFullDataRowInDGV(dgvVisionTestAppointments, (DataTable)dgvVisionTestAppointments.DataSource, ref NewValues, RowIndex);
        }
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvVisionTestAppointments.SelectedRows.Count == 1)
            {
                clsTestAppointment TestAppointment = clsTestAppointment.Find((int)dgvVisionTestAppointments.SelectedRows[0].Cells["Appointment ID"].Value);
                frmScheduleTest frm;
                if (clsTest.HasPassedTheTest(_LDLAppId, 1))
                {
                    frm = new frmScheduleTest(_LDLAppId, frmScheduleTest.enTestType.VisionTest, frmScheduleTest.enTestTrial.Taken, TestAppointment);
                    frm._SetControlsForLockedAppointment(true);
                }

                else if ((bool)dgvVisionTestAppointments.SelectedRows[0].Cells["Is Locked"].Value)
                {
                    frm = new frmScheduleTest(_LDLAppId, frmScheduleTest.enTestType.VisionTest, frmScheduleTest.enTestTrial.Taken, TestAppointment);
                    frm._SetControlsForLockedAppointment(false);
                }
                else
                {
                    frm = new frmScheduleTest(_LDLAppId, frmScheduleTest.enTestType.VisionTest, frmScheduleTest.enTestTrial.FirstTime, TestAppointment, (short)dgvVisionTestAppointments.SelectedRows[0].Index);
                    frm.OnEditingScheduledAppointment += _EditDataRowInDGV;
                }
                    frm.ShowDialog();
                
            }
            else
                MessageBox.Show("You can select one appointment to edit!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmsAppointment_Paint(object sender, PaintEventArgs e)
        {
            if (dgvVisionTestAppointments.SelectedRows.Count == 0)
                cmsAppointment.Close();
        }

        private void _LockTestAppoitment(short RowIndex)
        {
            clsUtility.EditOneColumnValueInDGV(dgvVisionTestAppointments, (DataTable)dgvVisionTestAppointments.DataSource, "Is Locked", true, RowIndex);
        }

        private void tsmiTakeTest_Click(object sender, EventArgs e)
        {
            if (dgvVisionTestAppointments.SelectedRows.Count == 1)
            {
                if((bool)dgvVisionTestAppointments.SelectedRows[0].Cells["Is Locked"].Value)
                {
                    if(clsTest.HasPassedTheTest(_LDLAppId, 1))
                        MessageBox.Show("This person already passed this test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    else
                        MessageBox.Show("This person already taken this test and failed in it , schedule new test for the person in order to retake it.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                }

                clsTestAppointment TestAppointment = clsTestAppointment.Find((int)dgvVisionTestAppointments.SelectedRows[0].Cells["Appointment ID"].Value);
                frmTakeTest frm = new frmTakeTest(TestAppointment, frmScheduleTest.enTestType.VisionTest,(short)dgvVisionTestAppointments.SelectedRows[0].Index);
                frm.OnTestTaken += _LockTestAppoitment;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("You can select one appointment to take test!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }
    }
}
