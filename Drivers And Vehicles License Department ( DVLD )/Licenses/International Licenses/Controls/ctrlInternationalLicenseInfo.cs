using System;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        private clsInternationalLicense _InternationalLicense;
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void LoadPersonImage()
        {
            string ImagePath = _InternationalLicense.DriverInfo.PersonalInfo.ImagePath;
            
            if (!string.IsNullOrEmpty(ImagePath))
                if (File.Exists(ImagePath))
                    pbPersonalImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not found image with image path : " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
                pbPersonalImage.Image = (_InternationalLicense.DriverInfo.PersonalInfo.Gendor == clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
        }

        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (_InternationalLicense != null)
            {
                lblIntLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
                lblPersonFullName.Text = _InternationalLicense.PersonInfo.FullName;
                lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
                lblNationalNo.Text = _InternationalLicense.PersonInfo.NationalNo;
                lblGendor.Text = (_InternationalLicense.PersonInfo.Gendor == clsPerson.enGendor.Male) ? "Male" : "Female";
                lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
                lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
                lblIsActive.Text = (_InternationalLicense.IsActive) ? "Yes" : "No";
                lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.PersonInfo.DateOfBirth);
                lblDriverID.Text = _InternationalLicense.DriverID.ToString();
                lblExpDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);

                LoadPersonImage();
            }
            else
                MessageBox.Show("International License Is Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
