using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Controls;
using DVLDPresentationLayer.Licenses;
using Utility_Library;

namespace DVLDPresentationLayer.TestTypes
{
    public partial class frmDetainedLicensesManagement : Form
    {
        private string _PreviousCbFilterSelectedItem;
        private string _PreviousCbIsReleasedSelectedItem;
        private string _LastColumnNameDgvSortedBy;
        private string _PrimaryKeyViewedColumnName;
        private int _LastBroughtDetainID;
        private string _ColumnNameToFilterBy;
        private string _LocalLicensesViewedColumnName = "L.ID";

        private string _PreviousFilterText = "";
        private bool _PreventedInput;

        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDGVColumnSortDirection;
        private clsDataGridViewUtilityLib _DgvUtilityLib;

        public frmDetainedLicensesManagement()
        {
            InitializeComponent();

            _PrimaryKeyViewedColumnName = "D.ID";
            _LastBroughtDetainID = -1;

            _CurrentDGVColumnSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
            _DgvUtilityLib = new clsDataGridViewUtilityLib();
        }

        private void _AddComboBoxesFilterItems()
        {
            object[] Items = new object[] { "None", "Detain ID", "Is Released", "National No.", "Full Name", "Release Application ID" };
            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";

            object[] cbIsReleasedItems = new object[] { "All", "Yes", "No" };
            cbIsReleased.Items.AddRange(cbIsReleasedItems);
            cbIsReleased.SelectedItem = "All";
        }

        private void frmDetainedLicensesManagement_Load(object sender, EventArgs e)
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicense.GetDetainedLicensesInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

            if (dgvDetainedLicenses.DataSource != null)
                _AddComboBoxesFilterItems();

            lblRecordsNumber.Text = clsDetainedLicense.GetTotalCount().ToString();
        }

        private void DrawComboBoxItems(object sender, DrawItemEventArgs e)
        {
            clsGeneralUtility.DrawComboBoxItems((ComboBox)sender, e);
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (_PreventedInput || _PreviousFilterText == txtFilter.Text.Trim())
                return;

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);

            if (txtFilter.Text != "")
                dgvDetainedLicenses.DataSource = _GetFilteredData();

            else
                dgvDetainedLicenses.DataSource = clsDetainedLicense.GetDetainedLicensesInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

