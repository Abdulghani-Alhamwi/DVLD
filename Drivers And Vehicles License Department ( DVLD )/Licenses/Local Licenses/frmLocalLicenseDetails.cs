using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmLocalLicenseDetails : Form
    {
        public frmLocalLicenseDetails(int LocalLicenseID)
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, pbLicenseView);
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);

            uctrlDriverLicenseInfo.LoadInfo(LocalLicenseID);
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