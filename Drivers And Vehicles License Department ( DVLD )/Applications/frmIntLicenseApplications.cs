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
        string _PreviousCbFilterSelectedItem;
        string _PreviousCbIsActiveSelectedItem;
        private string _LastColumnNameDgvSortedBy;
        private clsUtility.enDataGridViewSortDirection _CurrentDgvSortDirection;
        private clsUtility _UtilityLib;
        public frmIntLicenseApplications()
        {
            InitializeComponent();
            _CurrentDgvSortDirection = clsUtility.enDataGridViewSortDirection.Descending;
            _UtilityLib = new clsUtility();
        }
        private void frmInternationalLicensesManagement_Load(object sender, EventArgs e)
        {
            dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetAllInternationalLicenses(clsUtility.WantedNumOfRowsFromDB);

            if (dgvIntLicenseApplications.DataSource != null)
                _AddComboBoxesFilterItems();

            lblRecordsNumber.Text = clsInternationalLicense.GetTotalCount().ToString();
        }
        private void _AddComboBoxesFilterItems()
        {
            object[] Items = new object[dgvIntLicenseApplications.Columns.Count - 1];
            Items[0] = "None";

            List<string> ldgvColumnsNames = clsUtility.GetDgvColumnsNames(dgvIntLicenseApplications, new string[] { "Issue Date", "Expiration Date" });

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
            clsUtility.DrawComboBoxItems((ComboBox)sender, e);
        }

        private DataTable _GetFilteredDataOnIsActive(bool ScrollCase = false)
        {
            DataTable dtFilteredData;

            if (!ScrollCase)
                dtFilteredData = clsInternationalLicense.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(),
                   _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), null);

            else
                dtFilteredData = clsInternationalLicense.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(),
               _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection),
               (int)dgvIntLicenseApplications?.Rows[dgvIntLicenseApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Int.License ID"].Value, null);

            return dtFilteredData;
        }
        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtFilteredInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() != "Is Active" && cbFilterBy.SelectedItem.ToString() != "None") 
                {
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), null);
                }

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtFilteredInfo = _GetFilteredDataOnIsActive(false);

                else
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() != "Is Active" && cbFilterBy.SelectedItem.ToString() != "None")
                {
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), (int)dgvIntLicenseApplications?.Rows[dgvIntLicenseApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Int.License ID"].Value, null);
                }

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtFilteredInfo = _GetFilteredDataOnIsActive(true);

                else
                    dtFilteredInfo = clsInternationalLicense.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), (int)dgvIntLicenseApplications?.Rows[dgvIntLicenseApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Int.License ID"].Value, '%');
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
            if(txtFilter.Text != "")
                dgvIntLicenseApplications.DataSource = _GetFilteredData();

            else
                dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetAllInternationalLicenses(clsUtility.WantedNumOfRowsFromDB);
        }
        private void _LoadDataAfterFirstTimeLoad(ref bool _AllowDataLoading)
        {
            if (_AllowDataLoading)
                dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetAllInternationalLicenses(clsUtility.WantedNumOfRowsFromDB);
            else
                _AllowDataLoading = true;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if ((_PreviousCbFilterSelectedItem == "Is Active" && cbIsActive.SelectedItem.ToString() != "All")
                || !clsUtility._IsRepeatedDataLoadToDgv(txtFilter))
            {
                dgvIntLicenseApplications.DataSource = clsInternationalLicense.GetAllInternationalLicenses(clsUtility.WantedNumOfRowsFromDB);
            }

            if (_PreviousCbFilterSelectedItem == "Is Active")
                cbIsActive.SelectedItem = "All";

            if (cbFilterBy.SelectedItem.ToString() != "None" && cbFilterBy.SelectedItem.ToString() != "Is Active")
            {
                txtFilter.Visible = true;
                cbIsActive.Visible = false;
            }

            else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;
            }

            else
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = false;
            }
            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";
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

            _UtilityLib.AddNewRowToDGV(dgvIntLicenseApplications,NewValues, dgvIntLicenseApplications.Columns[0].HeaderText);
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
            DataRow[] NewRows;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvIntLicenseApplications, NewRows, clsUtility.GetDgvColumnsNames(dgvIntLicenseApplications));
            }

            else
            {
                NewRows = clsInternationalLicense.GetAllInternationalLicenses(clsUtility.WantedNumOfRowsFromDB, (int)dgvIntLicenseApplications.Rows[dgvIntLicenseApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Int.License ID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvIntLicenseApplications, NewRows, clsUtility.GetDgvColumnsNames(dgvIntLicenseApplications));
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
                if (clsUtility.IsDgvLastRowDisplayed(dgvIntLicenseApplications))
                    _AppendPartOfRemainingData();
            }
        }
        private void dgvInternationalLicenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsUtility.IsDgvLastRowSelected(dgvIntLicenseApplications))
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
                dgvIntLicenseApplications.DataSource = _GetFilteredDataOnIsActive(false);
                _PreviousCbIsActiveSelectedItem = cbIsActive.SelectedItem.ToString();
            }
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            _CurrentDgvSortDirection = clsUtility.ReverseCurrentDgvSortDirection(_CurrentDgvSortDirection);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text == "" && cbFilterBy.SelectedItem.ToString() != "Is Active")
            {

                dtSortedInfo = clsInternationalLicense.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvIntLicenseApplications.Columns[e.ColumnIndex].HeaderText
                , clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsInternationalLicense.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvIntLicenseApplications.Columns[e.ColumnIndex].HeaderText
                , clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);
                }

                else
                {
                    dtSortedInfo = clsInternationalLicense.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvIntLicenseApplications.Columns[e.ColumnIndex].HeaderText
            , clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(), null);
                }
            }

            _UtilityLib.ModifyDataAfterUserOrdersColumn(dgvIntLicenseApplications, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvIntLicenseApplications_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           _SortData(e);
        }
    }
}
