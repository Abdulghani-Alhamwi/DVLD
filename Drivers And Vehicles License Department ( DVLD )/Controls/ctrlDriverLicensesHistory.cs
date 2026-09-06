using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Licenses;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlDriverLicensesHistory : UserControl
    {
        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();
        }
        private int _DriverID;
        public void LoadDriverLicenseHistory(int DriverID)
        {
            dgvLocalLicenses.DataSource = clsLocalLicense.GetLocalLicenses(DriverID,clsUtility.WantedNumOfRowsFromDB);
            lblLocalLicensesNum.Text = clsDriver.GetDriverLocalLicensesCount(DriverID).ToString();

            dgvInternationalLicenses.DataSource = clsInternationalLicense.GetDriverInternationalLicenses(DriverID, clsUtility.WantedNumOfRowsFromDB);
            lblInternationalLicensesNum.Text = clsDriver.GetDriverInternationalLicensesCount(DriverID).ToString();
            _DriverID = DriverID;
        }

        private void _AppendPartOfRemainingData(DataGridView Dgv,DataTable PartOfRemainingData)
        {
            DataRow [] NewRows =PartOfRemainingData?.Select();

            if (NewRows != null)
                clsUtility.AddNewRowsToDgv(Dgv, (DataTable)Dgv.DataSource, NewRows, clsUtility.GetDgvColumnsNames(Dgv));
        }

        private void dgvLocalLicenses_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsUtility.IsDgvLastRowDisplayed(dgvLocalLicenses))
                    _AppendPartOfRemainingData(dgvLocalLicenses, clsLocalLicense.GetLocalLicenses(_DriverID, clsUtility.WantedNumOfRowsFromDB, (int)dgvLocalLicenses.Rows[dgvLocalLicenses.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Lic.ID"].Value));
            }
        }

        private void dgvLocalLicenses_KeyDown(object sender, KeyEventArgs e)
        {
                if (clsUtility.IsDgvLastRowSelected(dgvLocalLicenses))
                _AppendPartOfRemainingData(dgvLocalLicenses, clsLocalLicense.GetLocalLicenses(_DriverID, clsUtility.WantedNumOfRowsFromDB, (int)dgvLocalLicenses.Rows[dgvLocalLicenses.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Lic.ID"].Value));
        }

        private void dgvInternationalLicenses_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsUtility.IsDgvLastRowDisplayed(dgvInternationalLicenses))
                    _AppendPartOfRemainingData(dgvInternationalLicenses, clsInternationalLicense.GetDriverInternationalLicenses(_DriverID, clsUtility.WantedNumOfRowsFromDB, (int)dgvInternationalLicenses.Rows[dgvInternationalLicenses.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Int.License ID"].Value));
            }
        }

        private void dgvInternationalLicenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsUtility.IsDgvLastRowSelected(dgvInternationalLicenses))
                _AppendPartOfRemainingData(dgvInternationalLicenses, clsInternationalLicense.GetDriverInternationalLicenses(_DriverID, clsUtility.WantedNumOfRowsFromDB, (int)dgvInternationalLicenses.Rows[dgvInternationalLicenses.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["Int.License ID"].Value));
        }

        private void tsmiShowLicenseInfo_Click(object sender, EventArgs e)
        {
            if(tcLicensesHistory.SelectedTab == tpLocalLicenses)
            {
                frmLocalLicenseDetails frm = new frmLocalLicenseDetails((int)dgvLocalLicenses.SelectedRows[0].Cells["Lic.ID"].Value);
                frm.ShowDialog();
            }
            else
            {
                frmInternationalLicenseDetails frm = new frmInternationalLicenseDetails((int)dgvInternationalLicenses.SelectedRows[0].Cells["Int.License ID"].Value);
                frm.ShowDialog();
            }
        }

        private void cmsLicenseHistory_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tcLicensesHistory.SelectedTab == tpLocalLicenses)
            {
                if (dgvLocalLicenses.Rows.Count == 0)
                    cmsLicenseHistory.Close();

                else if (dgvLocalLicenses.SelectedRows.Count > 1)
                    MessageBox.Show("You can choose only one local license to show its info");
            }
            else
            {
                if (dgvInternationalLicenses.Rows.Count == 0)
                    cmsLicenseHistory.Close();

                else if (dgvInternationalLicenses.SelectedRows.Count > 1)
                    MessageBox.Show("You can choose only one international license to show its info");
            }
        }
    }
}
