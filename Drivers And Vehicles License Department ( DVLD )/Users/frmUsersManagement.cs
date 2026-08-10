using System;
using System.Drawing;
using System.Windows.Forms;
using MyLib;
using DVLDBusinessLayer;
using System.Data;

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
            object[] Items = new object[] { "None", "User ID", "UserName", "Person ID", "Full Name", "Is Active" };
            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";

            lblRecordsNumber.Text = clsUser.GetTotalUsersCount().ToString();
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }
        private void _SetComboBoxItemBackColor(ComboBox cb)
        {
          cb.BackColor = Color.FromArgb(221, 232, 240);
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = Color.FromArgb(240, 240, 240);
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                _SetComboBoxItemBackColor(cbFilterBy);
            else
                cbFilterBy.BackColor = Color.FromArgb(228, 228, 228);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = false;
                dgvUsers.DataSource = clsUser.GetAllUsersInfo(100);
                 
                if(dgvUsers.DataSource != null)
                _DecryptUsersNames((DataTable)dgvUsers.DataSource);
            }
            else if (cbFilterBy.SelectedItem.ToString() != "Is Active")
            {
                txtFilter.Visible = true;
                cbIsActive.Visible = false;
                txtFilter.Focus();
            }
            else
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;

                if(cbIsActive.Items.Count==0)
                {
                    object[] Items = new object[] { "All", "Yes", "No" };
                    cbIsActive.Items.AddRange(Items);
                }
                cbIsActive.SelectedIndex = 0;
            }
            txtFilter.Text = "";
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
            if (txtFilter.Text != "")
                _AddFilteredData(null);
            else
            {
                DataTable dtUsersInfo = clsUser.GetAllUsersInfo(100);
                _DecryptUsersNames(dtUsersInfo);
                dgvUsers.DataSource = dtUsersInfo;
            }
        }

        private void cbIsActive_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }

        private void cbIsActive_DropDownClosed(object sender, EventArgs e)
        {
              _SetComboBoxItemBackColor(cbIsActive);
        }

        private void _AddNewRowToDGV(ref object[] NewValues)
        {
            clsUtility.AddNewRowToDGV(dgvUsers, (DataTable)dgvUsers.DataSource, ref NewValues, "User ID");
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void _AddNewUserScreen()
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo();
            frm.AfterSavingNewUserInfo += _AddNewRowToDGV;
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
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.Rows.Count == 0)
                MessageBox.Show("There are no users to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                if (dgvUsers.SelectedRows.Count > 5)
                {
                    MessageBox.Show("You Can Delete Maximum 5 Users In One Time!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult result;
                if (dgvUsers.SelectedRows.Count == 1)
                    result = MessageBox.Show("Are you sure you want to delete this user?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                else
                    result = MessageBox.Show("Are you sure you want to delete those users?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    int[] SelectedRowsIndex = new int[dgvUsers.SelectedRows.Count];
                    for (byte i = 0; i < dgvUsers.SelectedRows.Count; i++)
                    {
                        if (!clsUser.DeleteUser((int)dgvUsers.SelectedRows[i].Cells["User ID"].Value))
                        {
                            MessageBox.Show($"User who has ID : {Convert.ToInt32(dgvUsers.SelectedRows[i].Cells["User ID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SelectedRowsIndex[i] = -1;
                        }
                        else
                            SelectedRowsIndex[i] = dgvUsers.SelectedRows[i].Index;

                    }
                    clsUtility.DeleteSelectedRowsFromView(dgvUsers, SelectedRowsIndex);
                    lblRecordsNumber.Text = clsUser.GetTotalUsersCount().ToString();
                }
            }
        }

        private void tsmiAddNewUser_Click(object sender, EventArgs e)
        {
            _AddNewUserScreen();
        }

        private void _EditDataRowInDGV(ref object[] NewValues, int RowIndex, string NewFullName = null)
        {
            if (NewFullName != null)
                clsUtility.EditOneDataRowColumnValueInDGV(dgvUsers, (DataTable)dgvUsers.DataSource, "Full Name", NewFullName, RowIndex);

            else
                clsUtility.EditFullDataRowInDGV(dgvUsers, (DataTable)dgvUsers.DataSource, ref NewValues, RowIndex);
        }
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("No Selected User To Edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvUsers.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one user to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                clsUser User = clsUser.Find((int)dgvUsers.SelectedRows[0].Cells["User ID"].Value);

                frmAddEditUserInfo frm = new frmAddEditUserInfo(User, dgvUsers.SelectedRows[0].Index, dgvUsers.SelectedRows[0].Cells["Full Name"].Value.ToString());
                frm.AfterSavingEditedUserInfo += _EditDataRowInDGV;

                frm.ShowDialog();
            }
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
        private DataTable _FilterOnIsActive()
        {
            DataTable dtFilteredData = clsUser.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(), null);

            return dtFilteredData;
        }

        private void _AddFilteredData(DataTable EmptyDataTable, bool ScrollCase = false)
        {
            DataTable dtUsersInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "User ID" || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtUsersInfo = clsUser.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);

                else if (cbFilterBy.SelectedItem.ToString() == "UserName")
                    dtUsersInfo = clsUser.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), clsUtility.EncryptUserName(txtFilter.Text));

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtUsersInfo = _FilterOnIsActive();

                else
                    dtUsersInfo = clsUser.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "User ID" || cbFilterBy.SelectedItem.ToString() == "Person ID")
                    dtUsersInfo = clsUser.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null, (int)dgvUsers?.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value);

                else if (cbFilterBy.SelectedItem.ToString() == "UserName")
                    dtUsersInfo = clsUser.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), clsUtility.EncryptUserName(txtFilter.Text));

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtUsersInfo = _FilterOnIsActive();

                else
                    dtUsersInfo = clsUser.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%', (int)dgvUsers?.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value);
            }

            _DecryptUsersNames(dtUsersInfo);
            dgvUsers.DataSource = dtUsersInfo;

            if (EmptyDataTable != null && ScrollCase)
                EmptyDataTable = (DataTable)dgvUsers.DataSource;
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
                    clsUtility.AddNewRowsToDGV(dgvUsers, (DataTable)dgvUsers.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvUsers));
            }

            else
            {
                NewRows = clsUser.GetAllUsersInfo(100, (int)dgvUsers.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvUsers, (DataTable)dgvUsers.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvUsers));
            }
        }
        private void dgvUsers_Scroll(object sender, ScrollEventArgs e)
        {
            if (dgvUsers.Rows.GetLastRow(DataGridViewElementStates.None) == dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                _AppendPartOfRemainingData();
        }

        private void dgvUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvUsers.Rows.GetLastRow(DataGridViewElementStates.None) == dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Selected))
                _AppendPartOfRemainingData();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvUsers.DataSource = _FilterOnIsActive();
            _DecryptUsersNames((DataTable)dgvUsers.DataSource);
        }
    }
}
