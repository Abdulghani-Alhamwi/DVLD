using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApp:clsApplication
    {
        public enum enMode : byte { AddNew = 0 , Update = 1};
        private enMode _CurrentMode;
        public int LDLAppID { get; set; }

        public clsLicenseClass LicenseClass;
        public clsLocalDrivingLicenseApp(int ApplicantPersonID, byte LicenseClassID, string LicenseClassName, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
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

        private clsLocalDrivingLicenseApp(int LDLApplicationID,int ApplicationID,clsLicenseClass LicenseClass, int ApplicantPersonID, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              :base(ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID, ApplicationStatus,LastStatusDate,PaidApplicationFees,CreatedByUserID)            
        {
            this.LDLAppID = LDLApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClass = LicenseClass;
            _CurrentMode = enMode.Update;
            
        }
        public static DataTable GetLDLApplications(byte WantedNumOfRecords)
        {
            return clsLocalDrivingLicenseAppData.GetLDLApplications(WantedNumOfRecords);
        }
        public static DataTable GetLDLApplications(byte WantedNumOfRecords, int LastLowestBroughtLDLApplicationID)
        {
            return clsLocalDrivingLicenseAppData.GetLDLApplications(WantedNumOfRecords, LastLowestBroughtLDLApplicationID);
        }
        public static int GetTotalLDLApplicationsCount()
        {
            return clsLocalDrivingLicenseAppData.GetTotalLDLApplicationsCount();
        }
        public static new clsLocalDrivingLicenseApp Find(int LDLApplicationID)
        {
            byte ApplicationID = 0,LicenseClassID = 0;
            string LicenseClassName = "";

            if (clsLocalDrivingLicenseAppData.Find(LDLApplicationID, ref ApplicationID, ref LicenseClassID, ref LicenseClassName))
            {
            clsApplication Application = clsApplication.Find(ApplicationID);

                if(Application!=null)
                {
                    clsLicenseClass LicenseClass = new clsLicenseClass(LicenseClassID, LicenseClassName);
                    return new clsLocalDrivingLicenseApp(LDLApplicationID, Application.ApplicationID,LicenseClass, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        private bool _AddLDLApplication()
        {
           LDLAppID = clsLocalDrivingLicenseAppData.AddLDLApplication(ApplicationID, LicenseClass.ID);

            return (LDLAppID != -1);
        }

        private bool UpdateLDLApplication()
        {
            return clsLocalDrivingLicenseAppData.UpdateLDLApplication(LDLAppID,ApplicationID, LicenseClass.ID);
        }

        public static bool DeleteLDLApplication(int LDLApplicationID,int ApplicationID)
        {
            clsLocalDrivingLicenseAppData.DeleteLDLApplication(LDLApplicationID);
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
            return clsLocalDrivingLicenseAppData.GetApplicationID(LDLApplicationID);
        }

        public static bool HasPersonApplied(int ApplicantPersonID, byte LicenseClassID,ref enApplicationStatus PersonApplicationStatus)
        {
            byte ApplicationStatus = 0;
            if (clsLocalDrivingLicenseAppData.HasPersonApplied(ApplicantPersonID, LicenseClassID, ref ApplicationStatus))
            {
                PersonApplicationStatus = _GetApplicationStatus(ApplicationStatus);
                return true;
            }
            else
                return false;
        }

        public static bool IsPersonAgeAppropriate(int PersonID, byte LicenseClassID)
        {
            return clsLocalDrivingLicenseAppData.IsPersonAgeAppropriate(PersonID, LicenseClassID);
        }

    public static int GetLDLApplicationID(int ApplicantPersonID)
    {
            return clsLocalDrivingLicenseAppData.GetLDLApplicationID(ApplicantPersonID);
    }

    public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null)
    {
        return clsLocalDrivingLicenseAppData.GetFilteredData(WantedNumOfRecords,ColumnNameToFilter,ValueToFilterBy,-1,WildChar);
    }

   public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, int LastLowestBroughtLDLAppID, char? WildChar = null)
   {
       return clsLocalDrivingLicenseAppData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastLowestBroughtLDLAppID, WildChar);
   }

        public static sbyte GetPassedTests(int LDLApplicationID)
    {
            return clsLocalDrivingLicenseAppData.GetPassedTests(LDLApplicationID);
    }
    public static DataTable GetColumnsNamesForView()
    {
        return clsLocalDrivingLicenseAppData.GetColumnsNamesForView();
    }
    }
}
