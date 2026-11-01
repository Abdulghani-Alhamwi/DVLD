using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsSettings
    {
        public static byte GetInternationalLicenseValidityLength()
        {
            return clsSettingsData.GetInternationalLicenseValidityLength();
        }
    }
}
