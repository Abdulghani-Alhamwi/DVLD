using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmInternationalLicenseDetails : Form
    {
        public frmInternationalLicenseDetails(int InternationalLicenseID)
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            clsUtility.CenterControlHorizontally(this, pbLicenseView);

            uctrlInternationalLicenseDetails.LoadInternationalLicenseInfo(InternationalLicenseID);
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
