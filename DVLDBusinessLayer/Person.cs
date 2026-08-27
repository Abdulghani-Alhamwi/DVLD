using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsPerson
    {
         public enum enMode : byte { AddNew = 0 , Update = 1 }
         enMode _CurrentMode;
        public enum enGendor : byte { Male = 0 , Female = 1 }
        public enGendor? Gendor;
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string CountryName { get; set; }
        public string ImagePath { get; set; }
        public string FullName
        {
            get
            {
                if (PersonID != -1)
                {
                    if (ThirdName != null)
                        return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
                    else
                        return FirstName + " " + SecondName + " " + LastName;
                }
                return null;
            }
        }

        public clsPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, enGendor Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = -1;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gendor = Gendor;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.CountryName = CountryName;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
        }

        private clsPerson(int PersonID ,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,byte Gendor , string Address, string Phone, string Email,string CountryName,int NationalityCountryID , string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gendor = (Gendor == 0) ? enGendor.Male:enGendor.Female;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.CountryName = CountryName;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

            this._CurrentMode = enMode.Update;
        }

        public static clsPerson Find(int PersonID)
        {
            byte Gendor = 0;
            string FirstName = "",SecondName = "",ThirdName = "",LastName = "",
            NationalNo = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            string Address = "",Phone = "",Email = "",CountryName = "" ,
            ImagePath = "";

            if (clsPeopleData.Find(PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
               ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                CountryName = clsCountriesData.GetCountryName(NationalityCountryID);

                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                     DateOfBirth, Gendor, Address, Phone, Email, CountryName, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }

        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            byte Gendor = 0;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            string Address = "", Phone = "", Email = "", CountryName = "",
            ImagePath = "";

            if (clsPeopleData.Find(NationalNo,ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
               ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                CountryName = clsCountriesData.GetCountryName(NationalityCountryID);

                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                                     DateOfBirth, Gendor, Address, Phone, Email, CountryName, NationalityCountryID, ImagePath);
            }
            else
                return null;
        }

        private byte _GetGendorNumericValue()
        {
            if (Gendor == null)
                return 0;

            return Convert.ToByte((Gendor == enGendor.Male) ? 0 : 1);
        }
        private bool _AddNewPerson()
        {
            PersonID = clsPeopleData.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, _GetGendorNumericValue(), Address, Phone, Email, NationalityCountryID, ImagePath);

            return (PersonID != -1);
        }
        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, _GetGendorNumericValue(), Address, Phone, Email, NationalityCountryID, ImagePath);
        }
        public static DataTable GetPeopleInfo(byte WantedNumberOfRecords, int LastLowestbroughtPersonID = -1)
        {
            return clsPeopleData.GetPeopleInfo(WantedNumberOfRecords,LastLowestbroughtPersonID);
        }
        public bool Save()
        {
            switch(_CurrentMode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _CurrentMode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdatePerson();                        
            }
            return false;
        }
        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleData.DeletePerson(PersonID);
        }

        public static bool SearchForNationalNo(string NationalNo)
        {
              return clsPeopleData.SearchForNationalNo(NationalNo);
        }

        public static uint GetTotalPeopleCount()
        {
            return clsPeopleData.GetTotalPeopleCount();
        }

        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null)
        {
            return clsPeopleData.GetFilteredData(WantedNumberOfRecords, ColumnNameToFilter, ValueToFilterBy, WildChar,-1);
        }

        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy,int LastLowestbroughtPersonID ,char? WildChar = null)
        {
            return clsPeopleData.GetFilteredData(WantedNumberOfRecords, ColumnNameToFilter, ValueToFilterBy, WildChar,LastLowestbroughtPersonID);
        }

        public static string GetFullName(int PersonID)
        {
            return clsPeopleData.GetPersonFullName(PersonID);
        }

        public static string GetNationalNumber(int PersonID)
        {
            return clsPeopleData.GetNationalNumber(PersonID);
        }
        public static int GetPersonID(string NationalNo)
        {
            return clsPeopleData.GetPersonID(NationalNo);
        }

        public static DataTable GetColumnsNamesForView()
        {
            return clsPeopleData.GetColumnsNamesForView();
        }

    }
}
