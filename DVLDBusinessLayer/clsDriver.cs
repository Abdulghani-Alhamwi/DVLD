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
        public int CreatedByUserID { get; set;}
        public DateTime CreatedDate { get; set; }

        public clsDriver(int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
        }
        private bool _AddNewDriver()
        {
            DriverID = clsDriversData.AddNewDriver(PersonID, CreatedByUserID, CreatedDate);

            return (DriverID != -1);
        }

        public bool Save()
        {
            if (_CurrentMode == _enMode.Update)
                return false;

            if (_AddNewDriver())
            {
                _CurrentMode = _enMode.Update;
                return true;
            }
            else
                return false;
        }
        public static bool IsPersonAlreadyADriver(int PersonID)
        {
            return clsDriversData.IsPersonAlreadyADriver(PersonID);
        }

        public static int GetDriverID(int PersonID)
        {
            return clsDriversData.GetDriverID(PersonID);
        }

        public static int GetDriverPersonID(int DriverID)
        {
            return clsDriversData.GetDriverPersonID(DriverID);
        }

        public static DataTable GetDriversInfo(byte WantedNumOfRecords)
        {
            return clsDriversData.GetDriversInfo(WantedNumOfRecords);
        }

        public static DataTable GetDriversInfo(byte WantedNumOfRecords, int LastBroughtDriverID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsDriversData.GetDriversInfo(WantedNumOfRecords, LastBroughtDriverID, LastColumnNameDataOrderedBy
                    , NumberOfRowsToOffset, SortDirection);
        }

        public static int GetTotalDriversCount()
        {
            return clsDriversData.GetTotalDriversCount();
        }
        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection, char? WildChar = null)
        {
            return clsDriversData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, ColumnNameToOrderBy, SortDirection, -1, -1, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy,
            string ColumnNameToOrderBy, string SortDirection, int LastBroughtDriverID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            return clsDriversData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, ColumnNameToOrderBy,
                SortDirection, LastBroughtDriverID, NumberOfRowsToOffset, WildChar);
        }

        public static bool HasActiveLicenseFromClass(int DriverID, int LicenseClassID,out int LicenseID)
        {
            return clsDriversData.HasActiveLicenseFromClass(DriverID, LicenseClassID,out LicenseID);
        }

        public static short GetDriverLocalLicensesCount(int DriverID)
        {
            return clsDriversData.GetDriverLocalLicensesCount(DriverID);
        }

        public static short GetDriverInternationalLicensesCount(int DriverID)
        {
            return clsDriversData.GetDriverInternationalLicensesCount(DriverID);
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection
        , string ColumnNameToFilterBy = null, string valueToFilterBy = null, char? WildChar = null)
        {
            return clsDriversData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,
                ColumnNameToFilterBy, valueToFilterBy, WildChar);
        }

    }
}
