using System;
using System.Windows.Forms;

namespace DVLDPresentationLayer
{
    public partial class frmUserDetails : Form
    {
        public frmUserDetails(int UserID)
        {
            InitializeComponent();

            if (!uctrlUserDetails.LoadUserInformation(UserID))
                this.Close();
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
