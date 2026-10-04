using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmDriversList : Form
    {
        private string _PreviousCbFilterSelectedItem;
        private string _LastColumnNameDgvSortedBy;

        private clsDataGridViewUtilityLib _DgvUtilityLib;
        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDgvColumnSortDirection;

        private int _LastBroughtDriverID;
        private string _PrimaryKeyViewedColumnName;

        public frmDriversList()
        {
            InitializeComponent();

            _DgvUtilityLib = new clsDataGridViewUtilityLib();
            _CurrentDgvColumnSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
            _LastBroughtDriverID = -1;
            _PrimaryKeyViewedColumnName = "Driver ID";
        }

        private void frmDriversManagements_Load(object sender, EventArgs e)
        {
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
            dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);

            if(dgvDrivers.DataSource != null)
            _AddDropDownItems();

            clsGeneralUtility.CenterControlHorizontally(this, pbDrivers);
            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtDriversInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == _PrimaryKeyViewedColumnName || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtDriversInfo = clsDriver.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text
                        ,_LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text
                        ,_LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == _PrimaryKeyViewedColumnName || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtDriversInfo = clsDriver.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection),_LastBroughtDriverID, _DgvUtilityLib.NumberOfRowsToOffset, null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection),_LastBroughtDriverID, _DgvUtilityLib.NumberOfRowsToOffset, '%');
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
            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDgvColumnSortDirection);

            if (txtFilter.Text != "")
                dgvDrivers.DataSource = _GetFilteredData();

            else

                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == _PrimaryKeyViewedColumnName || cbFilterBy.SelectedItem.ToString() == "Person ID")
            {
                if (Char.IsDigit((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else if (cbFilterBy.SelectedItem.ToString() == "Full Name")
            {
                if (Char.IsLetter((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else
                txtFilter.ReadOnly = false;
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
            clsGeneralUtility.DrawComboBoxItems(sender, e);
        }

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtDriverID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvDrivers, _PrimaryKeyViewedColumnName, _CurrentDgvColumnSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_PrimaryKeyViewedColumnName, _LastColumnNameDgvSortedBy);

            DataRow[] NewRows;
            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvDrivers), _CurrentDgvColumnSortDirection);
            }

            else
            {
                DataTable dtDriversInfo = clsDriver.GetDriversInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _LastBroughtDriverID, _LastColumnNameDgvSortedBy
                    , _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection));

                NewRows = dtDriversInfo?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvDrivers), _CurrentDgvColumnSortDirection);
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
            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
                txtFilter.Focus();

                if(!clsDataGridViewUtilityLib._IsRepeatedDataLoadToDgv(txtFilter))
                {
                    dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
                }
            }
            else
            {
                txtFilter.Visible = false;

                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
            }

            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDgvColumnSortDirection);
        }

        private void dgvDrivers_Scroll(object sender, ScrollEventArgs e)
        {
                if(e.ScrollOrientation == ScrollOrientation.VerticalScroll)
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
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvDrivers, ref _LastColumnNameDgvSortedBy, ref _CurrentDgvColumnSortDirection, e);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text == "")
            {

                dtSortedInfo = clsDriver.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsDriver.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                    , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);
                }

                else
                {
                    dtSortedInfo = clsDriver.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                     , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
                }
            }

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvDrivers, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvDrivers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }
    }
}