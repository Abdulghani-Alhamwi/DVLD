using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Driver_And_Vehicle_Licenses_Department___DVLD__.Properties;
using MyLib;
using DVLDBusinessLayer;

namespace Driver_And_Vehicle_Licenses_Department___DVLD__
{
    public partial class frmAddEditPersonInfo : Form
    {
        internal delegate void AddedNewPersonEventHandler (int PersonID);
        internal event AddedNewPersonEventHandler AfterAddingNewPerson;
        internal event Action AfterEditingPersonInfo;

        internal delegate void AfterSavingNewInfo(ref object[] NewValues);
        internal delegate void AfterSavingEditedInfo(ref object[] NewValues,int RowIndex);
        internal event AfterSavingNewInfo AfterSavingNewPersonInfo;
        internal event AfterSavingEditedInfo AfterSavingEditedPersonInfo;

        string _SavedPersonalImagePath;
        clsPerson _Person;
        int _IndexOfWantedDataRowToEdit = -1;

        public frmAddEditPersonInfo(clsPerson PersonInfo = null)
        {
            _InitializeForm(PersonInfo);
        }

        public frmAddEditPersonInfo(clsPerson PersonInfo,int IndexOfWantedRowToEdit)
        {
            _InitializeForm(PersonInfo);
            _IndexOfWantedDataRowToEdit = IndexOfWantedRowToEdit;
        }

        private void _InitializeForm(clsPerson PersonInfo)
        {
            InitializeComponent();

            if (PersonInfo == null)
                _SetTitles(clsPerson.enMode.AddNew);

            else
            {
                this._Person = PersonInfo;
                _SetTitles(clsPerson.enMode.Update);
            }
            lblFormBigTitle.Location = new Point((this.Width / 2) - (lblFormBigTitle.Width / 2), lblFormBigTitle.Location.Y);
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
            clsUtility.DrawComboBoxItems(sender, e,"CountryName");
        }
        private bool _ValidateName(object sender, CancelEventArgs e)
        {
            if ((((TextBox)sender).Text == "" || string.IsNullOrWhiteSpace(((TextBox)sender).Text)) && ((TextBox)sender).Tag.ToString() != "Third Name")
            {
                clsUtility.EnableErrorProvider(erTextBox, (TextBox)sender, $"It is required to enter your {((TextBox)sender).Tag.ToString()}!", e);
                return false;
            }

            else if (!(((TextBox)sender).Text.All(Char.IsLetter) || ((TextBox)sender).Text.Contains("-") || ((TextBox)sender).Text.Contains("_")))
            {
                clsUtility.EnableErrorProvider(erTextBox, (TextBox)sender, $"{((TextBox)sender).Tag.ToString()} must contain only letters!", e);
                return false;
            }

            else
                erTextBox.Dispose();

            return true;
        }

        private bool _ValidateAddress(CancelEventArgs e)
        {
            if (txtAddress.Text == "" || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                clsUtility.EnableErrorProvider(erTextBox,txtAddress, $"It is required to enter your Address!", e);
                return false;
            }
            else
                erTextBox.Dispose();

            return true;
        }
        private void txtBoxName_Validating(object sender, CancelEventArgs e)
        {
            _ValidateName(sender, e);
        }

        private void txtBoxAddress_Validating(object sender, CancelEventArgs e)
        {
            _ValidateAddress(e);
        }

        private bool _ValidateNationalNo(CancelEventArgs e)
        {
            if (txtNationalNo.Text == "" || string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                clsUtility.EnableErrorProvider(erTextBox,txtNationalNo, "It is required to enter your National No!", e);
                return false;
            }

            else if (_Person != null)
            {

                if (txtNationalNo.Text == _Person.NationalNo)
                    return true;

                else if (clsPerson.SearchForNationalNo(txtNationalNo.Text))
                {
                    clsUtility.EnableErrorProvider(erTextBox,txtNationalNo, $"National Number is used for another person!", e);
                    return false;
                }
                else
                    erTextBox.Dispose();

                return true;
            }

            else if (clsPerson.SearchForNationalNo(txtNationalNo.Text))
            {
                clsUtility.EnableErrorProvider(erTextBox,txtNationalNo, $"National Number is used for another person!", e);
                return false;
            }

            else
                erTextBox.Dispose();

            return true;
        }
        private void txtBoxNationalNo_Validating(object sender, CancelEventArgs e)
        {
            _ValidateNationalNo(e);
        }
        private bool _ValidatePhone(CancelEventArgs e)
        {
            if (txtPhone.Text == "" || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                clsUtility.EnableErrorProvider(erTextBox,txtPhone, "It is required to enter your Phone Number!", e);
                return false;
            }

            else if (!txtPhone.Text.All(Char.IsDigit))
            {
                clsUtility.EnableErrorProvider(erTextBox,txtPhone, "Phone Number must contains only digits!", e);
                return false;
            }
            else
                erTextBox.Dispose();

            return true;
        }
        private void txtBoxPhone_Validating(object sender, CancelEventArgs e)
        {
            _ValidatePhone(e);
        }
        private bool _ValidateEmailStart(string Email)
        {
            if (!(txtEmail.Text.Contains("@") && txtEmail.Text.Contains("."))
                || txtEmail.Text.StartsWith(".") || txtEmail.Text.StartsWith("-")
                || txtEmail.Text.StartsWith("_") || txtEmail.Text.StartsWith("+"))
                return false;
            else
                return true;
        }

