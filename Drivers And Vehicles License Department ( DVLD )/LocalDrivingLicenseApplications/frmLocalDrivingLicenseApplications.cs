using System;
using System.Data;
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

        DataView _dataview;

        private void _PrepareComboBoxWithFilterItems()
        {
            object[] ComboBoxItems = { "None", "L.D.L.AppID", "National No.", "Full Name", "Status" };
            cbFilterBy.DataSource = ComboBoxItems;
            cbFilterBy.SelectedItem = "None";
        }
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _dataview = clsLocalDrivingLicenseApplications.GetLDLApplications()?.DefaultView;
            dgvLDLApplications.DataSource = _dataview;

            lblRecordsNumber.Text = dgvLDLApplications.Rows.Count.ToString();
            _PrepareComboBoxWithFilterItems();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsUtility.DrawComboBoxItems(cbFilterBy, e);
        }

        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            clsUtility.FilterDataView(_dataview, cbFilterBy.SelectedItem.ToString(), txtFilter.Text, e);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedItem.ToString() != "None")
                txtFilter.Visible = true;
            else
                txtFilter.Visible = false;
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
            clsUtility.RefreshInformationView(dgvLDLApplications,clsLocalDrivingLicenseApplications.GetLDLApplications().DefaultView);
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
                clsLocalDrivingLicenseApplications LDLApplication = clsLocalDrivingLicenseApplications.Find((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

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
                        int ApplicationID = clsLocalDrivingLicenseApplications.GetApplicationID((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value);
                        if (dgvLDLApplications.SelectedRows[i].Cells["Status"].Value.ToString() == "Completed")
                        {
                            MessageBox.Show($"Local license application who has ID : {Convert.ToInt32(dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value)} is not deleted because it is completed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }

                        if (!clsLocalDrivingLicenseApplications.DeleteLDLApplication((int)dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value, ApplicationID))
                        {
                            MessageBox.Show($"Local license application who has ID : {Convert.ToInt32(dgvLDLApplications.SelectedRows[i].Cells["L.D.L.AppID"].Value)} is not deleted due to a data connected to it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    int ApplicationID = clsLocalDrivingLicenseApplications.GetApplicationID((int)dgvLDLApplications.SelectedRows[0].Cells["L.D.L.AppID"].Value);

                    if (clsApplication.ChangeApplicationStatus(ApplicationID, clsApplication.enApplicationStatus.Canceled))
                        _RefreshLDLApplicationsView();
                    else
                        MessageBox.Show("Failed To Cancel Application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
    }
}
