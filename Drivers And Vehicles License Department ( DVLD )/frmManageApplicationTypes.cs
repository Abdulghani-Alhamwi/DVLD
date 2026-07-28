using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmManageApplicationTypes : Form
    {
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dgvApplicationTypes.DataSource = clsApplicationTypes.GetApplicationTypes();
            lblRecordsNumber.Text = dgvApplicationTypes.Rows.Count.ToString();
            dgvApplicationTypes.Font = new Font("Tahoma", 19);         
        }

        private void _RefreshApplicationTypesView()
        {
            dgvApplicationTypes.DataSource = null;
            dgvApplicationTypes.DataSource = clsApplicationTypes.GetApplicationTypes();
        }

        private void tsmiEditApplicationType_Click(object sender, EventArgs e)
        {
            if (dgvApplicationTypes.Rows.Count == 0)
                MessageBox.Show("There is no application types!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (dgvApplicationTypes.SelectedRows.Count > 1)
                MessageBox.Show("Select one application type to edit!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                frmUpdateApplicationType frm = new frmUpdateApplicationType((int)dgvApplicationTypes.SelectedRows[0].Cells["ID"].Value ,
                (string)dgvApplicationTypes.SelectedRows[0].Cells["Title"].Value,Convert.ToString(dgvApplicationTypes.SelectedRows[0].Cells["Fees"].Value));

                frm.ShowDialog();

                _RefreshApplicationTypesView();
            }
        }
    }
}
