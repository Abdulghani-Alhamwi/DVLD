using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmLocalDrivingLicenseApplications : Form
    {
        public frmLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            //dgvLDLApplications.DataSource = clsLocalDrivingLicenseApplications
        }
    }
}
