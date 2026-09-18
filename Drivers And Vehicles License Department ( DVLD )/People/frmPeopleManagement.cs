using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library; 

namespace DVLDPresentationLayer
{
    public partial class frmPeopleManagement : Form
    {
        private string _LastColumnNameDgvSortedBy;
        string _PreviousCbFilterSelectedItem;
        private clsDgvUtilityLib.enDataGridViewSortDirection _CurrentDgvSortDirection;
        private clsDgvUtilityLib _DgvUtilityLib;
        public frmPeopleManagement()
        {
            InitializeComponent();
            _CurrentDgvSortDirection = clsDgvUtilityLib.enDataGridViewSortDirection.Descending;
            _DgvUtilityLib = new clsDgvUtilityLib();
        }
        private void _AddDropDownItems()
        {
            object[] Items = new object[dgvPeople.Columns.Count];
            Items[0] = "None";

            List<string> lColumnsNames = clsDgvUtilityLib.GetDgvColumnsNames(dgvPeople,"Date Of Birth");

            for (byte i = 0; i < lColumnsNames.Count; i++)
            {
                Items[i + 1] = lColumnsNames[i];
            }
            
            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPeopleManagement_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = clsPerson.GetPeopleInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
            
            if (dgvPeople.DataSource != null)
            {
                dgvPeople.Font = new Font("Tahoma", 15.5f);
                _AddDropDownItems();
            }

            lblRecordsNumber.Text = clsPerson.GetTotalPeopleCount().ToString();
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

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if(!clsDgvUtilityLib._IsRepeatedDataLoadToDgv(txtFilter))
            dgvPeople.DataSource = clsPerson.GetPeopleInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);

            if (((ComboBox)sender).SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
                txtFilter.Focus();
            }
            else
            {
                txtFilter.Visible = false;
            }
            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";
        }

