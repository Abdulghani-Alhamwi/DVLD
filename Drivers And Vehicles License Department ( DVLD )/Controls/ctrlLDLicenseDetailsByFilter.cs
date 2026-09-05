using System;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class ctrlLDLicenseDetailsByFilter : UserControl
    {
        public delegate void SelectedLocalLicense(clsLocalLicense LocalLicenseInfo);
        public event SelectedLocalLicense OnSelectedLocalLicense;
        public ctrlLDLicenseDetailsByFilter()
        {
            InitializeComponent();
        }
        private void btnSearchForLicense_Click(object sender, EventArgs e)
        {
            if (txtLicenseID.Text != "")
            {
               clsLocalLicense LocalLicenseInfo = clsLocalLicense.Find(Convert.ToInt32(txtLicenseID.Text));

                if (LocalLicenseInfo != null)
                {
                        uctrlLDLDetails.LoadDriverLicenseInfo(LocalLicenseInfo.LicenseID);
                        OnSelectedLocalLicense?.Invoke(LocalLicenseInfo);
                }
                else
                    MessageBox.Show("There is no local license for the entered ID!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("No entered ID!", "Enter License ID First", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
