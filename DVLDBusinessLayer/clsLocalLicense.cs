using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalLicense
    {
        public enum enIssueReason : byte { FirstTime = 1 , Renew = 2 , ReplacementForDamaged = 3 , ReplacementForLost = 4}
        private enum _enMode : byte { AddNew = 0 , Update = 1 }
        private _enMode _CurrentMode;
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int PersonID{get;set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public byte LicenseClassID{get;set;}
        public DateTime IssueDate{get;set;}
        public DateTime ExpirationDate{get;set;}
        public string Notes{get;set;}
        public decimal PaidFees{get;set;}
        public bool IsActive{get;set;}
        public enIssueReason IssueReason {get;set;}
        public int CreatedByUserID{get;set;}

        public clsLocalLicense(int ApplicationID, int PersonID, byte LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = -1;
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            DriverInfo = clsDriver.FindByPersonID(PersonID);
            DriverID = (DriverInfo != null) ? DriverInfo.DriverID : -1;
        }

        private clsLocalLicense(int LicenseID, int ApplicationID, int DriverID, byte LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            DriverInfo = clsDriver.FindByDriverID(DriverID);
            PersonID = (DriverInfo != null) ? DriverInfo.PersonID : -1;

            _CurrentMode = _enMode.Update;
        }

        public static DataTable GetLocalLicenses(int DriverID,byte WantedNumOfRecords)
        {
            return clsLocalLicenseData.GetLocalLicenses(DriverID,WantedNumOfRecords);
        }

        public static DataTable GetLocalLicenses(int DriverID, byte WantedNumOfRecords, int LastBroughtLicenseID
                 , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsLocalLicenseData.GetLocalLicenses(DriverID,WantedNumOfRecords, LastBroughtLicenseID
                     , LastColumnNameDataOrderedBy, NumberOfRowsToOffset, SortDirection);
        }

        public string GetIssueReasonAsString()
        {
            if (LicenseID != -1)
            {
                switch (IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";

                    case enIssueReason.Renew:
                        return "Renew";

                    case enIssueReason.ReplacementForDamaged:
                        return "Replacement For Damaged";

                    case enIssueReason.ReplacementForLost:
                        return "Replacement For Lost";
                }
            }
            return null;
        }

        private static byte _GetIssueReasonAsNumber(enIssueReason IssueReason)
        {
            return (byte)IssueReason;
        }

        private static enIssueReason _GetIssueReasonAsEnum(byte IssueReason)
        {
            return (enIssueReason)IssueReason;
        }

        public bool IsPersonAlreadyDriver()
        {
            return (DriverInfo != null);
        }

        private bool _IssueDrivingLicense()
        {
            if (!IsPersonAlreadyDriver())
            {
                clsDriver NewDriver = new clsDriver(PersonID, CreatedByUserID);

                if (!NewDriver.Save())
                    return false;

                else
                    DriverID = NewDriver.DriverID;
            }

            bool HasIssuedInternationalLicense = false;

            if (LicenseClassID == clsLicenseClass.GetLicenseClassID(clsLicenseClass.enLicenseClasses.OrdinaryDrivingClass))
            {
                if (clsInternationalLicense.HasDriverActiveInternationalLicense(DriverID))
                {
                    HasIssuedInternationalLicense = true;
                }
            }

            LicenseID = clsLocalLicenseData.IssueLocalDrivingLicense(ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, _GetIssueReasonAsNumber(IssueReason), CreatedByUserID);

            if(HasIssuedInternationalLicense)
            {
                clsInternationalLicense.LinkWithNewLocalLicense(DriverID, LicenseID);
            }

            return (LicenseID != -1);
        }

        public bool Save()
        {
            if (_CurrentMode == _enMode.Update)
                return false;

            if (_IssueDrivingLicense())
            {
                _CurrentMode = _enMode.Update;
                return true;
            }
            else
                return false;
        }

        public static clsLocalLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, CreatedByUserID = -1;
            byte LicenseClassID = 0;
            string Notes = "";
            byte IssueReason =0;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            decimal PaidFees = -1;
            bool IsActive = false;

            if (clsLocalLicenseData.Find(LicenseID,ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLocalLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, _GetIssueReasonAsEnum(IssueReason), CreatedByUserID);
            }

            else
                return null;
        }

        public static int GetLicenseID(int ApplicationID)
        {
            return clsLocalLicenseData.GetLicenseID(ApplicationID);
        }

        public bool IsExpired()
        {
            return (ExpirationDate < DateTime.Now);
        }

        public static string GetLicenseNotes(int LocalLicenseID)
        {
            return clsLocalLicenseData.GetLicenseNotes(LocalLicenseID);
        }

        public static bool DeactivateLicense(int LocalLicenseID)
        {
            return clsLocalLicenseData.DeactivateLicense(LocalLicenseID);
        }

        public static bool HasDriverRenewedLicense(int DriverID, int LicenseClassID,out int RenewedLicenseID)
        {
            RenewedLicenseID = 0;
            return clsLocalLicenseData.HasDriverRenewedLicense(DriverID, LicenseClassID,ref RenewedLicenseID);
        }

        public static int GetDriverID(int LocalLicenseID)
        {
            return clsLocalLicenseData.GetDriverID(LocalLicenseID);
        }

        public static DataTable GetSortedInfo(int DriverID,byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection)
        {
            return clsLocalLicenseData.GetSortedInfo(DriverID,WantedNumOfRecords, ColumnNameToOrderBy, SortDirection);
        }

        public static int GetLocalLicenseIdByLicenseClass(int DriverID,byte LicenseClassID)
        {
            return clsLocalLicenseData.GetLocalLicenseIdByLicenseClass(DriverID, LicenseClassID);
        }
    }
}
