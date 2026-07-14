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
        clsPeople _Person;
        public void LoadPersonDetails(int PersonID)
        {

            _Person = clsPeople.Find(PersonID);

            if (_Person != null)
            {
                _ShowPersonDetails();
            }
            else
                MessageBox.Show("Person is not found!", "Details", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void _ShowPersonDetails()
        {
            lblPersonID.Text = _Person.ID.ToString();
            lblName.Text = _Person.FirstName+ " "+ _Person.SecondName+ " "+
                           _Person.ThirdName+" "+ _Person.LastName;
            lblNationalNo.Text = _Person.NationalNo;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();

            if (_Person.Gendor == clsPeople.enGendor.Female)
                pbGendor.Image = Resources.Woman_32;


            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;

            lblCountry.Text = _Person.CountryName;
            lblAddress.Text = _Person.Address;

            if (File.Exists(_Person.ImagePath))
            {
                pbPersonalImage.ImageLocation = _Person.ImagePath;
            }
            else
                pbPersonalImage.Image = (_Person.Gendor == clsPeople.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
        }

        private void _UpdatePersonData()
        {
            if (_Person != null)
            _Person = clsPeople.Find(_Person.ID);

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
