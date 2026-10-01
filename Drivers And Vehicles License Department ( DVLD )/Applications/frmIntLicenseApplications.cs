using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Applications;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmIntLicenseApplications : Form
    {
        private string _PreviousCbFilterSelectedItem;
        private string _PreviousCbIsActiveSelectedItem;
        private string _LastColumnNameDgvSortedBy;
        private string _PrimaryKeyViewedColumnName;
        private int _LastBroughtIntLicenseID;

        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDGVColumnSortDirection;
        private clsDataGridViewUtilityLib _DgvUtilityLib;

        public frmIntLicenseApplications()
        {
            InitializeComponent();

            _LastBroughtIntLicenseID = -1;
            _PrimaryKeyViewedColumnName = "Int.License ID"; 

            _CurrentDGVColumnSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
            _DgvUtilityLib = new clsDataGridViewUtilityLib();
        }

        private void frmInternationalLicensesManagement_Load(object sender, EventArgs e)
        {
            dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetAllInternationalLicensesData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);

            if (dgvIntLicenseApplications.DataSource != null)
                _AddComboBoxesFilterItems();

            lblRecordsNumber.Text = clsInternationalLicense.GetTotalCount().ToString();
        }

        private void _AddComboBoxesFilterItems()
        {
            object[] Items = new object[dgvIntLicenseApplications.Columns.Count - 1];
            Items[0] = "None";

            List<string> ldgvColumnsNames = clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvIntLicenseApplications, new string[] { "Issue Date", "Expiration Date" });

            for (byte i = 0; i < ldgvColumnsNames.Count; i++)
            {
                Items[i + 1] = ldgvColumnsNames[i];
            }

            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";

            object[] cbIsActive = new object[] { "All", "Yes", "No" };
            this.cbIsActive.DataSource = cbIsActive;
            this.cbIsActive.SelectedItem = "All";
        }

        private void DrawComboBoxItems(object sender, DrawItemEventArgs e)
        {
            clsGeneralUtility.DrawComboBoxItems((ComboBox)sender, e);
        }

        private DataTable _GetFilteredDataOnIsActive(bool ScrollCase = false)
        {
            DataTable dtFilteredData;

            if (!ScrollCase)
                dtFilteredData = clsInternationalLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(),
                   _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), null);

            else
                dtFilteredData = clsInternationalLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(),
               _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection),_LastBroughtIntLicenseID, _DgvUtilityLib.NumberOfRowsToOffset, null);

            return dtFilteredData;
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtFilteredInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() != "Is Active" && cbFilterBy.SelectedItem.ToString() != "None") 
                {
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), null);
                }

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtFilteredInfo = _GetFilteredDataOnIsActive(false);

                else
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() != "Is Active" && cbFilterBy.SelectedItem.ToString() != "None")
                {
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtIntLicenseID, _DgvUtilityLib.NumberOfRowsToOffset, null);
                }

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtFilteredInfo = _GetFilteredDataOnIsActive(true);

                else
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), _LastBroughtIntLicenseID, _DgvUtilityLib.NumberOfRowsToOffset, '%');
            }

            if (dtFilteredInfo != null)
            {
                return dtFilteredInfo;
            }
            else
                return null;
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);

            if (txtFilter.Text != "")
                dgvIntLicenseApplications.DataSource = _GetFilteredData();

            else
                dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetAllInternationalLicensesData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if ((_PreviousCbFilterSelectedItem == "Is Active" && cbIsActive.SelectedItem.ToString() != "All")
                || !clsDataGridViewUtilityLib._IsRepeatedDataLoadToDgv(txtFilter))
            {
                dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetAllInternationalLicensesData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
            }

            if (_PreviousCbFilterSelectedItem == "Is Active")
                cbIsActive.SelectedItem = "All";

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = false;
                _PreviousCbIsActiveSelectedItem = null;
            }

            else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;
            }

            else
            {
                txtFilter.Visible = true;
                cbIsActive.Visible = false;
                _PreviousCbIsActiveSelectedItem = null;
            }

            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.IsDigit((char)e.KeyData) || e.KeyData == Keys.Back)
                txtFilter.ReadOnly = false;

            else
                txtFilter.ReadOnly = true;            
        }

        private void _AddNewValuesToDGV(object[] NewValues)
        {
            if (dgvIntLicenseApplications.DataSource == null)
                dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetColumnsNamesForView();

            _DgvUtilityLib.AddNewRowToDGV(dgvIntLicenseApplications,NewValues, dgvIntLicenseApplications.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void btnAddIntLicenseApplication_Click(object sender, EventArgs e)
        {
            frmNewIntLicenseApplication frm = new frmNewIntLicenseApplication();
            frm.OnIssuedLicense += _AddNewValuesToDGV;
            frm.ShowDialog();
        }

        private void cmsInternationalLicense_Paint(object sender, PaintEventArgs e)
        {
            if (dgvIntLicenseApplications.Rows.Count == 0)
                cmsInternationalLicense.Close();
        }       

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtIntLicenseID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvIntLicenseApplications, _PrimaryKeyViewedColumnName, _CurrentDGVColumnSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_PrimaryKeyViewedColumnName, _LastColumnNameDgvSortedBy);

            DataRow[] NewRows;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvIntLicenseApplications, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvIntLicenseApplications), _CurrentDGVColumnSortDirection);
            }

            else
            {
                DataTable dtInternationalLicenses = clsInternationalLicense.GetAllInternationalLicensesData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _LastBroughtIntLicenseID
                    , _LastColumnNameDgvSortedBy, _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));

                NewRows = dtInternationalLicenses?.Select();

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvIntLicenseApplications, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvIntLicenseApplications), _CurrentDGVColumnSortDirection);
            }
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = clsGlobalSettings.ComboBoxItemsBackColor;
        }

        private void cbIsActive_DropDownClosed(object sender, EventArgs e)
        {
            cbIsActive.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxBackColor;
        }

        private void tsmiShowLicenseDetails_Click(object sender, EventArgs e)
        {
            if (dgvIntLicenseApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You have to select only one international driving license application in order to show license info.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmInternationalLicenseDetails frm = new frmInternationalLicenseDetails(clsInternationalLicense.GetLicenseID((int)dgvIntLicenseApplications.SelectedRows[0].Cells["Application ID"].Value));
            frm.ShowDialog();
        }

        private void tsmiShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            if (dgvIntLicenseApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You have to select only one international driving license application in order to show driver license history.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsDriver.GetDriverPersonID((int)dgvIntLicenseApplications.SelectedRows[0].Cells["Driver ID"].Value));
            frm.ShowDialog();
        }

        private void dgvInternationalLicenses_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDataGridViewUtilityLib.IsDgvLastRowDisplayed(dgvIntLicenseApplications))
                    _AppendPartOfRemainingData();
            }
        }

        private void dgvInternationalLicenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDataGridViewUtilityLib.IsDgvLastRowSelected(dgvIntLicenseApplications))
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

        private void tsmiShowPersonDetails_Click(object sender, EventArgs e)
        {
            if (dgvIntLicenseApplications.SelectedRows.Count > 1)
            {
                MessageBox.Show("You have to select only one international driving license application in order to show person details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmPersonDetails frm = new frmPersonDetails(clsDriver.GetDriverPersonID((int)dgvIntLicenseApplications.SelectedRows[0].Cells["Driver ID"].Value));
            frm.ShowDialog();
        }

        private bool _IsNumericColumn(string ColumnName)
        {
            switch (ColumnName)
            {
                case "Int.License ID":
                case "Application ID":
                case "Driver ID":
                case "L.License ID":
                    return true;
            }
            return false;
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbIsActiveSelectedItem == cbIsActive.SelectedItem.ToString())
                return;

            if (cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection);

                dgvIntLicenseApplications.DataSource = _GetFilteredDataOnIsActive(false);
                _PreviousCbIsActiveSelectedItem = cbIsActive.SelectedItem.ToString();
            }
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvIntLicenseApplications, ref _LastColumnNameDgvSortedBy, ref _CurrentDGVColumnSortDirection, e);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text == "" && cbFilterBy.SelectedItem.ToString() != "Is Active")
            {

                dtSortedInfo = clsInternationalLicense.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvIntLicenseApplications.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsInternationalLicense.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvIntLicenseApplications.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);
                }

                else
                {
                    dtSortedInfo = clsInternationalLicense.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvIntLicenseApplications.Columns[e.ColumnIndex].HeaderText
            , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDGVColumnSortDirection), cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(), null);
                }
            }

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvIntLicenseApplications, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvIntLicenseApplications_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           _SortData(e);
        }
    }
}