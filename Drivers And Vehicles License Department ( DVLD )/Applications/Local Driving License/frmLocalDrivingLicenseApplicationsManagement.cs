using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Controls;
using DVLDPresentationLayer.Core;
using DVLDPresentationLayer.LocalDrivingLicenseApplications;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmLocalDrivingLicenseApplicationsManagement : Form
    {
        private string _PreviousCbFilterSelectedItem;
        private string _PreviousCbStatusSelectedItem;
        private string _LastColumnNameDgvSortedBy;
        private string _PrimaryKeyViewedColumnName;
        private int _LastBroughtLDLAppID;

        private string _PreviousFilterText = "";
        private bool _PreventedInput;

        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDGVColumnSortDirection;
        private clsDataGridViewUtilityLib _DgvUtilityLib;

        public frmLocalDrivingLicenseApplicationsManagement()
        {
            InitializeComponent();

            _LastBroughtLDLAppID = -1;
            _PrimaryKeyViewedColumnName = "L.D.L.AppID";
            _CurrentDGVColumnSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
            _DgvUtilityLib = new clsDataGridViewUtilityLib();
        }

        private void _AddComboBoxesFilterItems()
        {
            object[] Items = new object[dgvLDLApplications.Columns.Count - 2];
            Items[0] = "None";

            List<string> ldgvColumnsNames = clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvLDLApplications, new string[] { "Driving Class", "Application Date", "Passed Tests" });

            for (byte i = 0; i < ldgvColumnsNames.Count; i++)
            {
                Items[i + 1] = ldgvColumnsNames[i];
            }

            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";

            object[] cbStatusItems = new object[] { "All", "New", "Canceled", "Completed" };
            cbStatus.DataSource = cbStatusItems;
            cbStatus.SelectedItem = "All";
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplications(clsGlobalSettings.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

            if(dgvLDLApplications.DataSource != null)
            _AddComboBoxesFilterItems();

            lblRecordsNumber.Text = clsLocalDrivingLicenseApplication.GetTotalLDLApplicationsCount().ToString();
        }

        private void DrawComboBoxItems(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems((ComboBox)sender, e);
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (_PreventedInput || _PreviousFilterText == txtFilter.Text.Trim())
                return;

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);

            if (txtFilter.Text != "")
                dgvLDLApplications.DataSource = _GetFilteredData();

            else
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplications(clsGlobalSettings.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

            _PreviousFilterText = txtFilter.Text.Trim();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if ((_PreviousCbFilterSelectedItem == "Status" && cbStatus.SelectedItem.ToString() != "All")
                || !clsDataGridViewUtilityLib.IsRepeatedDataLoadToDgv(txtFilter))
            {
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplications(clsGlobalSettings.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            if (_PreviousCbFilterSelectedItem == "Status")
                cbStatus.SelectedItem = "All";

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbStatus.Visible = false;
                _PreviousCbStatusSelectedItem = null;
            }

            else if (cbFilterBy.SelectedItem.ToString() == "Status")
            {
                txtFilter.Visible = false;
                cbStatus.Visible = true;
            }

            else
            {
                txtFilter.Visible = true;
                txtFilter.Focus();

                cbStatus.Visible = false;
                _PreviousCbStatusSelectedItem = null;
            }

            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);
        }

        private void _AddNewValuesToDGV(object[] NewAppDetails)
        {
            if (dgvLDLApplications.DataSource == null)
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetColumnsNamesForView();

            _DgvUtilityLib.AddNewRowToDGV(dgvLDLApplications,NewAppDetails,dgvLDLApplications.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(dgvLDLApplications,"National No.");
            frm.OnAddedLDLApplication += _AddNewValuesToDGV;
            frm.OnEditedLDLApplication += _EditDataRowInDGV;
            frm.OnEditedApplicantPersonalInfo += _EditApplicantPersonalInfo;
            frm.ShowDialog();
        }

        private void _EditDataRowInDGV(object[] ModifiedAppDetails,int DgvRowIndex)
        {
            _DgvUtilityLib.EditFullDataRowInDgv(dgvLDLApplications,ModifiedAppDetails,DgvRowIndex);
        }

        private void tsmiEditApplication_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one application to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

                frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(LDLApplication,dgvLDLApplications.SelectedRows[0].Index);
                frm.OnEditedLDLApplication += _EditDataRowInDGV;
                frm.OnEditedApplicantPersonalInfo += _EditApplicantPersonalInfo;
                frm.ShowDialog();
            }
        }

        private void cmsLDLApplication_Paint(object sender, PaintEventArgs e)
        {
            if (dgvLDLApplications.Rows.Count == 0)
                cmsLDLApplication.Close();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 5)
            {
                MessageBox.Show("You Can Delete Maximum 5 Local Driving License Applications In One Time!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result;
            if (dgvLDLApplications.SelectedRows.Count == 1)
                result = MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            else
                result = MessageBox.Show("Are you sure you want to delete those applications?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                int[] SelectedRowsIndex = new int[dgvLDLApplications.SelectedRows.Count];
                byte TotalDeletedRecords = 0;

                for (short i = 0; i < dgvLDLApplications.SelectedRows.Count; i++)
                {
                    int ApplicationID = clsLocalDrivingLicenseApplication.GetApplicationID((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value);

                    if (!clsLocalDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value, ApplicationID))
                    {
                        MessageBox.Show($"Local driving license application who has ID : {Convert.ToInt32(dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SelectedRowsIndex[i] = -1;
                    }
                    else if (dgvLDLApplications.SelectedRows[i].Cells["Status"].Value.ToString() == "Completed")
                    {
                        MessageBox.Show($"Local driving license application who has ID : {Convert.ToInt32(dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value)} cannot be deleted because it is completed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }
                    else
                    {
                        SelectedRowsIndex[i] = dgvLDLApplications.SelectedRows[i].Index;
                        TotalDeletedRecords++;
                    }
                }
                _DgvUtilityLib.DeleteSelectedDgvRows(dgvLDLApplications, SelectedRowsIndex);
                lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) - TotalDeletedRecords).ToString();
            }
        }

        private void tsmiCancelApplication_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
                MessageBox.Show("You can select only one local driving license application to cancel!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    int ApplicationID = clsLocalDrivingLicenseApplication.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

                    if (clsApplication.ChangeApplicationStatus(ApplicationID, clsApplication.enApplicationStatus.Canceled))
                        _DgvUtilityLib.EditOneColumnValueInDgv<string>(dgvLDLApplications, "Status", "Canceled", dgvLDLApplications.SelectedRows[0].Index);
                    else
                        MessageBox.Show("Failed To Cancel Application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          }

        private DataTable _GetFilteredDataForStatus(bool ScrollCase = false)
        {
            DataTable dtFilteredData;

            if (!ScrollCase)
                dtFilteredData = clsLocalDrivingLicenseApplication.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbStatus.SelectedItem.ToString(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), null);

            else
                dtFilteredData = clsLocalDrivingLicenseApplication.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbStatus.SelectedItem.ToString(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtLDLAppID, _DgvUtilityLib.NumberOfRowsToOffset, null);

            return dtFilteredData;
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtLDLApplicationsInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), null);

                else if (cbFilterBy.SelectedItem.ToString() == "Status")
                    dtLDLApplicationsInfo = _GetFilteredDataForStatus(false);

                else
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtLDLAppID, _DgvUtilityLib.NumberOfRowsToOffset, null);

                else if (cbFilterBy.SelectedItem.ToString() == "Status")
                    dtLDLApplicationsInfo = _GetFilteredDataForStatus(true);

                else
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtLDLAppID, _DgvUtilityLib.NumberOfRowsToOffset, '%');
            }

            return dtLDLApplicationsInfo;
        }

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtLDLAppID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvLDLApplications, _PrimaryKeyViewedColumnName, _CurrentDGVColumnSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_LastColumnNameDgvSortedBy);

            DataRow[] NewRows = null;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvLDLApplications, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvLDLApplications), _CurrentDGVColumnSortDirection);
            }

            else
            {
                DataTable dtLDLApplications = clsLocalDrivingLicenseApplication.GetLocalDrivingLicenseApplications(clsGlobalSettings.WantedNumOfRowsFromDB, _LastBroughtLDLAppID, _LastColumnNameDgvSortedBy
                    , _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

                NewRows = dtLDLApplications?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvLDLApplications, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvLDLApplications), _CurrentDGVColumnSortDirection);
            }
        }
        
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbStatusSelectedItem == cbStatus.SelectedItem.ToString())
                return;


            if (cbFilterBy.SelectedItem.ToString() == "Status")
            {
                clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);

                dgvLDLApplications.DataSource = _GetFilteredDataForStatus(false);
                _PreviousCbStatusSelectedItem = cbStatus.SelectedItem.ToString();
            }
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = clsGlobalSettings.ComboBoxItemsBackColor;
        }

        private void cbStatus_DropDownClosed(object sender, EventArgs e)
        {
            cbStatus.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxBackColor;
        }

        private bool _CanScheduleTest()
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application to schedule test to it", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmsLDLApplication.Close();
                return false;
            }
            else
                return true;
        }

        private void cmsLDLApplication_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseAppID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

            if(LocalDrivingLicenseApplication != null)
            {
                bool LicenseExists = LocalDrivingLicenseApplication.HasActiveLocalLicense();
                tsmiEditApplication.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
                tsmiDeleteApplication.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
                tsmiCancelApplication.Enabled = !LicenseExists && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);

                byte TotalPassedTests = LocalDrivingLicenseApplication.GetTotalPassedTests();
                tsmiScheduleTests.Enabled = !LicenseExists && (TotalPassedTests != 3) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
                
                bool PassedVisionTest = LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.VisionTest);
                bool PassedWrittenTest= LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.WrittenTest);
                bool PassedStreetTest = LocalDrivingLicenseApplication.DoesPassedTestType(clsTestType.enTestType.StreetTest);

                if(tsmiScheduleTests.Enabled)
                {
                    tsmiScheduleVisionTest.Enabled = !PassedVisionTest;
                    tsmiScheduleWrittenTest.Enabled = PassedVisionTest && !PassedWrittenTest;
                    tsmiScheduleStreetTest.Enabled = PassedVisionTest && PassedWrittenTest && !PassedStreetTest;
                }

                tsmiShowLicense.Enabled = LicenseExists;
                tsmiIssueDrivingLicense_FirstTime.Enabled = !LicenseExists && (TotalPassedTests == 3) && (LocalDrivingLicenseApplication.ApplicationStatus == clsApplication.enApplicationStatus.New);
            }
        }

        private void _EditRowForPassedTest(int DgvRowIndex)
        {
            byte PassedTests = Convert.ToByte(dgvLDLApplications.Rows[DgvRowIndex].Cells["Passed Tests"].Value);
            PassedTests += 1;

            _DgvUtilityLib.EditOneColumnValueInDgv<byte>(dgvLDLApplications, "Passed Tests", PassedTests, DgvRowIndex);
        }

        private void _EditApplicantPersonalInfo(clsPerson UpdatedPersonInfo, int LDLAppDGVRowIndex)
        {
            _DgvUtilityLib.EditOneColumnValueInDgv(dgvLDLApplications, "Full Name", UpdatedPersonInfo.FullName, LDLAppDGVRowIndex);

            _DgvUtilityLib.EditOneColumnValueInDgv(dgvLDLApplications, "National No.", UpdatedPersonInfo.NationalNo, LDLAppDGVRowIndex);
        }

        private void tsmiScheduleVisionTest_Click(object sender, EventArgs e)
        {
            if (_CanScheduleTest())
            {
                frmTestsAppointments frm = new frmTestsAppointments((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index, clsTestType.enTestType.VisionTest);
                frm.AfterPassingTest += _EditRowForPassedTest;
                frm.OnEditingApplicantPersonalInfo += _EditApplicantPersonalInfo;
                frm.ShowDialog();
            }
        }
            
        private void tsmiScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            if(_CanScheduleTest())
            {
                frmTestsAppointments frm = new frmTestsAppointments((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index, clsTestType.enTestType.WrittenTest);
                frm.AfterPassingTest += _EditRowForPassedTest;
                frm.OnEditingApplicantPersonalInfo += _EditApplicantPersonalInfo;
                frm.ShowDialog();
            }
        }

        private void tsmiScheduleStreetTest_Click(object sender, EventArgs e)
        {
            if(_CanScheduleTest())
            {
                frmTestsAppointments frm = new frmTestsAppointments((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index, clsTestType.enTestType.StreetTest);
                frm.AfterPassingTest += _EditRowForPassedTest;
                frm.OnEditingApplicantPersonalInfo += _EditApplicantPersonalInfo;
                frm.ShowDialog();
            }
        }

        private void _EditAppStatusToCompleted(int DGVRowIndex)
        {
            clsApplication.ChangeApplicationStatus
              (
                clsLocalDrivingLicenseApplication.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value),
                clsApplication.enApplicationStatus.Completed
              );

            _DgvUtilityLib.EditOneColumnValueInDgv<string>(dgvLDLApplications, "Status", "Completed", DGVRowIndex);
        }

        private void tsmiIssueDLFirstTime_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application in order to issue license for it.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmIssueDrivingLicenseFirstTime frm = new frmIssueDrivingLicenseFirstTime((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index);
            frm.AfterLicenseIssuance += _EditAppStatusToCompleted;
            frm.OnEditedApplicantPersonalInfo += _EditApplicantPersonalInfo;
            frm.ShowDialog();
        }

        private void tsmiShowLicense_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application in order to show license.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmShowDriverLiecnseInfo frm = new frmShowDriverLiecnseInfo(clsLocalLicense.GetLicenseID(clsLocalDrivingLicenseApplication.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value)));
            frm.ShowDialog();
        }

        private void tsmiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application in order to show person license history.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsPerson.GetPersonID(dgvLDLApplications.SelectedRows[0].Cells["National No."].Value.ToString()));
            frm.OnEditedDriverPersonalInfo += _EditApplicantPersonalInfo;
            frm.ShowDialog();
        }

        private void tsmiShowApplicationDetails_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application in order to view the application details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index);
            frm.OnApplicantEditedIfo += _EditApplicantPersonalInfo;
            frm.ShowDialog();
        }

        private void dgvLDLApplications_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDataGridViewUtilityLib.IsDgvLastRowDisplayed(dgvLDLApplications))
                    _AppendPartOfRemainingData();
            }
        }

        private void dgvLDLApplications_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDataGridViewUtilityLib.IsDgvLastRowSelected(dgvLDLApplications))
                _AppendPartOfRemainingData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvLDLApplications, _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection, e);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text.Trim() == "" && cbFilterBy.SelectedItem.ToString() != "Status")
            {
                dtSortedInfo = clsLocalDrivingLicenseApplication.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvLDLApplications.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                    dtSortedInfo = clsLocalDrivingLicenseApplication.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvLDLApplications.Columns[e.ColumnIndex].HeaderText
                            , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection),cbFilterBy.SelectedItem.ToString(),txtFilter.Text.Trim(), null);

                else if (cbFilterBy.SelectedItem.ToString() == "Status")
                    dtSortedInfo = clsLocalDrivingLicenseApplication.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvLDLApplications.Columns[e.ColumnIndex].HeaderText
                            , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), cbFilterBy.SelectedItem.ToString(), cbStatus.SelectedItem.ToString(), null);

                else
                    dtSortedInfo = clsLocalDrivingLicenseApplication.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvLDLApplications.Columns[e.ColumnIndex].HeaderText,
                               clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim(), '%');
            }

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvLDLApplications, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvLDLApplications_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            else if(cbFilterBy.SelectedItem.ToString() == "Full Name")
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);

            _PreventedInput = e.Handled;
        }
    }
 }