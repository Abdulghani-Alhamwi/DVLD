using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsDetainedLicenses
    {
        private enum _enMode : byte { AddNew = 0 , Update = 1};

        private _enMode _CurrentMode;
        public int DetainID { get; set; }
        public int LocalLicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set;}
        public int ReleaseApplicationID { get; set; }

        public clsDetainedLicenses(int LocalLicenseID,DateTime DetainDate, decimal FineFees, int CreatedByUserID)
        {
            this.LocalLicenseID = LocalLicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            IsReleased = false;
            ReleaseDate = DateTime.MinValue;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
        }

        private clsDetainedLicenses(int DetainID,int LocalLicenseID, DateTime DetainDate, decimal FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LocalLicenseID = LocalLicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;

            _CurrentMode = _enMode.Update;
        }

        private bool _AddNewDetainedLicense()
        {
           DetainID = clsDetainedLicensesData.AddNewDetainedLicense(LocalLicenseID, DetainDate, FineFees, CreatedByUserID);

            return (DetainID != -1);
        }

        public bool ReleaseDetainedLicense(DateTime ReleaseDate,int ReleasedByUserID,int ReleaseApplicationID)
        {
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleaseDate = ReleaseDate;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleasedByUserID = ReleasedByUserID;

            return clsDetainedLicensesData.ReleaseDetainedLicense(this.LocalLicenseID, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
        }

        public bool Save()
        {
            if (_CurrentMode == _enMode.AddNew)
            {
                if (_AddNewDetainedLicense())
                {
                    _CurrentMode = _enMode.Update;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public static bool IsDetainedLicense(int LocalLicenseID)
        {
            return clsDetainedLicensesData.IsDetainedLicense(LocalLicenseID);
        }

        public static clsDetainedLicenses Find(int LocalLicenseID)
        {
            int DetainID = -1;
            DateTime DetainDate = DateTime.MinValue;
            decimal FineFees = -1;
            int CreatedByUserID = -1;
            bool IsReleased = false;
            DateTime ReleaseDate = DateTime.MinValue;
            int ReleasedByUserID = -1;
            int ReleaseApplicationID = -1;

            if (clsDetainedLicensesData.Find(LocalLicenseID,ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
            {
                return new clsDetainedLicenses(DetainID,LocalLicenseID,DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            }

            else
                return null;
        }


        public static DataTable GetDetainedLicensesInfo(byte WantedNumOfRecords)
        {
            return clsDetainedLicensesData.GetDetainedLicensesInfo(WantedNumOfRecords);
        }
        public static DataTable GetDetainedLicensesInfo(byte WantedNumOfRecords, int LastLowestBroughtDetainID)
        {
            return clsDetainedLicensesData.GetDetainedLicensesInfo(WantedNumOfRecords, LastLowestBroughtDetainID);
        }

        public static int GetTotalCount()
        {
            return clsDetainedLicensesData.GetTotalCount();
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy,char? WildChar = null)
        {
            return clsDetainedLicensesData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, -1, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, int LastLowestbroughtDetainID = -1, char? WildChar = null)
        {
            return clsDetainedLicensesData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastLowestbroughtDetainID, WildChar);
        }

        public static DataTable GetColumnsNamesForView()
        {
            return clsDetainedLicensesData.GetColumnsNamesForView();
        }
    }
}
