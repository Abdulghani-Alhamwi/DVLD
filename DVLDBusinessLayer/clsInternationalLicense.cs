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
            return clsInternationalLicensesData.GetDriverInternationalLicenses(DriverID, WantedNumOfRecords);
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID, byte WantedNumOfRecords, int LowestBroughtIntLicID)
        {
            return clsInternationalLicensesData.GetDriverInternationalLicenses(DriverID, WantedNumOfRecords, LowestBroughtIntLicID);
        }
        public static DataTable GetAllInternationalLicenses(byte WantedNumOfRecords)
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses(WantedNumOfRecords);
        }
        public static DataTable GetAllInternationalLicenses(byte WantedNumOfRecords, int LowstBroughtIntLicID)
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses(WantedNumOfRecords,LowstBroughtIntLicID);
        }
        private bool _IssueDriverLicense()
        {
            InternationalLicenseID = clsInternationalLicensesData.IssueDriverLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);

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
            if (clsInternationalLicensesData.Find(InternationalLicenseID, ref ApplicationID, ref DriverID, ref LocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                return new clsInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            }
            else
                return null;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null)
        {
            return clsInternationalLicensesData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, -1, WildChar);
        }
        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, int LowstBroughtIntLicID, char? WildChar = null)
        {
            return clsInternationalLicensesData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LowstBroughtIntLicID, WildChar);
        }
        public static int GetTotalCount()
        {
            return clsInternationalLicensesData.GetTotalCount();
        }
        public static bool HasDriverActiveInternationalLicense(int LocalLicenseID, out int InternationalLicenseID)
        {
            InternationalLicenseID = -1;
            return clsInternationalLicensesData.HasDriverActiveInternationalLicense(LocalLicenseID,ref InternationalLicenseID);
        }
        public bool IsExpired()
        {
            return (ExpirationDate < DateTime.Now);
        }
        public static DataTable GetColumnsNamesForView()
        {
            return clsInternationalLicensesData.GetColumnsNamesForView();
        }
        public static int GetLicenseID(int InternationalLicenseAppID)
        {
            return clsInternationalLicensesData.GetLicenseID(InternationalLicenseAppID);
        }
    }
}
