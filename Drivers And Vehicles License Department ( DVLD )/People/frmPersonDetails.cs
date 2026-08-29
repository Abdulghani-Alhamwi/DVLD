using System;
using System.Windows.Forms;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmPersonDetails : Form
    {
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();

            if (UctrlPersonDetails.LoadPersonDetails(PersonID) == null)
                this.Close();

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
