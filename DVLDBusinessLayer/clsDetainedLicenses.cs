using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsDetainedLicenses
    {
        public static bool IsDetainedLicense(int LicenseID)
        {
            return clsDetainedLicensesData.IsDetainedLicense(LicenseID);
        }
    }
}
