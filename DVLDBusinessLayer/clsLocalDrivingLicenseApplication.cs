    using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        public enum enMode : byte { AddNew = 0 , Update = 1};
        private enMode _CurrentMode;
        public int LocalDrivingLicenseApplicationID { get; set; }

        public clsLicenseClass LicenseClassInfo;

        private int _OldLicenseClassID;

        public clsLocalDrivingLicenseApplication(int ApplicantPersonID, byte LicenseClassID, string LicenseClassName, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              : base(ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidApplicationFees, CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassInfo = new clsLicenseClass(LicenseClassID, LicenseClassName);
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidApplicationFees = PaidApplicationFees;
            this.CreatedByUserID = CreatedByUserID;
            _OldLicenseClassID = LicenseClassID;
        }

        private clsLocalDrivingLicenseApplication(int LDLApplicationID,int ApplicationID,clsLicenseClass LicenseClass, int ApplicantPersonID, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              :base(ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID, ApplicationStatus,LastStatusDate,PaidApplicationFees,CreatedByUserID)            
        {
            this.LocalDrivingLicenseApplicationID = LDLApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassInfo = LicenseClass;
            _CurrentMode = enMode.Update;

            _OldLicenseClassID = LicenseClass.ID;
        }

        public static DataTable GetLocalDrivingLicenseApplications(byte WantedNumOfRecords, string SortDirection)
        {
            return clsLocalDrivingLicenseApplicationData.GetLDLApplications(WantedNumOfRecords, -1, null, -1, SortDirection);
        }

        public static DataTable GetLocalDrivingLicenseApplications(byte WantedNumOfRecords, int LastBroughtLDLAppID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsLocalDrivingLicenseApplicationData.GetLDLApplications(WantedNumOfRecords, LastBroughtLDLAppID
                , LastColumnNameDataOrderedBy, NumberOfRowsToOffset, SortDirection);
        }

        public static int GetTotalLDLApplicationsCount()
        {
            return clsLocalDrivingLicenseApplicationData.GetTotalLDLApplicationsCount();
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseAppID(int LDLApplicationID)
        {
            int ApplicationID = -1;
            byte LicenseClassID = 0;
            string LicenseClassName = "";

            if (clsLocalDrivingLicenseApplicationData.FindByLocalDrivingLicenseAppID(LDLApplicationID, ref ApplicationID, ref LicenseClassID, ref LicenseClassName))
            {
            clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                if(Application!=null)
                {
                    clsLicenseClass LicenseClass = new clsLicenseClass(LicenseClassID, LicenseClassName);
                    return new clsLocalDrivingLicenseApplication(LDLApplicationID, Application.ApplicationID,LicenseClass, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            byte LicenseClassID = 0;
            string LicenseClassName = "";

            if (clsLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID, ref LicenseClassName))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                if (Application != null)
                {
                    clsLicenseClass LicenseClass = new clsLicenseClass(LicenseClassID, LicenseClassName);
                    return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID, LicenseClass, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        private bool _AddLDLApplication()
        {
           LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddLDLApplication(ApplicationID, LicenseClassInfo.ID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        public bool AreAllFieldsOldValuesNotChanged()
        {
            return (_OldLicenseClassID == LicenseClassInfo.ID && OldApplicationData.ApplicantPersonID == ApplicantPersonID);
        }

        private bool _IsLicenseClassNotChanged()
        {
            return (_OldLicenseClassID == LicenseClassInfo.ID);
        }

        private bool UpdateLDLApplication()
        {
            if(clsLocalDrivingLicenseApplicationData.UpdateLDLApplication(LocalDrivingLicenseApplicationID, LicenseClassInfo.ID, _OldLicenseClassID))
            {
                _OldLicenseClassID = LicenseClassInfo.ID;
                return true;
            }

            return false;
        }

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,int ApplicationID)
        {
            if (clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID))
                return clsApplication.DeleteApplication(ApplicationID);

            else
                return false;
        }

        private bool _SaveApplication()
        {
            base.CurrentMode = (clsApplication.enMode)_CurrentMode;

            if (_CurrentMode == enMode.Update)
            {
                if (HasOldApplicationDataNotChanged())
                    return true;
            }

                return base.Save();
        }

        public bool Save()
        {
            if (_SaveApplication())
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
                        if(_IsLicenseClassNotChanged())
                            return true;

                        else
                            return UpdateLDLApplication();
                }
            }
                return false;
        }

        public static int GetApplicationID(int LocalDrivingLicenseApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.GetApplicationID(LocalDrivingLicenseApplicationID);
        }

        public static bool HasPersonApplied(int ApplicantPersonID, byte LicenseClassID,ref enApplicationStatus PersonApplicationStatus)
        {
            byte ApplicationStatus = 0;
            if (clsLocalDrivingLicenseApplicationData.HasPersonApplied(ApplicantPersonID, LicenseClassID, ref ApplicationStatus))
            {
                PersonApplicationStatus = _GetApplicationStatus(ApplicationStatus);
                return true;
            }
            else
                return false;
        }

        public static bool IsPersonAgeAppropriate(int PersonID, byte LicenseClassID)
        {
            return clsLocalDrivingLicenseApplicationData.IsPersonAgeAppropriate(PersonID, LicenseClassID);
        }

        public static int GetLocalDrivingLicenseApplicationID(int ApplicantPersonID)
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationID(ApplicantPersonID);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string LastColumnNameDataOrderedBy, string SortDirection, char? WildChar = null)
        {
            return clsLocalDrivingLicenseApplicationData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection, -1, -1, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string LastColumnNameDataOrderedBy, string SortDirection
            , int LastBroughtLDLAppID, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            return clsLocalDrivingLicenseApplicationData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection, LastBroughtLDLAppID,NumberOfRowsToOffset, WildChar);
        }

        public static byte GetTotalPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetTotalPassedTests(LocalDrivingLicenseApplicationID);
        }

        public byte GetTotalPassedTests()
        {
            return clsTest.GetTotalPassedTests(LocalDrivingLicenseApplicationID);
        }

        public bool DoesPassedTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.HasPassedTheTest(LocalDrivingLicenseApplicationID, Convert.ToByte(TestTypeID));
        }

        public clsTest GetLastTestByTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.GetLastTestByPersonAndTestTypeAndLicenseClass(ApplicantPersonID, LicenseClassInfo.ID, (byte)TestTypeID);
        }

        public bool HasActiveLocalLicense()
        {
            return clsLocalLicense.GetLicenseID(ApplicationID) != -1;
        }

        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(LocalDrivingLicenseApplicationID, (byte) TestTypeID);
        }

        public bool HasActiveScheduledAppointment(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointment.HasActiveScheduledAppointment(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static short GetTotalTrialsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTest.GetTotalTrialsPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public short GetTotalTrialsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.GetTotalTrialsPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static DataTable GetColumnsNamesForView()
        {
            return clsLocalDrivingLicenseApplicationData.GetColumnsNamesForView();
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection
             , string ColumnNameToFilterBy = null, string valueToFilterBy = null, char? WildChar = null)
        {
            return clsLocalDrivingLicenseApplicationData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,
                ColumnNameToFilterBy, valueToFilterBy, WildChar);
        }

    }
}
