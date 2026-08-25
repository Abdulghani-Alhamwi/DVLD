using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

namespace DVLDPresentationLayer
{
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshTestTypesView()
        {
            dgvTestTypes.DataSource = clsTestType.GetTestTypes();
            _SetDataViewColumnsWidth();
        }

        private void _SetDataViewColumnsWidth()
        {
            dgvTestTypes.Columns["ID"].FillWeight = 100;
            dgvTestTypes.Columns["Title"].FillWeight = 250  ;
            dgvTestTypes.Columns["Description"].FillWeight = 350;
            dgvTestTypes.Columns["Fees"].FillWeight = 150;
        } 
        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            dgvTestTypes.DataSource = clsTestType.GetTestTypes();
            lblRecordsNumber.Text = dgvTestTypes.Rows.Count.ToString();
            //_SetDataViewColumnsWidth();
        }

        private void EditDGVRowData(object[] NewValues, byte DGVRowIndex)
        {
            clsUtility.EditFullDataRowInDGV(dgvTestTypes, (DataTable)dgvTestTypes.DataSource, ref NewValues, DGVRowIndex);
        }

        private void tsmiEditTestType_Click(object sender, EventArgs e)
        {
            if (dgvTestTypes.SelectedRows.Count > 1)
                MessageBox.Show("Please select only one test type to edit its info!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                frmUpdateTestType frm = new frmUpdateTestType(Convert.ToByte(dgvTestTypes.SelectedRows[0].Cells["ID"].Value),
                (string)dgvTestTypes.SelectedRows[0].Cells["Title"].Value, (string)dgvTestTypes.SelectedRows[0].Cells["Description"].Value, Convert.ToDouble(dgvTestTypes.SelectedRows[0].Cells["Fees"].Value), (byte)dgvTestTypes.SelectedRows[0].Index);

                frm.AfterUpdatingInfo += EditDGVRowData;

            frm.ShowDialog();

            _RefreshTestTypesView();
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
    }
}
