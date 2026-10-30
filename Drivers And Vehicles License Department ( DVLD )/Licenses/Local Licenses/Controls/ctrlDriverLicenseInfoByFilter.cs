using System;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlDriverLicenseInfoByFilter : UserControl
    {
        public event Action<int> OnSelectedLicense;
        public bool FilterEnabled
        {
            get { return gbFilter.Enabled; }

            set
            {
                gbFilter.Enabled = value;
            }
        }
        public clsLocalLicense SelectedLicenseInfo { get { return uctrlLocalDrivingLicenseInfo.LicenseInfo; } }

        public ctrlDriverLicenseInfoByFilter()
        {
            InitializeComponent();
        }

        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnSelectedLicense;

            if(handler != null)
            {
                handler(LicenseID);
            }
        }

        public void LoadInfo(int LocalLicenseID)
        {
            if (SelectedLicenseInfo == null)
                txtLicenseID.Text = LocalLicenseID.ToString();

            uctrlLocalDrivingLicenseInfo.LoadInfo(LocalLicenseID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fields has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadInfo(int.Parse(txtLicenseID.Text.Trim()));

            if (SelectedLicenseInfo != null)
            {
                LicenseSelected(SelectedLicenseInfo.LicenseID);
            }
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if(e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }

        private void txtLicenseID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                clsValidation.EnableErrorProvider(ertxtBoxInput, txtLicenseID, "This field is required!", e);
            }
            else
                clsValidation.DisableErrorProvider(ertxtBoxInput, txtLicenseID);
        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }
    }
}
