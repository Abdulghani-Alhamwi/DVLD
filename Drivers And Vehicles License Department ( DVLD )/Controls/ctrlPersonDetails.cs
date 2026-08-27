using System;
using System.IO;
using System.Windows.Forms;
using DVLDPresentationLayer.Properties;
using DVLDBusinessLayer;

namespace DVLDPresentationLayer
{
    public partial class ctrlPersonDetails : UserControl
    {
        public event Action AfterEditingPersonInfo;
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }
        clsPerson _Person;
        bool _ShownDefaultValues = false;
        public clsPerson LoadPersonDetails (string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person != null)
            {
                _ShowPersonDetails();
                return _Person;
            }
            else
            {
                _ShowDefaultDetails();
                
                MessageBox.Show($"No Person With National No. = {NationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public clsPerson LoadPersonDetails(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person != null)
            {
                _ShowPersonDetails();
                return _Person;
            }

            else
            {
                _ShowDefaultDetails();
                MessageBox.Show($"No Person With Person ID = {PersonID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }
        private void _ShowDefaultDetails()
        {
            if (!_ShownDefaultValues)
            {
                lblPersonID.Text = "????";

                lblName.Text = "????";

                lblNationalNo.Text = "????";
                lblDateOfBirth.Text = "????";

                pbGendor.Image = Resources.Man_32;

                lblGendor.Text = "????";

                lblPhone.Text = "????";
                lblEmail.Text = "????";

                lblCountry.Text = "????";
                lblAddress.Text = "????";

                pbPersonalImage.Image = Resources.Male_512;
            }

            _ShownDefaultValues = true;
            lnlblEditPersonInfo.Enabled = false;
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

            lnlblEditPersonInfo.Enabled = true;
            _ShownDefaultValues = false;
        }

        private void _RefreshView()
        {
            AfterEditingPersonInfo?.Invoke();
        }

        private void lnlblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_Person != null)
            {
                frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_Person);
                frm.AfterEditingPersonInfo += _RefreshView;
                frm.ShowDialog();
                _ShowPersonDetails();

                
            }
            else
                MessageBox.Show("Person is not found!", "Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void gbPersonInformation_Enter(object sender, EventArgs e)
        {

        }
    }
}
