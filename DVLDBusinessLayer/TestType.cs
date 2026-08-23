using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTestType
    {
        public enum enTestType : byte { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public static DataTable GetTestTypes()
        {
            return clsTestTypesData.GetTestTypes();
        }
        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            return clsTestTypesData.UpdateTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }
        public static decimal GetTestTypeFees(byte TestTypeID)
        {
            return clsTestTypesData.GetTestTypeFees(TestTypeID);
        }
        public static byte GetTestTypeID(enTestType TestType)
        {
            switch (TestType)
            {
                case enTestType.VisionTest:
                    return 1;

                case enTestType.WrittenTest:
                    return 2;

                case enTestType.StreetTest:
                    return 3;
            }
            return 0;
        }
    }
}
