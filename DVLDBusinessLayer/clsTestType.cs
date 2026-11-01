using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsTestType
    {
        public enum enTestType : byte { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        public enTestType TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }

        public clsTestTypeData.clsOldTestTypeData _OldTestTypeData;

        public clsTestType(enTestType TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            _OldTestTypeData = new clsTestTypeData.clsOldTestTypeData(TestTypeTitle,TestTypeDescription,TestTypeFees);
        }

        public static DataTable GetTestTypes()
        {
            return clsTestTypeData.GetTestTypes();
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
            if(clsTestTypeData.UpdateTestType((byte)TestTypeID, TestTypeTitle, TestTypeDescription,
                TestTypeFees, _OldTestTypeData, _HasOldDataChangedFully()))
            {
                _OldTestTypeData = new clsTestTypeData.clsOldTestTypeData(TestTypeTitle, TestTypeDescription, TestTypeFees);
                return true;
            }

            return false;
        }

        public static float GetTestTypeFees(byte TestTypeID)
        {
            return clsTestTypeData.GetTestTypeFees(TestTypeID);
        }

        public static byte GetTestTypeID(enTestType TestType)
        {
            return (byte)TestType;
        }

        public bool Save()
        {
            return _UpdateTestType();
        }
    }
}
