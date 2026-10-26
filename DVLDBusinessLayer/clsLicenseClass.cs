using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClass
    {
        public enum enLicenseClasses : byte
        { SmallMotorcycle = 1, HeavyMotorcycleClass = 2, OrdinaryDrivingClass = 3,CommercialClass = 4,
          AgriculturalClass = 5, SmallAndMediumBusClass = 6, TruckAndHeavyVehicleClass = 7 };
        public byte ID { get; set; }
        public string ClassName { get; set; }

        public clsLicenseClass(byte ID,string ClassName)
        {
            this.ID = ID;
            this.ClassName = ClassName;
        }

        public static enLicenseClasses GetLicenseClassEnumMember(byte LicenseClassID)
        {
            return (enLicenseClasses)LicenseClassID;
        }

        public static byte GetLicenseClassID(enLicenseClasses LicenseClass)
        {
            return (byte)LicenseClass;
        }

        public static DataTable GetLicenseClassesNames()
        {
            return clsLicenseClasseData.GetLicenseClassesNames();
        }

        public static byte GetLicenseClassID(string LicenseClassName)
        {
            return clsLicenseClasseData.GetLicenseClassID(LicenseClassName);
        }

        public static string GetLicenseClassName(byte LicenseClassID)
        {
            return clsLicenseClasseData.GetLicenseClassName(LicenseClassID);
        }

        public static string GetLicenseClassName(enLicenseClasses LicenseClass)
        {
            byte LicenseClassID = GetLicenseClassID(LicenseClass);
            return clsLicenseClasseData.GetLicenseClassName(LicenseClassID);
        }

        public static byte GetLicenseValidityLength(byte LicenseClassID)
        {
            return clsLicenseClasseData.GetLicenseValidityLength(LicenseClassID);
        }

        public static decimal GetLicenseClassFees(byte LicenseClassID)
        {
            return clsLicenseClasseData.GetLicenseClassFees(LicenseClassID);
        }

        public static byte GetMinimumAllowedAge(byte LicenseClassID)
        {
            return clsLicenseClasseData.GetMinimumAllowedAge(LicenseClassID);
        }
    }
}
