using System;
using System.Data;
using System.Windows.Forms;
using DVLDBusinessLayer;
using MyLib;

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
            lblRecordsNumber.Text = clsLocalLicense.GetTotalDriverLicensesCount(DriverID).ToString();
            _DriverID = DriverID;
        }

        private void _AppendPartOfRemainingData()
        {
            DataRow [] NewRows = clsLocalLicense.GetLocalLicenses(_DriverID, clsUtility.WantedNumOfRowsFromDB, (int)dgvLocalLicenses.Rows[dgvLocalLicenses.Rows.GetLastRow(DataGridViewElementStates.Displayed)].Cells["User ID"].Value)?.Select();

            if (NewRows != null)
                clsUtility.AddNewRowsToDGV(dgvLocalLicenses, (DataTable)dgvLocalLicenses.DataSource, NewRows, clsUtility.GetdgvColumnsNames(dgvLocalLicenses));
        }

        private void dgvLocalLicenses_Scroll(object sender, ScrollEventArgs e)
        {
            if (dgvLocalLicenses.Rows.GetLastRow(DataGridViewElementStates.None) == dgvLocalLicenses.Rows.GetLastRow(DataGridViewElementStates.Displayed))
                _AppendPartOfRemainingData();
        }

        private void dgvLocalLicenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvLocalLicenses.Rows.GetLastRow(DataGridViewElementStates.None) == dgvLocalLicenses.Rows.GetLastRow(DataGridViewElementStates.Selected))
                _AppendPartOfRemainingData();
        }


    }
}
