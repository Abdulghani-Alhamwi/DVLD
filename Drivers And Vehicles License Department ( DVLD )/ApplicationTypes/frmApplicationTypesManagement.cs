using System;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmApplicationTypesManagement : Form
    {
        private clsUtility _UtilityLib;
        public frmApplicationTypesManagement()
        {
            InitializeComponent();
            _UtilityLib = new clsUtility();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            dgvApplicationTypes.DataSource = clsApplicationType.GetApplicationTypes();
            lblRecordsNumber.Text = dgvApplicationTypes.Rows.Count.ToString();
            dgvApplicationTypes.Font = new Font("Tahoma", 19);         
        }

        private void EditDGVRowData(object[] NewValues,byte DGVRowIndex)
        {
            _UtilityLib.EditFullDataRowInDgv(dgvApplicationTypes, NewValues, DGVRowIndex);
        }

        private void tsmiEditApplicationType_Click(object sender, EventArgs e)
        {
            if (dgvApplicationTypes.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one application type to edit its info!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                frmUpdateApplicationType frm = new frmUpdateApplicationType(Convert.ToByte(dgvApplicationTypes.SelectedRows[0].Cells["ID"].Value) ,
                dgvApplicationTypes.SelectedRows[0].Cells["Title"].Value.ToString(),dgvApplicationTypes.SelectedRows[0].Cells["Fees"].Value.ToString(), (byte)dgvApplicationTypes.SelectedRows[0].Index);
                frm.AfterUpdatingInfo += EditDGVRowData;
                frm.ShowDialog();
            }
        }

        private void cmsApplicationTypes_Paint(object sender, PaintEventArgs e)
        {
            if (dgvApplicationTypes.Rows.Count == 0)
            {
                cmsApplicationTypes.Close();
                MessageBox.Show("There is'nt any application type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
