using System;
using System.Data;
using DVLDDataAccessLayer;
using static DVLDBusinessLayer.clsLocalLicense;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApplication:clsApplication
    {
        public enum enMode : byte { AddNew = 0 , Update = 1};
        private enMode _CurrentMode;
        public int LocalDrivingLicenseApplicationID { get; set; }

        public clsLicenseClass LicenseClassInfo;

        private byte _OldLicenseClassID;

        public clsLocalDrivingLicenseApplication(int ApplicantPersonID, byte LicenseClassID, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              : base(ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidApplicationFees, CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassInfo = clsLicenseClass.FindByLicenseClassID(LicenseClassID);
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidApplicationFees = PaidApplicationFees;
            this.CreatedByUserID = CreatedByUserID;
            _OldLicenseClassID = LicenseClassID;
        }

        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID,int ApplicationID,byte LicenseClassID, int ApplicantPersonID, DateTime ApplicationDate, byte ApplicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
              :base(ApplicationID,ApplicantPersonID,ApplicationDate,ApplicationTypeID, ApplicationStatus,LastStatusDate,PaidApplicationFees,CreatedByUserID)            
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassInfo = clsLicenseClass.FindByLicenseClassID(LicenseClassID);
            _CurrentMode = enMode.Update;

            _OldLicenseClassID = LicenseClassInfo.LicenseClassID;
        }

        public static DataTable GetLocalDrivingLicenseApplications(byte WantedNumOfRecords, string SortDirection)
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplications(WantedNumOfRecords, -1, null, -1, SortDirection);
        }

        public static DataTable GetLocalDrivingLicenseApplications(byte WantedNumOfRecords, int LastBroughtLDLAppID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplications(WantedNumOfRecords, LastBroughtLDLAppID
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

            if (clsLocalDrivingLicenseApplicationData.FindByLocalDrivingLicenseAppID(LDLApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                if(Application != null)
                {
                    return new clsLocalDrivingLicenseApplication(LDLApplicationID, Application.ApplicationID, LicenseClassID, Application.ApplicantPersonID
                        , Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            byte LicenseClassID = 0;

            if (clsLocalDrivingLicenseApplicationData.FindByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                if (Application != null)
                {
                    clsLicenseClass LicenseClass = clsLicenseClass.FindByLicenseClassID(LicenseClassID);
                    return new clsLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, Application.ApplicationID, LicenseClassID, Application.ApplicantPersonID, Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate, Application.PaidApplicationFees, Application.CreatedByUserID);
                }
            }

            return null;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
           LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData._AddNewLocalDrivingLicenseApplication(ApplicationID, LicenseClassInfo.LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        public bool AreAllFieldsOldValuesNotChanged()
        {
            return (_OldLicenseClassID == LicenseClassInfo.LicenseClassID && OldApplicationData.ApplicantPersonID == ApplicantPersonID);
        }

        private bool _IsLicenseClassNotChanged()
        {
            return (_OldLicenseClassID == LicenseClassInfo.LicenseClassID);
        }

        private bool UpdateLocalDrivingLicenseApplication()
        {
            if(clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID, LicenseClassInfo.LicenseClassID, _OldLicenseClassID))
            {
                _OldLicenseClassID = LicenseClassInfo.LicenseClassID;
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
                        if (_AddNewLocalDrivingLicenseApplication())
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
                            return UpdateLocalDrivingLicenseApplication();
                }
            }
                return false;
        }

        public int IssueDrivingLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            clsDriver NewDriver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (NewDriver == null)
            {
                NewDriver = new clsDriver(this.ApplicantPersonID, CreatedByUserID);

                if (!NewDriver.Save())
                    return -1;
            }
            
            clsLocalLicense NewLicense = new clsLocalLicense
              (
                this.ApplicationID, this.ApplicantPersonID, NewDriver.DriverID, this.LicenseClassInfo.LicenseClassID, DateTime.Now, DateTime.Now.AddYears(LicenseClassInfo.DefaultValidityLength)
               , Notes, true, enIssueReason.FirstTime, CreatedByUserID
              );

            if (!NewLicense.Save())
                return -1;

            else
            {
                this.SetComplete();
                return NewLicense.LicenseID;
            }
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
                PersonApplicationStatus = (enApplicationStatus) ApplicationStatus;
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

        public bool PassedAllTests()
        {
            return (clsTest.GetTotalPassedTests(LocalDrivingLicenseApplicationID) == 3);
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
            return clsTest.GetLastTestByPersonAndTestTypeAndLicenseClass(ApplicantPersonID, LicenseClassInfo.LicenseClassID, (byte)TestTypeID);
        }

        public bool HasActiveLocalLicense()
        {
            return clsLocalLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID,this.LicenseClassInfo.LicenseClassID) != -1;
        }

        public int GetActiveLicenseID()
        {
            return clsLocalLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassInfo.LicenseClassID);
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
