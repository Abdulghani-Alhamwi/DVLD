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
    public partial class frmLDLApplicationsManagement : Form
    {
        bool _AllowDataLoading;
        public frmLDLApplicationsManagement()
        {
            InitializeComponent();
        }
        private void _AddComboBoxesFilterItems()
        {
            object[] Items = new object[dgvLDLApplications.Columns.Count - 2];
            Items[0] = "None";

            List<string> ldgvColumnsNames = clsUtility.GetDgvColumnsNames(dgvLDLApplications, new string[] { "Driving Class", "Application Date", "Passed Tests" });

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
            dgvLDLApplications.DataSource = clsLocalDrivingLicenseApp.GetLDLApplications(clsUtility.WantedNumOfRowsFromDB);

            if(dgvLDLApplications.DataSource != null)
            _AddComboBoxesFilterItems();

            lblRecordsNumber.Text = clsLocalDrivingLicenseApp.GetTotalLDLApplicationsCount().ToString();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DrawComboBoxItems(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems((ComboBox)sender, e);
        }
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtFilter.Text != "")
                dgvLDLApplications.DataSource = _GetFilteredData();

            else
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApp.GetLDLApplications(clsUtility.WantedNumOfRowsFromDB);
        }
        private void _LoadDataAfterFirstTimeLoad(ref bool _AllowDataLoading)
        {
            if (_AllowDataLoading)
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApp.GetLDLApplications(clsUtility.WantedNumOfRowsFromDB);
            else
                _AllowDataLoading = true;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None" && cbFilterBy.SelectedItem.ToString() != "Status")
            {
                txtFilter.Visible = true;
                cbStatus.Visible = false;
                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
            }

            else if (cbFilterBy.SelectedItem.ToString() != "None" && cbFilterBy.SelectedItem.ToString() == "Status")
            {
                txtFilter.Visible = false;
                cbStatus.Visible = true;
                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
            }

            else
            {
                txtFilter.Visible = false;
                cbStatus.Visible = false;
               
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApp.GetLDLApplications(clsUtility.WantedNumOfRowsFromDB);
                _AllowDataLoading = false;
                txtFilter.Text = "";
            }
        }
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
            {
                if (char.IsDigit((char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else if (cbFilterBy.SelectedItem.ToString() == "Status")
            {
                if (char.IsLetter((char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
        }
        private void _AddNewValuesToDGV(ref object[] NewAppDetails)
        {
            if (dgvLDLApplications.DataSource == null)
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApp.GetColumnsNamesForView();

            clsUtility.AddNewRowToDGV(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource,ref NewAppDetails, dgvLDLApplications.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            frmNewLDLApplication frm = new frmNewLDLApplication();
            frm.OnAddedLDLApplication += _AddNewValuesToDGV;
            frm.ShowDialog();
        }

        private void _EditDataRowInDGV(ref object[] ModifiedAppDetails,int DgvRowIndex)
        {
            clsUtility.EditFullDataRowInDgv(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource, ref ModifiedAppDetails,DgvRowIndex);
        }

        private void tsmiEditApplication_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one application to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                clsLocalDrivingLicenseApp LDLApplication = clsLocalDrivingLicenseApp.Find((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

                frmNewLDLApplication frm = new frmNewLDLApplication(LDLApplication,dgvLDLApplications.SelectedRows[0].Index);
                frm.OnEditedLDLApplication += _EditDataRowInDGV;
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
                if(dgvLDLApplications.SelectedRows.Count == 1)
                result = MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                else
                result = MessageBox.Show("Are you sure you want to delete those applications?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                int[] SelectedRowsIndex = new int[dgvLDLApplications.SelectedRows.Count];
                byte TotalDeletedRecords = 0;

                for (short i = 0; i < dgvLDLApplications.SelectedRows.Count; i++)
                    {
                    int ApplicationID = clsLocalDrivingLicenseApp.GetApplicationID((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value);

                    if (!clsLocalDrivingLicenseApp.DeleteLDLApplication((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value, ApplicationID))
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
                    clsUtility.DeleteSelectedRowsFromView(dgvLDLApplications, SelectedRowsIndex);
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
                    int ApplicationID = clsLocalDrivingLicenseApp.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

                    if (clsApplication.ChangeApplicationStatus(ApplicationID, clsApplication.enApplicationStatus.Canceled))
                        clsUtility.EditOneColumnValueInDgv(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource,"Status", "Canceled", dgvLDLApplications.SelectedRows[0].Index);
                    else
                        MessageBox.Show("Failed To Cancel Application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          }
        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtLDLApplicationsInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApp.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);

                else if (cbFilterBy.SelectedItem.ToString() == "Status")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApp.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbStatus.SelectedItem.ToString(), null);

                else
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApp.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApp.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvLDLApplications?.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.None)].Cells["L.D.L.AppID"].Value, null);
    
                else if (cbFilterBy.SelectedItem.ToString() == "Status")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApp.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbStatus.SelectedItem.ToString(), (int)dgvLDLApplications?.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.None)].Cells["L.D.L.AppID"].Value, null);

                else
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApp.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvLDLApplications?.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.None)].Cells["L.D.L.AppID"].Value, '%');
            }

            return dtLDLApplicationsInfo;
        }

        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows = null;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource, NewRows, clsUtility.GetDgvColumnsNames(dgvLDLApplications));
            }

            else
            {
                NewRows = clsLocalDrivingLicenseApp.GetLDLApplications(clsUtility.WantedNumOfRowsFromDB, (int)dgvLDLApplications.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["L.D.L.AppID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource, NewRows, clsUtility.GetDgvColumnsNames(dgvLDLApplications));
            }
        }
        
        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() == "Status")
            dgvLDLApplications.DataSource = clsLocalDrivingLicenseApp.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbStatus.SelectedItem.ToString(), null);
        }
        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = clsUtility.ComboBoxItemsBackColor;
        }
        private void cbStatus_DropDownClosed(object sender, EventArgs e)
        {
            cbStatus.BackColor = clsUtility.ComboBoxHighlightedBackColor;
        }
        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsUtility.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsUtility.ComboBoxBackColor;
        }
        private void _DisableSpecificMenuOptions()
        {
            tsmiCancelApplication.Enabled = false;
            tsmiDelete.Enabled = false;
            tsmiEditApplication.Enabled = false;
            tsmiScheduleTests.Enabled = false;
            tsmiIssueDLFirstTime.Enabled = false;
        }
        private void _EnableSpecificMenuOptions()
        {
            tsmiCancelApplication.Enabled = true;
            tsmiEditApplication.Enabled = true;
            tsmiDelete.Enabled = true;
            tsmiScheduleTests.Enabled = true;
        }
        private void _SetNewApplicationMenuLogic()
        {
            switch (Convert.ToByte(dgvLDLApplications.SelectedRows[0].Cells["Passed Tests"].Value))
            {
                case 0:
                    tsmiScheduleVisionTest.Enabled = true;
                    tsmiScheduleWrittenTest.Enabled = false;
                    tsmiScheduleStreetTest.Enabled = false;
                    tsmiIssueDLFirstTime.Enabled = false;
                    tsmiScheduleTests.Enabled = true;
                    break;

                case 1:
                    tsmiScheduleVisionTest.Enabled = false;
                    tsmiScheduleWrittenTest.Enabled = true;
                    tsmiScheduleStreetTest.Enabled = false;
                    tsmiIssueDLFirstTime.Enabled = false;
                    tsmiScheduleTests.Enabled = true;
                    break;

                case 2:
                    tsmiScheduleVisionTest.Enabled = false;
                    tsmiScheduleWrittenTest.Enabled = false;
                    tsmiScheduleStreetTest.Enabled = true;
                    tsmiIssueDLFirstTime.Enabled = false;
                    tsmiScheduleTests.Enabled = true;
                    break;

                case 3:
                    tsmiScheduleTests.Enabled = false;
                    tsmiIssueDLFirstTime.Enabled = true;
                    break;
            }
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
            switch ((string)dgvLDLApplications.SelectedRows[0].Cells["Status"].Value)
            {
                case "New":
                    _EnableSpecificMenuOptions();
                    _SetNewApplicationMenuLogic();
                    break;

                case "Completed":
                    _DisableSpecificMenuOptions();
                    tsmiShowLicense.Enabled = true;
                    break;

                case "Canceled":
                    _DisableSpecificMenuOptions();
                    tsmiShowLicense.Enabled = false;
                    break;
            }
        }
        private void _EditRowForPassedTest(int RowIndex)
        {
            byte PassedTests = Convert.ToByte(dgvLDLApplications.Rows[RowIndex].Cells["Passed Tests"].Value);
            clsUtility.EditOneColumnValueInDgv(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource, "Passed Tests", PassedTests + 1, RowIndex);
        }
        private void tsmiScheduleVisionTest_Click(object sender, EventArgs e)
        {
            if (_CanScheduleTest())
            {
                frmTestsAppointments frm = new frmTestsAppointments((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index, clsTestType.enTestType.VisionTest);
                frm.AfterPassingTest += _EditRowForPassedTest;
                frm.ShowDialog();
            }
        }
        private void tsmiScheduleWrittenTest_Click(object sender, EventArgs e)
        {
            if(_CanScheduleTest())
            {
                frmTestsAppointments frm = new frmTestsAppointments((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index, clsTestType.enTestType.WrittenTest);
                frm.AfterPassingTest += _EditRowForPassedTest;
                frm.ShowDialog();
            }
        }
        private void tsmiScheduleStreetTest_Click(object sender, EventArgs e)
        {
            if(_CanScheduleTest())
            {
                frmTestsAppointments frm = new frmTestsAppointments((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index, clsTestType.enTestType.StreetTest);
                frm.AfterPassingTest += _EditRowForPassedTest;
                frm.ShowDialog();
            }
        }

        private void _EditAppStatusToCompleted(int DGVRowIndex)
        {
            clsApplication.ChangeApplicationStatus
                (
                clsLocalDrivingLicenseApp.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value),
                clsApplication.enApplicationStatus.Completed
                );

            clsUtility.EditOneColumnValueInDgv(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource, "Status", "Completed", DGVRowIndex);
        }

        private void tsmiIssueDLFirstTime_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application in order to issue license for it.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

           frmIssueDrivingLicense frm = new frmIssueDrivingLicense((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value, dgvLDLApplications.SelectedRows[0].Index);
            frm.AfterLicenseIssuance += _EditAppStatusToCompleted;
            frm.ShowDialog();
        }

        private void tsmiShowLicense_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application in order to show license.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmLocalLicenseDetails frm = new frmLocalLicenseDetails(clsLocalLicense.GetLicenseID(clsLocalDrivingLicenseApp.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value)));
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
            frm.ShowDialog();
        }

        private void tsmiShowApplicationDetails_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one local driving license application in order to view the application details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmApplicationDetails frm = new frmApplicationDetails((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);
            frm.ShowDialog();
        }
        private void dgvLDLApplications_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsUtility.IsDgvLastRowDisplayed(dgvLDLApplications))
                    _AppendPartOfRemainingData();
            }
        }
        private void dgvLDLApplications_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsUtility.IsDgvLastRowSelected(dgvLDLApplications))
                _AppendPartOfRemainingData();
        }
    }
 }