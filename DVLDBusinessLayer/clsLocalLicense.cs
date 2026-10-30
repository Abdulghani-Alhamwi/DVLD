using System;
using System.Data;
using System.Threading;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalLicense
    {
        private enum _enMode : byte { AddNew = 0 , Update = 1 }
        private _enMode _CurrentMode;
        public enum enIssueReason : byte { FirstTime = 1 , Renew = 2 , ReplacementForDamaged = 3 , ReplacementForLost = 4}
        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int PersonID{get;set; }
        public int DriverID { get; set; }
        public clsDriver DriverInfo;
        public byte LicenseClassID{get;set;}
        public clsLicenseClass LicenseClassInfo;
        public DateTime IssueDate{get;set;}
        public DateTime ExpirationDate{get;set;}
        public string Notes{get;set;}
        public decimal PaidFees{get;set;}
        public bool IsActive{get;set;}
        public enIssueReason IssueReason {get;set;}
        public string IssueReasonText { get { return GetIssueReasonText(); } }    
        public clsDetainedLicense DetainInfo { set; get; }
        public int CreatedByUserID{get;set;}

        public bool IsDetained { get { return clsDetainedLicense.IsDetainedLicense(LicenseID); } }

        public clsLocalLicense(int ApplicationID, int PersonID,int DriverID, byte LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = -1;
            this.ApplicationID = ApplicationID;
            this.PersonID = PersonID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            DriverInfo = clsDriver.FindByDriverID(DriverID);
            LicenseClassInfo = clsLicenseClass.FindByLicenseClassID(LicenseClassID);
            this.PaidFees = Convert.ToDecimal((LicenseClassInfo != null) ? LicenseClassInfo.ClassFees : 0);
        }

        private clsLocalLicense(int LocalLicenseID, int ApplicationID, int DriverID, byte LicenseClassID, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LocalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClassID = LicenseClassID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            LicenseClassInfo = clsLicenseClass.FindByLicenseClassID(LicenseClassID);
            DriverInfo = clsDriver.FindByDriverID(DriverID);
            DetainInfo = clsDetainedLicense.Find(LocalLicenseID);
            this.PaidFees = PaidFees;
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

        public string GetIssueReasonText()
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

        private bool _AddNewLocalLicense()
        {
            LicenseID = clsLocalLicenseData.IssueLocalDrivingLicense(ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);

            return (LicenseID != -1);
        }

        public bool Save()
        {
            if (_CurrentMode == _enMode.Update)
                return false;

            if (_AddNewLocalLicense())
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
                return new clsLocalLicense(LicenseID, ApplicationID, DriverID, LicenseClassID, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
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

        public bool DeactivateLicense()
        {
            return clsLocalLicenseData.DeactivateLicense(LicenseID);
        }

        public clsLocalLicense Renew(string Notes, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication
             (
                this.PersonID, DateTime.Now, (byte)clsApplication.enApplicationType.RenewLicense,
                clsApplication.enApplicationStatus.New, DateTime.Now, clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.RenewLicense),
                CreatedByUserID
             );


            if (!Application.Save())
            {
                return null;
            }

            clsLocalLicense NewLicense = new clsLocalLicense
              (
              Application.ApplicationID, this.PersonID,this.DriverID, this.LicenseClassID, DateTime.Now, DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength)
              , Notes, true, enIssueReason.Renew, CreatedByUserID
              );

            if (!NewLicense.Save())
            {
                return null;
            }

            if (this.DeactivateLicense())
            {
                if (Application.SetComplete())
                    return NewLicense;
            }

            return null;
        }

        public int Detain(decimal FineFees,int CreatedByUserID)
        {
            DetainInfo = new clsDetainedLicense(LicenseID, DateTime.Now, FineFees, CreatedByUserID);

            if (!DetainInfo.Save())
                return -1;

            return DetainInfo.DetainID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID)
        {
            clsApplication ReleaseApplication = new clsApplication
             (
             PersonID, DateTime.Now, (byte)clsApplication.enApplicationType.ReleaseDetainedLicense,
             clsApplication.enApplicationStatus.New, DateTime.Now, clsApplicationType.GetApplicationTypeFees(clsApplication.enApplicationType.ReleaseDetainedLicense),
             ReleasedByUserID
             );

            if(!ReleaseApplication.Save())
            {
                return false;
            }

            if(this.DetainInfo.ReleaseDetainedLicense(ReleasedByUserID, ReleaseApplication.ApplicationID))
            {
                return ReleaseApplication.SetComplete();
            }

            else
            {
                return false;
            }
        }

        public clsLocalLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication
             (
               this.PersonID, DateTime.Now, (IssueReason == enIssueReason.ReplacementForDamaged) ? (byte)enIssueReason.ReplacementForDamaged : (byte)enIssueReason.ReplacementForLost
               , clsApplication.enApplicationStatus.New, DateTime.Now, 0, CreatedByUserID
             );

            if (!Application.Save())
            {
                return null;
            }

            clsLocalLicense NewLicense = new clsLocalLicense
              (
                 Application.ApplicationID, this.PersonID,this.DriverID, this.LicenseClassID,DateTime.Now, this.ExpirationDate
                , this.Notes, true, (IssueReason == enIssueReason.ReplacementForDamaged) ? enIssueReason.ReplacementForDamaged : enIssueReason.ReplacementForLost, CreatedByUserID
              );

            if (!NewLicense.Save())
            {
                return null;
            }

            if (this.DeactivateLicense())
            {
                if (clsInternationalLicenseData.LinkWithNewLocalLicense(this.DriverID, NewLicense.LicenseID))
                {
                    if (Application.SetComplete())
                        return NewLicense;
                }
            }

            return null;
        }

        public static bool HasDriverRenewedLicense(int DriverID, int LicenseClassID,out int RenewedLicenseID)
        {
            RenewedLicenseID = 0;
            return clsLocalLicenseData.HasDriverRenewedLicense(DriverID, LicenseClassID,ref RenewedLicenseID);
        }

        public static bool IsLicenseExistsByPersonID(int PersonID, byte LicenseClassID)
        {
            return (GetActiveLicenseIDByPersonID(PersonID, LicenseClassID) != -1);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, byte LicenseClassID)
        {
            return clsLocalLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
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
