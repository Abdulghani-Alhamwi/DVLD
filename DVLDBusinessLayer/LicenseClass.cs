using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClass
    {
        public enum enLicenseClasses : byte { SmallMotorcycle = 1, HeavyMotorcycleClass = 2, OrdinaryDrivingClass = 3,
            CommercialClass = 4, AgriculturalClass = 5, SmallAndMediumBusClass = 6, TruckAndHeavyVehicleClass = 7 };
        public byte ID { get; set; }
        public string ClassName { get; set; }
        public clsLicenseClass(byte ID,string ClassName)
        {
            this.ID = ID;
            this.ClassName = ClassName;
        }

        public static enLicenseClasses GetLicenseClassEnumMember(int LicenseClassID)
        {
            switch(LicenseClassID)
            {
                case 1:
                    return enLicenseClasses.SmallMotorcycle;

                case 2:
                    return enLicenseClasses.HeavyMotorcycleClass;

                case 3:
                    return enLicenseClasses.OrdinaryDrivingClass;

                case 4:
                    return enLicenseClasses.CommercialClass;

                case 5:
                    return enLicenseClasses.AgriculturalClass;

                case 6:
                    return enLicenseClasses.SmallAndMediumBusClass;

                case 7:
                    return enLicenseClasses.TruckAndHeavyVehicleClass;

                default:
                    return enLicenseClasses.SmallMotorcycle;
            }
        }
        public static int GetLicenseClassID(enLicenseClasses LicenseClass)
        {
            switch (LicenseClass)
            {
                case enLicenseClasses.SmallMotorcycle:
                    return 1;

                case enLicenseClasses.HeavyMotorcycleClass:
                    return 2;

                case enLicenseClasses.OrdinaryDrivingClass:
                    return 3;

                case enLicenseClasses.CommercialClass:
                    return 4;

                case enLicenseClasses.AgriculturalClass:
                    return 5;

                case enLicenseClasses.SmallAndMediumBusClass:
                    return 6;

                case enLicenseClasses.TruckAndHeavyVehicleClass:
                    return 7;

                default:
                    return -1;
            }
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