        private bool _IsNumericColumn(string ColumnNameToFilterBy)
        {
            return (ColumnNameToFilterBy == "Person ID" || ColumnNameToFilterBy == "Phone");
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtPeopleInfo;

            if (!ScrollCase)
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtPeopleInfo = clsPerson.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection));

                else
                    dtPeopleInfo = clsPerson.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                       _LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), '%');
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtPeopleInfo = clsPerson.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                       _LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), (int)dgvPeople?.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value, null);

                else
                    dtPeopleInfo = clsPerson.GetFilteredData(clsDgvUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                       _LastColumnNameDgvSortedBy, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), (int)dgvPeople?.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value, '%');
            }

            if (dtPeopleInfo != null)
            {
                return dtPeopleInfo;
            }

            else
                return null;
        }
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text != "")
                dgvPeople.DataSource = _GetFilteredData();
            else
                dgvPeople.DataSource = clsPerson.GetPeopleInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB);
        }
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
             if (cbFilterBy.SelectedItem.ToString() == "National No.")
                txtFilter.ReadOnly = false;

            else if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
            {
                if (Char.IsDigit((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else
            {
                if (Char.IsLetter((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
        }    
        
        private void _AddNewRowToDGV(object[] NewPersonDetails)
        {
            if (dgvPeople.DataSource == null)
                dgvPeople.DataSource = clsPerson.GetColumnsNamesForView();

            _DgvUtilityLib.AddNewRowToDGV(dgvPeople,NewPersonDetails, dgvPeople.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }
        private void _AddNewPersonScreen()
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.AfterSavingNewInfo += _AddNewRowToDGV;

            frm.ShowDialog();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _AddNewPersonScreen();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
             if (dgvPeople.SelectedRows.Count > 5)
             {
                 MessageBox.Show("You Can Delete Maximum 5 People In One Time!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 return;
             }

             DialogResult result;
             if (dgvPeople.SelectedRows.Count == 1)
                 result = MessageBox.Show("Are you sure you want to delete this person?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
             else
                 result = MessageBox.Show("Are you sure you want to delete those people?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

             if (result == DialogResult.OK)
             {
                 int[] SelectedRowsIndex = new int[dgvPeople.SelectedRows.Count];
                 byte TotalDeletedRecords = 0;
                for (byte i = 0; i < dgvPeople.SelectedRows.Count; i++)
                 {
                     if (!clsPerson.DeletePerson(Convert.ToInt32(dgvPeople.SelectedRows[i].Cells["Person ID"].Value)))
                     {
                         MessageBox.Show($"Person who has ID : {Convert.ToInt32(dgvPeople.SelectedRows[i].Cells["Person ID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         SelectedRowsIndex[i] = -1;
                     }
                     else
                    {
                        SelectedRowsIndex[i] = dgvPeople.SelectedRows[i].Index;
                        TotalDeletedRecords++;
                    }
                 }
                _DgvUtilityLib.DeleteSelectedDgvRows(dgvPeople,SelectedRowsIndex);
                 lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) - TotalDeletedRecords).ToString();
            }
        }

        private void _EditDataRowInDGV(object[] ModifiedPersonDetails, int PeopleDgvRowIndex)
        {
            _DgvUtilityLib.EditFullDataRowInDgv(dgvPeople, ModifiedPersonDetails, PeopleDgvRowIndex);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if(dgvPeople.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please select only one person to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            clsPerson PersonInfo = clsPerson.Find((int) dgvPeople.SelectedRows[0].Cells["Person ID"].Value);

            if (PersonInfo != null)
            {
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo(PersonInfo,Convert.ToInt32(dgvPeople.SelectedRows[0].Index));
                frm.AfterSavingEditedInfo += _EditDataRowInDGV;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Person is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsmiAddNewPerson_Click(object sender, EventArgs e)
        {
            _AddNewPersonScreen();
        }

        private void tsmiSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsmiPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void _ShowPersonDetails()
        {
            if (dgvPeople.SelectedRows.Count == 1)
            {
                frmPersonDetails frm = new frmPersonDetails((int)dgvPeople.SelectedRows[0].Cells[0].Value);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("You must select a person first to show their details , and you can view only one person details!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            _ShowPersonDetails();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows;
            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    clsDgvUtilityLib.AddNewRowsToDgv(dgvPeople, NewRows, clsDgvUtilityLib.GetDgvColumnsNames(dgvPeople));
            }

            else
            {
                 NewRows = clsPerson.GetPeopleInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, (int)dgvPeople.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value)?.Select();

                if (NewRows != null)
                    clsDgvUtilityLib.AddNewRowsToDgv(dgvPeople, NewRows, clsDgvUtilityLib.GetDgvColumnsNames(dgvPeople));
            }
        }

        private void dgvPeople_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDgvUtilityLib.IsDgvLastRowDisplayed(dgvPeople))
                    _AppendPartOfRemainingData();
            }
        }
        private void dgvPeople_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDgvUtilityLib.IsDgvLastRowSelected(dgvPeople))
                _AppendPartOfRemainingData();
        }

        private void cmsPeopleMenu_Paint(object sender, PaintEventArgs e)
        {
            if (dgvPeople.SelectedRows.Count == 0)
                cmsPeopleMenu.Close();
        }

        private void dgvPeople_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!clsDgvUtilityLib.WasDgvColumnHeaderClicked(dgvPeople, e))
                _ShowPersonDetails();
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            _CurrentDgvSortDirection = clsDgvUtilityLib.ReverseCurrentDgvSortDirection(_CurrentDgvSortDirection);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text == "")
            {
                dtSortedInfo = clsPerson.GetSortedInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvPeople.Columns[e.ColumnIndex].HeaderText
                    , clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsPerson.GetSortedInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvPeople.Columns[e.ColumnIndex].HeaderText
                      , clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);
                }

                else
                {
                    dtSortedInfo = clsPerson.GetSortedInfo(clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvPeople.Columns[e.ColumnIndex].HeaderText, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDgvSortDirection),
                    cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
                }
            }

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvPeople, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
        }

        private void dgvPeople_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }

    }
}