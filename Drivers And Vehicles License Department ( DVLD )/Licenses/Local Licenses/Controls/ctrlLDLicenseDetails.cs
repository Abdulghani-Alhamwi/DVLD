using System;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlDriverLocalLicenseDetails : UserControl
    {
        public clsLocalLicense License;
        public ctrlDriverLocalLicenseDetails()
        {
            InitializeComponent();
        }

        private void LoadPersonImage(clsPerson UpdatedPersonInfo = null)
        {
            string ImagePath;
            if (UpdatedPersonInfo == null)
                ImagePath = License.DriverInfo.PersonalInfo.ImagePath;
            else
                ImagePath = UpdatedPersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonalImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not found image with image path : " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
                pbPersonalImage.Image = (License.DriverInfo.PersonalInfo.Gendor == clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
        }

        public void LoadInfo(int LocalLicenseID)
        {
            License = clsLocalLicense.Find(LocalLicenseID);

            if (License == null)
            {
                MessageBox.Show("Could not found license with license id : " + License.LicenseID.ToString()
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblLicenseID.Text = License.LicenseID.ToString();
            lblPersonFullName.Text = License.DriverInfo.PersonalInfo.FullName;
            lblNationalNo.Text = License.DriverInfo.PersonalInfo.NationalNo;
            lblGendor.Text = (License.DriverInfo.PersonalInfo.Gendor == clsPerson.enGendor.Male) ? "Male" : "Female";
            lblIsActive.Text = (License.IsActive) ? "Yes" : "No";
            lblIsDetained.Text = (License.IsDetained) ? "Yes" : "No";
            lblClassName.Text = License.LicenseClassInfo.ClassName;
            lblIssueDate.Text = clsFormat.DateToShort(License.IssueDate);
            lblIssueReason.Text = License.IssueReasonText;
            lblDateOfBirth.Text = clsFormat.DateToShort(License.DriverInfo.PersonalInfo.DateOfBirth);
            lblDriverID.Text = License.DriverID.ToString();
            lblExpDate.Text = clsFormat.DateToShort(License.ExpirationDate);
            lblNotes.Text = (License.Notes != "") ? License.Notes : "No Notes";

        }

        public void EditDriverPersonalInfo(clsPerson UpdatedPersonalInfo)
        {
            lblPersonFullName.Text = UpdatedPersonalInfo.FullName;
            lblNationalNo.Text = UpdatedPersonalInfo.NationalNo;
            lblGendor.Text = (UpdatedPersonalInfo.Gendor == clsPerson.enGendor.Male) ? "Male" : "Female";
            lblDateOfBirth.Text = UpdatedPersonalInfo.DateOfBirth.ToString(clsFormat.GetCustomDateFormat(clsFormat.enCustomDateFormat.DateAppreviatedMonthName));

            LoadPersonImage(UpdatedPersonalInfo);
        }
    }
}