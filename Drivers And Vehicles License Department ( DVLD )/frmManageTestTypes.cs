using System;
using System.Windows.Forms;
using System.Xml.Serialization;
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
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
            dgvTestTypes.DataSource = null;
            dgvTestTypes.DataSource = clsTestTypes.GetTestTypes();
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
            dgvTestTypes.DataSource = clsTestTypes.GetTestTypes();
            lblRecordsNumber.Text = dgvTestTypes.Rows.Count.ToString();
            _SetDataViewColumnsWidth();
        }

        private void tsmiEditTestType_Click(object sender, EventArgs e)
        {
            if (dgvTestTypes.Rows.Count == 0)
                MessageBox.Show("There is'nt any test type!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
            {
                frmUpdateTestType frm = new frmUpdateTestType((int)dgvTestTypes.SelectedRows[0].Cells["ID"].Value,
                (string)dgvTestTypes.SelectedRows[0].Cells["Title"].Value, (string)dgvTestTypes.SelectedRows[0].Cells["Description"].Value, Convert.ToDouble(dgvTestTypes.SelectedRows[0].Cells["Fees"].Value));

            frm.ShowDialog();

            _RefreshTestTypesView();
            }
        }
    }
}
