using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTest
    {
        enum _enMode {AddNew = 1,Update=2};
        _enMode _CurrentMode;
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            CreatedByUserID = -1;
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
