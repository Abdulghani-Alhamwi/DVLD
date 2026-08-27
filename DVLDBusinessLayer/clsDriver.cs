using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
        private enum _enMode : byte { AddNew = 1 , Update = 2}

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
        public static DataTable GetDriversInfo(byte WantedNumberOfRecords, int LastLowestBroughtDriverID)
        {
            return clsDriversData.GetDriversInfo(WantedNumberOfRecords, LastLowestBroughtDriverID);
        }
        public static DataTable GetDriversInfo(byte WantedNumberOfRecords)
        {
            return clsDriversData.GetDriversInfo(WantedNumberOfRecords,-1);
        }
        public static int GetTotalDriversCount()
        {
            return clsDriversData.GetTotalDriversCount();
        }
        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char WildChar, int LastLowestBroughtDriverID)
        {
            return clsDriversData.GetFilteredData(WantedNumberOfRecords, ColumnNameToFilter, ValueToFilterBy, WildChar, LastLowestBroughtDriverID);
        }

        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy)
        {
            return clsDriversData.GetFilteredData(WantedNumberOfRecords, ColumnNameToFilter, ValueToFilterBy);
        }

    }
}
