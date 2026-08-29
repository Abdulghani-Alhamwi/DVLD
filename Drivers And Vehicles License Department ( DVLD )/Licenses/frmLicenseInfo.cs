using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmLicenseInfo : Form
    {
        public frmLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, pbLicenseView);
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);

            uctrlDriverLicenseInfo.LoadDriverLicenseInfo(LicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
