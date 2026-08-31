using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsDetainedLicenses
    {
        public static bool IsDetainedLicense(int LocalLicenseID)
        {
            return clsDetainedLicensesData.IsDetainedLicense(LocalLicenseID);
        }
    }
}
