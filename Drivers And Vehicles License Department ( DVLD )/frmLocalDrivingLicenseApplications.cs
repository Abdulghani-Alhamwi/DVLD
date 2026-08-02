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
            clsUtility.RefreshInformationView(dgvLDLApplications,clsLocalDrivingLicenseApplications.GetLDLApplications());
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

    }
}
