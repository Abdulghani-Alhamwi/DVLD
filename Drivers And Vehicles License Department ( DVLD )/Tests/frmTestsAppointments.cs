using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmTestsAppointments : Form
    {
        public event Action<int> AfterPassingTest;

        private string _LastColumnNameDgvSortedBy;
        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDgvSortDirection;
        private clsDataGridViewUtilityLib _DgvUtilityLib;

        private clsTestType.enTestType _CurrentTestType;

        private int _LDLAppId;
        private int _TestsDGVRowIndex;
        private byte _TestTypeID;
        private int _LastBroughtAppointmentID;
        private string _PrimaryKeyViewedColumnName;

        public frmTestsAppointments(int LDLApplicationID,int TestsDGVRowIndex, clsTestType.enTestType TestType)
        {
            InitializeComponent();

            uctrlDLApplicationInfo.LoadLDLAppInfo(LDLApplicationID);
            _ShowInfoByTestType(TestType);

            _CurrentDgvSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;

            _LDLAppId = LDLApplicationID;
            _TestsDGVRowIndex = TestsDGVRowIndex;
            _CurrentTestType = TestType;
            _TestTypeID = clsTestType.GetTestTypeID(_CurrentTestType);
            _LastBroughtAppointmentID = -1;
            _PrimaryKeyViewedColumnName = "Appointment ID";

            _DgvUtilityLib = new clsDataGridViewUtilityLib();
        }

        private void _ShowInfoByTestType(clsTestType.enTestType TestType)
        {
            switch(TestType)
            {
                case clsTestType.enTestType.VisionTest:
                    lblFormBigTitle.Text = "Vision Test Appointments";
                    lblFormTitle.Text = lblFormBigTitle.Text;
                    pbTestType.Image = Resources.Vision_512;
                    break;

                case clsTestType.enTestType.WrittenTest:
                    lblFormBigTitle.Text = "Written Test Appointments";
                    lblFormTitle.Text = lblFormBigTitle.Text;
                    pbTestType.Image = Resources.Written_Test_512;
                    break;

                case clsTestType.enTestType.StreetTest:
                    lblFormBigTitle.Text = "Street Test Appointments";
                    lblFormTitle.Text = lblFormBigTitle.Text;
                    pbTestType.Image = Resources.driving_test_512;
                    break;
            }
            clsGeneralUtility.CenterControlHorizontally(this,pbTestType);
            clsGeneralUtility.CenterControlHorizontally(this,lblFormBigTitle);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _AddNewRowToDGV(object[] NewAppointmentDetails)
        {
            if (dgvTestAppointments.DataSource == null)
                dgvTestAppointments.DataSource = clsTestAppointment.GetColumnsNamesForView();

            _DgvUtilityLib.AddNewRowToDGV(dgvTestAppointments,NewAppointmentDetails, dgvTestAppointments.Columns[0].HeaderText);
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
                        frm = new frmScheduleTest(_LDLAppId, _CurrentTestType, frmScheduleTest.enTestTrial.FirstTime);
                    
                    else
                        frm = new frmScheduleTest(_LDLAppId, _CurrentTestType, frmScheduleTest.enTestTrial.ReTake);

                    frm.AfterSchedulingAppointment += _AddNewRowToDGV;
                    frm.ShowDialog();
                }
            }
            else
                MessageBox.Show("This person already passed this test, you can only retake failed test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            dgvTestAppointments.DataSource = clsTestAppointment.GetTestAppointments(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _TestTypeID, _LDLAppId);
            lblRecordsNumber.Text = clsTestAppointment.GetTotalAppointmentsCount(_LDLAppId, _TestTypeID).ToString();
        }

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtAppointmentID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvTestAppointments, _PrimaryKeyViewedColumnName, _CurrentDgvSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_PrimaryKeyViewedColumnName, _LastColumnNameDgvSortedBy);

            DataTable dtTestAppointments = clsTestAppointment.GetTestAppointments(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _TestTypeID, _LDLAppId, _LastBroughtAppointmentID
                , _LastColumnNameDgvSortedBy, _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection));

            DataRow[] NewRows = dtTestAppointments?.Select();

            if (NewRows != null)
                _DgvUtilityLib.AddNewRowsToDgv(dgvTestAppointments, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvTestAppointments), _CurrentDgvSortDirection);
        }

        private void _EditDataRowInDGV(string NewDateTime,int DgvRowIndex)
        {
            _DgvUtilityLib.EditOneColumnValueInDgv<string>(dgvTestAppointments, "Appointment Date", NewDateTime, DgvRowIndex);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.SelectedRows.Count == 1)
            {
                clsTestAppointment TestAppointment = clsTestAppointment.Find((int)dgvTestAppointments.SelectedRows[0].Cells[_PrimaryKeyViewedColumnName].Value);
                frmScheduleTest frm;
                if (clsTest.HasPassedTheTest(_LDLAppId, _TestTypeID))
                {
                    frm = new frmScheduleTest(_LDLAppId, _CurrentTestType, frmScheduleTest.enTestTrial.Taken, TestAppointment);
                    frm._SetControlsForLockedAppointment(true);
                }

                else if ((bool)dgvTestAppointments.SelectedRows[0].Cells["Is Locked"].Value)
                {
                    frm = new frmScheduleTest(_LDLAppId, _CurrentTestType, frmScheduleTest.enTestTrial.Taken, TestAppointment);
                    frm._SetControlsForLockedAppointment(false);
                }
                else
                {
                    frm = new frmScheduleTest(_LDLAppId, _CurrentTestType, frmScheduleTest.enTestTrial.FirstTime, TestAppointment, (int)dgvTestAppointments.SelectedRows[0].Index);
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

        private void _LockTestAppointment(int DGVRowIndex)
        {
            _DgvUtilityLib.EditOneColumnValueInDgv<bool>(dgvTestAppointments, "Is Locked", true, DGVRowIndex);
        }

        private void _UpdateLDLAppDgv()
        {
            AfterPassingTest.Invoke(_TestsDGVRowIndex);
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

                clsTestAppointment TestAppointment = clsTestAppointment.Find((int)dgvTestAppointments.SelectedRows[0].Cells[_PrimaryKeyViewedColumnName].Value);
                frmTakeTest frm = new frmTakeTest(TestAppointment, _CurrentTestType, (int)dgvTestAppointments.SelectedRows[0].Index);
                frm.AfterPassingTest += _UpdateLDLAppDgv;
                frm.AfterTestTaken += _LockTestAppointment;
                
                frm.ShowDialog();
            }
            else
                MessageBox.Show("You can select one appointment to take test!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
         }

        private void dgvVisionTestAppointments_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDataGridViewUtilityLib.IsDgvLastRowDisplayed(dgvTestAppointments))
                    _AppendPartOfRemainingData();
            }
        }

        private void dgvVisionTestAppointments_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDataGridViewUtilityLib.IsDgvLastRowSelected(dgvTestAppointments))
                _AppendPartOfRemainingData();
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            _CurrentDgvSortDirection = clsDataGridViewUtilityLib.ReverseCurrentDgvSortDirection(_CurrentDgvSortDirection);

            DataTable dtSortedInfo = clsTestAppointment.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvTestAppointments.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection),_LDLAppId, _TestTypeID);

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvTestAppointments, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvTestAppointments_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }
    }
}