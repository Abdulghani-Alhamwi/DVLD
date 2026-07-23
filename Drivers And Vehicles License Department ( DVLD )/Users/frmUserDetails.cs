using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
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
