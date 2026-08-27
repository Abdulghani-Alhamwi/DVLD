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
        public int DriverID{get;set;}
        public byte LicenseClassID{get;set;}
        public DateTime IssueDate{get;set;}
        public DateTime ExpirationDate{get;set;}
        public string Notes{get;set;}
        public decimal PaidFees{get;set;}
        public bool IsActive{get;set;}
        public enIssueReason IssueReason {get;set;}
        public int CreatedByUserID{get;set;}

        public clsLocalLicense(int ApplicationID, int DriverID, byte LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = -1;
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
            _CurrentMode = _enMode.Update;
        }

        public static DataTable GetLocalLicenses(int DriverID,byte WantedNumberOfRecords)
        {
            return clsLocalLicensesData.GetLocalLicenses(DriverID,WantedNumberOfRecords, -1);
        }

        public static DataTable GetLocalLicenses(int DriverID,byte WantedNumberOfRecords, int LastLowstBroughtLicID)
        {
            return clsLocalLicensesData.GetLocalLicenses(DriverID,WantedNumberOfRecords, LastLowstBroughtLicID);
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
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return 1;

                case enIssueReason.Renew:
                    return 2;

                case enIssueReason.ReplacementForDamaged:
                    return 3;

                case enIssueReason.ReplacementForLost:
                    return 4;
            }
            return 0;
        }
        private static enIssueReason _GetIssueReasonAsEnum(byte IssueReason)
        {
            switch (IssueReason)
            {
                case 1:
                    return enIssueReason.FirstTime;

                case 2:
                    return enIssueReason.Renew;

                case 3:
                    return enIssueReason.ReplacementForDamaged;

                case 4:
                    return enIssueReason.ReplacementForLost;
            }
            return enIssueReason.FirstTime;
        }
        private bool _IssueDrivingLicense()
        {
            LicenseID = clsLocalLicensesData.IssueDrivingLicense(ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, _GetIssueReasonAsNumber(IssueReason), CreatedByUserID);

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

            if (clsLocalLicensesData.Find(LicenseID,ref ApplicationID, ref DriverID, ref LicenseClassID, ref IssueDate,
                ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLocalLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, _GetIssueReasonAsEnum(IssueReason), CreatedByUserID);
            }

            else
                return null;
        }

        public static int GetLicenseID(int ApplicationID)
        {
            return clsLocalLicensesData.GetLicenseID(ApplicationID);
        }
        public static short GetTotalDriverLicensesCount(int DriverID)
        {
            return clsLocalLicensesData.GetTotalDriverLicensesCount(DriverID);
        }
    }
}
