using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        enum _enMode : byte {AddNew = 0 , Update = 1};
        _enMode _CurrentMode;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            TestID = -1;
            this.TestAppointmentID = TestAppointmentID;
            this.TestResult = TestResult;
            this.CreatedByUserID = CreatedByUserID;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }
        private bool _AddNewTest()
        {
            TestID = clsTestsData.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);

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

        public static bool HasPassedTheTest(int LDLApplicationID, int TestTypeID)
        {
            return clsTestsData.HasPassedTheTest(LDLApplicationID, TestTypeID);
        }
        public static sbyte GetTotalPassedTestsCount(int LDLApplicationID)
        {
            return clsTestsData.GetTotalPassedTestsCount(LDLApplicationID);
        }

    }
}
