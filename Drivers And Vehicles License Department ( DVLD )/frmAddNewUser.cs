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
    public partial class frmAddNewUser : Form
    {
        public frmAddNewUser()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(uctrlpersonInfoByFilter.FindPerson())
            tcAddNewUser.SelectedTab = tpLoginInfo;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
