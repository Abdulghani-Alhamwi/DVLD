using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense
    {
        private enum _enMode : byte { AddNew = 0 , Update = 1 }
        private _enMode _CurrentMode;   
        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        public clsInternationalLicense(int ApplicationID, int DriverID, int LocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = LocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;
        }

        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int LocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = LocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            _CurrentMode = _enMode.Update;
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

        public static DataTable GetAllInternationalLicensesData(byte WantedNumOfRecords, string SortDirection)
        {
            return clsInternationalLicenseData.GetAllInternationalLicensesData(WantedNumOfRecords, -1, null, -1, SortDirection);
        }

        public static DataTable GetAllInternationalLicensesData(byte WantedNumOfRecords, int LastBroughIntLicenseID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsInternationalLicenseData.GetAllInternationalLicensesData(WantedNumOfRecords,LastBroughIntLicenseID
                , LastColumnNameDataOrderedBy, NumberOfRowsToOffset, SortDirection);
        }

        private bool _IssueDriverLicense()
        {
            InternationalLicenseID = clsInternationalLicenseData.IssueDriverLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            return InternationalLicenseID != -1;
        }

        public bool Save()
        {
            if (_CurrentMode == _enMode.AddNew)
            {
                if (_IssueDriverLicense())
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

        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LocalLicenseID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

            if (clsInternationalLicenseData.Find(InternationalLicenseID, ref ApplicationID, ref DriverID, ref LocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
                return null;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection, char? WildChar = null)
        {
            return clsInternationalLicenseData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, ColumnNameToOrderBy, SortDirection, -1, -1, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection,
            int LastBroughIntLicenseID, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            return clsInternationalLicenseData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, ColumnNameToOrderBy, SortDirection, LastBroughIntLicenseID, NumberOfRowsToOffset, WildChar);
        }

        public static int GetTotalCount()
        {
            return clsInternationalLicenseData.GetTotalCount();
        }

        public static bool HasDriverActiveInternationalLicense(int LocalLicenseID, out int InternationalLicenseID)
        {
            InternationalLicenseID = -1;
            return clsInternationalLicenseData.HasDriverActiveInternationalLicense(LocalLicenseID,ref InternationalLicenseID);
        }

        public static bool HasDriverActiveInternationalLicense(int DriverID)
        {
            return clsInternationalLicenseData.HasDriverActiveInternationalLicense(DriverID);
        }

        public static bool LinkWithNewLocalLicense(int DriverID, int NewLocalLicenseID)
        {
            return clsInternationalLicenseData.LinkWithNewLocalLicense(DriverID, NewLocalLicenseID);
        }

        public static DataTable GetColumnsNamesForView()
        {
            return clsInternationalLicenseData.GetColumnsNamesForView();
        }

        public static int GetLicenseID(int InternationalLicenseAppID)
        {
            return clsInternationalLicenseData.GetLicenseID(InternationalLicenseAppID);
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection
          , string ColumnNameToFilterBy = null, string valueToFilterBy = null, char? WildChar = null)
        {
            return clsInternationalLicenseData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,
                ColumnNameToFilterBy, valueToFilterBy, WildChar);
        }

        public static DataTable GetSortedInfo(int DriverID, byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection)
        {
            return clsInternationalLicenseData.GetSortedInfo(DriverID, WantedNumOfRecords, ColumnNameToOrderBy, SortDirection);
        }
    }
}
