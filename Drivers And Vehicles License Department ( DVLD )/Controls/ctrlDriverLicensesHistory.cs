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
        private int _DriverID;

        private string _LastColumnDGVLocalLicSortedBy;
        private string _LastColumnDGVIntLicSortedBy;

        private string _LocalLicensesPKViewedColumnName;
        private int _LastBroughtLocalLicenseID;

        private string _IntLicensesPKViewedColumnName;
        private int _LastBroughtIntLicenseID;

        private clsDgvUtilityLib.enDataGridViewSortDirection _CurrentDGVLocalLicSortDirection;
        private clsDgvUtilityLib.enDataGridViewSortDirection _CurrentDGVIntLicSortDirection;
        private clsDgvUtilityLib _DgvUtilityLib;

        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();

            _LastBroughtLocalLicenseID = -1;
            _LastBroughtIntLicenseID = -1;

            _LocalLicensesPKViewedColumnName = "Lic.ID";
            _IntLicensesPKViewedColumnName = "Int.License ID";

            _DgvUtilityLib = new clsDgvUtilityLib();
            _CurrentDGVLocalLicSortDirection = clsDgvUtilityLib.enDataGridViewSortDirection.Descending;
            _CurrentDGVIntLicSortDirection = clsDgvUtilityLib.enDataGridViewSortDirection.Descending;
        }

        public void LoadDriverLicenseHistory(int DriverID)
        {
            dgvLocalLicenses.DataSource = clsLocalLicense.GetLocalLicenses(DriverID, clsDgvUtilityLib.WantedNumOfRowsFromDB);
            lblLocalLicensesNum.Text = clsDriver.GetDriverLocalLicensesCount(DriverID).ToString();

            dgvInternationalLicenses.DataSource = clsInternationalLicense.GetDriverInternationalLicenses(DriverID, clsDgvUtilityLib.WantedNumOfRowsFromDB);
            lblInternationalLicensesNum.Text = clsDriver.GetDriverInternationalLicensesCount(DriverID).ToString();
            _DriverID = DriverID;
        }

        private void _SetPropertiesToAppendData(DataGridView DGV)
        {
            if (DGV == dgvLocalLicenses)
            {
                if (_LastColumnDGVLocalLicSortedBy == null || _LastColumnDGVLocalLicSortedBy == _LocalLicensesPKViewedColumnName)
                    _LastBroughtLocalLicenseID = clsDgvUtilityLib.GetLastBroughtValueOfPKColumn(dgvLocalLicenses, _LocalLicensesPKViewedColumnName, _CurrentDGVLocalLicSortDirection);

                _DgvUtilityLib.SetNumberOfRowsToOffset(_LocalLicensesPKViewedColumnName, _LastColumnDGVLocalLicSortedBy);
            }

            else
            {
                if (_LastColumnDGVIntLicSortedBy == null || _LastColumnDGVIntLicSortedBy == _IntLicensesPKViewedColumnName)
                    _LastBroughtIntLicenseID = clsDgvUtilityLib.GetLastBroughtValueOfPKColumn(dgvInternationalLicenses, _IntLicensesPKViewedColumnName, _CurrentDGVIntLicSortDirection);

                _DgvUtilityLib.SetNumberOfRowsToOffset(_IntLicensesPKViewedColumnName, _LastColumnDGVIntLicSortedBy);
            }
        }

        private void _AppendPartOfRemainingData(DataGridView DGV,DataTable PartOfRemainingData)
        {
            clsDgvUtilityLib.enDataGridViewSortDirection CurrentSortDirection;

            if (DGV == dgvLocalLicenses)
                CurrentSortDirection = _CurrentDGVLocalLicSortDirection;
            else
                CurrentSortDirection = _CurrentDGVIntLicSortDirection;

            DataRow[] NewRows = PartOfRemainingData?.Select();

            if (NewRows != null)
                _DgvUtilityLib.AddNewRowsToDgv(DGV,NewRows, clsDgvUtilityLib.GetDgvColumnsNames(DGV), CurrentSortDirection);
        }

        private void _AppendDataToDGV(DataGridView DGV)
        {
            _SetPropertiesToAppendData(DGV);

            DataTable dtPartOfRemainingData;

            if (DGV == dgvLocalLicenses)
            {
                dtPartOfRemainingData = clsLocalLicense.GetLocalLicenses(_DriverID, clsDgvUtilityLib.WantedNumOfRowsFromDB,
                    _LastBroughtLocalLicenseID, _LastColumnDGVLocalLicSortedBy, _DgvUtilityLib.NumberOfRowsToOffset, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDGVLocalLicSortDirection));

                _AppendPartOfRemainingData(dgvLocalLicenses, dtPartOfRemainingData);
            }

            else
            {
                dtPartOfRemainingData = clsInternationalLicense.GetDriverInternationalLicenses(_DriverID, clsDgvUtilityLib.WantedNumOfRowsFromDB
                    , _LastBroughtIntLicenseID, _LastColumnDGVIntLicSortedBy, _DgvUtilityLib.NumberOfRowsToOffset, clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDGVIntLicSortDirection));

                _AppendPartOfRemainingData(dgvInternationalLicenses, dtPartOfRemainingData);
            }
        }

        private void DataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDgvUtilityLib.IsDgvLastRowDisplayed((DataGridView)sender))
                {
                    _AppendDataToDGV((DataGridView)sender);
                }
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDgvUtilityLib.IsDgvLastRowSelected((DataGridView)sender))
            {
                _AppendDataToDGV((DataGridView)sender);
            }
        }

        private void tsmiShowLicenseInfo_Click(object sender, EventArgs e)
        {
            if(tcLicenseHistory.SelectedTab == tpLocalLicenses)
            {
                frmLocalLicenseDetails frm = new frmLocalLicenseDetails((int)dgvLocalLicenses.SelectedRows[0].Cells[_LocalLicensesPKViewedColumnName].Value);
                frm.ShowDialog();
            }
            else
            {
                frmInternationalLicenseDetails frm = new frmInternationalLicenseDetails((int)dgvInternationalLicenses.SelectedRows[0].Cells[_IntLicensesPKViewedColumnName].Value);
                frm.ShowDialog();
            }
        }

        private void cmsLicenseHistory_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tcLicenseHistory.SelectedTab == tpLocalLicenses)
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

        private void _SortLocalLicensesDataData(DataGridViewCellMouseEventArgs e)
        {
            _CurrentDGVLocalLicSortDirection = clsDgvUtilityLib.ReverseCurrentDgvSortDirection(_CurrentDGVLocalLicSortDirection);

            DataTable dtSortedInfo = null;

            dtSortedInfo = clsLocalLicense.GetSortedInfo(_DriverID,clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvLocalLicenses.Columns[e.ColumnIndex].HeaderText
            , clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDGVLocalLicSortDirection));

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvLocalLicenses, dtSortedInfo, e, ref _LastColumnDGVLocalLicSortedBy);
        }

        private void _SortInternationalLicensesData(DataGridViewCellMouseEventArgs e)
        {
            _CurrentDGVIntLicSortDirection = clsDgvUtilityLib.ReverseCurrentDgvSortDirection(_CurrentDGVIntLicSortDirection);

            DataTable dtSortedInfo = null;

            dtSortedInfo = clsInternationalLicense.GetSortedInfo(_DriverID,clsDgvUtilityLib.WantedNumOfRowsFromDB, dgvInternationalLicenses.Columns[e.ColumnIndex].HeaderText
            , clsDgvUtilityLib.GetDataGridViewSortDirection(_CurrentDGVIntLicSortDirection));

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvInternationalLicenses, dtSortedInfo, e, ref _LastColumnDGVIntLicSortedBy);
        }

        private void DataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((DataGridView)sender == dgvLocalLicenses)
                _SortLocalLicensesDataData(e);

            else
                _SortInternationalLicensesData(e);
        }

        private void tcLicenseHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcLicenseHistory.SelectedTab == tpLocalLicenses)
                _CurrentDGVLocalLicSortDirection = clsDgvUtilityLib.enDataGridViewSortDirection.Descending;

            else
                _CurrentDGVIntLicSortDirection = clsDgvUtilityLib.enDataGridViewSortDirection.Descending;
        }
    }
}
