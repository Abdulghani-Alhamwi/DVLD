using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer.Licenses
{
    public partial class frmInternationalLicenseInfo : Form
    {
        int _InternationalLicenseID;
        public frmInternationalLicenseInfo(int InternationalLicenseID)
        {
            InitializeComponent();
            _InternationalLicenseID = InternationalLicenseID;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            clsUtility.CenterControlHorizontally(this, pbLicenseView);

            uctrlInternationalLicenseDetails.LoadInfo(_InternationalLicenseID);
        }
    }
}
