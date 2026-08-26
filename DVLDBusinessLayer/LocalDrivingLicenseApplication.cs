using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLDLApplication:clsApplication
    {
        public enum enMode : byte { AddNew = 0 , Update = 1};
        private enMode _CurrentMode;
        public int LDLAppID { get; set; }

        public clsLicenseClass LicenseClass;
        public clsLDLApplication(int ApplicantPersonID, byte LicenseClassID, string LicenseClassName, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              : base(ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidApplicationFees, CreatedByUserID)
        {
            this.LDLAppID = -1;
            this.ApplicationID = -1;
            this.LicenseClass = new clsLicenseClass(LicenseClassID, LicenseClassName);
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidApplicationFees = PaidApplicationFees;
            this.CreatedByUserID = CreatedByUserID;
        }

        private clsLDLApplication(int LDLApplicationID,int ApplicationID,clsLicenseClass LicenseClass, int ApplicantPersonID, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              :base(ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID, ApplicationStatus,LastStatusDate,PaidApplicationFees,CreatedByUserID)            
        {
            this.LDLAppID = LDLApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClass = LicenseClass;
            _CurrentMode = enMode.Update;
            
        }
        public static DataTable GetLDLApplications(byte WantedNumberOfRecords)
        {
            return clsLDLApplicationsData.GetLDLApplications(WantedNumberOfRecords,-1);
        }
        public static DataTable GetLDLApplications(byte WantedNumberOfRecords, int LastLowestBroughtLDLApplicationID)
        {
            return clsLDLApplicationsData.GetLDLApplications(WantedNumberOfRecords, LastLowestBroughtLDLApplicationID);
        }
        public static uint GetTotalLDLApplicationsCount()
        {
            return clsLDLApplicationsData.GetTotalLDLApplicationsCount();
        }
        public static new clsLDLApplication Find(int LDLApplicationID)
        {
            byte ApplicationID = 0,LicenseClassID = 0;
            string LicenseClassName = "";

            if (clsLDLApplicationsData.Find(LDLApplicationID, ref ApplicationID, ref LicenseClassID, ref LicenseClassName))
            {
            clsApplication Application = clsApplication.Find(ApplicationID);

                if(Application!=null)
                {
                    clsLicenseClass LicenseClass = new clsLicenseClass(LicenseClassID, LicenseClassName);
                    return new clsLDLApplication(LDLApplicationID, Application.ApplicationID,LicenseClass, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        private bool _AddLDLApplication()
        {
           LDLAppID = clsLDLApplicationsData.AddLDLApplication(ApplicationID, LicenseClass.ID);

            return (LDLAppID != -1);
        }

        private bool UpdateLDLApplication()
        {
            return clsLDLApplicationsData.UpdateLDLApplication(LDLAppID,ApplicationID, LicenseClass.ID);
        }

        public static bool DeleteLDLApplication(int LDLApplicationID,int ApplicationID)
        {
            clsLDLApplicationsData.DeleteLDLApplication(LDLApplicationID);
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
            return clsLDLApplicationsData.GetApplicationID(LDLApplicationID);
        }

        public static bool HasPersonApplied(int ApplicantPersonID, byte LicenseClassID,out enApplicationStatus?PersonApplicationStatus)
        {
            byte ApplicationStatus;
            PersonApplicationStatus = null;
            if (clsLDLApplicationsData.HasPersonApplied(ApplicantPersonID, LicenseClassID, out ApplicationStatus))
            {
                return true;
            }
            else
                PersonApplicationStatus = _GetApplicationStatus(ApplicationStatus);
                return false;
        }

        public static bool IsPersonAgeAppropriate(int PersonID, byte LicenseClassID)
        {
            return clsLDLApplicationsData.IsPersonAgeAppropriate(PersonID, LicenseClassID);
        }

    public static int GetLDLApplicationID(int ApplicantPersonID)
    {
            return clsLDLApplicationsData.GetLDLApplicationID(ApplicantPersonID);
    }

    public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null)
    {
        return clsLDLApplicationsData.GetFilteredData(WantedNumberOfRecords,ColumnNameToFilter,ValueToFilterBy,WildChar,-1);
    }

   public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, int LastLowestBroughtLDLAppID, char? WildChar = null)
   {
       return clsLDLApplicationsData.GetFilteredData(WantedNumberOfRecords, ColumnNameToFilter, ValueToFilterBy, WildChar, LastLowestBroughtLDLAppID);
   }

        public static sbyte GetPassedTests(int LDLApplicationID)
    {
            return clsLDLApplicationsData.GetPassedTests(LDLApplicationID);
    }
    public static DataTable GetColumnsNamesForView()
    {
        return clsLDLApplicationsData.GetColumnsNamesForView();
    }
    }
}
