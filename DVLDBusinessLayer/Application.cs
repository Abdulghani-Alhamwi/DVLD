using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplication
    {
        public enum enApplicationStatus : byte { New = 0 , Canceled = 1 , Completed = 2};

        private enum _enMode : byte { AddNew = 0 , Update = 1}
        private _enMode _CurrentMode;

        public int ApplicationID { get; set;}
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public byte ApplicationTypeID { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidApplicationFees { get; set; }
        public int CreatedByUserID { get; set; }

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
            _CurrentMode = _enMode.Update;
        }

        private static byte _GetApplicationStatus(enApplicationStatus Status)
        {
            switch(Status)
            {
                case enApplicationStatus.New:
                    return 1;

                case enApplicationStatus.Canceled:
                    return 2;

                case enApplicationStatus.Completed:
                    return 3;
            }
            return 0;
        }

        public string GetApplicationStatus()
        {
            switch (ApplicationStatus)
            {
                case enApplicationStatus.New:
                    return "New";

                case enApplicationStatus.Canceled:
                    return "Canceled";

                case enApplicationStatus.Completed:
                    return "Completed";
            }
            return "";
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
            ApplicationID = clsApplicationsData.AddApplication(ApplicantPersonID, ApplicationDate, ApplicationTypeID, _GetApplicationStatus(ApplicationStatus), LastStatusDate, PaidApplicationFees, CreatedByUserID);

            return (ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, _GetApplicationStatus(ApplicationStatus), LastStatusDate, PaidApplicationFees, CreatedByUserID);
        }

        public static clsApplication Find(int ApplicationID)
        {
            int ApplicantPersonID = -1, CreatedByUserID = -1;
            byte ApplicationTypeID = 0;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 0;
            decimal PaidApplicationFees = -1;

            if (clsApplicationsData.Find(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidApplicationFees, ref CreatedByUserID))
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, _GetApplicationStatus(ApplicationStatus), LastStatusDate, PaidApplicationFees, CreatedByUserID);
            }
            else
                return null;
        }

       public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsData.DeleteApplication(ApplicationID);
        }
        public bool Save()
        {
            switch(_CurrentMode)
            {
                case _enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        _CurrentMode = _enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case _enMode.Update:
                    return _UpdateApplication();
            }
            return false;
        }

        public static bool ChangeApplicationStatus(int ApplicationID ,enApplicationStatus TheNewStatus)
        {
            return clsApplicationsData.ChangeApplicationStatus(ApplicationID, _GetApplicationStatus(TheNewStatus));
        }

    }
}