            _PreviousFilterText = txtFilter.Text.Trim();
        }

        private bool _IsNumericColumn(string ColumnNameToFilterBy)
        {
            return (ColumnNameToFilterBy == "Detain ID" || ColumnNameToFilterBy == "Release Application ID");
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if ((_PreviousCbFilterSelectedItem == "Is Released" && _PreviousCbIsReleasedSelectedItem != "All")
                    || !clsDataGridViewUtilityLib.IsRepeatedDataLoadToDgv(txtFilter))
            {
                dgvDetainedLicenses.DataSource = clsDetainedLicense.GetDetainedLicensesInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            if (_PreviousCbFilterSelectedItem == "Is Released")
                cbIsReleased.SelectedItem = "All";

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbIsReleased.Visible = false;
                _PreviousCbIsReleasedSelectedItem = null;
            }

            else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
            {
                txtFilter.Visible = false;
                cbIsReleased.Visible = true;

                if (!clsDataGridViewUtilityLib.IsRepeatedDataLoadToDgv(txtFilter))
                    dgvDetainedLicenses.DataSource = clsDetainedLicense.GetDetainedLicensesInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            else
            {
                txtFilter.Visible = true;
                txtFilter.Focus();
                cbIsReleased.Visible = false;
                _PreviousCbIsReleasedSelectedItem = null;
            }

            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);
        }

        private void _ReleaseDetainedLicense(int DetainedLicenseID, DateTime ReleaseDate, int ReleaseApplicationID)
        {
            foreach (DataGridViewRow DetainInfo in dgvDetainedLicenses.Rows)
            {
                if ((int)DetainInfo.Cells["D.ID"].Value == DetainedLicenseID)
                {
                    _DgvUtilityLib.EditOneColumnValueInDgv<bool>(dgvDetainedLicenses, "Is Released", true, dgvDetainedLicenses.Rows.IndexOf(DetainInfo));
                    _DgvUtilityLib.EditOneColumnValueInDgv<string>(dgvDetainedLicenses, "Release Date", ReleaseDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)), dgvDetainedLicenses.Rows.IndexOf(DetainInfo));
                    _DgvUtilityLib.EditOneColumnValueInDgv<int>(dgvDetainedLicenses, "Release App.ID", ReleaseApplicationID, dgvDetainedLicenses.Rows.IndexOf(DetainInfo));
                }
            }
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(dgvDetainedLicenses, _LocalLicensesViewedColumnName);
            frm.AfterReleasingALicense += _ReleaseDetainedLicense;
            frm.OnUpdatedDriverPersonalInfo += _EditDriverPersonalInfo;
            frm.ShowDialog();
        }

        private void _AddNewRowToDGV(object[] NewAppDetails)
        {
            if (dgvDetainedLicenses.DataSource == null)
                dgvDetainedLicenses.DataSource = clsDetainedLicense.GetColumnsNamesForView();

            _DgvUtilityLib.AddNewRowToDGV(dgvDetainedLicenses, NewAppDetails, dgvDetainedLicenses.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void _EditDriverPersonalInfo(clsPerson UpdatedPersonalInfo, int DGVRowIndex)
        {
            _DgvUtilityLib.EditOneColumnValueInDgv(dgvDetainedLicenses, "Full Name", UpdatedPersonalInfo.FullName, DGVRowIndex);
            _DgvUtilityLib.EditOneColumnValueInDgv(dgvDetainedLicenses, "N.No.", UpdatedPersonalInfo.NationalNo, DGVRowIndex);
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLocalLicense frm = new frmDetainLocalLicense(dgvDetainedLicenses, _LocalLicensesViewedColumnName);
            frm.AfterDetainingLicense += _AddNewRowToDGV;
            frm.OnUpdatedDriverPersonalInfo += _EditDriverPersonalInfo;
            frm.ShowDialog();
        }

        private DataTable _GetFilteredDataForIsReleased(bool ScrollCase = false)
        {
            DataTable dtFilteredData;
            if (!ScrollCase)
                dtFilteredData = clsDetainedLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsReleased.SelectedItem.ToString(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), null);

            else
                dtFilteredData = clsDetainedLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsReleased.SelectedItem.ToString(),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtDetainID, _DgvUtilityLib.NumberOfRowsToOffset, null);

            return dtFilteredData;
        }

        private void _SetFilterColumnNameToOriginal()
        {
            switch (cbFilterBy.SelectedItem.ToString())
            {
                case "Detain ID":
                    _ColumnNameToFilterBy = _PrimaryKeyViewedColumnName;
                    break;

                case "National No.":
                    _ColumnNameToFilterBy = "N.No.";
                    break;

                case "Release Application ID":
                    _ColumnNameToFilterBy = "Release App.ID";
                    break;

                default:
                    _ColumnNameToFilterBy = cbFilterBy.SelectedItem.ToString();
                    break;
            }
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            _SetFilterColumnNameToOriginal();

            DataTable dtDetainLicensesInfo;
            if (!ScrollCase)
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtDetainLicensesInfo = clsDetainedLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _ColumnNameToFilterBy, txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
                    dtDetainLicensesInfo = _GetFilteredDataForIsReleased(false);

                else
                    dtDetainLicensesInfo = clsDetainedLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _ColumnNameToFilterBy, txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), '%');
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtDetainLicensesInfo = clsDetainedLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _ColumnNameToFilterBy, txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtDetainID, _DgvUtilityLib.NumberOfRowsToOffset, null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
                    dtDetainLicensesInfo = _GetFilteredDataForIsReleased(true);

                else
                    dtDetainLicensesInfo = clsDetainedLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _ColumnNameToFilterBy, txtFilter.Text.Trim()
                        , _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtDetainID, _DgvUtilityLib.NumberOfRowsToOffset, '%');
            }

            return dtDetainLicensesInfo;
        }

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtDetainID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvDetainedLicenses, _PrimaryKeyViewedColumnName, _CurrentDGVColumnSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_LastColumnNameDgvSortedBy);

            DataRow[] NewRows = null;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDetainedLicenses, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvDetainedLicenses), _CurrentDGVColumnSortDirection);
            }

            else
            {
                DataTable dtDetainedLicenses = clsDetainedLicense.GetDetainedLicensesInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _LastBroughtDetainID, _LastColumnNameDgvSortedBy
                    , _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

                NewRows = dtDetainedLicenses?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvDetainedLicenses, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvDetainedLicenses), _CurrentDGVColumnSortDirection);
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbIsReleasedSelectedItem == cbIsReleased.SelectedItem.ToString())
                return;

            if (cbFilterBy.Text == "Is Released")
            {
                clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);

                dgvDetainedLicenses.DataSource = _GetFilteredDataForIsReleased(false);
                _PreviousCbIsReleasedSelectedItem = cbIsReleased.SelectedItem.ToString();
            }
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = clsGlobalSettings.ComboBoxItemsBackColor;
        }

        private void cbIsReleased_DropDownClosed(object sender, EventArgs e)
        {
            cbIsReleased.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxBackColor;
        }

        private void dgvDetainedLicenses_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDataGridViewUtilityLib.IsDgvLastRowDisplayed(dgvDetainedLicenses))
                    _AppendPartOfRemainingData();
            }
        }

        private void dgvDetainedLicenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDataGridViewUtilityLib.IsDgvLastRowSelected(dgvDetainedLicenses))
                _AppendPartOfRemainingData();
        }

        private void tsmiShowPersonDetails_Click(object sender, EventArgs e)
        {
            frmPersonInfo frm = new frmPersonInfo(clsPerson.GetPersonID(dgvDetainedLicenses.SelectedRows[0].Cells["N.No."].Value.ToString()), dgvDetainedLicenses.SelectedRows[0].Index);
            frm.AfterUpdatingPersonalInfo += _EditDriverPersonalInfo;
            frm.ShowDialog();
        }

        private void tsmiShowLicenseDetails_Click(object sender, EventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails((int)dgvDetainedLicenses.SelectedRows[0].Cells[_LocalLicensesViewedColumnName].Value);
            frm.ShowDialog();
        }

        private void tsmishowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsPerson.GetPersonID(dgvDetainedLicenses.SelectedRows[0].Cells["N.No."].Value.ToString()));
            frm.OnEditedDriverPersonalInfo += _EditDriverPersonalInfo;
            frm.ShowDialog();
        }

        private void EditRowAfterReleasingLicense(int ReleaseApplicationID, DateTime ReleaseDate, int DetainedLicensesDgvRowIndex)
        {
            _DgvUtilityLib.EditOneColumnValueInDgv<bool>(dgvDetainedLicenses, "Is Released", true, DetainedLicensesDgvRowIndex);
            _DgvUtilityLib.EditOneColumnValueInDgv<string>(dgvDetainedLicenses, "Release Date", ReleaseDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)), DetainedLicensesDgvRowIndex);
            _DgvUtilityLib.EditOneColumnValueInDgv<int>(dgvDetainedLicenses, "Release App.ID", ReleaseApplicationID, DetainedLicensesDgvRowIndex);
        }

        private void tsmiReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells[_LocalLicensesViewedColumnName].Value, (int)dgvDetainedLicenses.SelectedRows[0].Index);
            frm.AfterReleasingSentLicense += EditRowAfterReleasingLicense;
            frm.OnUpdatedDriverPersonalInfo += _EditDriverPersonalInfo;
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsDetainedLicense_Paint(object sender, PaintEventArgs e)
        {
            if (dgvDetainedLicenses.Rows.Count == 0)
                cmsDetainedLicense.Close();

            else if (dgvDetainedLicenses.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one row!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmsDetainedLicense.Close();
            }
            else
            {
                if ((bool)dgvDetainedLicenses.SelectedRows[0].Cells["Is Released"].Value == true)
                {
                    tsmiReleaseDetainedLicense.Enabled = false;
                }
                else
                    tsmiReleaseDetainedLicense.Enabled = true;
            }
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvDetainedLicenses, _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection, e);

            _SetFilterColumnNameToOriginal();

            DataTable dtSortedInfo = null;

            if (txtFilter.Text.Trim() == "" && cbFilterBy.SelectedItem.ToString() != "Is Released")
            {

                dtSortedInfo = clsDetainedLicense.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvDetainedLicenses.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtSortedInfo = clsDetainedLicense.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvDetainedLicenses.Columns[e.ColumnIndex].HeaderText
                    , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _ColumnNameToFilterBy, txtFilter.Text.Trim(), null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
                    dtSortedInfo = clsDetainedLicense.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvDetainedLicenses.Columns[e.ColumnIndex].HeaderText
                    , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _ColumnNameToFilterBy, cbIsReleased.SelectedItem.ToString(), null);

                else
                    dtSortedInfo = clsDetainedLicense.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvDetainedLicenses.Columns[e.ColumnIndex].HeaderText,
                        clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _ColumnNameToFilterBy, txtFilter.Text.Trim(), '%');
            }

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvDetainedLicenses, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvDetainedLicenses_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
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