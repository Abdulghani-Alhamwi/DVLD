using System;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlLDLicenseDetails : UserControl
    {
        public ctrlLDLicenseDetails()
        {
            InitializeComponent();
        }

        internal void LoadDriverLicenseInfo(int LicenseID)
        {
            clsLocalLicense License = clsLocalLicense.Find(LicenseID);

            if(License != null)
            {
                clsPerson Person = clsPerson.Find(clsDriver.GetDriverPersonID(License.DriverID));

                lblClassName.Text = clsLicenseClass.GetLicenseClassName(License.LicenseClassID);
                lblPersonFullName.Text = Person.FullName;
                lblLicenseID.Text = License.LicenseID.ToString();
                lblNationalNo.Text = Person.NationalNo;
                lblGendor.Text = (Person.Gendor == clsPerson.enGendor.Male) ? "Male" : "Female";
                lblIssueDate.Text = License.IssueDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblIssueReason.Text = License.GetIssueReasonAsString();
                lblNotes.Text = (License.Notes != null) ? License.Notes : "No Notes";
                lblIsActive.Text = (License.IsActive) ? "Yes" : "No";
                lblDateOfBirth.Text = Person.DateOfBirth.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblDriverID.Text = License.DriverID.ToString();
                lblExpDate.Text =  License.ExpirationDate.ToString(clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblIsDetained.Text = (clsDetainedLicenses.IsDetainedLicense(License.LicenseID)) ? "Yes" : "No";

                if (File.Exists(Person.ImagePath))
                    pbPersonalImage.ImageLocation = Person.ImagePath;
                else
                    pbPersonalImage.Image = (Person.Gendor == clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
            }
        }
    }
}