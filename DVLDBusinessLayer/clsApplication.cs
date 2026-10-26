using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {
        public enum enMode : byte { AddNew = 0 , Update = 1}
        public enMode CurrentMode;
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
            PersonInfo = CreatedByUserInfo.PersonalInfo;

            OldApplicationData = new clsApplicationData.clsOldApplicationData(ApplicantPersonID, _GetApplicationStatusNumericValue(ApplicationStatus));
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
            PersonInfo = CreatedByUserInfo.PersonalInfo;
            CurrentMode = enMode.Update;

            OldApplicationData = new clsApplicationData.clsOldApplicationData(ApplicantPersonID, _GetApplicationStatusNumericValue(ApplicationStatus));
        }

        private static byte _GetApplicationStatusNumericValue(enApplicationStatus Status)
        {
            return (byte)Status;
        }

        protected static enApplicationStatus _GetApplicationStatus(byte ApplicationStatus)
        {
            switch(ApplicationStatus)
            {
                case 1:
                    return enApplicationStatus.New;

                case 2:
                    return enApplicationStatus.Canceled;

                case 3:
                    return enApplicationStatus.Completed;

                default:
                    return enApplicationStatus.New;
            }
            
        }

        private bool _AddNewApplication()
        {
            ApplicationID = clsApplicationData.AddApplication(ApplicantPersonID, ApplicationDate, ApplicationTypeID, _GetApplicationStatusNumericValue(ApplicationStatus), LastStatusDate, PaidApplicationFees, CreatedByUserID);

            return (ApplicationID != -1);
        }

        protected bool HasOldApplicationDataNotChanged()
        {
            return (OldApplicationData.ApplicantPersonID == ApplicantPersonID
                && OldApplicationData.ApplicationStatus == _GetApplicationStatusNumericValue(ApplicationStatus));
        }

        private bool _UpdateApplication()
        {
            if(clsApplicationData.UpdateApplication(ApplicationID, ApplicantPersonID, _GetApplicationStatusNumericValue(ApplicationStatus), LastStatusDate,OldApplicationData))
            {
                OldApplicationData = new clsApplicationData.clsOldApplicationData(ApplicantPersonID, _GetApplicationStatusNumericValue(ApplicationStatus));
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
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, _GetApplicationStatus(ApplicationStatus), LastStatusDate, PaidApplicationFees, CreatedByUserID);
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

        public static bool ChangeApplicationStatus(int ApplicationID ,enApplicationStatus TheNewStatus)
        {
            return clsApplicationData.ChangeApplicationStatus(ApplicationID, _GetApplicationStatusNumericValue(TheNewStatus));
        }

        public bool Cancel()
        {
            return ChangeApplicationStatus(ApplicationID, enApplicationStatus.Canceled);
        }

        public bool SetComplete ()
        {
            return ChangeApplicationStatus(ApplicationID, enApplicationStatus.Completed);
        }

    }
}
