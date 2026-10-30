using System;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlDriverLocalLicenseInfo : UserControl
    {
        public clsLocalLicense LicenseInfo;
        public ctrlDriverLocalLicenseInfo()
        {
            InitializeComponent();
        }
        private void LoadPersonImage(clsPerson UpdatedPersonInfo = null)
        {
            string ImagePath;
            if (UpdatedPersonInfo == null)
                ImagePath = LicenseInfo.DriverInfo.PersonalInfo.ImagePath;
            else
                ImagePath = UpdatedPersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonalImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not found image with image path : " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else
                pbPersonalImage.Image = (LicenseInfo.DriverInfo.PersonalInfo.Gendor == clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
        }

        public void LoadInfo(int LocalLicenseID)
        {
            LicenseInfo = clsLocalLicense.Find(LocalLicenseID);

            if (LicenseInfo == null)
            {
                MessageBox.Show("Could not found license with license id : " + LocalLicenseID.ToString()
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            lblLicenseID.Text = LicenseInfo.LicenseID.ToString();
            lblPersonFullName.Text = LicenseInfo.DriverInfo.PersonalInfo.FullName;
            lblNationalNo.Text = LicenseInfo.DriverInfo.PersonalInfo.NationalNo;
            lblGendor.Text = (LicenseInfo.DriverInfo.PersonalInfo.Gendor == clsPerson.enGendor.Male) ? "Male" : "Female";
            lblIsActive.Text = (LicenseInfo.IsActive) ? "Yes" : "No";
            lblIsDetained.Text = (LicenseInfo.IsDetained) ? "Yes" : "No";
            lblClassName.Text = LicenseInfo.LicenseClassInfo.ClassName;
            lblIssueDate.Text = clsFormat.DateToShort(LicenseInfo.IssueDate);
            lblIssueReason.Text = LicenseInfo.IssueReasonText;
            lblDateOfBirth.Text = clsFormat.DateToShort(LicenseInfo.DriverInfo.PersonalInfo.DateOfBirth);
            lblDriverID.Text = LicenseInfo.DriverID.ToString();
            lblExpDate.Text = clsFormat.DateToShort(LicenseInfo.ExpirationDate);
            lblNotes.Text = (LicenseInfo.Notes != "") ? LicenseInfo.Notes : "No Notes";

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