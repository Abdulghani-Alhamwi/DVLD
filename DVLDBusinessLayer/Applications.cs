using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplications
    {
        public enum enApplicationStatus {New = 0 , Canceled = 1 , Completed = 2};

        private enum _enMode {AddNew = 0 , Update = 1}
        private _enMode _CurrentMode;

        public int ApplicationID { get; set;}
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public double PaidApplicationFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplications()
        {
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidApplicationFees = -1;
            CreatedByUserID = -1;
            _CurrentMode = _enMode.AddNew;
        }

        private clsApplications(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate,int ApplicationTypeID,enApplicationStatus ApplicationStatus,DateTime LastStatusDate,double PaidApplicationFees,int CreatedByUserID)
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

        private static short _GetApplicationStatus(enApplicationStatus Status)
        {
            switch(Status)
            {
                case enApplicationStatus.New:
                    return 0;

                case enApplicationStatus.Canceled:
                    return 1;

                case enApplicationStatus.Completed:
                    return 2;
            }
            return -1;
        }

        private static enApplicationStatus _GetApplicationStatus(short ApplicationStatus)
        {
            switch(ApplicationStatus)
            {
                case 0:
                    return enApplicationStatus.New;

                case 1:
                    return enApplicationStatus.Canceled;

                case 2:
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

        public static clsApplications Find(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            short ApplicationStatus = -1;
            double PaidApplicationFees = -1;

            if (clsApplicationsData.Find(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidApplicationFees, ref CreatedByUserID))
            {
                return new clsApplications(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, _GetApplicationStatus(ApplicationStatus), LastStatusDate, PaidApplicationFees, CreatedByUserID);
            }
            else
                return null;
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
    }
}
