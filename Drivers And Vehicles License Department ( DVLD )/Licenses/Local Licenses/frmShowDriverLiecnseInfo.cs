using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmShowDriverLiecnseInfo : Form
    {
        private int _LocalLicenseID;
        public frmShowDriverLiecnseInfo(int LocalLicenseID)
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, pbLicenseView);
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            _LocalLicenseID = LocalLicenseID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowDriverLiecnseInfo_Load(object sender, EventArgs e)
        {
            uctrlDriverLicenseInfo.LoadInfo(_LocalLicenseID);
        }
    }
}