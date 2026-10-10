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

        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentLLicDGVColSortDirection;
        private clsDataGridViewUtilityLib.enDataGridViewSortDirection _CurrentIntLicDGVColSortDirection;
        private clsDataGridViewUtilityLib _DgvUtilityLib;

        public ctrlDriverLicensesHistory()
        {
            InitializeComponent();

            _LastBroughtLocalLicenseID = -1;
            _LastBroughtIntLicenseID = -1;

            _LocalLicensesPKViewedColumnName = "Lic.ID";
            _IntLicensesPKViewedColumnName = "Int.License ID";

            _DgvUtilityLib = new clsDataGridViewUtilityLib();
            _CurrentLLicDGVColSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
            _CurrentIntLicDGVColSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
        }

        public void LoadDriverLicenseHistory(int DriverID)
        {
            dgvLocalLicenses.DataSource = clsLocalLicense.GetLocalLicenses(DriverID, clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
            lblLocalLicensesNum.Text = clsDriver.GetDriverLocalLicensesCount(DriverID).ToString();

            dgvInternationalLicenses.DataSource = clsInternationalLicense.GetDriverInternationalLicenses(DriverID, clsDataGridViewUtilityLib.WantedNumOfRowsFromDB);
            lblInternationalLicensesNum.Text = clsDriver.GetDriverInternationalLicensesCount(DriverID).ToString();
            _DriverID = DriverID;
        }

        private void _SetPropertiesToAppendData(DataGridView DGV)
        {
            if (DGV == dgvLocalLicenses)
            {
                if (_LastColumnDGVLocalLicSortedBy == null || _LastColumnDGVLocalLicSortedBy == _LocalLicensesPKViewedColumnName)
                    _LastBroughtLocalLicenseID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvLocalLicenses, _LocalLicensesPKViewedColumnName, _CurrentLLicDGVColSortDirection);

                _DgvUtilityLib.SetNumberOfRowsToOffset(_LocalLicensesPKViewedColumnName, _LastColumnDGVLocalLicSortedBy);
            }

            else
            {
                if (_LastColumnDGVIntLicSortedBy == null || _LastColumnDGVIntLicSortedBy == _IntLicensesPKViewedColumnName)
                    _LastBroughtIntLicenseID = clsDataGridViewUtilityLib.GetLastBroughtValueOfPKColumn(dgvInternationalLicenses, _IntLicensesPKViewedColumnName, _CurrentIntLicDGVColSortDirection);

                _DgvUtilityLib.SetNumberOfRowsToOffset(_IntLicensesPKViewedColumnName, _LastColumnDGVIntLicSortedBy);
            }
        }

        private void _AppendPartOfRemainingData(DataGridView DGV,DataTable PartOfRemainingData)
        {
            clsDataGridViewUtilityLib.enDataGridViewSortDirection CurrentSortDirection;

            if (DGV == dgvLocalLicenses)
                CurrentSortDirection = _CurrentLLicDGVColSortDirection;
            else
                CurrentSortDirection = _CurrentIntLicDGVColSortDirection;

            DataRow[] NewRows = PartOfRemainingData?.Select();

            if (NewRows != null)
                _DgvUtilityLib.AddNewRowsToDgv(DGV,NewRows, clsDataGridViewUtilityLib.GetDgvColumnsNames(DGV), CurrentSortDirection);
        }

        private void _AppendDataToDGV(DataGridView DGV)
        {
            _SetPropertiesToAppendData(DGV);

            DataTable dtPartOfRemainingData;

            if (DGV == dgvLocalLicenses)
            {
                dtPartOfRemainingData = clsLocalLicense.GetLocalLicenses(_DriverID, clsDataGridViewUtilityLib.WantedNumOfRowsFromDB,
                    _LastBroughtLocalLicenseID, _LastColumnDGVLocalLicSortedBy, _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentLLicDGVColSortDirection));

                _AppendPartOfRemainingData(dgvLocalLicenses, dtPartOfRemainingData);
            }

            else
            {
                dtPartOfRemainingData = clsInternationalLicense.GetDriverInternationalLicenses(_DriverID, clsDataGridViewUtilityLib.WantedNumOfRowsFromDB
                    , _LastBroughtIntLicenseID, _LastColumnDGVIntLicSortedBy, _DgvUtilityLib.NumberOfRowsToOffset, clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentIntLicDGVColSortDirection));

                _AppendPartOfRemainingData(dgvInternationalLicenses, dtPartOfRemainingData);
            }
        }

        private void DataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (clsDataGridViewUtilityLib.IsDgvLastRowDisplayed((DataGridView)sender))
                {
                    _AppendDataToDGV((DataGridView)sender);
                }
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (clsDataGridViewUtilityLib.IsDgvLastRowSelected((DataGridView)sender))
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
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvLocalLicenses, ref _LastColumnDGVLocalLicSortedBy, ref _CurrentLLicDGVColSortDirection, e);

            DataTable dtSortedInfo = null;

            dtSortedInfo = clsLocalLicense.GetSortedInfo(_DriverID,clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvLocalLicenses.Columns[e.ColumnIndex].HeaderText
            , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentLLicDGVColSortDirection));

            _DgvUtilityLib.SetDataSourceAfterColumnOrdering(dgvLocalLicenses, dtSortedInfo, e, ref _LastColumnDGVLocalLicSortedBy);
        }

        private void _SortInternationalLicensesData(DataGridViewCellMouseEventArgs e)
        {
            clsDataGridViewUtilityLib.ReverseCurrentDGVSortDirection(dgvInternationalLicenses, ref _LastColumnDGVIntLicSortedBy, ref _CurrentIntLicDGVColSortDirection, e);

            DataTable dtSortedInfo = null;

            dtSortedInfo = clsInternationalLicense.GetSortedInfo(_DriverID,clsDataGridViewUtilityLib.WantedNumOfRowsFromDB, dgvInternationalLicenses.Columns[e.ColumnIndex].HeaderText
            , clsDataGridViewUtilityLib.GetDataGridViewSortDirection(_CurrentIntLicDGVColSortDirection));

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
                _CurrentLLicDGVColSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;

            else
                _CurrentIntLicDGVColSortDirection = clsDataGridViewUtilityLib.enDataGridViewSortDirection.Descending;
        }
    }
}
