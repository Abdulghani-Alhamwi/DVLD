using System;
using System.IO;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class ctrlPersonDetails : UserControl
    {

        public ctrlPersonDetails()
        {
            InitializeComponent();
        }
        clsPerson _Person;
        public void LoadPersonDetails(int PersonID)
        {

            _Person = clsPerson.Find(PersonID);

            if (_Person != null)
            {
                _ShowPersonDetails();
            }
            else
                MessageBox.Show("Person is not found!", "Details", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public void LoadPersonDetails(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person != null)
            {
                _ShowPersonDetails();
            }
            else
                MessageBox.Show("Person is not found!", "Details", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void _ShowPersonDetails()
        {
            lblPersonID.Text = _Person.PersonID.ToString();

            if (_Person.ThirdName != "")
                lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " +
                               _Person.ThirdName + " " + _Person.LastName;
            else
                lblName.Text = _Person.FirstName + " " + _Person.SecondName
                               + " " + _Person.LastName;

            lblNationalNo.Text = _Person.NationalNo;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();

            if (_Person.Gendor == clsPerson.enGendor.Female)
                pbGendor.Image = Resources.Woman_32;

            lblGendor.Text = (_Person.Gendor == clsPerson.enGendor.Male) ? "Male" : "Female";

            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;

            lblCountry.Text = _Person.CountryName;
            lblAddress.Text = _Person.Address;

            if (File.Exists(_Person.ImagePath))
            {
                pbPersonalImage.ImageLocation = _Person.ImagePath;
            }
            else
                pbPersonalImage.Image = (_Person.Gendor == clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
        }

        private void _UpdatePersonData()
        {
            if (_Person != null)
            _Person = clsPerson.Find(_Person.PersonID);

            if (_Person != null)
                _ShowPersonDetails();
            else
                MessageBox.Show("Person is not found!", "Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void lnlblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Person != null)
            {
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_Person);
                
                frm.ShowDialog();
                _UpdatePersonData();
            }
            else
                MessageBox.Show("There is no person to edit their details!", "Problem Details", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }
}
