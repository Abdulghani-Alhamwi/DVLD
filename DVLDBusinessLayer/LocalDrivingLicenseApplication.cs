using System;
using System.Data;
using System.Diagnostics.Contracts;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        public enum enMode { AddNew = 0 , Update = 1};
        private enMode _CurrentMode;
        public int LDLApplicationID { get; set; }

        public clsLicenseClasses LicenseClass;

        public clsLocalDrivingLicenseApplication()
        {
            LDLApplicationID = -1;
            LicenseClass = new clsLicenseClasses();
            _CurrentMode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int LDLApplicationID,int ApplicationID,clsLicenseClasses LicenseClass, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              :base(ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID, ApplicationStatus,LastStatusDate,PaidApplicationFees,CreatedByUserID)            
        {
            this.LDLApplicationID = LDLApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClass = LicenseClass;
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

        public static new clsLocalDrivingLicenseApplication Find(int LDLApplicationID)
        {
            int ApplicationID = -1,LicenseClassID = -1;
            string LicenseClassName = "";

            if (clsLocalDrivingLicenseApplicationsData.Find(LDLApplicationID, ref ApplicationID, ref LicenseClassID, ref LicenseClassName))
            {
            clsApplication Application = clsApplication.Find(ApplicationID);

                if(Application!=null)
                {
                    clsLicenseClasses LicenseClass = new clsLicenseClasses() { ID = LicenseClassID, ClassName =  LicenseClassName};
                    return new clsLocalDrivingLicenseApplication(LDLApplicationID, Application.ApplicationID,LicenseClass, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        private bool _AddLDLApplication()
        {
           LDLApplicationID = clsLocalDrivingLicenseApplicationsData.AddLDLApplication(ApplicationID, LicenseClass.ID);

            return (LDLApplicationID != -1);
        }

        private bool UpdateLDLApplication()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLDLApplication(LDLApplicationID,ApplicationID, LicenseClass.ID);
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

    public static sbyte GetPassedTests(int LDLApplicationID)
    {
            return clsLocalDrivingLicenseApplicationsData.GetPassedTests(LDLApplicationID);
    }
    public static DataTable GetColumnsNamesForView()
    {
        return clsLocalDrivingLicenseApplicationsData.GetColumnsNamesForView();
    }
    }
}
