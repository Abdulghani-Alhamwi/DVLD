using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmUsersManagement : Form
    {
        string _PreviousCbFilterSelectedItem;
        string _PreviousCbIsActiveSelectedItem;
        private string _LastColumnNameDgvSortedBy;
        private clsUtility.enDataGridViewSortDirection _CurrentDgvSortDirection;
        private clsUtility _UtilityLib;
        public frmUsersManagement()
        {
            InitializeComponent();
            _LastColumnNameDgvSortedBy = null;
            _CurrentDgvSortDirection = clsUtility.enDataGridViewSortDirection.Descending;
            _UtilityLib = new clsUtility();
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
        private void _AddComboBoxesItems()
        {
            object[] cbFilterByItems = new object[dgvUsers.Columns.Count + 1];
            cbFilterByItems[0] = "None";

            string[] lColumnsNames = clsUtility.GetDgvColumnsNames(dgvUsers);

            for (byte i = 0; i < lColumnsNames.Length; i++)
            {
                cbFilterByItems[i + 1] = lColumnsNames[i];
            }

            cbFilterBy.Items.AddRange(cbFilterByItems);
            cbFilterBy.SelectedItem = "None";

            object[] cbIsActiveItems = new object[] { "All", "Yes", "No" };

            cbIsActive.Items.AddRange(cbIsActiveItems);
            cbIsActive.SelectedItem = "All";
        }

        private void frmUsersManagement_Load(object sender, EventArgs e)
        {
            dgvUsers.DataSource = clsUser.GetUsersInfo(clsUtility.WantedNumOfRowsFromDB);
            _DecryptUsersNames((DataTable)dgvUsers.DataSource);

            if (dgvUsers.DataSource != null) 
            _AddComboBoxesItems();

            lblRecordsNumber.Text = clsUser.GetTotalUsersCount().ToString();
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }
    
        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = clsGlobalSettings.ComboBoxItemsBackColor;
        }

        private void cbIsActive_DropDownClosed(object sender, EventArgs e)
        {
            cbIsActive.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsGlobalSettings.ComboBoxBackColor;
        }
        
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbFilterSelectedItem == cbFilterBy.SelectedItem.ToString())
                return;

            if ((_PreviousCbFilterSelectedItem == "Is Active" && cbIsActive.SelectedItem.ToString() != "All")
                || !clsUtility._IsRepeatedDataLoadToDgv(txtFilter))
            {
                dgvUsers.DataSource = clsUser.GetUsersInfo(clsUtility.WantedNumOfRowsFromDB);
                _DecryptUsersNames((DataTable)dgvUsers.DataSource);
            }

            if (_PreviousCbFilterSelectedItem == "Is Active")
                cbIsActive.SelectedItem = "All";

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = false;

                _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            }
            else if (cbFilterBy.SelectedItem.ToString() != "Is Active")
            {
                txtFilter.Visible = true;
                cbIsActive.Visible = false;
                txtFilter.Focus();

                _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            }
            else
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;

                _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            }

            txtFilter.Text = "";
        }

        private bool _IsNumericColumn(string ColumnNameToFilterBy)
        {
            return (ColumnNameToFilterBy == "Person ID" || ColumnNameToFilterBy == "User ID");
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
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
            {
                DataTable dtFilteredInfo = _GetFilteredData();
                dgvUsers.DataSource = dtFilteredInfo;
            }
            else
            {
                DataTable dtUsersInfo = clsUser.GetUsersInfo(clsUtility.WantedNumOfRowsFromDB);
                _DecryptUsersNames(dtUsersInfo);
                dgvUsers.DataSource = dtUsersInfo;
            }
        }

        private void cbIsActive_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(sender, e);
        }

        private void _AddNewRowToDGV(object[] NewUserDetails)
        {
            if (dgvUsers.DataSource == null)
                dgvUsers.DataSource = clsUser.GetColumnsNamesForView();

            _UtilityLib.AddNewRowToDGV(dgvUsers, NewUserDetails, dgvUsers.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void _AddNewUserScreen()
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo();
            frm.AfterSavingNewInfo += _AddNewRowToDGV;
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
               byte TotalDeletedRecords = 0;
               for (byte i = 0; i < dgvUsers.SelectedRows.Count; i++)
               {
               if (!clsUser.DeleteUser((int)dgvUsers.SelectedRows[i].Cells["User ID"].Value))
               {
                   MessageBox.Show($"User who has ID : {Convert.ToInt32(dgvUsers.SelectedRows[i].Cells["User ID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   SelectedRowsIndex[i] = -1;
               }
               else
               {
                   SelectedRowsIndex[i] = dgvUsers.SelectedRows[i].Index;
                   TotalDeletedRecords++;
               }

               }
                _UtilityLib.DeleteSelectedDgvRows(dgvUsers, SelectedRowsIndex);
               lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) -TotalDeletedRecords).ToString();
           }
            }

        private void tsmiAddNewUser_Click(object sender, EventArgs e)
        {
            _AddNewUserScreen();
        }

        private void _EditDataRowInDGV(object[] ModifiedUserDetails, int UsersDgvRowIndex, string NewFullName = null)
        {
            if (NewFullName != null)
                _UtilityLib.EditOneColumnValueInDgv<string>(dgvUsers, "Full Name", NewFullName, UsersDgvRowIndex);

            else
                _UtilityLib.EditFullDataRowInDgv(dgvUsers, ModifiedUserDetails, UsersDgvRowIndex);
        }
        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one user to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                clsUser User = clsUser.Find((int)dgvUsers.SelectedRows[0].Cells["User ID"].Value);

                frmAddEditUserInfo frm = new frmAddEditUserInfo(User, Convert.ToInt16(dgvUsers.SelectedRows[0].Index), dgvUsers.SelectedRows[0].Cells["Full Name"].Value.ToString());
                frm.AfterSavingEditedInfo += _EditDataRowInDGV;

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
            else
                MessageBox.Show("You must select a user first to show their details , and you can view only one person details!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            _ShowUserDetails();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 1)
                MessageBox.Show("Please choose one user to change their password!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                frmChangePassword frm = new frmChangePassword((short)dgvUsers.SelectedRows[0].Cells["User ID"].Value);
                frm.ShowDialog();
            }
        }
        private DataTable _FilterOnIsActive(bool ScrollCase = false)
        {
            DataTable dtFilteredData;
            if (!ScrollCase)
             dtFilteredData = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(),
                _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), null);

            else
                 dtFilteredData = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(),
                _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), (int)dgvUsers?.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value, null);

            return dtFilteredData;
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtUsersInfo;
            
            if (!ScrollCase)
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtUsersInfo = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), null);

                else if (cbFilterBy.SelectedItem.ToString() == "UserName")
                    dtUsersInfo = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), clsUtility.EncryptUserName(txtFilter.Text),
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection),null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtUsersInfo = _FilterOnIsActive();

                else
                    dtUsersInfo = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), '%');
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtUsersInfo = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), (int)dgvUsers?.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value, null);

                else if (cbFilterBy.SelectedItem.ToString() == "UserName")
                    dtUsersInfo = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), clsUtility.EncryptUserName(txtFilter.Text),
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), (int)dgvUsers?.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value, null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtUsersInfo = _FilterOnIsActive(true);

                else
                    dtUsersInfo = clsUser.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection),
                        (int)dgvUsers?.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value, '%');
            }

            if (dtUsersInfo != null)
            {
                _DecryptUsersNames(dtUsersInfo);
                return dtUsersInfo;
            }
            else
                return null;
        }
        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvUsers, NewRows, clsUtility.GetDgvColumnsNames(dgvUsers));
            }

            else
            {
                NewRows = clsUser.GetUsersInfo(clsUtility.WantedNumOfRowsFromDB, (int)dgvUsers.Rows[dgvUsers.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvUsers, NewRows, clsUtility.GetDgvColumnsNames(dgvUsers));
            }
        }
        private void dgvUsers_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsUtility.IsDgvLastRowDisplayed(dgvUsers))
                    _AppendPartOfRemainingData();
            }
        }

        private void dgvUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsUtility.IsDgvLastRowSelected(dgvUsers))
                _AppendPartOfRemainingData();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbIsActiveSelectedItem == cbIsActive.SelectedItem.ToString())
                return;

            if (cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                dgvUsers.DataSource = _FilterOnIsActive();
                _DecryptUsersNames((DataTable)dgvUsers.DataSource);
            }
            _PreviousCbIsActiveSelectedItem = cbIsActive.SelectedItem.ToString();
        }

        private void cmsUsersMenu_Paint(object sender, PaintEventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
                cmsUsersMenu.Close();
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            _CurrentDgvSortDirection = clsUtility.ReverseCurrentDgvSortDirection(_CurrentDgvSortDirection);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text == "" && cbFilterBy.SelectedItem.ToString() != "Is Active")
            {

                dtSortedInfo = clsUser.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText
                , clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsUser.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText
                , clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), txtFilter.Text, cbFilterBy.SelectedItem.ToString(), null);
                }

                else
                {
                    switch (cbFilterBy.SelectedItem)
                    {
                        case "UserName":
                            dtSortedInfo = clsUser.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection),
                            cbFilterBy.SelectedItem.ToString(), clsUtility.EncryptUserName(txtFilter.Text), null);
                            break;

                        case "Full Name":
                            dtSortedInfo = clsUser.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText, clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection),
                            cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
                            break;

                        case "Is Active":
                            dtSortedInfo = clsUser.GetSortedInfo(clsUtility.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText
                    , clsUtility.GetDataGridViewSortDirection(_CurrentDgvSortDirection), cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(), null);
                            break;

                    }
                }
            }

            _UtilityLib.ModifyDataAfterUserOrdersColumn(dgvUsers, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
            _DecryptUsersNames((DataTable)dgvUsers.DataSource);
        }

        private void dgvUsers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }

        private void dgvUsers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!clsUtility.WasDgvColumnHeaderClicked(dgvUsers,e))
                _ShowUserDetails();
        }
    }
}