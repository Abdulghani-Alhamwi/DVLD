using System;
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

        public void LoadDriverLicenseHistory(int DriverID)
        {
            dgvLocalLicenses.DataSource = clsLocalLicense.GetLocalLicenses(DriverID,clsUtility.WantedNumOfRowsFromDB);
            lblRecordsNumber.Text = clsLocalLicense.GetTotalDriverLicensesCount(DriverID).ToString();
        }
    }
}
