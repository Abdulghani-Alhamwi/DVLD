using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmDriverLicenseHistory : Form
    {
        public frmDriverLicenseHistory(int PersonID)
        {
            InitializeComponent();
            uctrlPersonDetailsByFilter.SearchForPerson(PersonID);
            uctrlDriverLicensesHistory.LoadDriverLicenseHistory(clsDriver.GetDriverID(PersonID));
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
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
