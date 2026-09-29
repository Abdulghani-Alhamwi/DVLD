using System;
using System.Data;
using System.Runtime.Remoting.Messaging;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsPerson
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }
        enMode _CurrentMode;
        public enum enGendor : byte { Male = 0, Female = 1 }
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

        private clsPeopleData.clsOldPersonData _OldPersonData;

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

            _OldPersonData = new clsPeopleData.clsOldPersonData
            (NationalNo, FirstName, SecondName, ThirdName, LastName,DateOfBirth,
             _GetGendorNumericValue(),Address, Phone, Email, NationalityCountryID, ImagePath);
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
        private byte? _GetGendorNumericValue()
        {
            if (Gendor == null)
                return null;

            return Convert.ToByte((Gendor == enGendor.Male) ? 0 : 1);
        }
        private bool _AddNewPerson()
        {
            PersonID = clsPeopleData.AddNewPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, _GetGendorNumericValue()
                , Address, Phone, Email, NationalityCountryID, ImagePath);

            return (PersonID != -1);
        }

        public bool AreAllFieldsOldValuesNotChanged()
        {
            return (_OldPersonData.NationalNo == NationalNo && _OldPersonData.FirstName == FirstName && _OldPersonData.SecondName == SecondName
                 && _OldPersonData.ThirdName == ThirdName && _OldPersonData.LastName == LastName && _OldPersonData.DateOfBirth == DateOfBirth
                 && _OldPersonData.Gendor == _GetGendorNumericValue() && _OldPersonData.Address == Address && _OldPersonData.Phone == Phone
                 && _OldPersonData.Email == Email && _OldPersonData.NationalityCountryID == NationalityCountryID && _OldPersonData.ImagePath == ImagePath);
        }

        private bool _HasOldDataChangedFully()
        {
            return (_OldPersonData.NationalNo != NationalNo && _OldPersonData.FirstName != FirstName && _OldPersonData.SecondName != SecondName
                 && _OldPersonData.ThirdName != ThirdName && _OldPersonData.LastName != LastName && _OldPersonData.DateOfBirth != DateOfBirth
                 && _OldPersonData.Gendor != _GetGendorNumericValue() && _OldPersonData.Address != Address && _OldPersonData.Phone != Phone
                 && _OldPersonData.Email != Email && _OldPersonData.NationalityCountryID != NationalityCountryID && _OldPersonData.ImagePath != ImagePath);
        }

        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, _GetGendorNumericValue()
                , Address, Phone, Email, NationalityCountryID, ImagePath,_OldPersonData, _HasOldDataChangedFully());
        }
        public static DataTable GetPeopleInfo(byte WantedNumOfRecords)
        {
            return clsPeopleData.GetPeopleInfo(WantedNumOfRecords);
        }

        public static DataTable GetPeopleInfo(byte WantedNumOfRecords, int _LastBroughtPersonID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsPeopleData.GetPeopleInfo(WantedNumOfRecords, _LastBroughtPersonID, LastColumnNameDataOrderedBy,
                   NumberOfRowsToOffset, SortDirection);
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

        public static int GetTotalPeopleCount()
        {
            return clsPeopleData.GetTotalPeopleCount();
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy,
            string LastColumnNameDataOrderedBy, string SortDirection, char? WildChar = null)
        {
            return clsPeopleData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection, -1,-1, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy,
            string LastColumnNameDataOrderedBy, string SortDirection, int _LastBroughtPersonID,int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            return clsPeopleData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection
                , _LastBroughtPersonID, NumberOfRowsToOffset, WildChar);
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
        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsPeopleData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,
                    ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }
    }
}
