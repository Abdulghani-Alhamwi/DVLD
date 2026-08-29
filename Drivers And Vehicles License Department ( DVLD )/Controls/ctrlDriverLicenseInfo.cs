using System;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlDriverLicenseInfo : UserControl
    {
        public ctrlDriverLicenseInfo()
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
                lblIssueDate.Text = License.IssueDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblIssueReason.Text = License.GetIssueReasonAsString();
                lblNotes.Text = (License.Notes != null) ? License.Notes : "No Notes";
                lblIsActive.Text = (License.IsActive) ? "Yes" : "No";
                lblDateOfBirth.Text = Person.DateOfBirth.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblDriverID.Text = License.DriverID.ToString();
                lblExpDate.Text =  License.ExpirationDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblIsDetained.Text = (clsDetainedLicenses.IsDetainedLicense(License.LicenseID)) ? "Yes" : "No";

                if (File.Exists(Person.ImagePath))
                    pbPersonalImage.ImageLocation = Person.ImagePath;
            }
        }
    }
}
