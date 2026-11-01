using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsInternationalLicense : clsApplication
    {
        private enum _enMode : byte { AddNew = 0 , Update = 1 }
        private _enMode _CurrentMode;   
        public int InternationalLicenseID { get; set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }

        public clsInternationalLicense(int DriverID, int LocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID,int ApplicantPersonID
            ,DateTime ApplicationDate,clsApplication.enApplicationType ApplicationTypeID
            ,enApplicationStatus ApplicationStatus,DateTime LastStatusDate,decimal PaidApplicationFees)
            :base(ApplicantPersonID, ApplicationDate,(byte)ApplicationTypeID, ApplicationStatus
              , LastStatusDate, PaidApplicationFees, CreatedByUserID)
        {
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = LocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

            DriverInfo = clsDriver.FindByDriverID(DriverID);
        }

        private clsInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int LocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID, int ApplicantPersonID
            , DateTime ApplicationDate, byte ApplicationTypeID
            , enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees)
            : base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus
              , LastStatusDate, PaidApplicationFees, CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = LocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;

            DriverInfo = clsDriver.FindByDriverID(DriverID);

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

        private bool _AddNewInternationalLicense()
        {
            InternationalLicenseID = clsInternationalLicenseData._AddNewInternationalLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

            if (InternationalLicenseID != -1)
            {
                if (base.SetComplete())
                    return true;
            }

            return false;

        }

        public bool Save()
        {
            base.CurrentMode = (clsApplication.enMode)_CurrentMode;

            if (!base.Save())
                return false;

            if (_CurrentMode == _enMode.AddNew)
            {
                if (_AddNewInternationalLicense())
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
                clsApplication ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);

                if (ApplicationInfo == null)
                    return null;

                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate
                  , IsActive, CreatedByUserID, ApplicationInfo.ApplicantPersonID, ApplicationInfo.ApplicationDate,
                  ApplicationInfo.ApplicationTypeID, ApplicationInfo.ApplicationStatus, ApplicationInfo.LastStatusDate, ApplicationInfo.PaidApplicationFees);
            }
            else
                return null;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection,
            int LastBroughIntLicenseID, int NumberOfRowsToOffset, char? WildChar = null)
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

        public static int GetActiveInternationalLicenseID(int DriverID)
        {
            return clsInternationalLicenseData.GetActiveInternationalLicenseID(DriverID);
        }

        public static bool LinkWithNewLocalLicense(int DriverID, int NewLocalLicenseID)
        {
            if (HasDriverActiveInternationalLicense(DriverID))
                return clsInternationalLicenseData.LinkWithNewLocalLicense(DriverID, NewLocalLicenseID);

            else
                return true;
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
