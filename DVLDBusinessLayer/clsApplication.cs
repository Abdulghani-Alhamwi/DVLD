using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {
        public enum enMode : byte { AddNew = 0 , Update = 1}
        public enMode CurrentMode;

        public enum enApplicationType : byte { NewLocalDrivingLicense = 1, RenewLicense = 2, ReplacementForLostLicense = 3, ReplacementForDamagedLicense = 4, ReleaseDetainedLicense = 5, NewInternationlLicense = 6, ReTakeTest = 7 }
        public enum enApplicationStatus : byte { New = 1 , Canceled = 2 , Completed = 3};

        public int ApplicationID { get; set;}
        public int ApplicantPersonID { get; set; }
        public clsPerson PersonInfo;
        public DateTime ApplicationDate { get; set; }
        public byte ApplicationTypeID { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText 
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";

                    case enApplicationStatus.Canceled:
                        return "Canceled";

                    case enApplicationStatus.Completed:
                        return "Completed";

                    default:
                        return "Unknown";
                }   
            }
        }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidApplicationFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        protected clsApplicationData.clsOldApplicationData OldApplicationData;

        public clsApplication(int ApplicantPersonID, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidApplicationFees = PaidApplicationFees;
            this.CreatedByUserID = CreatedByUserID;
            CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            PersonInfo = clsPerson.Find(ApplicantPersonID);

            OldApplicationData = new clsApplicationData.clsOldApplicationData(ApplicantPersonID, (byte)ApplicationStatus);
        }

        protected clsApplication(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate,byte ApplicationTypeID,enApplicationStatus ApplicationStatus,DateTime LastStatusDate, decimal PaidApplicationFees,int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidApplicationFees = PaidApplicationFees;
            this.CreatedByUserID = CreatedByUserID;
            CreatedByUserInfo = clsUser.Find(CreatedByUserID);
            PersonInfo = clsPerson.Find(ApplicantPersonID);
            CurrentMode = enMode.Update;

            OldApplicationData = new clsApplicationData.clsOldApplicationData(ApplicantPersonID, (byte) ApplicationStatus);
        }

        private bool _AddNewApplication()
        {
            ApplicationID = clsApplicationData.AddApplication(ApplicantPersonID, ApplicationDate, ApplicationTypeID, (byte)ApplicationStatus, LastStatusDate, PaidApplicationFees, CreatedByUserID);

            return (ApplicationID != -1);
        }

        protected bool HasOldApplicationDataNotChanged()
        {
            return (OldApplicationData.ApplicantPersonID == ApplicantPersonID
                && OldApplicationData.ApplicationStatus == (byte) ApplicationStatus);
        }

        private bool _UpdateApplication()
        {
            if(clsApplicationData.UpdateApplication(ApplicationID, ApplicantPersonID, (byte)ApplicationStatus, LastStatusDate,OldApplicationData))
            {
                OldApplicationData = new clsApplicationData.clsOldApplicationData(ApplicantPersonID, (byte)ApplicationStatus);
                return true;
            }

            return false;
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, CreatedByUserID = -1;
            byte ApplicationTypeID = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 0;
            decimal PaidApplicationFees = -1;

            if (clsApplicationData.Find(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidApplicationFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus) ApplicationStatus, LastStatusDate, PaidApplicationFees, CreatedByUserID);
            }
            else
                return null;
        }

       public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public bool Save()
        {
            switch(CurrentMode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        CurrentMode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        public static bool ChangeApplicationStatus(int ApplicationID,enApplicationStatus ApplicationStatus)
        {
            return clsApplicationData.ChangeApplicationStatus(ApplicationID, (byte) ApplicationStatus);
        }

        public bool Cancel()
        {
            return clsApplicationData.ChangeApplicationStatus(ApplicationID, (byte) enApplicationStatus.Canceled);
        }

        public bool SetComplete ()
        {
            return clsApplicationData.ChangeApplicationStatus(ApplicationID,(byte) enApplicationStatus.Completed);
        }

    }
}
