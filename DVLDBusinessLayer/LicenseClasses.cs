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
    }
}
