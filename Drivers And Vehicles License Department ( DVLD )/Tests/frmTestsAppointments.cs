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
        public event Action<clsPerson, int> OnEditingApplicantPersonalInfo;

        private string _LastColumnNameDgvSortedBy;
        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDGVColumnSortDirection;
        private clsDataGridViewUtilityLib _DGVUtilityLib;

        private clsTestType.enTestType _TestTypeID;

        private int _LocalDrivingLicenseApplicationId;
        private int _DGVRowIndex;
        private int _LastBroughtAppointmentID;
        private string _PrimaryKeyViewedColumnName;

        public frmTestsAppointments(int LDLApplicationID,int DGVRowIndex, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();

            uctrlDLApplicationInfo.LoadInfoByLocalDrivingLicenseAppID(LDLApplicationID);
            _ShowInfoByTestType(TestTypeID);

            _CurrentDGVColumnSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;

            _LocalDrivingLicenseApplicationId = LDLApplicationID;
            _DGVRowIndex = DGVRowIndex;
            _TestTypeID = TestTypeID;
            _LastBroughtAppointmentID = -1;
            _PrimaryKeyViewedColumnName = "Appointment ID";

            _DGVUtilityLib = new clsDataGridViewUtilityLib();
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

        private void _AddNewRowToDGV(object[] NewAppointmentDetails)
        {
            if (dgvTestAppointments.DataSource == null)
                dgvTestAppointments.DataSource = clsTestAppointment.GetColumnsNamesForView();

            _DGVUtilityLib.AddNewRowToDGV(dgvTestAppointments,NewAppointmentDetails, dgvTestAppointments.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt16(lblRecordsNumber.Text) + 1).ToString();
        }

        private void btnScheduleTest_Click(object sender, EventArgs e)
        {
            if (!clsTest.HasPassedTheTest(_LocalDrivingLicenseApplicationId,(byte) _TestTypeID))
            {
                if (clsTestAppointment.HasActiveScheduledAppointment(_LocalDrivingLicenseApplicationId, _TestTypeID))
                    MessageBox.Show("Person already has an active appointment for this test, you cannot add new appointment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                else
                {
                    frmScheduleTest frm;
                    if (dgvTestAppointments.Rows.Count == 0)
                        frm = new frmScheduleTest(_LocalDrivingLicenseApplicationId, _TestTypeID);
                    
                    else
                        frm = new frmScheduleTest(_LocalDrivingLicenseApplicationId, _TestTypeID);

                    frm.AfterSchedulingAppointment += _AddNewRowToDGV;
                    frm.ShowDialog();
                }
            }
            else
                MessageBox.Show("This person already passed this test, you can only retake failed test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            dgvTestAppointments.DataSource = clsTestAppointment.GetTestAppointments(clsGlobalSettings.WantedNumOfRowsFromDB, (byte)_TestTypeID, _LocalDrivingLicenseApplicationId);
            lblRecordsNumber.Text = clsTestAppointment.GetTotalAppointmentsCount(_LocalDrivingLicenseApplicationId, (byte)_TestTypeID).ToString();
        }

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtAppointmentID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvTestAppointments, _PrimaryKeyViewedColumnName, _CurrentDGVColumnSortDirection);

            _DGVUtilityLib.SetNumberOfRowsToOffset(_LastColumnNameDgvSortedBy);

            DataTable dtTestAppointments = clsTestAppointment.GetTestAppointments(clsGlobalSettings.WantedNumOfRowsFromDB, (byte)_TestTypeID, _LocalDrivingLicenseApplicationId, _LastBroughtAppointmentID
                , _LastColumnNameDgvSortedBy, _DGVUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

            DataRow[] NewRows = dtTestAppointments?.Select();

            if (NewRows != null)
                _DGVUtilityLib.AddNewRowsToDgv(dgvTestAppointments, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvTestAppointments), _CurrentDGVColumnSortDirection);
        }

        private void _EditDataRowInDGV(string NewDateTime,int DgvRowIndex)
        {
            _DGVUtilityLib.EditOneColumnValueInDgv<string>(dgvTestAppointments, "Appointment Date", NewDateTime, DgvRowIndex);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.SelectedRows.Count == 1)
            {
                int TestAppointmentID = (int)dgvTestAppointments.SelectedRows[0].Cells[_PrimaryKeyViewedColumnName].Value;

                frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationId, _TestTypeID
                    , TestAppointmentID, (int)dgvTestAppointments.SelectedRows[0].Index);

                    frm.AfterEditingAppointment += _EditDataRowInDGV;
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
            _DGVUtilityLib.EditOneColumnValueInDgv<bool>(dgvTestAppointments, "Is Locked", true, DGVRowIndex);
        }

        private void _UpdateLDLAppDgv()
        {
            AfterPassingTest.Invoke(_DGVRowIndex);
        }

        private void tsmiTakeTest_Click(object sender, EventArgs e)
        {
            if (dgvTestAppointments.SelectedRows.Count == 1)
            {
                if((bool)dgvTestAppointments.SelectedRows[0].Cells["Is Locked"].Value)
                {
                    if(clsTest.HasPassedTheTest(_LocalDrivingLicenseApplicationId, (byte) _TestTypeID))
                        MessageBox.Show("This person already passed this test.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    else
                        MessageBox.Show("This person already taken this test and failed in it , schedule new test for the person in order to retake it.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                }

                frmTakeTest frm = new frmTakeTest((int)dgvTestAppointments.SelectedRows[0].Cells[_PrimaryKeyViewedColumnName].Value
                    , _TestTypeID, (int)dgvTestAppointments.SelectedRows[0].Index);

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
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvTestAppointments, _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection, e);
            
            DataTable dtSortedInfo = clsTestAppointment.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvTestAppointments.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection),_LocalDrivingLicenseApplicationId, (byte) _TestTypeID);

            _DGVUtilityLib.SetDataSourceAfterColumnOrdering(dgvTestAppointments, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvTestAppointments_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }

        private void uctrlDLApplicationInfo_OnPersonEditedInfo(clsPerson UpdatedPersonInfo)
        {
                OnEditingApplicantPersonalInfo?.Invoke(UpdatedPersonInfo, _DGVRowIndex);
        }
    }
}