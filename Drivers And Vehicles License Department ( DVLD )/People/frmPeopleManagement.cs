using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib; 

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
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

            List<string> ldgvColumnsNames = clsUtility.GetdgvColumnsNames(dgvPeople,"Date Of Birth");

            for (byte i = 0; i < ldgvColumnsNames.Count; i++)
            {
                Items[i + 1] = ldgvColumnsNames[i];
            }
            
            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool PreventLoadingDataAgain = false;
        private void frmPeopleManagement_Load(object sender, EventArgs e)
        {
            DataTable dtPeople = clsPerson.GetAllPeopleInfo(100);
            
            if (dtPeople != null)
            {
                dgvPeople.Font = new Font("Tahoma", 15.5f);
                dgvPeople.DataSource = dtPeople;
                PreventLoadingDataAgain = true;
            }
             _AddDropDownItems();
            PreventLoadingDataAgain = false;

            lblRecordsNumber.Text = clsPerson.GetTotalNumberOfPeople().ToString();
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = Color.FromArgb(221, 232, 240);
            else
                cbFilterBy.BackColor = Color.FromArgb(228, 228, 228);
        }

        private void cbFilterBy_DropDown(object sender, EventArgs e)
        {
            cbFilterBy.BackColor = Color.FromArgb(245, 245, 245);
        }
        
        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem.ToString() != "None")
            {
                txtFilter.Visible = true;
                txtFilter.Focus();
            }
            else
            {
                txtFilter.Visible = false;
                txtFilter.Text = "";

                if(!PreventLoadingDataAgain)
                dgvPeople.DataSource = clsPerson.GetAllPeopleInfo(100);
            }
        }

        private string _GetColumnNameToFilter()
        {
            if (cbFilterBy.SelectedItem.ToString() == "Gendor" || cbFilterBy.SelectedItem.ToString() == "Phone"
            || cbFilterBy.SelectedItem.ToString() == "Email" || cbFilterBy.SelectedItem.ToString() == "Nationality")
                return cbFilterBy.SelectedItem.ToString();

            else
            {
                switch(cbFilterBy.SelectedItem)
                {
                    case "Person ID":
                        return "PersonID";

                    case "National No.":
                        return "NationalNo"; 

                    case "First Name":
                        return "FirstName";

                    case "Second Name":
                        return "SecondName";

                    case "Third Name":
                        return "ThirdName";

                    case "Last Name":
                        return "LastName";
                }
            }
            return null;
        }

        private void _AddFilteredData(DataTable EmptyDataTable, bool ScrollCase = false)
        {
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "Person ID" || cbFilterBy.SelectedItem.ToString() == "Phone")
                    dgvPeople.DataSource = clsPerson.GetFilteredData(100, _GetColumnNameToFilter(), txtFilter.Text, null);

                else
                    dgvPeople.DataSource = clsPerson.GetFilteredData(100, _GetColumnNameToFilter(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "Person ID" || cbFilterBy.SelectedItem.ToString() == "Phone")
                    dgvPeople.DataSource = clsPerson.GetFilteredData(100, _GetColumnNameToFilter(), txtFilter.Text, null, (int)dgvPeople?.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value);

                else
                    dgvPeople.DataSource = clsPerson.GetFilteredData(100, _GetColumnNameToFilter(), txtFilter.Text, '%', (int)dgvPeople?.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value);
            }

            if (EmptyDataTable != null && ScrollCase)
                EmptyDataTable = (DataTable)dgvPeople.DataSource;
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text != "")
                _AddFilteredData(null);
            else
                dgvPeople.DataSource = clsPerson.GetAllPeopleInfo(100);
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
            clsUtility.AddNewRowToDGV(dgvPeople,(DataTable)dgvPeople.DataSource,ref NewValues,"Person ID");
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }
        private void _AddNewPersonScreen()
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.AfterSavingNewPersonInfo += _AddNewRowToDGV;

            frm.ShowDialog();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _AddNewPersonScreen();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvPeople.Rows.Count == 0)
                MessageBox.Show("There is'nt any person to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
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
                    for (byte i = 0; i < dgvPeople.SelectedRows.Count; i++)
                    {
                        if (!clsPerson.DeletePerson(Convert.ToInt32(dgvPeople.SelectedRows[i].Cells["Person ID"].Value)))
                        {
                            MessageBox.Show($"Person who has ID : {Convert.ToInt32(dgvPeople.SelectedRows[i].Cells["Person ID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SelectedRowsIndex[i] = -1;
                        }
                        else
                            SelectedRowsIndex[i] = dgvPeople.SelectedRows[i].Index;
                    }
                    clsUtility.DeleteSelectedRowsFromView(dgvPeople,SelectedRowsIndex);
                    lblRecordsNumber.Text = clsPerson.GetTotalNumberOfPeople().ToString();
                }
            }
        }

        private void _EditDataRowInDGV(ref object[] NewValues, int RowIndex)
        {
            clsUtility.EditFullDataRowInDGV(dgvPeople, (DataTable)dgvPeople.DataSource,ref NewValues, RowIndex);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count == 0)
                {
                MessageBox.Show("No selected person to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                }

            if(dgvPeople.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please select only one person to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            clsPerson PersonInfo = clsPerson.Find((int) dgvPeople.SelectedRows[0].Cells["Person ID"].Value);

            if (PersonInfo != null)
            {
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo(PersonInfo,dgvPeople.SelectedRows[0].Index);
                frm.AfterSavingEditedPersonInfo += _EditDataRowInDGV;
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
            else if(dgvPeople.SelectedRows.Count > 1)
                MessageBox.Show("You must select a person first to show their details , and you can view only one person details!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("There is'nt any person to show their details!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void _AppendRemainingDataForFilterCase()
        {
            DataRow[] NewRows;
            DataTable dtFilteredData = new DataTable();

            _AddFilteredData(dtFilteredData,true);
            NewRows = dtFilteredData.Select();

            if (NewRows != null)
                clsUtility.AddNewRowsToDGV(dgvPeople, (DataTable)dgvPeople.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvPeople));
        }

        private void _AppendRemainingData()
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                _AppendRemainingDataForFilterCase();

            else
            {
                DataRow[] NewRows = clsPerson.GetAllPeopleInfo(100, (int)dgvPeople.Rows[dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Person ID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvPeople, (DataTable)dgvPeople.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvPeople));
            }
        }

        private void _AppendPartOfRemainingDataAfterReachingLastRow(bool ScrollCase)
        {
            if (ScrollCase)
            {
                if (dgvPeople.Rows.GetLastRow(DataGridViewElementStates.None) == dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                {
                    _AppendRemainingData();
                }
            }
            else
            {
                if (dgvPeople.Rows.GetLastRow(DataGridViewElementStates.None) == dgvPeople.Rows.GetLastRow(DataGridViewElementStates.Selected))
                {
                    _AppendRemainingData();
                }
            }
        }
        private void dgvPeople_Scroll(object sender, ScrollEventArgs e)
        {
            _AppendPartOfRemainingDataAfterReachingLastRow(true);
        }
        private void dgvPeople_KeyDown(object sender, KeyEventArgs e)
        {
            _AppendPartOfRemainingDataAfterReachingLastRow(false);
        }
    }
}
