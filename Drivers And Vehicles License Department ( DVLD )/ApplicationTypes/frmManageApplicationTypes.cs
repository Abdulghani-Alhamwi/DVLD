using System;
using System.Drawing;
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

        private void tsmiEditApplicationType_Click(object sender, EventArgs e)
        {
            if (dgvApplicationTypes.Rows.Count == 0)
                MessageBox.Show("There is'nt any application type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                frmUpdateApplicationType frm = new frmUpdateApplicationType((byte)dgvApplicationTypes.SelectedRows[0].Cells["ID"].Value ,
                (string)dgvApplicationTypes.SelectedRows[0].Cells["Title"].Value,Convert.ToString(dgvApplicationTypes.SelectedRows[0].Cells["Fees"].Value));

                frm.ShowDialog();

                dgvApplicationTypes.DataSource = clsApplicationTypes.GetApplicationTypes();
            }
        }
    }
}
