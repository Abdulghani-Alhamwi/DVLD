using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmPeopleManagement : Form
    {
        public frmPeopleManagement()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
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
            dgvPeople.Font = new Font("Calibri", 16);
            _dataview = clsPeople.GetAllPeopleBasicInfo().DefaultView;
            dgvPeople.DataSource = _dataview;
            cbFilterBy.ForeColor = Color.FromArgb(15, 15, 15);

            object[] Items = new object[] { "None", "Person ID", "National No.", "First Name", "Second Name", "Third Name", "Last Name", "Nationality", "Gendor", "Phone", "Email" };
            _AddDropDownItems(Items);
            cbFilterBy.SelectedItem = Items[0];

            lblRecordsNumber.Text = dgvPeople.Rows.Count.ToString();

        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            cbFilterBy.BackColor = Color.FromArgb(221, 232, 240);
        }

        private void cbFilterBy_DropDown(object sender, EventArgs e)
        {
            cbFilterBy.BackColor = Color.FromArgb(245, 245, 245);
        }
        
        internal static void DrawComboBoxItems(object sender , DrawItemEventArgs e,string ColumnName =null)
        {
            if (e.Index < 0)
                return;

            e.DrawBackground();

            string ItemText;

            if (ColumnName != null)
            {
                DataRowView RowView = (DataRowView)((ComboBox)sender).Items[e.Index];
                ItemText = RowView[ColumnName].ToString();
            }
            else
                ItemText = ((ComboBox)sender).Items[e.Index].ToString();

                using (SolidBrush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(ItemText, e.Font, brush, e.Bounds);
                }
            e.DrawFocusRectangle();
        }
        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            DrawComboBoxItems(sender, e);
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

        private void _FilterDataView(string ColumnName, string FilterOnValue)
        {
            if (_dataview.Table.Rows.Count == 0)
                return;

            if(FilterOnValue.All(Char.IsLetter))
            _dataview.RowFilter = $"[{ColumnName}] LIKE '{FilterOnValue}%'";
            else
            _dataview.RowFilter = $"[{ColumnName}] = '{FilterOnValue}'";
        }
    
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text == "")
            {
                _dataview.RowFilter = "";
                return;
            }

            _FilterDataView(cbFilterBy.SelectedItem.ToString(), txtFilter.Text);
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
            _dataview = clsPeople.GetAllPeopleBasicInfo().DefaultView;
            dgvPeople.DataSource = _dataview;
        }

        private void _AddNewPerson()
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo();
            frm.AddEditPersonData += _RefreshPeopleDataView;

            frm.ShowDialog();
        }
        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _AddNewPerson();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if(dgvPeople.SelectedRows.Count ==0)
            {
                MessageBox.Show("No people selected to delete.\nPlease select the people you want to delete first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (short i = 0; i < dgvPeople.SelectedRows.Count; i++)
            {
                clsPeople.DeletePerson(Convert.ToInt32(dgvPeople.SelectedRows[0].Cells["Person ID"].Value));
            }

            _RefreshPeopleDataView();
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvPeople.SelectedRows.Count == 0)
                {
                MessageBox.Show("No Selected Person To Edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                }

            clsPeople PersonInfo = clsPeople.Find((int) dgvPeople.SelectedRows[0].Cells["Person ID"].Value);

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
            _AddNewPerson();
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
