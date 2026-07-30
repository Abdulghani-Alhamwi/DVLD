using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClasses
    {
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
