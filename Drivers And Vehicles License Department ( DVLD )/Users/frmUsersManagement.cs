using System;
using System.Drawing;
using System.Windows.Forms;
using MyLib;
using DVLDBusinessLayer;
using System.Linq;
using System.Data;
using System.Text;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmUsersManagement : Form
    {
        public frmUsersManagement()
        {
            InitializeComponent();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataView _dataview;
        private void frmUsersManagement_Load(object sender, EventArgs e)
        {
            DataTable dtUsers = clsUser.GetAllUsers();

            if (dtUsers != null)
            {
                _dataview = dtUsers.DefaultView;
                dgvUsers.DataSource = _dataview;
                dgvUsers.Font = new Font("Tahoma", 16);
            }
            lblRecordsNumber.Text = dgvUsers.Rows.Count.ToString();

            object[] Items = new object[] { "None", "User ID", "UserName", "Person ID", "Full Name", "Is Active" };
            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }

        private void cbFilterBy_DropDown(object sender, EventArgs e)
        {
            cbFilterBy.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = Color.FromArgb(221, 232, 240);
            else
                cbFilterBy.BackColor = Color.FromArgb(228, 228, 228);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbFilterOnIsActive.Visible = false;
            }
            else if (cbFilterBy.SelectedItem.ToString() != "Is Active")
            {
                txtFilter.Visible = true;
                cbFilterOnIsActive.Visible = false;
                txtFilter.Focus();
            }
            else
            {
                txtFilter.Visible = false;
                cbFilterOnIsActive.Visible = true;

                object[] Items = new object[] { "All", "Yes", "No" };
                cbFilterOnIsActive.Items.AddRange(Items);
                cbFilterOnIsActive.SelectedIndex = 0;
            }

        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "Person ID" || cbFilterBy.SelectedItem.ToString() == "User ID")
            {
                if (Char.IsDigit((Char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else 
                txtFilter.ReadOnly = false;

        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            clsUtility._FilterDataView(_dataview, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, e);            
        }

        private void cbFilterOnIsActive_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }

        private void cbFilterOnIsActive_DropDown(object sender, EventArgs e)
        {
            cbFilterOnIsActive.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void cbFilterOnIsActive_DropDownClosed(object sender, EventArgs e)
        {
            if(cbFilterOnIsActive.SelectedItem.ToString() == "All")
                cbFilterOnIsActive.BackColor = Color.FromArgb(228,228,228);
            else
                cbFilterOnIsActive.BackColor = Color.FromArgb(221, 232, 240);

        }

        private void cbFilterOnIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbFilterOnIsActive.SelectedItem.ToString())
            {
                case "All":
                _dataview.RowFilter = null;
                    break;

                case "Yes":
                _dataview.RowFilter = "[Is Active] = 'true'";
                    break;

                case "No":
                _dataview.RowFilter = "[Is Active] = 'false'";
                    break;
            }
        }

        private void _AddNewUserScreen()
        {
            frmAddNewUser frm = new frmAddNewUser();
            frm.ShowDialog();
        }
        private void btnAddNewUser_Click_1(object sender, EventArgs e)
        {
            _AddNewUserScreen();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshUsersDataView()
        {
            dgvUsers.DataSource = null;
            _dataview = clsUser.GetAllUsers().DefaultView;
            dgvUsers.DataSource = _dataview;
        }


        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.Rows.Count == 0)
            {
                MessageBox.Show("There are no users to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if(dgvUsers.SelectedRows.Count > 0)
            {
                for (short i = 0; i < dgvUsers.SelectedRows.Count; i++)
                {
                    clsUser.DeleteUser((int)dgvUsers.SelectedRows[0].Cells["User ID"].Value);
                }
                _RefreshUsersDataView();
            }
            else
                MessageBox.Show("No selected users to delete!", "Select a User", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void tsmiAddNewUser_Click(object sender, EventArgs e)
        {
            _AddNewUserScreen();
        }
    }
}
