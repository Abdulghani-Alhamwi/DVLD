using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
        private enum _enMode : byte { AddNew = 0 , Update = 1}
        private _enMode _CurrentMode;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonalInfo;
        public int CreatedByUserID { get; set;}
        public DateTime CreatedDate { get; set; }

        public clsDriver(int PersonID,int CreatedByUserID)
        {
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = DateTime.Now;
            PersonalInfo = clsPerson.Find(PersonID);
        }

        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            PersonalInfo = clsPerson.Find(PersonID);

            _CurrentMode = _enMode.Update;
        }

        private bool _AddNewDriver()
        {
            DriverID = clsDriverData.AddNewDriver(PersonID, CreatedByUserID);

            return (DriverID != -1);
        }

        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(DriverID, PersonID);
        }

        public bool Save()
        {
            switch (_CurrentMode)
            {
                case _enMode.AddNew:

                    if (_AddNewDriver())
                    {
                        _CurrentMode = _enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case _enMode.Update:
                    return _UpdateDriver();
            }

            return false;
        }

        public static bool IsPersonAlreadyADriver(int PersonID)
        {
            return clsDriverData.IsPersonAlreadyADriver(PersonID);
        }

        public static int GetDriverID(int PersonID)
        {
            return clsDriverData.GetDriverID(PersonID);
        }

        public static int GetDriverPersonID(int DriverID)
        {
            return clsDriverData.GetDriverPersonID(DriverID);
        }

        public static DataTable GetDriversInfo(byte WantedNumOfRecords, string SortDirection)
        {
            return clsDriverData.GetDriversInfo(WantedNumOfRecords,-1,null,-1,SortDirection);
        }

        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.FindByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }

            else
                return null;
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.Now;

            if (clsDriverData.FindByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }

            else
                return null;
        }

        public static DataTable GetDriversInfo(byte WantedNumOfRecords, int LastBroughtDriverID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsDriverData.GetDriversInfo(WantedNumOfRecords, LastBroughtDriverID, LastColumnNameDataOrderedBy
                    , NumberOfRowsToOffset, SortDirection);
        }

        public static int GetTotalDriversCount()
        {
            return clsDriverData.GetTotalDriversCount();
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection, int NumberOfRowsToOffset, char? WildChar = null)
        {
            return clsDriverData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, ColumnNameToOrderBy, SortDirection, -1, NumberOfRowsToOffset, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy,
               string ColumnNameToOrderBy, string SortDirection, int LastBroughtDriverID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            return clsDriverData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, ColumnNameToOrderBy,
                SortDirection, LastBroughtDriverID, NumberOfRowsToOffset, WildChar);
        }

        public static bool HasActiveLicenseFromClass(int DriverID, int LicenseClassID,out int LicenseID)
        {
            return clsDriverData.HasActiveLicenseFromClass(DriverID, LicenseClassID,out LicenseID);
        }

        public static DataTable GetLocalLicenses(int DriverID, byte WantedNumOfRecords)
        {
            return clsLocalLicenseData.GetLocalLicenses(DriverID, WantedNumOfRecords);
        }

        public static DataTable GetLocalLicenses(int DriverID, byte WantedNumOfRecords, int LastBroughtLicenseID
                 , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsLocalLicenseData.GetLocalLicenses(DriverID, WantedNumOfRecords, LastBroughtLicenseID
                     , LastColumnNameDataOrderedBy, NumberOfRowsToOffset, SortDirection);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID, byte WantedNumOfRecords)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID, WantedNumOfRecords);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID, byte WantedNumOfRecords, int LastBroughIntLicenseID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID, WantedNumOfRecords
                , LastBroughIntLicenseID, LastColumnNameDataOrderedBy, NumberOfRowsToOffset, SortDirection);
        }

        public static short GetDriverLocalLicensesCount(int DriverID)
        {
            return clsDriverData.GetDriverLocalLicensesCount(DriverID);
        }

        public static short GetDriverInternationalLicensesCount(int DriverID)
        {
            return clsDriverData.GetDriverInternationalLicensesCount(DriverID);
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection
             , string ColumnNameToFilterBy = null, string valueToFilterBy = null, char? WildChar = null)
        {
            return clsDriverData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,
                   ColumnNameToFilterBy, valueToFilterBy, WildChar);
        }

    }
}
