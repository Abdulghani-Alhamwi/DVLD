using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClass
    {
        public byte ID { get; set; }
        public string ClassName { get; set; }
        public clsLicenseClass(byte ID,string ClassName)
        {
            this.ID = ID;
            this.ClassName = ClassName;
        }

        public static DataTable GetLicenseClassesNames()
        {
            return clsLicenseClassesData.GetLicenseClassesNames();
        }
        public static byte GetLicenseClassID(string LicenseClassName)
        {
            return clsLicenseClassesData.GetLicenseClassID(LicenseClassName);
        }
        public static string GetLicenseClassName(byte LicenseClassID)
        {
            return clsLicenseClassesData.GetLicenseClassName(LicenseClassID);
        }
        public static byte GetLicenseValidityLength(byte LicenseClassID)
        {
            return clsLicenseClassesData.GetLicenseValidityLength(LicenseClassID);
        }
        public static decimal GetLicenseClassFees(byte LicenseClassID)
        {
            return clsLicenseClassesData.GetLicenseClassFees(LicenseClassID);
        }
        public static byte GetMinimumAllowedAge(byte LicenseClassID)
        {
            return clsLicenseClassesData.GetMinimumAllowedAge(LicenseClassID);
        }
    }
}
