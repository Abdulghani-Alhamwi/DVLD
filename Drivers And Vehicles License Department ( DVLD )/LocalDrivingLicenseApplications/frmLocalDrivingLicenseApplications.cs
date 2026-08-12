using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Core;
using DVLDBusinessLayer;
using MyLib;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }
        private void _AddComboBoxesFilterItems()
        {
            object[] cbFilterItems = new object[] { "None", "L.D.L.AppID", "National No.", "Full Name", "Status" };
            cbFilterBy.DataSource = cbFilterItems;
            cbFilterBy.SelectedItem = "None";

            object[] cbStatusItems = new object[] { "All", "New", "Canceled", "Completed" };
            cbStatus.DataSource = cbStatusItems;
            cbStatus.SelectedItem = "All";
        }

        bool _AllowDataLoading;
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            lblRecordsNumber.Text = clsLocalDrivingLicenseApplication.GetTotalLDLApplicationsCount().ToString();
            _AddComboBoxesFilterItems();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DrawComboBoxItems(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems((ComboBox)sender, e);
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtFilter.Text != "")
            _AddFilteredData(null);

            else
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetLDLApplications(100);

        }
        private void _LoadDataAfterFirstTimeLoad(ref bool _AllowDataLoading)
        {
            if (_AllowDataLoading)
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetLDLApplications(100);
            else
                _AllowDataLoading = true;
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None" && cbFilterBy.SelectedItem.ToString() != "Status")
            {
                txtFilter.Visible = true;
                cbStatus.Visible = false;
                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
            }

            else if (cbFilterBy.SelectedItem.ToString() != "None" && cbFilterBy.SelectedItem.ToString() == "Status")
            {
                txtFilter.Visible = false;
                cbStatus.Visible = true;
                _LoadDataAfterFirstTimeLoad(ref _AllowDataLoading);
            }

            else
            {
                txtFilter.Visible = false;
                cbStatus.Visible = false;
               
                dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetLDLApplications(100);
                _AllowDataLoading = false;
                txtFilter.Text = "";
            }
            
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
            {
                if (char.IsDigit((char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
            else if (cbFilterBy.SelectedItem.ToString() == "Status")
            {
                if (char.IsLetter((char)e.KeyData) || e.KeyData == Keys.Back)
                    txtFilter.ReadOnly = false;
                else
                    txtFilter.ReadOnly = true;
            }
        }

        private void _RefreshLDLApplicationsView()
        {
            clsUtility.RefreshInformationView(dgvLDLApplications,clsLocalDrivingLicenseApplication.GetLDLApplications(100).DefaultView);
        }
        private void btnAddLDLApplication_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication();
            frm.AfterAddOrUpdate += _RefreshLDLApplicationsView;
            frm.ShowDialog();
        }

        private void tsmiEditApplication_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one application to edit", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            else
            {
                clsLocalDrivingLicenseApplication LDLApplication = clsLocalDrivingLicenseApplication.Find((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

                frmNewLocalDrivingLicenseApplication frm = new frmNewLocalDrivingLicenseApplication(LDLApplication);
                frm.ShowDialog();
            }
        }
        private void cmsLDLApplications_Paint(object sender, PaintEventArgs e)
        {
            if (dgvLDLApplications.Rows.Count == 0)
                cmsLDLApplications.Close();
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.Rows.Count == 0)
                MessageBox.Show("There are no local driving license applications to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (dgvLDLApplications.SelectedRows.Count > 5)
                {
                    MessageBox.Show("You Can Delete Maximum 5 Local Driving License Applications In One Time!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult result;
                if(dgvLDLApplications.SelectedRows.Count == 1)
                result = MessageBox.Show("Are you sure you want to delete this application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                else
                result = MessageBox.Show("Are you sure you want to delete those applications?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    for (short i = 0; i < dgvLDLApplications.SelectedRows.Count; i++)
                    {
                        int ApplicationID = clsLocalDrivingLicenseApplication.GetApplicationID((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value);
                        if (dgvLDLApplications.SelectedRows[i].Cells["Status"].Value.ToString() == "Completed")
                        {
                            MessageBox.Show($"Local driving license application who has ID : {Convert.ToInt32(dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value)} cannot be deleted because it is completed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }

                        if (!clsLocalDrivingLicenseApplication.DeleteLDLApplication((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value, ApplicationID))
                        {
                            MessageBox.Show($"Local driving license application who has ID : {Convert.ToInt32(dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    _RefreshLDLApplicationsView();
                }
            }
        }

        private void tsmiCancelApplication_Click(object sender, EventArgs e)
        {
            if (dgvLDLApplications.Rows.Count == 0)
                MessageBox.Show("There are no local driving license applications to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else if (dgvLDLApplications.SelectedRows.Count > 1)
                MessageBox.Show("You can select only one local driving license application to cancel!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                DialogResult result = MessageBox.Show("Are you sure you want to cancel this application?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    int ApplicationID = clsLocalDrivingLicenseApplication.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

                    if (clsApplication.ChangeApplicationStatus(ApplicationID, clsApplication.enApplicationStatus.Canceled))
                        _RefreshLDLApplicationsView();
                    else
                        MessageBox.Show("Failed To Cancel Application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
          }        
        private void _AddFilteredData(DataTable EmptyDataTable, bool ScrollCase = false)
        {
            DataTable dtLDLApplicationsInfo;
            if (!ScrollCase)
            {
                if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null);                    

                else
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%');
            }

            else
            {
                if (cbFilterBy.SelectedItem.ToString() == "L.D.L.AppID")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, null, (int)dgvLDLApplications?.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["L.D.L.AppID"].Value);

                else if (cbFilterBy.SelectedItem.ToString() == "Status")
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(),cbStatus.SelectedItem.ToString(), null, (int)dgvLDLApplications?.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["L.D.L.AppID"].Value);

                else
                    dtLDLApplicationsInfo = clsLocalDrivingLicenseApplication.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, '%', (int)dgvLDLApplications?.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["L.D.L.AppID"].Value);
            }

                dgvLDLApplications.DataSource = dtLDLApplicationsInfo;

            if (dtLDLApplicationsInfo != null)
            {
                if (EmptyDataTable != null && ScrollCase)
                    EmptyDataTable = (DataTable)dgvLDLApplications.DataSource;
            }
        }
        private void _AppendPartOfRemainingData()
        {
            DataRow[] NewRows;

            if (cbFilterBy.SelectedItem.ToString() != "None")
            {
                DataTable dtFilteredData = new DataTable();

                _AddFilteredData(dtFilteredData, true);
                NewRows = dtFilteredData?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvLDLApplications));
            }

            else
            {
                NewRows = clsLocalDrivingLicenseApplication.GetLDLApplications(100, (int)dgvLDLApplications.Rows[dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["L.D.L.AppID"].Value)?.Select();

                if (NewRows != null)
                    clsUtility.AddNewRowsToDGV(dgvLDLApplications, (DataTable)dgvLDLApplications.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvLDLApplications));
            }
        }

        private void dgvLDLApplications_Scroll(object sender, ScrollEventArgs e)
        {
            if (dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.None) == dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                _AppendPartOfRemainingData();
        }

        private void dgvLDLApplications_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.None) == dgvLDLApplications.Rows.GetLastRow(DataGridViewElementStates.Selected))
                _AppendPartOfRemainingData();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() == "Status")
            dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplication.GetFilteredData(100, cbFilterBy.SelectedItem.ToString(), cbStatus.SelectedItem.ToString(), null);
        }

        private void ComboBoxes_DropDown(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = clsUtility.ComboBoxItemsBackColor;
        }

        private void cbStatus_DropDownClosed(object sender, EventArgs e)
        {
            cbStatus.BackColor = clsUtility.ComboBoxHighlightedBackColor;
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            if(cbFilterBy.SelectedItem.ToString() != "None")
                cbFilterBy.BackColor = clsUtility.ComboBoxHighlightedBackColor;
            else
                cbFilterBy.BackColor = clsUtility.ComboBoxBackColor;
        }
    }
    }
