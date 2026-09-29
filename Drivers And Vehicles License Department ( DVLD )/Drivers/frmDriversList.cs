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
        private clsDgvUtilityLib _DgvUtilityLib;
        private string _LastColumnNameDgvSortedBy;
        private clsDgvUtilityLib.enDataGridViewSortDirection _CurrentDgvSortDirection;

        private int _LastBroughtDriverID;
        private string _PrimaryKeyViewedColumnName;
        public frmDriversList()
        {
            InitializeComponent();

            _DgvUtilityLib = new clsDgvUtilityLib();
            _CurrentDgvSortDirection = clsDgvUtilityLib.enDataGridViewSortDirection.Descending;
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

            List<string> lColumnsNames = clsDgvUtilityLib.GetDgvColumnsNames(dgvDrivers, new string[] {"Date Created","Active Licenses"});
            
            for (byte i = 0; i < lColumnsNames.Count; i++)
            {
                Items[i + 1] = lColumnsNames[i];
            }

            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";
        }
        private void _SetCertainControlsPosition()
        {
            dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);

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
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text
                        ,_LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text
                        ,_LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == _PrimaryKeyViewedColumnName || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection),_LastBroughtDriverID, _DgvUtilityLib.NumberOfRowsToOffset, null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection),_LastBroughtDriverID, _DgvUtilityLib.NumberOfRowsToOffset, '%');
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
            if (txtFilter.Text != "")
                dgvDrivers.DataSource = _GetFilteredData();
            else
            {
                clsDgvUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDgvSortDirection);

                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
            }
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
                _LastBroughtDriverID = clsDgvUtilityLib.GetLastBroughtValueOfPKColumn(dgvDrivers, _PrimaryKeyViewedColumnName, _CurrentDgvSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_PrimaryKeyViewedColumnName, _LastColumnNameDgvSortedBy);

            DataRow[] NewRows;
            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDgvUtilityLib.GetDgvColumnsNames(dgvDrivers), _CurrentDgvSortDirection);
            }

            else
            {
                DataTable dtDriversInfo = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, _LastBroughtDriverID, _LastColumnNameDgvSortedBy
                    , _DgvUtilityLib.NumberOfRowsToOffset, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection));

                NewRows = dtDriversInfo?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDgvUtilityLib.GetDgvColumnsNames(dgvDrivers), _CurrentDgvSortDirection);
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

                if(!clsDgvUtilityLib._IsRepeatedDataLoadToDgv(txtFilter))
                {
                    dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
                }
            }
            else
            {
                txtFilter.Visible = false;

                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
            }

            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";

            clsDgvUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDgvSortDirection);
        }
        private void dgvDrivers_Scroll(object sender, ScrollEventArgs e)
        {
                if(e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                {
                    if (dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.None) == dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                        _AppendPartOfRemainingData();
                }
        }

        private void tsmiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            if (dgvDrivers.SelectedRows.Count > 1)
            {
                MessageBox.Show("You have to select only one driver in order to show their license history.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID((int)dgvDrivers.SelectedRows[0].Cells["Driver ID"].Value));
            frm.ShowDialog();
        }

        private bool _IsNumericColumn(string ColumnName)
        {
            return (ColumnName == _PrimaryKeyViewedColumnName || ColumnName == "Person ID");
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            _CurrentDgvSortDirection = clsDgvUtilityLib.ReverseCurrentDgvSortDirection(_CurrentDgvSortDirection);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text == "")
            {

                dtSortedInfo = clsDriver.GetSortedInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                , clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsDriver.GetSortedInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                    , clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);
                }

                else
                {
                    dtSortedInfo = clsDriver.GetSortedInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvDrivers.Columns[e.ColumnIndex].HeaderText
                     , clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
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