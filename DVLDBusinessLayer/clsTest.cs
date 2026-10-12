using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        private enum _enMode : byte {AddNew = 0 , Update = 1};
        private _enMode _CurrentMode;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointment TestAppointmentInfo;
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            TestID = -1;
            this.TestAppointmentID = TestAppointmentID;
            TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.CreatedByUserID = CreatedByUserID;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }

        private clsTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.CreatedByUserID = CreatedByUserID;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }

        private bool _AddNewTest()
        {
            TestID = clsTestData.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);

            return (TestID != -1);
        }

        public bool Save()
        {
            if (_CurrentMode == _enMode.Update)
                return false;
            else
            {
                if (_AddNewTest())
                {
                    _CurrentMode = _enMode.Update;
                    return true;
                }
                else
                    return false;
            }
        }

        public static bool HasPassedTheTest(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            return clsTestData.HasPassedTheTest(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static byte GetTotalPassedTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetTotalPassedTests(LocalDrivingLicenseApplicationID);
        }

        public static short GetTotalTrialsPerTestType(int LocalDrivingLicenseApplicaitonID, clsTestType.enTestType TestTypeID)
        {
            return clsTestData.GetTotalTrialsPerTestType(LocalDrivingLicenseApplicaitonID,(byte) TestTypeID);
        }

        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestData.Find(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }

            else
                return null;
        }

        public static clsTest GetLastTestByTestType(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestData.GetLastTestByTestType(LocalDrivingLicenseApplicationID, TestTypeID,
                ref TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }

            else
                return null;
        }

        public static clsTest GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, byte LicenseClassID, byte TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false;
            string Notes = "";
            int CreatedByUserID = -1;

            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, TestTypeID, ref TestID,
                ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }

            else
                return null;
        }
    }
}
