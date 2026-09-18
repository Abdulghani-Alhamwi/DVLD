using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Licenses;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmDriversManagement : Form
    {
        public frmDriversManagement()
        {
            InitializeComponent();
        }
        bool _AllowDataLoading;
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
                if (cbFilterBy.SelectedItem.ToString() == "Driver ID" || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "Driver ID" || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvDrivers?.Rows[dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Driver ID"].Value, null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvDrivers?.Rows[dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Driver ID"].Value, '%');
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
                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
        }
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "Driver ID" || cbFilterBy.SelectedItem.ToString() == "Person ID")
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
            DataRow[] NewRows;
            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData.Select();

                if (NewRows != null)
                    clsDgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDgvUtilityLib.GetDgvColumnsNames(dgvDrivers));
            }

            else
            {
                NewRows = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, (int)dgvDrivers.Rows[dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value)?.Select();

                if (NewRows != null)
                    clsDgvUtilityLib.AddNewRowsToDgv(dgvDrivers, NewRows, clsDgvUtilityLib.GetDgvColumnsNames(dgvDrivers));
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

        private void _LoadDataAfterFirstTimeLoad(ref bool _AllowDataLoading)
        {
            if (_AllowDataLoading)
            {
                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
            }
            else
                _AllowDataLoading = true;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
                txtFilter.Focus();

                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
            }
            else
            {
                txtFilter.Visible = false;

                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
                _AllowDataLoading = false;
            }
            txtFilter.Text = "";
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
    }
}