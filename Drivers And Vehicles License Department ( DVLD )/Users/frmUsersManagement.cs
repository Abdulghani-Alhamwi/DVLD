using System;
using System.Collections.Generic;
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
        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentDgvColumnSortDirection;
        private clsDataGridViewUtilityLib _DgvUtilityLib;

        private int _LastBroughtUserID;
        private string _PrimaryKeyViewedColumnName;
        private string _UserNameColumnName;

        private Dictionary<int, string> _dUserIDsWithIVsPairs;

        public frmUsersManagement()
        {
            InitializeComponent();

            _CurrentDgvColumnSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
            _DgvUtilityLib = new clsDataGridViewUtilityLib();
            _LastBroughtUserID = -1;
            _PrimaryKeyViewedColumnName = "User ID";
            _UserNameColumnName = "UserName";

            _dUserIDsWithIVsPairs = clsUser.GetUsersIDsWithIVs();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _AddComboBoxesItems()
        {
            object[] cbFilterByItems = new object[dgvUsers.Columns.Count + 1];
            cbFilterByItems[0] = "None";

            string[] lColumnsNames = clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvUsers);

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
            dgvUsers.DataSource = clsUser.GetUsersInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
            clsDataGridViewUtilityLib.DecryptDGVColumnValues(clsGlobalSettings.EncryptionKey, _dUserIDsWithIVsPairs, dgvUsers, _PrimaryKeyViewedColumnName, _UserNameColumnName);

            if (dgvUsers.DataSource != null) 
            _AddComboBoxesItems();

            lblRecordsNumber.Text = clsUser.GetTotalUsersCount().ToString();
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsGeneralUtility.DrawComboBoxItems(sender, e);
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

            if ((_PreviousCbFilterSelectedItem == "Is Active" && cbIsActive.SelectedItem.ToString() == "All")
                || !clsDataGridViewUtilityLib._IsRepeatedDataLoadToDgv(txtFilter))
            {
                dgvUsers.DataSource = clsUser.GetUsersInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
                clsDataGridViewUtilityLib.DecryptDGVColumnValues(clsGlobalSettings.EncryptionKey, _dUserIDsWithIVsPairs, dgvUsers, _PrimaryKeyViewedColumnName, _UserNameColumnName);
            }

            if (_PreviousCbFilterSelectedItem == "Is Active")
            {
                cbIsActive.SelectedItem = "All";
                _PreviousCbIsActiveSelectedItem = null;
            }

            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = false;
                _PreviousCbIsActiveSelectedItem = null;
            }
            else if (cbFilterBy.SelectedItem.ToString() != "Is Active")
            {
                txtFilter.Visible = true;
                cbIsActive.Visible = false;
                txtFilter.Focus();
                _PreviousCbIsActiveSelectedItem = null;
            }
            else
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;
            }

            _PreviousCbFilterSelectedItem = cbFilterBy.SelectedItem.ToString();
            txtFilter.Text = "";

            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDgvColumnSortDirection);
        }

        private bool _IsNumericColumn(string ColumnNameToFilterBy)
        {
            return (ColumnNameToFilterBy == _PrimaryKeyViewedColumnName || ColumnNameToFilterBy == "Person ID");
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
            clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDgvColumnSortDirection);

            if (txtFilter.Text != "")
            {
                dgvUsers.DataSource = _GetFilteredData();
            }
            else
            {
                DataTable dtUsersInfo = clsUser.GetUsersInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
                dgvUsers.DataSource = dtUsersInfo;
            }

            clsDataGridViewUtilityLib.DecryptDGVColumnValues(clsGlobalSettings.EncryptionKey, _dUserIDsWithIVsPairs, dgvUsers, _PrimaryKeyViewedColumnName, _UserNameColumnName);
        }

        private void cbIsActive_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsGeneralUtility.DrawComboBoxItems(sender, e);
        }

        private void _AddNewRowToDGV(object[] NewUserDetails, int NewUserID, string UsernameIV)
        {
            if (dgvUsers.DataSource == null)
                dgvUsers.DataSource = clsUser.GetColumnsNamesForView();

            _dUserIDsWithIVsPairs.Add(NewUserID, UsernameIV);

            _DgvUtilityLib.AddNewRowToDGV(dgvUsers, NewUserDetails, dgvUsers.Columns[0].HeaderText);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void _AddNewUserScreen()
        {
            frmAddEditUserInfo frm = new frmAddEditUserInfo();
            frm.AfterSavingNewInfo += _AddNewRowToDGV;
            frm.AfterSavingEditedInfo += _EditDataRowInDGV;
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
                    if (!clsUser.DeleteUser((int)dgvUsers.SelectedRows[i].Cells[_PrimaryKeyViewedColumnName].Value))
                    {
                        MessageBox.Show($"User who has ID : {Convert.ToInt32(dgvUsers.SelectedRows[i].Cells[_PrimaryKeyViewedColumnName].Value)} is not deleted due to a data connected to it."
                            , "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SelectedRowsIndex[i] = -1;
                    }
                    else
                    {
                        SelectedRowsIndex[i] = dgvUsers.SelectedRows[i].Index;
                        TotalDeletedRecords++;
                    }

                }
                _DgvUtilityLib.DeleteSelectedDgvRows(dgvUsers, SelectedRowsIndex);
                lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) - TotalDeletedRecords).ToString();
            }
        }

        private void tsmiAddNewUser_Click(object sender, EventArgs e)
        {
            _AddNewUserScreen();
        }

        private void _EditDataRowInDGV(object[] ModifiedUserDetails, int UsersDgvRowIndex, int UserID, string UsernameIV = null, string NewFullName = null)
        {
            if (UsernameIV != null)
                _dUserIDsWithIVsPairs[UserID] = UsernameIV;

            if (NewFullName != null)
                _DgvUtilityLib.EditOneColumnValueInDgv<string>(dgvUsers, "Full Name", NewFullName, UsersDgvRowIndex);

            else
                _DgvUtilityLib.EditFullDataRowInDgv(dgvUsers, ModifiedUserDetails, UsersDgvRowIndex);
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one user to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                clsUser User = clsUser.Find((int)dgvUsers.SelectedRows[0].Cells[_PrimaryKeyViewedColumnName].Value);

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

        private void _EditUserPersonalInfo(clsPerson UpdatedPersonInfo,int DGVRowIndex)
        {
            _DgvUtilityLib.EditOneColumnValueInDgv<string>(dgvUsers, "Full Name", UpdatedPersonInfo.FullName, DGVRowIndex);
        }

        private void _ShowUserDetails()
        {   
            if (dgvUsers.SelectedRows.Count == 1)
            {
                frmUserDetails frm = new frmUserDetails((int)dgvUsers.SelectedRows[0].Cells[_PrimaryKeyViewedColumnName].Value, dgvUsers.SelectedRows[0].Index);
                frm.OnEditedUserPersonalInfo += _EditUserPersonalInfo;
                frm.ShowDialog();
            }
            else
                MessageBox.Show("You must select a user first to show their details , and you can view only one person details!"
                    , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tsmiShowDetails_Click(object sender, EventArgs e)
        {
            _ShowUserDetails();
        }

        private void tsmiChangePassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 1)
                MessageBox.Show("Please choose one user to change their password!"
                    , "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                frmChangePassword frm = new frmChangePassword((int)dgvUsers.SelectedRows[0].Cells[_PrimaryKeyViewedColumnName].Value, dgvUsers.SelectedRows[0].Index);
                frm.OnEditedPersonInfo += _EditUserPersonalInfo;
                frm.ShowDialog();
            }
        }

        private DataTable _GetFilteredDataOnIsActive(bool ScrollCase = false)
        {
            DataTable dtFilteredData;

            if (!ScrollCase)
             dtFilteredData = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(),
                _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), null);

            else
                 dtFilteredData = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString()
                ,_LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), _LastBroughtUserID, _DgvUtilityLib.NumberOfRowsToOffset, null);

            return dtFilteredData;
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtUsersInfo;
            
            if (!ScrollCase)
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtUsersInfo = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), null);

                else if (cbFilterBy.SelectedItem.ToString() == "UserName")
                    dtUsersInfo = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), clsGeneralUtility.EncryptString(clsGlobalSettings.EncryptionKey, txtFilter.Text),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection),null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtUsersInfo = _GetFilteredDataOnIsActive();

                else
                    dtUsersInfo = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), '%');
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                    dtUsersInfo = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), _LastBroughtUserID, _DgvUtilityLib.NumberOfRowsToOffset, null);

                else if (cbFilterBy.SelectedItem.ToString() == "UserName")
                    dtUsersInfo = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), clsGeneralUtility.EncryptString(clsGlobalSettings.EncryptionKey, txtFilter.Text),
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), _LastBroughtUserID, _DgvUtilityLib.NumberOfRowsToOffset, null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Active")
                    dtUsersInfo = _GetFilteredDataOnIsActive(true);

                else
                    dtUsersInfo = clsUser.GetFilteredData(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text,
                        _LastColumnNameDgvSortedBy, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection),
                        _LastBroughtUserID, _DgvUtilityLib.NumberOfRowsToOffset, '%');
            }

            if (dtUsersInfo != null)
            {
                return dtUsersInfo;
            }
            else
                return null;
        }

        private void _AppendPartOfRemainingData()
        {
            if (_LastColumnNameDgvSortedBy == null || _LastColumnNameDgvSortedBy == _PrimaryKeyViewedColumnName)
                _LastBroughtUserID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvUsers, _PrimaryKeyViewedColumnName, _CurrentDgvColumnSortDirection);

            _DgvUtilityLib.SetNumberOfRowsToOffset(_PrimaryKeyViewedColumnName, _LastColumnNameDgvSortedBy);

            DataRow[] NewRows;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);

                clsDataGridViewUtilityLib.DecryptDGVColumnValues(clsGlobalSettings.EncryptionKey, _dUserIDsWithIVsPairs, dtFilteredData, _PrimaryKeyViewedColumnName, _UserNameColumnName, out NewRows);

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvUsers, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvUsers),_CurrentDgvColumnSortDirection);
            }

            else
            {
                DataTable dtUsersInfo = clsUser.GetUsersInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, _LastBroughtUserID, _LastColumnNameDgvSortedBy
                    , _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection));

                clsDataGridViewUtilityLib.DecryptDGVColumnValues(clsGlobalSettings.EncryptionKey, _dUserIDsWithIVsPairs, dtUsersInfo, _PrimaryKeyViewedColumnName, _UserNameColumnName, out NewRows);

                if (NewRows != null)
                    _DgvUtilityLib.AddNewRowsToDgv(dgvUsers, NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(dgvUsers), _CurrentDgvColumnSortDirection);
            }
        }

        private void dgvUsers_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDataGridViewUtilityLib.IsDgvLastRowDisplayed(dgvUsers))
                    _AppendPartOfRemainingData();
            }
        }

        private void dgvUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDataGridViewUtilityLib.IsDgvLastRowSelected(dgvUsers))
            {
                _AppendPartOfRemainingData();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_PreviousCbIsActiveSelectedItem == cbIsActive.SelectedItem.ToString())
                return;

            if (cbFilterBy.SelectedItem.ToString() == "Is Active")
            {
                clsDataGridViewUtilityLib.ResetSortPropertiesToDefault(ref _LastColumnNameDgvSortedBy, ref _CurrentDgvColumnSortDirection);
                dgvUsers.DataSource = _GetFilteredDataOnIsActive();

                clsDataGridViewUtilityLib.DecryptDGVColumnValues(clsGlobalSettings.EncryptionKey, _dUserIDsWithIVsPairs, dgvUsers, _PrimaryKeyViewedColumnName, _UserNameColumnName);
                _PreviousCbIsActiveSelectedItem = cbIsActive.SelectedItem.ToString();
            }
        }

        private void cmsUsersMenu_Paint(object sender, PaintEventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
                cmsUsersMenu.Close();
        }

        private void _SortData(DataGridViewCellMouseEventArgs e)
        {
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvUsers, ref _LastColumnNameDgvSortedBy, ref _CurrentDgvColumnSortDirection, e);

            DataTable dtSortedInfo = null;

            if (txtFilter.Text == "" && cbFilterBy.SelectedItem.ToString() != "Is Active")
            {
                dtSortedInfo = clsUser.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection));
            }

            else
            {
                if (_IsNumericColumn(cbFilterBy.SelectedItem.ToString()))
                {
                    dtSortedInfo = clsUser.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText
                , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);
                }

                else
                {
                    switch (cbFilterBy.SelectedItem)
                    {
                        case "UserName":
                            dtSortedInfo = clsUser.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText,
                                clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), cbFilterBy.SelectedItem.ToString(), clsGeneralUtility.EncryptString(clsGlobalSettings.EncryptionKey, txtFilter.Text), null);
                            break;

                        case "Full Name":
                            dtSortedInfo = clsUser.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText,
                                clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
                            break;

                        case "Is Active":
                            dtSortedInfo = clsUser.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText
                               , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), cbFilterBy.SelectedItem.ToString(), cbIsActive.SelectedItem.ToString(), null);
                            break;

                        case "Permissions":
                            dtSortedInfo = clsUser.GetSortedInfo(clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvUsers.Columns[e.ColumnIndex].HeaderText,
                                clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentDgvColumnSortDirection), cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);
                            break;
                    }
                }
            }

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvUsers, dtSortedInfo, e, ref _LastColumnNameDgvSortedBy);
            clsDataGridViewUtilityLib.DecryptDGVColumnValues(clsGlobalSettings.EncryptionKey, _dUserIDsWithIVsPairs, dgvUsers, _PrimaryKeyViewedColumnName, _UserNameColumnName);
        }

        private void dgvUsers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SortData(e);
        }

        private void dgvUsers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!clsDataGridViewUtilityLib.WasDgvColumnHeaderClicked(dgvUsers,e))
                _ShowUserDetails();
        }
    }
}