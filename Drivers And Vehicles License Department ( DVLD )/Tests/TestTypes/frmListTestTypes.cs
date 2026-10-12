using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmListTestTypes : Form
    {
        private clsDataGridViewUtilityLib _DgvUtilityLib;

        public frmListTestTypes()
        {
            InitializeComponent();
            _DgvUtilityLib = new clsDataGridViewUtilityLib();
        }

        private void SetDGVColumnsWidth()
        {
            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns["ID"].Width = 80;
                dgvTestTypes.Columns["Title"].Width = 220;
                dgvTestTypes.Columns["Description"].Width = 400;
                dgvTestTypes.Columns["Fees"].Width = 120;
            }
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypes.DataSource = clsTestType.GetTestTypes();
            lblRecordsNumber.Text = dgvTestTypes.Rows.Count.ToString();
            SetDGVColumnsWidth();
        }

        private void EditDGVRowData(object[] NewValues, byte DGVRowIndex)
        {
            _DgvUtilityLib.EditFullDataRowInDgv(dgvTestTypes, NewValues, DGVRowIndex);
        }

        private void tsmiEditTestType_Click(object sender, EventArgs e)
        {
            if (dgvTestTypes.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one test type to edit its info!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)(Convert.ToByte(dgvTestTypes.SelectedRows[0].Cells["ID"].Value)),
                (string)dgvTestTypes.SelectedRows[0].Cells["Title"].Value, (string)dgvTestTypes.SelectedRows[0].Cells["Description"].Value, Convert.ToDecimal(dgvTestTypes.SelectedRows[0].Cells["Fees"].Value), (byte)dgvTestTypes.SelectedRows[0].Index);

                frm.AfterUpdatingInfo += EditDGVRowData;

                frm.ShowDialog();
            }
        }

        private void cmsApplicationTypes_Paint(object sender, PaintEventArgs e)
        {
            if (dgvTestTypes.Rows.Count == 0)
            {
                cmsApplicationTypes.Close();
                MessageBox.Show("There is'nt any test type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
