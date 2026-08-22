using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClasses
    {
        public byte ID { get; set; }
        public string ClassName { get; set; }
        public static DataTable GetLicenseClassesNames()
        {
            return clsLicenseClassesData.GetLicenseClassesNames();
        }

        public static byte GetLicenseClassID(string LicenseClassName)
        {
            return clsLicenseClassesData.GetLicenseClassID(LicenseClassName);
        }
    }
}
