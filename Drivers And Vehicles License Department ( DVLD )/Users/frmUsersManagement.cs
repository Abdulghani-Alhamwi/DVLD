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

        private void _DecryptUsersNames(DataTable dtUsers)
        {
            if(dtUsers != null)
            foreach (DataRow datarow in dtUsers.Rows)
            {
                datarow["UserName"] = clsUtility.DecryptUserName(datarow["UserName"].ToString());
            }
        }
        private void frmUsersManagement_Load(object sender, EventArgs e)
        {
            DataTable dtUsers = clsUser.GetAllUsers();

            if (dtUsers != null)
            {
            _DecryptUsersNames(dtUsers);
                _dataview = dtUsers.DefaultView;
                dgvUsers.DataSource = _dataview;
                dgvUsers.Font = new Font("Tahoma", 16f);
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
            frmAddEditUserInfo frm = new frmAddEditUserInfo();
            frm.OnAddedOrEditedUser += _RefreshUsersDataView;
            frm.ShowDialog();
        }
        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            _AddNewUserScreen();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshUsersDataView()
        {
            _dataview = clsUser.GetAllUsers()?.DefaultView;
            _DecryptUsersNames(_dataview?.Table);
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
                    if (!clsUser.DeleteUser((int)dgvUsers.SelectedRows[0].Cells["User ID"].Value))
                    {
                        MessageBox.Show($"User who has ID : {Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["User ID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (dgvUsers.SelectedRows.Count == 1)
                            return;
                    }
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

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("No Selected User To Edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvUsers.SelectedRows.Count > 1)
            {
                MessageBox.Show("Please select only one user to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            clsUser User = clsUser.Find((int)dgvUsers.SelectedRows[0].Cells["User ID"].Value);

            if(User != null)
            {
                frmAddEditUserInfo frm = new frmAddEditUserInfo(User);
                frm.OnAddedOrEditedUser += _RefreshUsersDataView;

                frm.ShowDialog();
            }
            else
                MessageBox.Show("User is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void tsmiPhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsmiSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void _ShowUserDetails()
        {
            if (dgvUsers.SelectedRows.Count == 1)
            {
                frmUserDetails frm = new frmUserDetails((int)dgvUsers.SelectedRows[0].Cells["User ID"].Value);
                frm.ShowDialog();
            }
            else if (dgvUsers.SelectedRows.Count > 1)
                MessageBox.Show("You must select a user first to show their details , and you can view only one person details!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("There is'nt any user to show their details!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }
        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            _ShowUserDetails();
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            _ShowUserDetails();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.Rows.Count == 0)
                MessageBox.Show("There is'nt any user", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else if (dgvUsers.SelectedRows.Count > 1)
                MessageBox.Show("Please choose one user to change their password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                frmChangePassword frm = new frmChangePassword((int)dgvUsers.SelectedRows[0].Cells["User ID"].Value);
                frm.ShowDialog();
            }
        }
    }
}
