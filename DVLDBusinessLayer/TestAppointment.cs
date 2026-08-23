using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTestAppointment
    {
        private enum _enMode : byte {AddNew = 0 , Update = 1}
        _enMode _CurrentMode;
        public int TestAppointmentID { get; set; }
        public byte TestTypeID { get; set; }
        public int LDLApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public clsTestAppointment(byte TestTypeID, int LDLApplicationID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            TestAppointmentID = -1;
            this.LDLApplicationID = LDLApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.IsLocked = IsLocked;
            this.CreatedByUserID = CreatedByUserID;
        }
        private clsTestAppointment(int TestAppointmentID,byte TestTypeID,int LDLApplicationID,DateTime AppointmentDate,decimal PaidFees,int CreatedByUserID,bool IsLocked)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LDLApplicationID = LDLApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            _CurrentMode = _enMode.Update;
        }

        public static DataTable GetTestAppointments(byte WantedNumberOfRecords,byte TestTypeID,int LDLAppId, int LowestBroughtAppointmentID = -1)
        {
            return clsTestAppointmentsData.GetTestAppointments(WantedNumberOfRecords,TestTypeID,LDLAppId, LowestBroughtAppointmentID);
        }
        private bool _AddNewAppointment()
        {
            TestAppointmentID = clsTestAppointmentsData.AddNewAppointment(TestTypeID, LDLApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);

            return (TestAppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            return clsTestAppointmentsData.UpdateAppointment(TestAppointmentID, TestTypeID, LDLApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
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
            int LDLApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = -1;
            int CreatedByUserID = -1;
            bool IsLocked = false;

            if (clsTestAppointmentsData.Find(TestAppointmentID, ref TestTypeID, ref LDLApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked))
                return new clsTestAppointment(TestAppointmentID, TestTypeID, LDLApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked);
            else
                return null;

        }

        public static ushort GetTotalAppointmentsCount(int LDLApplicationID, byte TestTypeID)
        {
            return clsTestAppointmentsData.GetTotalAppointmentsCount(LDLApplicationID, TestTypeID);
        }

        public static bool IsAppointmentSchedulingAvailable(int LocalDrivingLicenseAppID, byte TestTypeID)
        {
            return clsTestAppointmentsData.IsAppointmentSchedulingAvailable(LocalDrivingLicenseAppID, TestTypeID);
        }
        public static DataTable GetColumnsNamesForView()
        {
            return clsTestAppointmentsData.GetColumnsNamesForView();
        }
    }
}
