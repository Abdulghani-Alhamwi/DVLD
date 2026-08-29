using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer.Controls
{
    public partial class frmApplicationDetails : Form
    {
        public frmApplicationDetails(int LDLApplicationID)
        {
            InitializeComponent();
            clsUtility.CenterControlHorizontally(this, lblFormBigTitle);
            uctrlDLApplicationInfo.LoadLDLAppInfo(LDLApplicationID);
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
