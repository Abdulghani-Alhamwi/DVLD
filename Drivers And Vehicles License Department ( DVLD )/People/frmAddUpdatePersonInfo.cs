using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmAddUpdatePersonInfo : Form
    {
        internal delegate void AddedNewPersonEventHandler (int PersonID);
        internal event AddedNewPersonEventHandler AfterAddingNewPerson;
        internal event Action<clsPerson> AfterEditingPersonInfo;

        internal delegate void SavedNewInfo(object[] NewPersonDetails);
        internal delegate void SavedEditedInfo(object[] ModifiedPersonDetails,int PeopleDgvRowIndex);
        internal event SavedNewInfo AfterSavingNewInfo;
        internal event SavedEditedInfo AfterSavingEditedInfo;

        private enum _enMode { AddNew = 0, Update = 1}
        private _enMode _CurrentMode;

        private clsPerson _Person;
        private int _PeopleDGVRowIndex = -1;

        public frmAddUpdatePersonInfo()
        {
            InitializeComponent();
            _InitializeFormData(null);
            _Person = new clsPerson("", "", "", "", "", DateTime.Now, 0, "", "", "", 0, "");
        }

        public frmAddUpdatePersonInfo(clsPerson PersonInfo,int PeopleDGVRowIndex = -1)
        {
            InitializeComponent();
            _InitializeFormData(PersonInfo);
            _PeopleDGVRowIndex = PeopleDGVRowIndex;
            _CurrentMode = _enMode.Update;
        }

        private void _InitializeFormData(clsPerson PersonInfo)
        {
            if (PersonInfo == null)
                _SetTitles(clsPerson.enMode.AddNew);

            else
            {
                this._Person = PersonInfo;
                _SetTitles(clsPerson.enMode.Update);
            }

            clsGeneralUtility.CenterControlHorizontally(this, lblFormBigTitle);
        }

        private void _SetTitles(clsPerson.enMode Mode)
        {
            if (Mode == clsPerson.enMode.AddNew)
            {
                lblFormTitle.Text = "Add New Person";
                lblFormBigTitle.Text = "Add New Person";
            }
            else
            {
                lblFormTitle.Text = "Update Person";
                lblFormBigTitle.Text = "Update Person";
            }
        }

        private void _ShowPersonData(DataView dataview)
        {
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;

            if (_CurrentMode != _enMode.AddNew)
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                dtpDateOfBirth.Value = _Person.DateOfBirth;
                cbCountries.SelectedIndex = dataview.Find(_Person.CountryName);
            }
            else
                cbCountries.SelectedIndex = dataview.Find("Jordan");

            if (_Person.Gendor == clsPerson.enGendor.Female)
                rbFemale.Checked = true;

            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;


            txtAddress.Text = _Person.Address;           

            if (File.Exists(_Person.ImagePath))
            {
                pbPersonalImage.ImageLocation = _Person.ImagePath;
                lnlblRemove.Visible = true;
            }
            else
                pbPersonalImage.Image = (_Person.Gendor == clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
        }

        private void cbCountries_DropDownClosed(object sender, EventArgs e)
        {
            cbCountries.BackColor = Color.FromArgb(230, 230, 230);
        }

        private void cbCountries_DropDown(object sender, EventArgs e)
        {
            cbCountries.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void cbCountries_DrawItem(object sender, DrawItemEventArgs e)
        {
            clsGeneralUtility.DrawComboBoxItems(sender, e,"CountryName");
        }

        private void txtBoxName_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            if (txtBox == txtThirdName && string.IsNullOrEmpty(txtBox.Text.Trim()))
            {
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtBox);
                return;
            }

            else if (string.IsNullOrEmpty(txtBox.Text.Trim()))
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtBox, $"It is required to enter your {txtBox.Tag.ToString()}!", e);
            }

            else if (!clsGeneralUtility.ValidateNamePart(txtBox.Text))
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtBox, $"{txtBox.Tag.ToString()} must contain only letters!", e);
            }

            else
            {
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtBox);
            }
        }

        private void txtBoxAddress_Validating(object sender, CancelEventArgs e)
        {
            if (txtAddress.Text == "" || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtAddress, $"It is required to enter your Address!", e);
            }
            else
            {
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtAddress);
            }
        }

        private void ValidateNationalNumber(CancelEventArgs e)
        {
            bool IsNationalNoAlreadyExists;

            if(string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtNationalNo, "National number cannot be empty!", e);
                return;
            }

            if (_Person == null)
            {
                IsNationalNoAlreadyExists = clsPerson.SearchForNationalNo(txtNationalNo.Text.Trim());

                if(IsNationalNoAlreadyExists)
                {
                    clsGeneralUtility.EnableErrorProvider(erTextBox, txtNationalNo, "National number is used by another person!", e);
                    return;
                }
            }

            else
            {
                if (!(_Person.NationalNo == txtNationalNo.Text))
                {
                    IsNationalNoAlreadyExists = clsPerson.SearchForNationalNo(txtNationalNo.Text.Trim());

                    if (IsNationalNoAlreadyExists)
                    {
                        clsGeneralUtility.EnableErrorProvider(erTextBox, txtNationalNo, "National number is used by another person!", e);
                        return;
                    }
                }
            }

            clsGeneralUtility.DisableErrorProvider(erTextBox, txtNationalNo);
        }

        private void txtBoxNationalNo_Validating(object sender, CancelEventArgs e)
        {
            ValidateNationalNumber(e);
        }

        private void txtBoxPhone_Validating(object sender, CancelEventArgs e)
        {
            if (txtPhone.Text == "" || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtPhone, "It is required to enter your Phone Number!", e);
            }

            else if (!clsGeneralUtility.ValidateInteger(txtPhone.Text))
            {
                clsGeneralUtility.EnableErrorProvider(erTextBox, txtPhone, "Phone Number must contains only digits!", e);
            }

            else
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtPhone);
        }

        private void txtBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text != "")
            {
                if (!clsGeneralUtility.ValidateEmail(txtEmail.Text.Trim()))
                {
                    clsGeneralUtility.EnableErrorProvider(erTextBox, txtEmail, "Invalid Email Address Format!", e);
                    return;
                }
            }
                clsGeneralUtility.DisableErrorProvider(erTextBox, txtEmail);
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            btnExit.CausesValidation = false;
            btnClose.CausesValidation = false;

            clsGeneralUtility.SetDateConstraintForAge(dtpDateOfBirth, 18, 100);

            DataView dataview = clsCountries.GetAllCountries().DefaultView;

            dataview.Sort = "CountryName ASC";
            cbCountries.DataSource = dataview;

            _ShowPersonData(dataview);
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonalImage.ImageLocation == null)
                pbPersonalImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonalImage.ImageLocation == null)
                pbPersonalImage.Image = Resources.Female_512;
        }

        private void lnlblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdSelectImage.InitialDirectory = @"C:\Users\User\Desktop";
            ofdSelectImage.Filter = "All Images|*.JPG;*.PNG;*.JPEG;*.GIF|.JPG|*.JPG|.PNG|*.PNG|.JPEG|*.JPEG|.GIF|*.GIF";
            ofdSelectImage.ShowDialog();
        }

        private void ofdSelectImage_FileOk(object sender, CancelEventArgs e)
        {
            pbPersonalImage.ImageLocation = ofdSelectImage.FileName;
            lnlblRemove.Visible = true;
        }

        private void lnlblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonalImage.Image = (rbMale.Checked) ? Resources.Male_512 : Resources.Female_512;

            pbPersonalImage.ImageLocation = null;
            lnlblRemove.Visible = false;
        }

        private object[] _GetEnteredValuesInArray()
        {
            object[] Values = new object[] {lblPersonID.Text,txtNationalNo.Text, txtFirstName.Text , txtSecondName.Text , txtThirdName.Text ,
            txtLastName.Text,(rbMale.Checked) ? "Male":"Female",dtpDateOfBirth.Value.ToShortDateString(),
            ((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString(),txtPhone.Text,txtEmail.Text };            

            return Values;
        }

        private bool HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonalImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        if (File.Exists(_Person.ImagePath))
                            File.Delete(_Person.ImagePath);
                    }

                    catch (IOException e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                if (pbPersonalImage.ImageLocation != null)
                {
                    string ImageSource = pbPersonalImage.ImageLocation;
                    if (clsFileManager.CopyImageToProjectImageFolder(clsGlobalSettings.ImagesFolderPath, ref ImageSource))
                    {
                        pbPersonalImage.ImageLocation = ImageSource;
                    }

                    else
                        return false;
                }
            }
                return true;
        }

        private void _SetPersonInfo(out clsPerson Person)
        {
            if (_CurrentMode == _enMode.AddNew)
            {
                Person = new clsPerson
                (
                NationalNo: txtNationalNo.Text.Trim(),
                FirstName: txtFirstName.Text.Trim(),
                SecondName: txtSecondName.Text.Trim(),
                ThirdName: txtThirdName.Text.Trim(),
                LastName: txtLastName.Text.Trim(),
                DateOfBirth: dtpDateOfBirth.Value,
                Gendor: (rbMale.Checked) ? clsPerson.enGendor.Male : clsPerson.enGendor.Female,
                Address: txtAddress.Text.Trim(),
                Phone: txtPhone.Text.Trim(),
                Email: txtEmail.Text.Trim(),
                NationalityCountryID: clsCountries.Find(((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString()).CountryID,
                ImagePath: (pbPersonalImage.ImageLocation != null) ? pbPersonalImage.ImageLocation : null
                );
            }

            else
            {
                Person = _Person;

                Person.FirstName = txtFirstName.Text.Trim();
                Person.SecondName = txtSecondName.Text.Trim();
                Person.ThirdName = txtThirdName.Text.Trim();
                Person.LastName = txtLastName.Text.Trim();
                Person.NationalNo = txtNationalNo.Text.Trim();
                Person.Gendor = (rbMale.Checked) ? clsPerson.enGendor.Male : clsPerson.enGendor.Female;
                Person.DateOfBirth = dtpDateOfBirth.Value;
                Person.Phone = txtPhone.Text.Trim();

                Person.Email = txtEmail.Text.Trim();

                Person.NationalityCountryID = clsCountries.Find(((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString()).CountryID;
                Person.CountryName = clsCountries.Find(Person.NationalityCountryID).CountryName;

                Person.Address = txtAddress.Text.Trim();

                if(_Person.ImagePath != pbPersonalImage.ImageLocation)
                {
                    Person.ImagePath = (pbPersonalImage.ImageLocation != null) ? pbPersonalImage.ImageLocation : null;
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!HandlePersonImage())
                return;

             clsPerson Person;
            _SetPersonInfo(out Person);

            if (_CurrentMode == _enMode.Update)
            {
                if (_Person.AreAllFieldsOldValuesNotChanged())
                {
                    MessageBox.Show("There is'nt any change on the information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (Person.Save())
            {
                lblPersonID.Text = Person.PersonID.ToString();

                object[] NewDetails = _GetEnteredValuesInArray();

                if (_CurrentMode != _enMode.Update)
                {
                    AfterAddingNewPerson?.Invoke(Person.PersonID);
                    AfterSavingNewInfo?.Invoke(NewDetails);
                }

                AfterEditingPersonInfo?.Invoke(Person);

                AfterSavingEditedInfo?.Invoke(NewDetails, _PeopleDGVRowIndex);

                if (_CurrentMode == _enMode.AddNew)
                {
                    _SetTitles(clsPerson.enMode.Update);
                    _Person = Person;
                }

                MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _CurrentMode = _enMode.Update;
            }
            else
                MessageBox.Show("Saving Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}