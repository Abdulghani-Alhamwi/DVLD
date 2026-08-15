using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClasses
    {
        public int ID { get; set; }
        public string ClassName { get; set; }


        public static DataTable GetLicenseClassesNames()
        {
            return clsLicenseClassesData.GetLicenseClassesNames();
        }

        public static int GetLicenseClassID(string LicenseClassName)
        {
            return clsLicenseClassesData.GetLicenseClassID(LicenseClassName);
        }

    }
}
