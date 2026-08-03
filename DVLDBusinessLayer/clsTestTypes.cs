using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTestTypes
    {

        public static DataTable GetTestTypes()
        {
            return clsTestTypesData.GetTestTypes();
        }

        public static bool UpdateTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,double TestTypeFees)
        {
            return clsTestTypesData.UpdateTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
        }

    }
}
