using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Controls;
using DVLDPresentationLayer.Licenses;
using Utility_Library;

namespace DVLDPresentationLayer.TestTypes
{
    public partial class frmDetainedLicensesManagement : Form
    {
        private int _NumberOfDgvAddedRows;
        private bool _AllowDataLoading;
        public frmDetainedLicensesManagement()
        {
            InitializeComponent();
        }
        private void _AddComboBoxesFilterItems()
        {
            object[] Items = new object[] {"None","Detain ID","Is Released","National No.","Full Name","Release Application ID"};
            cbFilterBy.Items.AddRange(Items);
            cbFilterBy.SelectedItem = "None";

            object[] cbIsReleasedItems = new object[] { "All", "Yes", "No" };
            cbIsReleased.Items.AddRange(cbIsReleasedItems);
            cbIsReleased.SelectedItem = "All";
        }

        private void frmDetainedLicensesManagement_Load(object sender, EventArgs e)
        {
            dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetDetainedLicensesInfo(clsUtility.WantedNumOfRowsFromDB);

            if (dgvDetainedLicenses.DataSource != null)
                _AddComboBoxesFilterItems();

            lblRecordsNumber.Text = clsDetainedLicenses.GetTotalCount().ToString();
        }

        private void DrawComboBoxItems(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems((ComboBox)sender, e);
        }
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text != "")
                dgvDetainedLicenses.DataSource = _GetFilteredData();

