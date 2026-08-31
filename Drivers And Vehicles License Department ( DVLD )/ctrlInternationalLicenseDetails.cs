using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class ctrlInternationalLicenseDetails : UserControl
    {
        public ctrlInternationalLicenseDetails()
        {
            InitializeComponent();
        }

        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            clsInternationalLicense InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (InternationalLicense != null)
            {
                clsPerson Person = clsPerson.Find(clsDriver.GetDriverPersonID(InternationalLicense.DriverID));

                lblPersonFullName.Text = Person.FullName;
                lblLocalLicenseID.Text = InternationalLicense.IssuesUsingLocalLicenseID.ToString();
                lblNationalNo.Text = Person.NationalNo;
                lblGendor.Text = (Person.Gendor == clsPerson.enGendor.Male) ? "Male" : "Female";
                lblIssueDate.Text = InternationalLicense.IssueDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
                lblIsActive.Text = (InternationalLicense.IsActive) ? "Yes" : "No";
                lblDateOfBirth.Text = Person.DateOfBirth.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));
                lblDriverID.Text = InternationalLicense.DriverID.ToString();
                lblExpDate.Text = InternationalLicense.ExpirationDate.ToString(clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateAppreviatedMonthName));

                if (File.Exists(Person.ImagePath))
                    pbPersonalImage.ImageLocation = Person.ImagePath;
            }
            else
                MessageBox.Show("International License Is Not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
