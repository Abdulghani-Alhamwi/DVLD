using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace DVLDPresentationLayer
{
    public partial class frmDriversManagements : Form
    {
        public frmDriversManagements()
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

            List<string> lColumnsNames = clsUtility.GetdgvColumnsNames(dgvDrivers, new string[] {"Date Created","Active Licenses"});
            
            for (byte i = 0; i < lColumnsNames.Count; i++)
            {
                Items[i + 1] = lColumnsNames[i];
            }

            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";
        }
        private void _SetCertainControlsPosition()
        {
            dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsUtility.WantedNumOfRowsFromDB);

            if(dgvDrivers.DataSource != null)
            _AddDropDownItems();

            clsUtility.CenterControlHorizontally(this, pbDrivers);
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void _AddFilteredData(DataTable EmptyDataTable, bool ScrollCase = false)
        {
            DataTable dtDriversInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "Driver ID" || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtDriversInfo = clsDriver.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "Driver ID" || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtDriversInfo = clsDriver.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvDrivers?.Rows[dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Driver ID"].Value, null);

                else
                    dtDriversInfo = clsDriver.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvDrivers?.Rows[dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Driver ID"].Value, '%');
            }

            dgvDrivers.DataSource = dtDriversInfo;

            if (dtDriversInfo != null)
            {
                if (EmptyDataTable != null && ScrollCase)
                    EmptyDataTable = (DataTable)dgvDrivers.DataSource;
            }
        }
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text != "")
                _AddFilteredData(null);
            else
                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsUtility.WantedNumOfRowsFromDB);
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
                cbFilterBy.BackColor = clsUtility.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsUtility.ComboBoxBackColor;
        }

        private void cbFilterBy_DropDown(object sender, EventArgs e)
        {
            cbFilterBy.BackColor = clsUtility.ComboBoxItemsBackColor;
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }

        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows;
            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = new DataTable();

                _AddFilteredData(dtFilteredData, true);
                NewRows = dtFilteredData.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvDrivers, (DataTable)dgvDrivers.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvDrivers));
            }

            else
            {
                NewRows = clsDriver.GetDriversInfo(clsUtility.WantedNumOfRowsFromDB, (int)dgvDrivers.Rows[dgvDrivers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvDrivers, (DataTable)dgvDrivers.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvDrivers));
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
                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsUtility.WantedNumOfRowsFromDB);
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

                dgvDrivers.DataSource = clsDriver.GetDriversInfo(clsUtility.WantedNumOfRowsFromDB);
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
    }
}
