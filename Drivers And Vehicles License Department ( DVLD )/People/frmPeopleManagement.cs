using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib; 

namespace DVLDPresentationLayer
{
    public partial class frmPeopleManagement : Form
    {
        public frmPeopleManagement()
        {
            InitializeComponent();
        }
        private void _AddDropDownItems()
        {
            object[] Items = new object[dgvPeople.Columns.Count];
            Items[0] = "None";

            List<string> lColumnsNames = clsUtility.GetdgvColumnsNames(dgvPeople,"Date Of Birth");

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
        bool _AllowDataLoading;
        private void frmPeopleManagement_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = clsPerson.GetPeopleInfo(clsUtility.WantedNumOfRowsFromDB);
            
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
        private void _LoadDataAfterFirstTimeLoad(ref bool _AllowDataLoading)
        {
            if (_AllowDataLoading)
                dgvPeople.DataSource = clsPerson.GetPeopleInfo(clsUtility.WantedNumOfRowsFromDB);
            else
                _AllowDataLoading = true;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
                txtFilter.Focus();
                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
            }
            else
            {
                txtFilter.Visible = false;

                dgvPeople.DataSource = clsPerson.GetPeopleInfo(clsUtility.WantedNumOfRowsFromDB);
                _AllowDataLoading = false;
            }
                txtFilter.Text = "";
        }

        private void _AddFilteredData(DataTable EmptyDataTable, bool ScrollCase = false)
        {
            DataTable dtPeopleInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "Person ID" || cbFilterBy.SelectedItem.ToString() == "Phone")
                    dtPeopleInfo = clsPerson.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text);

                else
                    dtPeopleInfo = clsPerson.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "Person ID" || cbFilterBy.SelectedItem.ToString() == "Phone")
                    dtPeopleInfo = clsPerson.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvPeople?.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value, null);

                else
                    dtPeopleInfo = clsPerson.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvPeople?.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value, '%');
            }

                dgvPeople.DataSource = dtPeopleInfo;

            if (dtPeopleInfo != null)
            {
                if (EmptyDataTable != null && ScrollCase)
                    EmptyDataTable = (DataTable)dgvPeople.DataSource;
            }
        }
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text != "")
                _AddFilteredData(null);
            else
                dgvPeople.DataSource = clsPerson.GetPeopleInfo(clsUtility.WantedNumOfRowsFromDB);
        }
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "Person ID" || cbFilterBy.SelectedItem.ToString() == "Phone")
            {
                if (Char.IsDigit((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else if (  cbFilterBy.SelectedItem.ToString() == "First Name"    || cbFilterBy.SelectedItem.ToString() == "Second Name"
                    || cbFilterBy.SelectedItem.ToString() == "Third Name"  || cbFilterBy.SelectedItem.ToString() == "Last Name"
                    || cbFilterBy.SelectedItem.ToString() == "Nationality" || cbFilterBy.SelectedItem.ToString() == "Gendor")
            {
                if (Char.IsLetter((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else
                txtFilter.ReadOnly = false;
        }                     
        private void _AddNewRowToDGV(ref object[] NewValues)
        {
            if (dgvPeople.DataSource == null)
                dgvPeople.DataSource = clsPerson.GetColumnsNamesForView();

            clsUtility.AddNewRowToDGV(dgvPeople,(DataTable)dgvPeople.DataSource,ref NewValues,"Person ID");
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
                 clsUtility.DeleteSelectedRowsFromView(dgvPeople,SelectedRowsIndex);
                 lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) - TotalDeletedRecords).ToString();
            }
        }

        private void _EditDataRowInDGV(ref object[] NewValues, int RowIndex)
        {
            clsUtility.EditFullDataRowInDGV(dgvPeople, (DataTable)dgvPeople.DataSource,ref NewValues, RowIndex);
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
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo(PersonInfo,Convert.ToInt16(dgvPeople.SelectedRows[0].Index));
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

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
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
                DataTable dtFilteredData = new DataTable();

                _AddFilteredData(dtFilteredData, true);
                NewRows = dtFilteredData.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvPeople, (DataTable)dgvPeople.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvPeople));
            }

            else
            {
                 NewRows = clsPerson.GetPeopleInfo(clsUtility.WantedNumOfRowsFromDB, (int)dgvPeople.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvPeople, (DataTable)dgvPeople.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvPeople));
            }
        }

        private void dgvPeople_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (dgvPeople.Rows.GetLastRow(DataGridViewElementStates.None) == dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                    _AppendPartOfRemainingData();
            }
        }
        private void dgvPeople_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvPeople.Rows.GetLastRow(DataGridViewElementStates.None) == dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Selected))
                _AppendPartOfRemainingData();
        }

        private void cmsPeopleMenu_Paint(object sender, PaintEventArgs e)
        {
            if (dgvPeople.SelectedRows.Count == 0)
                cmsPeopleMenu.Close();
        }
    }
}