            else
                dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetDetainedLicensesInfo(clsUtility.WantedNumOfRowsFromDB);
        }
          
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "Detain ID" || cbFilterBy.SelectedItem.ToString() == "Release Application ID")
            {
                if (char.IsDigit((char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }

            else if (cbFilterBy.SelectedItem.ToString() == "Full Name")
            {
                if (char.IsLetter((char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else
                txtFilter.ReadOnly = false;
        }

        private void _LoadDataAfterFirstTimeLoad(ref bool _AllowDataLoading)
        {
            if (_AllowDataLoading)
                dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetDetainedLicensesInfo(clsUtility.WantedNumOfRowsFromDB);
            else
                _AllowDataLoading = true;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None" && cbFilterBy.SelectedItem.ToString() != "Is Released")
            {
                txtFilter.Visible = true;
                cbIsReleased.Visible = false;
                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
                txtFilter.Text = "";
            }

            else if (cbFilterBy.SelectedItem.ToString() != "None" && cbFilterBy.SelectedItem.ToString() == "Is Released")
            {
                txtFilter.Visible = false;
                cbIsReleased.Visible = true;
                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
            }

            else
            {
                txtFilter.Visible = false;
                cbIsReleased.Visible = false;

                dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetDetainedLicensesInfo(clsUtility.WantedNumOfRowsFromDB);
                _AllowDataLoading = false;
            }
        }

        private void _ReleaseDetainedLicense(int DetainedLicenseID,DateTime ReleaseDate,int ReleaseApplicationID)
        {
            foreach (DataGridViewRow DetainInfo in dgvDetainedLicenses.Rows)
            {
                if ((int)DetainInfo.Cells["D.ID"].Value == DetainedLicenseID)
                {
                    clsUtility.EditOneColumnValueInDgv<bool>((DataTable)dgvDetainedLicenses.DataSource, "Is Released", true, dgvDetainedLicenses.Rows.IndexOf(DetainInfo), _NumberOfDgvAddedRows);
                    clsUtility.EditOneColumnValueInDgv<string>((DataTable)dgvDetainedLicenses.DataSource, "Release Date", ReleaseDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)), dgvDetainedLicenses.Rows.IndexOf(DetainInfo), _NumberOfDgvAddedRows);
                    clsUtility.EditOneColumnValueInDgv<int>((DataTable)dgvDetainedLicenses.DataSource, "Release App.ID", ReleaseApplicationID, dgvDetainedLicenses.Rows.IndexOf(DetainInfo), _NumberOfDgvAddedRows);
                }
            }
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.AfterReleasingALicense += _ReleaseDetainedLicense;
            frm.ShowDialog();
        }

        private void _AddNewRowToDGV(object[] NewAppDetails)
        {
            if (dgvDetainedLicenses.DataSource == null)
                dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetColumnsNamesForView();

            clsUtility.AddNewRowToDGV((DataTable)dgvDetainedLicenses.DataSource, NewAppDetails, dgvDetainedLicenses.Columns[0].HeaderText, ref _NumberOfDgvAddedRows);
            lblRecordsNumber.Text = (Convert.ToInt32(lblRecordsNumber.Text) + 1).ToString();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLocalLicense frm = new frmDetainLocalLicense();
            frm.AfterDetainingLicense += _AddNewRowToDGV;
            frm.ShowDialog();
        }

        private DataTable _GetFilteredData(bool ScrollCase = false)
        {
            DataTable dtDetainLicensesInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "Detain ID" || cbFilterBy.SelectedItem.ToString() == "Release Application ID")
                    dtDetainLicensesInfo = clsDetainedLicenses.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
                    dtDetainLicensesInfo = clsDetainedLicenses.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsReleased.SelectedItem.ToString(), null);

                else
                    dtDetainLicensesInfo = clsDetainedLicenses.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "Detain ID" || cbFilterBy.SelectedItem.ToString() == "Release Application ID")
                    dtDetainLicensesInfo = clsDetainedLicenses.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvDetainedLicenses?.Rows[dgvDetainedLicenses.Rows.GetLastRow(DataGridViewElementStates.None)].Cells["D.ID"].Value, null);

                else if (cbFilterBy.SelectedItem.ToString() == "Is Released")
                    dtDetainLicensesInfo = clsDetainedLicenses.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsReleased.SelectedItem.ToString(), (int)dgvDetainedLicenses?.Rows[dgvDetainedLicenses.Rows.GetLastRow(DataGridViewElementStates.None)].Cells["D.ID"].Value, null);

                else
                    dtDetainLicensesInfo = clsDetainedLicenses.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, (int)dgvDetainedLicenses?.Rows[dgvDetainedLicenses.Rows.GetLastRow(DataGridViewElementStates.None)].Cells["D.ID"].Value, '%');
            }

            return dtDetainLicensesInfo;
        }
        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows = null;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = _GetFilteredData(true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvDetainedLicenses, (DataTable)dgvDetainedLicenses.DataSource, NewRows, clsUtility.GetDgvColumnsNames(dgvDetainedLicenses));
            }

            else
            {
                NewRows = clsDetainedLicenses.GetDetainedLicensesInfo(clsUtility.WantedNumOfRowsFromDB, (int)dgvDetainedLicenses.Rows[dgvDetainedLicenses.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["D.ID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDgv(dgvDetainedLicenses, (DataTable)dgvDetainedLicenses.DataSource, NewRows, clsUtility.GetDgvColumnsNames(dgvDetainedLicenses));
            }
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() == "Is Released")
                dgvDetainedLicenses.DataSource = clsDetainedLicenses.GetFilteredData(clsUtility.WantedNumOfRowsFromDB, cbFilterBy.SelectedItem.ToString(), cbIsReleased.SelectedItem.ToString(), null);
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = clsUtility.ComboBoxItemsBackColor;
        }

        private void cbIsReleased_DropDownClosed(object sender, EventArgs e)
        {
            cbIsReleased.BackColor = clsUtility.ComboBoxHighlightedBackColor;
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsUtility.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsUtility.ComboBoxBackColor;
        }

   
        private void dgvDetainedLicenses_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsUtility.IsDgvLastRowDisplayed(dgvDetainedLicenses))
                    _AppendPartOfRemainingData();
            }
        }

        private void dgvDetainedLicenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsUtility.IsDgvLastRowSelected(dgvDetainedLicenses))
                _AppendPartOfRemainingData();
        }

        private void tsmiShowPersonDetails_Click(object sender, EventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(clsPerson.GetPersonID(dgvDetainedLicenses.SelectedRows[0].Cells["N.No."].Value.ToString()));
            frm.ShowDialog();
        }

        private void tsmiShowLicenseDetails_Click(object sender, EventArgs e)
        {
            frmLocalLicenseDetails frm = new frmLocalLicenseDetails((int)dgvDetainedLicenses.SelectedRows[0].Cells["L.ID"].Value);
            frm.ShowDialog();
        }

        private void tsmishowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            frmDriverLicenseHistory frm = new frmDriverLicenseHistory(clsPerson.GetPersonID(dgvDetainedLicenses.SelectedRows[0].Cells["N.No."].Value.ToString()));
            frm.ShowDialog();
        }

        private void EditRowAfterReleasingLicense(int ReleaseApplicationID,DateTime ReleaseDate,int DetainedLicensesDgvRowIndex)
        {
            clsUtility.EditOneColumnValueInDgv<bool>((DataTable)dgvDetainedLicenses.DataSource, "Is Released", true, DetainedLicensesDgvRowIndex, _NumberOfDgvAddedRows);
            clsUtility.EditOneColumnValueInDgv<string>((DataTable)dgvDetainedLicenses.DataSource, "Release Date", ReleaseDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)), DetainedLicensesDgvRowIndex, _NumberOfDgvAddedRows);
            clsUtility.EditOneColumnValueInDgv<int>((DataTable)dgvDetainedLicenses.DataSource, "Release App.ID", ReleaseApplicationID, DetainedLicensesDgvRowIndex, _NumberOfDgvAddedRows);
        }

        private void tsmiReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dgvDetainedLicenses.SelectedRows[0].Cells["L.ID"].Value, (int)dgvDetainedLicenses.SelectedRows[0].Index);
            frm.AfterReleasingSentLicense += EditRowAfterReleasingLicense;
            frm.ShowDialog();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsDetainedLicense_Paint(object sender, PaintEventArgs e)
        {
            if (dgvDetainedLicenses.Rows.Count == 0)
                cmsDetainedLicense.Close();

            else if (dgvDetainedLicenses.SelectedRows.Count > 1)
            {
                MessageBox.Show("You can select only one row!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmsDetainedLicense.Close();
            }
            else
            {
                if ((bool)dgvDetainedLicenses.SelectedRows[0].Cells["Is Released"].Value == true)
                {
                    tsmiReleaseDetainedLicense.Enabled = false;
                }
                else
                    tsmiReleaseDetainedLicense.Enabled = true;
            }
        }
    }
}