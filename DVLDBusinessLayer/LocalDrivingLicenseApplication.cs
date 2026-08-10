using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        public enum enMode { AddNew = 0 , Update = 1};
        private enMode _CurrentMode;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            _CurrentMode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,int ApplicationID,int LicenseClassID,int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, double PaidApplicationFees, int CreatedByUserID)
              :base(ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID, ApplicationStatus,LastStatusDate,PaidApplicationFees,CreatedByUserID)            
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            _CurrentMode = enMode.Update;
            
        }

        public static DataTable GetLDLApplications(byte WantedNumberOfRecords, int LastLowestBroughtLDLApplicationID = -1)
        {
            return clsLocalDrivingLicenseApplicationsData.GetLDLApplications(WantedNumberOfRecords,LastLowestBroughtLDLApplicationID);
        }

        public static uint GetTotalLDLApplicationsCount()
        {
            return clsLocalDrivingLicenseApplicationsData.GetTotalLDLApplicationsCount();
        }

        public static new clsLocalDrivingLicenseApplication Find(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1,LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationsData.Find(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
            clsApplication Application = clsApplication.Find(ApplicationID);

                if(Application!=null)
                { 
                    return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID, LicenseClassID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        private bool _AddLDLApplication()
        {
           LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddLDLApplication(ApplicationID, LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        private bool UpdateLDLApplication()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLDLApplication(LocalDrivingLicenseApplicationID,ApplicationID, LicenseClassID);
        }

        public static bool DeleteLDLApplication(int LDLApplicationID,int ApplicationID)
        {
            clsLocalDrivingLicenseApplicationsData.DeleteLDLApplication(LDLApplicationID);
            return clsApplication.DeleteApplication(ApplicationID);
        }
        public new bool Save()
        {
            if (base.Save())
            {
                switch (_CurrentMode)
                {
                    case enMode.AddNew:
                        if (_AddLDLApplication())
                        {
                            _CurrentMode = enMode.Update;
                            return true;
                        }
                        else
                            return false;

                    case enMode.Update:
                        return UpdateLDLApplication();
                }
            }
                return false;
        }

        public static int GetApplicationID(int LDLApplicationID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetApplicationID(LDLApplicationID);
        }

        public static bool CanPersonApply(int ApplicantPersonID, int LicenseClassID,out enApplicationStatus?PersonApplicationStatus)
        {
            byte ApplicationStatus;
            PersonApplicationStatus = null;
            if (clsLocalDrivingLicenseApplicationsData.CanPersonApply(ApplicantPersonID, LicenseClassID, out ApplicationStatus))
            {
                return true;
            }
            else
                PersonApplicationStatus = _GetApplicationStatus(ApplicationStatus);
                return false;
        }

    public static int GetLDLApplicationID(int ApplicantPersonID)
    {
            return clsLocalDrivingLicenseApplicationsData.GetLDLApplicationID(ApplicantPersonID);
    }

        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null, int LastLowestBroughtLDLAppID = -1)
        {
            return clsLocalDrivingLicenseApplicationsData.GetFilteredData(WantedNumberOfRecords,ColumnNameToFilter,ValueToFilterBy,WildChar,LastLowestBroughtLDLAppID);
        }
}
}
