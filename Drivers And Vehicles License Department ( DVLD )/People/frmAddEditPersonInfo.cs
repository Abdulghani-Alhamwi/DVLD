using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using DVLDPresentationLayer.Properties;
using Utility_Library;

namespace DVLDPresentationLayer
{
    public partial class frmAddEditPersonInfo : Form
    {
        internal delegate void AddedNewPersonEventHandler (int PersonID);
        internal event AddedNewPersonEventHandler AfterAddingNewPerson;
        internal event Action<clsPerson> AfterEditingPersonInfo;

        internal delegate void SavedNewInfo(object[] NewPersonDetails);
        internal delegate void SavedEditedInfo(object[] ModifiedPersonDetails,int PeopleDgvRowIndex);
        internal event SavedNewInfo AfterSavingNewInfo;
        internal event SavedEditedInfo AfterSavingEditedInfo;

        private string _NewSelectedImagePath;
        private string _ImagesFolderPath = @"C:\DVLD-People-Images";
        private bool _UploadedPersonalImage = false;
        private string _SavedPersonalImagePath;
        private bool _RemovedSavedImage = false;
        private clsPerson _Person;
        private int _PeopleDGVRowIndex = -1;

        public frmAddEditPersonInfo()
        {
            InitializeComponent();
            _InitializeFormData(null);
        }

        public frmAddEditPersonInfo(clsPerson PersonInfo,int PeopleDGVRowIndex = -1)
        {
            InitializeComponent();
            _InitializeFormData(PersonInfo);
            _PeopleDGVRowIndex = PeopleDGVRowIndex;
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
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gendor == clsPerson.enGendor.Female)
                rbFemale.Checked = true;

            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;

            cbCountries.SelectedIndex = dataview.Find(_Person.CountryName);

            txtAddress.Text = _Person.Address;
            
           _SavedPersonalImagePath = _Person.ImagePath;

            if (File.Exists(_Person.ImagePath))
            {
                pbPersonalImage.ImageLocation = _Person.ImagePath;
                lnlblRemove.Visible = true;
                _UploadedPersonalImage = true;
            }
            else
                pbPersonalImage.Image = (_Person.Gendor == clsPerson.enGendor.Male) ? Resources.Male_512 : Resources.Female_512;
        }

        private void cbFilterBy_DropDownClosed(object sender, EventArgs e)
        {
            cbCountries.BackColor = Color.FromArgb(230, 230, 230);
        }

        private void cbFilterBy_DropDown(object sender, EventArgs e)
        {
            cbCountries.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void cbFilterBy_DrawItem(object sender, DrawItemEventArgs e)
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

            if (_Person != null)
            {
                _ShowPersonData(dataview);
            }
            else
                cbCountries.SelectedIndex = dataview.Find("Jordan");
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMale.Checked && !_UploadedPersonalImage)
                pbPersonalImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFemale.Checked && !_UploadedPersonalImage)
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
            if (!Directory.Exists(_ImagesFolderPath))
                Directory.CreateDirectory(_ImagesFolderPath);

            if (ofdSelectImage.FileName != "")
            {
                _NewSelectedImagePath = _ImagesFolderPath + @"\" + Guid.NewGuid().ToString() + ofdSelectImage.SafeFileName;

                pbPersonalImage.ImageLocation = ofdSelectImage.FileName;

                _UploadedPersonalImage = true;
                lnlblRemove.Visible = true;
            }
        }

        private void lnlblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pbPersonalImage.ImageLocation == _SavedPersonalImagePath && !String.IsNullOrEmpty(_SavedPersonalImagePath))
            {
                _RemovedSavedImage = true;
            }

            pbPersonalImage.Image = (rbMale.Checked) ? Resources.Male_512 : Resources.Female_512;

            pbPersonalImage.ImageLocation = null;
            
            _NewSelectedImagePath = null;
            _UploadedPersonalImage = false;
            lnlblRemove.Visible = false;
        }

        private object[] _GetEnteredValuesInArray()
        {
            object[] Values = new object[] {lblPersonID.Text,txtNationalNo.Text, txtFirstName.Text , txtSecondName.Text , txtThirdName.Text ,
            txtLastName.Text,(rbMale.Checked) ? "Male":"Female",dtpDateOfBirth.Value.ToShortDateString(),
            ((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString(),txtPhone.Text,txtEmail.Text };            

            return Values;
        }

        private void _SetPersonInfo(out clsPerson Person)
        {
            if (_Person == null)
            {
                Person = new clsPerson
                (
                NationalNo: txtNationalNo.Text,
                FirstName: txtFirstName.Text,
                SecondName: txtSecondName.Text,
                ThirdName: txtThirdName.Text,
                LastName: txtLastName.Text,
                DateOfBirth: dtpDateOfBirth.Value,
                Gendor: (rbMale.Checked) ? clsPerson.enGendor.Male : clsPerson.enGendor.Female,
                Address: txtAddress.Text,
                Phone: txtPhone.Text,
                Email: txtEmail.Text,
                NationalityCountryID: clsCountries.Find(((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString()).CountryID,
                ImagePath: (_NewSelectedImagePath != null) ? _NewSelectedImagePath : (_NewSelectedImagePath == null && !_RemovedSavedImage) ? _SavedPersonalImagePath : null
                );
            }

            else
            {
                Person = _Person;

                Person.FirstName = txtFirstName.Text;
                Person.SecondName = txtSecondName.Text;
                Person.ThirdName = txtThirdName.Text;
                Person.LastName = txtLastName.Text;
                Person.NationalNo = txtNationalNo.Text;
                Person.Gendor = (rbMale.Checked) ? clsPerson.enGendor.Male : clsPerson.enGendor.Female;
                Person.DateOfBirth = dtpDateOfBirth.Value;
                Person.Phone = txtPhone.Text;

                Person.Email = txtEmail.Text;

                Person.NationalityCountryID = clsCountries.Find(((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString()).CountryID;
                Person.CountryName = clsCountries.Find(Person.NationalityCountryID).CountryName;

                Person.Address = txtAddress.Text;

                if (_NewSelectedImagePath != null)
                    Person.ImagePath = _NewSelectedImagePath;

                else if (_NewSelectedImagePath == null && !_RemovedSavedImage)
                    Person.ImagePath = _SavedPersonalImagePath;

                else
                    Person.ImagePath = null;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are has invalid input, hover on the red icon beside each field to know why not valid", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             clsPerson Person;
            _SetPersonInfo(out Person);

            if (_Person != null)
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

                    if (_NewSelectedImagePath != null)
                        File.Copy(ofdSelectImage.FileName, _NewSelectedImagePath);

                    if(_RemovedSavedImage)
                    {
                        if (File.Exists(_SavedPersonalImagePath))
                            File.Delete(_SavedPersonalImagePath);
                    }    


                    AfterAddingNewPerson?.Invoke(Person.PersonID);                    
                    AfterEditingPersonInfo?.Invoke(Person);

                    object[] NewDetails = _GetEnteredValuesInArray();
                    AfterSavingNewInfo?.Invoke(NewDetails);
                    AfterSavingEditedInfo?.Invoke(NewDetails, _PeopleDGVRowIndex);

                    if (_Person == null)
                    {
                        _SetTitles(clsPerson.enMode.Update);
                        _Person = Person;
                    }

                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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