        private bool _ValdiateEmailMiddle(string Email)
        {
            if (txtEmail.Text.Contains("@.") || txtEmail.Text.Contains("@-")
               || txtEmail.Text.Contains(".@") || txtEmail.Text.Contains("-@")
               || txtEmail.Text.Contains("@+") || txtEmail.Text.Contains("+@")
               || txtEmail.Text.Contains("@_") || txtEmail.Text.Contains("_@")
               || txtEmail.Text.Substring(txtEmail.Text.IndexOf("@") + 1, (txtEmail.Text.IndexOf(".")) - (txtEmail.Text.IndexOf("@") + 1)).Contains("_"))
                return false;
            else
                return true;
        }

        private bool _ValidateEmailEnd(string Email)
        {
            if (txtEmail.Text.EndsWith(".") || txtEmail.Text.EndsWith("-")
                || txtEmail.Text.EndsWith("_") || txtEmail.Text.EndsWith("+")
                || txtEmail.Text.EndsWith("@"))
                return false;
            else
                return true;
        }

        private bool _CheckIsValidEmail(string Email, CancelEventArgs e)
        {
            if ((txtEmail.Text.Contains(" ") && txtEmail.Text.Contains(",") && !(_ValidateEmailStart(Email)
            && _ValdiateEmailMiddle(Email) && _ValidateEmailEnd(Email))))
            {
                clsUtility.EnableErrorProvider(erTextBox,txtEmail, "Invalid Email Address Format!", e);
                return false;
            }
            else
                erTextBox.Dispose();

            return true;

        }
        private void txtBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text != "")
                _CheckIsValidEmail(txtEmail.Text, e);
        }

        private void _SetDateConstraint()
        {
            DateTime MaxdateOfBirth = DateTime.Now.AddYears(-18);

            dtpDateOfBirth.Format = DateTimePickerFormat.Custom;
            dtpDateOfBirth.CustomFormat = "dd/MM/yyyy";
            dtpDateOfBirth.Value = MaxdateOfBirth;
            dtpDateOfBirth.MaxDate = MaxdateOfBirth;
            dtpDateOfBirth.MinDate = new DateTime(1935, 1, 1);

        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {
            btnExit.CausesValidation = false;
            btnClose.CausesValidation = false;
            
            _SetDateConstraint();

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
        string _SelectedImageNewPath;
        string _ImagesFolderPath = @"C:\DVLD-People-Images";
        bool _UploadedPersonalImage = false;
        private void ofdSelectImage_FileOk(object sender, CancelEventArgs e)
        {
            if (!Directory.Exists(_ImagesFolderPath))
                Directory.CreateDirectory(_ImagesFolderPath);

            if (ofdSelectImage.FileName != "")
            {
                _SelectedImageNewPath = _ImagesFolderPath + @"\" + Guid.NewGuid().ToString() + ofdSelectImage.SafeFileName;

                pbPersonalImage.ImageLocation = ofdSelectImage.FileName;

                _UploadedPersonalImage = true;
                lnlblRemove.Visible = true;
            }
        }

        bool _RemoveSavedImage = false;
        private void lnlblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pbPersonalImage.ImageLocation == _SavedPersonalImagePath && !String.IsNullOrEmpty(_SavedPersonalImagePath))
            {
                _RemoveSavedImage = true;
            }

            pbPersonalImage.Image = (rbMale.Checked) ? Resources.Male_512 : Resources.Female_512;

            pbPersonalImage.ImageLocation = null;

            
            _SelectedImageNewPath = null;
            _UploadedPersonalImage = false;
            lnlblRemove.Visible = false;
        }

        private bool _IsInfoUnchanged()
        {
            return (_Person.FirstName == txtFirstName.Text
                 && _Person.SecondName == txtSecondName.Text
                 && _Person.ThirdName == txtThirdName.Text
                 && _Person.LastName == txtLastName.Text
                 && _Person.NationalNo == txtNationalNo.Text
                 && _Person.DateOfBirth == dtpDateOfBirth.Value
                 && _Person.Gendor == (rbMale.Checked ? clsPerson.enGendor.Male : clsPerson.enGendor.Female)
                 && _Person.Phone == txtPhone.Text
                 && _Person.Email == txtEmail.Text
                 && _Person.Address == txtAddress.Text
                 && _Person.CountryName == ((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString()
                 && _Person.ImagePath == (pbPersonalImage.ImageLocation == null ? _Person.ImagePath : pbPersonalImage.ImageLocation)
                 );
        }
        private object[] _GetCurrentValuesInArray()
        {
            object[] Values = new object[] {lblPersonID.Text,txtNationalNo.Text, txtFirstName.Text , txtSecondName.Text , txtThirdName.Text ,
            txtLastName.Text,(rbMale.Checked) ? "Male":"Female",dtpDateOfBirth.Value.ToShortDateString(),
            ((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString(),txtPhone.Text,txtEmail.Text };            

            return Values;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            clsPerson Person;

            if (_Person == null)
                Person = new clsPerson();
            else
            {
                if (_IsInfoUnchanged())
                {
                    MessageBox.Show("There is'nt any change on the information", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }

                Person = _Person;
            }

            CancelEventArgs CancelEvent = new CancelEventArgs();

            bool IsValidEmail = (txtEmail.Text == "") ? true : _CheckIsValidEmail(txtEmail.Text, CancelEvent);

            if (_ValidateName(txtFirstName, CancelEvent)
             && _ValidateName(txtSecondName, CancelEvent)
             && _ValidateName(txtThirdName, CancelEvent)
             && _ValidateName(txtLastName, CancelEvent)
             && _ValidateNationalNo(CancelEvent)
             && IsValidEmail
             && _ValidatePhone(CancelEvent)
             && _ValidateAddress(CancelEvent))
            {
                Person.FirstName = txtFirstName.Text;
                Person.SecondName = txtSecondName.Text;
                Person.ThirdName = txtThirdName.Text;
                Person.LastName = txtLastName.Text;
                Person.NationalNo = txtNationalNo.Text;
                Person.Gendor = (rbMale.Checked) ? clsPerson.enGendor.Male : clsPerson.enGendor.Female;
                Person.DateOfBirth = dtpDateOfBirth.Value;
                Person.Phone = txtPhone.Text;

                Person.Email = txtEmail.Text;

                Person.NationalityCountryID = clsCountries.GetCountryID(((DataRowView)cbCountries.SelectedItem)["CountryName"].ToString());
                Person.Address = txtAddress.Text;

                if (_SelectedImageNewPath != null)
                    Person.ImagePath = _SelectedImageNewPath;

                else if (_SelectedImageNewPath == null && !_RemoveSavedImage)
                    Person.ImagePath = _SavedPersonalImagePath;
                
                else
                    Person.ImagePath = null;


                if (Person.Save())
                {
                    lblPersonID.Text = Person.PersonID.ToString();

                    if (_SelectedImageNewPath != null)
                        File.Copy(ofdSelectImage.FileName, _SelectedImageNewPath);

                    if(_RemoveSavedImage)
                    {
                        if (File.Exists(_SavedPersonalImagePath))
                            File.Delete(_SavedPersonalImagePath);

                    }    

                    MessageBox.Show("Data Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AfterAddingNewPerson?.Invoke(Person.PersonID);                    
                    AfterEditingPersonInfo?.Invoke();

                    object[] NewValues = _GetCurrentValuesInArray();
                    AfterSavingNewPersonInfo?.Invoke(ref NewValues);
                    AfterSavingEditedPersonInfo?.Invoke(ref NewValues, _IndexOfWantedDataRowToEdit);

                    if (_Person == null)
                    {
                        _SetTitles(clsPerson.enMode.Update);
                        _Person = Person;
                    }
                }
                else
                    MessageBox.Show("Saving Failed!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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