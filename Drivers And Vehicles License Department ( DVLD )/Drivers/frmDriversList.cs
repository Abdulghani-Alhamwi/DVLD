using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Applications;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmDriversList : Form
    {
        private string _PreviousCbFilterSelectedItem;
        private string _LastColumnNameDgvSortedBy;

        private clsDataGridViewUtilityLib _DgvUtilityLib;
        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDGVColumnSortDirection;

        private string _PreviousFilterText = "";
        private bool _PreventedInput;

        private int _LastBroughtDriverID;
        private string _PrimaryKeyViewedColumnName;
        private bool _FirstTimeLoad = true;

        public frmDriversList()
        {
            InitializeComponent();

            _DgvUtilityLib = new clsDataGridViewUtilityLib();
            _CurrentDGVColumnSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Ascending;
            _LastBroughtDriverID = -1;
            _PrimaryKeyViewedColumnName = "Driver ID";
        }

        private void frmDriversManagements_Load(object sender, EventArgs e)
        {
            dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsGlobalSettings.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

            if (dgvDrivers.DataSource != null)
                _AddDropDownItems();

            _SetCertainControlsPosition();
            lblRecordsNumber.Text = clsDriver.GetTotalDriversCount().ToString();
        }

        private void _AddDropDownItems()
        {
            object[] Items = new object[dgvDrivers.Columns.Count - 1];
            Items[0] = "None";

            List<string> lColumnsNames = clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvDrivers, new string[] {"Date Created","Active Licenses"});
            
            for (byte i = 0; i < lColumnsNames.Count; i++)
            {
                Items[i + 1] = lColumnsNames[i];
            }

            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";
        }

        private void _SetCertainControlsPosition()
        {
            clsUtility.CenterControlHorizontally(this, pbDrivers);
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtDriversInfo;
            if (!ScrollCase)
            {
                if (_IsNumericColumn())
                    dtDriversInfo = clsDriver.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _DgvUtilityLib.NumberOfRowsToOffset, null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _DgvUtilityLib.NumberOfRowsToOffset, '%');
            }

            else
            {
                if (_IsNumericColumn())

                    dtDriversInfo = clsDriver.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection),_LastBroughtDriverID, _DgvUtilityLib.NumberOfRowsToOffset, null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsGlobalSettings.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection),_LastBroughtDriverID, _DgvUtilityLib.NumberOfRowsToOffset, '%');
            }

            if (dtDriversInfo != null)
            {
                return dtDriversInfo;
            }
            else
                return null;
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (_PreventedInput || _PreviousFilterText == txtFilter.Text.Trim())
                return;

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection,clsDataGridViewUtilityLib.enDataGridViewSortDirection.Ascending);

            _DgvUtilityLib.NumberOfRowsToOffset = 0;
            
            if (txtFilter.Text != "")
                dgvDrivers.DataSource = _GetFilteredData();

            else
                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsGlobalSettings.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

            _PreviousFilterText = txtFilter.Text.Trim();
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxBackColor;
        }

        private void cbFilterBy_DropDown(object sender, EventArgs e)
        {
            cbFilterBy.BackColor = clsGlobalSettings.ComboBoxItemsBackColor;
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtDriverID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvDrivers, _PrimaryKeyViewedColumnName, _CurrentDGVColumnSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_LastColumnNameDgvSortedBy, true);

            DataRow[] NewRows;
            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvDrivers), _CurrentDGVColumnSortDirection);
            }

            else
            {
                DataTable dtDriversInfo = clsDriver.GetDriversInfo(clsGlobalSettings.WantedNumOfRowsFromDB, _LastBroughtDriverID, _LastColumnNameDgvSortedBy
                    , _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

                NewRows = dtDriversInfo?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvDrivers), _CurrentDGVColumnSortDirection);
            }
        }

        private void dgvDrivers_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.None) == dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Selected))
                _AppendPartOfRemainingData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_FirstTimeLoad)
            {
                _FirstTimeLoad = false;
                return;
            }

            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
                txtFilter.Focus();

                if(!clsDataGridViewUtilityLib.IsRepeatedDataLoadToDgv(txtFilter))
                {
                    dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsGlobalSettings.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
                }
            }
            else
            {
                txtFilter.Visible = false;

                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsGlobalSettings.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);
        }

        private void dgvDrivers_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.None) == dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                    _AppendPartOfRemainingData();
            }
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedPersonInfo,int DGVRowIndex)
        {
            _DgvUtilityLib.EditOneColumnValueInDgv(dgvDrivers, "Full Name", UpdatedPersonInfo.FullName, DGVRowIndex);
            _DgvUtilityLib.EditOneColumnValueInDgv(dgvDrivers, "National No.", UpdatedPersonInfo.NationalNo, DGVRowIndex);
        }

        private void tsmiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            if (dgvDrivers.SelectedRows.Count > 1)
            {
                MessageBox.Show("You have to select only one driver in order to show their license history.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID((int)dgvDrivers.SelectedRows[0].Cells["Driver ID"].Value), dgvDrivers.SelectedRows[0].Index);
            frm.OnEditedDriverPersonalInfo += _EditDriverPersonalInfo;
            frm.ShowDialog();
        }

        private bool _IsNumericColumn(string ColumnName)
        {
            return (ColumnName == _PrimaryKeyViewedColumnName || ColumnName == "Person ID");
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvDrivers, _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection, e);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text.Trim() == "")
            {
                dtSortedInfo = clsDriver.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsDriver.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                    , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim(), null);
                }

                else
                {
                    dtSortedInfo = clsDriver.GetSortedInfo(clsGlobalSettings.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                     , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text.Trim(), '%');
                }
            }

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvDrivers, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvDrivers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }

        private void tsmiShowPersonalInfo_Click(object sender, EventArgs e)
        {
            if (dgvDrivers.SelectedRows.Count == 1)
            {
                frmPersonInfo frm = new frmPersonInfo((int)dgvDrivers.SelectedRows[0].Cells["Person ID"].Value, dgvDrivers.SelectedRows[0].Index);
                frm.AfterUpdatingPersonalInfo += _EditDriverPersonalInfo;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("You can select only one driver to show their personal details."
                    , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void _EditInfoForNewIntLicenseIssuance(int DGVRowIndex)
        {
            string ColumnNameToEdit = "Active Licenses";
            _DgvUtilityLib.EditOneColumnValueInDgv(dgvDrivers, ColumnNameToEdit, (int)dgvDrivers.Rows[DGVRowIndex].Cells[ColumnNameToEdit].Value + 1, DGVRowIndex);
        }

        private void tsmiIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvDrivers.SelectedRows[0].Cells["Driver ID"].Value;
            clsLocalLicense LocalLicenseInfo = clsLocalLicense.Find(
                clsLocalLicense.GetLocalLicenseIdByLicenseClass(DriverID, clsLicenseClass.GetLicenseClassID(clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass)));

            frmNewIntLicenseApplication frm = new frmNewIntLicenseApplication(DriverID,LocalLicenseInfo, dgvDrivers.SelectedRows[0].Index);
            
            frm.OnUpdatedDriverPersonalInfo += _EditDriverPersonalInfo;
            frm.AfterIssueingLicense += _EditInfoForNewIntLicenseIssuance;
            frm.ShowDialog();
        }

        private bool _IsNumericColumn()
        {
            return cbFilterBy.SelectedItem.ToString() == _PrimaryKeyViewedColumnName || cbFilterBy.SelectedItem.ToString() == "Person ID";
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_IsNumericColumn())
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (cbFilterBy.SelectedItem.ToString() == "Full Name")
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            _PreventedInput = e.Handled;
        }
    }
}