using System;
using System.Data;
using System.Runtime.InteropServices;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
        private enum _enMode : byte {AddNew = 0 , Update = 1}
        private _enMode _CurrentMode;
        public int TestAppointmentID { get; set; }
        public clsTestType.enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        public clsApplication RetakeTestApplicationInfo;

        private clsTestAppointmentData.clsOldAppointmentData _OldAppointmentData;

        public clsTestAppointment(clsTestType.enTestType TestTypeID, int LDLApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked,int RetakeTestApplicationID = -1)
        {
            TestAppointmentID = -1;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LDLApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
            this.CreatedByUserID = CreatedByUserID;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);
            _OldAppointmentData = new clsTestAppointmentData.clsOldAppointmentData(AppointmentDate, IsLocked);
        }

        private clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID,int LDLApplicationID,DateTime AppointmentDate,decimal PaidFees,int CreatedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LDLApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            RetakeTestApplicationInfo = clsApplication.Find(RetakeTestApplicationID);
            _CurrentMode = _enMode.Update;

            _OldAppointmentData = new clsTestAppointmentData.clsOldAppointmentData(AppointmentDate, IsLocked);
        }

        public static DataTable GetTestAppointments(byte WantedNumOfRecords,byte TestTypeID,int LocalDrivingLicenseApplicationID)
        {
            return clsTestAppointmentData.GetTestAppointments(WantedNumOfRecords,TestTypeID,LocalDrivingLicenseApplicationID);
        }

        public static DataTable GetTestAppointments(byte WantedNumOfRecords, byte TestTypeID, int LocalDrivingLicenseApplicationID, int _LastBroughtAppointmentID = -1
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsTestAppointmentData.GetTestAppointments(WantedNumOfRecords, TestTypeID, LocalDrivingLicenseApplicationID, _LastBroughtAppointmentID
                , LastColumnNameDataOrderedBy, NumberOfRowsToOffset, SortDirection);
        }

        private bool _AddNewAppointment()
        {
            TestAppointmentID = clsTestAppointmentData.AddNewAppointment((byte)TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked,RetakeTestApplicationID);

            return (TestAppointmentID != -1);
        }

        public bool HasAppointmentDateChanged()
        {
            return (_OldAppointmentData.AppointmentDate == AppointmentDate);
        }

        private bool _HasOldDataChangedFully()
        {
            return (_OldAppointmentData.AppointmentDate == AppointmentDate
                && _OldAppointmentData.IsLocked == IsLocked);
        }

        private bool _UpdateAppointment()
        {
            if(clsTestAppointmentData.UpdateAppointment(TestAppointmentID,AppointmentDate,IsLocked, _OldAppointmentData, _HasOldDataChangedFully()))
            {
                _OldAppointmentData = new clsTestAppointmentData.clsOldAppointmentData(AppointmentDate, IsLocked);
                return true;
            }

            return false;
        }

        public bool Save()
        {
            switch(_CurrentMode)
            {
                case _enMode.AddNew:
                    if (_AddNewAppointment())
                    {
                        _CurrentMode = _enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case _enMode.Update:
                    return _UpdateAppointment();
            }
            return false;
        }

        public static clsTestAppointment Find(int TestAppointmentID)
        {
            byte TestTypeID = 0;
            int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestAppID = -1;

            if (clsTestAppointmentData.Find(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked,ref RetakeTestAppID))
                return new clsTestAppointment(TestAppointmentID,(clsTestType.enTestType) TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked,RetakeTestAppID);
            else
                return null;

        }

        public static clsTestAppointment GetLastTestAppointmentInfo(int LocalDrivingLicenseApplicationID,clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;
            int RetakeTestAppID = -1;

            if (clsTestAppointmentData.GetLastTestAppointmentInfo(LocalDrivingLicenseApplicationID, (byte)TestTypeID, ref TestAppointmentID, ref AppointmentDate
                , ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestAppID))

            {
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestAppID);
            }
            else
                return null;
        }

        public static int GetTotalAppointmentsCount(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            return clsTestAppointmentData.GetTotalAppointmentsCount(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool HasActiveScheduledAppointment(int LocalDrivingLicenseAppID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.HasActiveScheduledAppointment(LocalDrivingLicenseAppID,(byte) TestTypeID);
        }

        public static DataTable GetColumnsNamesForView()
        {
            return clsTestAppointmentData.GetColumnsNamesForView();
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection
            ,int LocalDrivingLicenseApplicationID ,byte CurrentTestTypeID)
        {
            return clsTestAppointmentData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, LocalDrivingLicenseApplicationID, CurrentTestTypeID);
        }
    }
}
