using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTestType
    {
        public enum enTestType : byte { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestTypesData.clsOldTestTypeData _OldTestTypeData;

        public clsTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            _OldTestTypeData = new clsTestTypesData.clsOldTestTypeData(TestTypeTitle,TestTypeDescription,TestTypeFees);
        }

        public static DataTable GetTestTypes()
        {
            return clsTestTypesData.GetTestTypes();
        }

        public bool AreAllFieldsOldValuesNotChanged()
        {
            return (TestTypeTitle == _OldTestTypeData.TestTypeTitle && TestTypeDescription == _OldTestTypeData.TestTypeDescription
                   && TestTypeFees == _OldTestTypeData.TestTypeFees);
        }

        private bool _HasOldDataChangedFully()
        {
            return (TestTypeTitle != _OldTestTypeData.TestTypeTitle && TestTypeDescription != _OldTestTypeData.TestTypeDescription
                   && TestTypeFees != _OldTestTypeData.TestTypeFees);
        }

        private bool _UpdateTestType()
        {
            return clsTestTypesData.UpdateTestType(TestTypeID, TestTypeTitle, TestTypeDescription,
                TestTypeFees, _OldTestTypeData, _HasOldDataChangedFully());
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

        public bool Save()
        {
            return _UpdateTestType();
        }
    }
}
