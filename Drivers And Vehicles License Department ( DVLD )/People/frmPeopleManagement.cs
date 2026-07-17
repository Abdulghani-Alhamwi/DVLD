using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MyLib; 
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmPeopleManagement : Form
    {
        public frmPeopleManagement()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _AddDropDownItems(object[] Items)
        {
            cbFilterBy.Items.AddRange(Items);
        }
        private DataView _dataview;
        private void frmPeopleManagement_Load(object sender, EventArgs e)
        {
            DataTable dtPeople = clsPerson.GetAllPeopleBasicInfo();

            if (dtPeople != null)
            {
                _dataview = dtPeople.DefaultView;
                dgvPeople.Font = new Font("Tahoma", 15.5f);
                dgvPeople.DataSource = _dataview;
            }

            object[] Items = new object[] { "None", "Person ID", "National No.", "First Name", "Second Name", "Third Name", "Last Name", "Nationality", "Gendor", "Phone", "Email" };
            _AddDropDownItems(Items);
            cbFilterBy.SelectedItem = 0;

            lblRecordsNumber.Text = dgvPeople.Rows.Count.ToString();

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
                txtFilter.Visible = false;
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            clsUtility._FilterDataView(_dataview,cbFilterBy.SelectedItem.ToString(), txtFilter.Text,e);
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

        private void _RefreshPeopleDataView()
        {
            dgvPeople.DataSource = null;
            _dataview = clsPerson.GetAllPeopleBasicInfo().DefaultView;
            dgvPeople.DataSource = _dataview;
        }

        private void _AddNewPersonScreen()
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.AddEditPersonData += _RefreshPeopleDataView;

            frm.ShowDialog();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _AddNewPersonScreen();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvPeople.Rows.Count == 0)
            {
                MessageBox.Show("There are no users to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvPeople.SelectedRows.Count > 0)
            {
                for (short i = 0; i < dgvPeople.SelectedRows.Count; i++)
                {
                clsPerson.DeletePerson(Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["Person ID"].Value));
                }
                _RefreshPeopleDataView();   
            }

            MessageBox.Show("No people selected to delete.\nPlease select the people you want to delete first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count == 0)
                {
                MessageBox.Show("No Selected Person To Edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                }

            clsPerson PersonInfo = clsPerson.Find((int) dgvPeople.SelectedRows[0].Cells["Person ID"].Value);

            if (PersonInfo != null)
            {
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo(PersonInfo);
                frm.AddEditPersonData += _RefreshPeopleDataView;
                frm.ShowDialog();
            }
            else
                MessageBox.Show($"Person is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                frmPersonDetails frm = new frmPersonDetails((int) dgvPeople.SelectedRows[0].Cells["Person ID"].Value);
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

    }
}
