using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLicenseClass
    {
        public enum enMode : byte { AddNew = 0, Update = 1}
        private enMode _CurrentMode;

        public enum enLicenseClasses : byte
        { SmallMotorcycle = 1, HeavyMotorcycleClass = 2, OrdinaryDrivingClass = 3,CommercialClass = 4,
          AgriculturalClass = 5, SmallAndMediumBusClass = 6, TruckAndHeavyVehicleClass = 7 };

        public byte LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        private clsLicenseClassData.clsOldLicenseClassInfo _OldLicenseClassInfo;

        public clsLicenseClass(string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = 0;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            _OldLicenseClassInfo = new clsLicenseClassData.clsOldLicenseClassInfo(ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
        }

        private clsLicenseClass (byte LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge
            , byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;

            _OldLicenseClassInfo = new clsLicenseClassData.clsOldLicenseClassInfo(ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            _CurrentMode = enMode.Update;
        }

        public static enLicenseClasses GetLicenseClass(byte LicenseClassID)
        {
            return (enLicenseClasses)LicenseClassID;
        }

        public static byte GetLicenseClassID(enLicenseClasses LicenseClass)
        {
            return (byte)LicenseClass;
        }

        public static clsLicenseClass FindByLicenseClassID(byte LicenseClassID)
        {
            string ClassName = "", ClassDescription = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription
                , ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }

            else
                return null;
        }

        public static clsLicenseClass FindByLicenseClassName(string ClassName)
        {
            byte LicenseClassID = 0, MinimumAllowedAge = 0, DefaultValidityLength = 0;
            string ClassDescription = "";
            float ClassFees = 0;

            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription
                , ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }

            else
                return null;
        }

        public static DataTable GetLicenseClassesNames()
        {
            return clsLicenseClassData.GetLicenseClassesNames();
        }

        public static byte GetLicenseClassID(string LicenseClassName)
        {
            return clsLicenseClassData.GetLicenseClassID(LicenseClassName);
        }

        public static string GetLicenseClassName(enLicenseClasses LicenseClassID)
        {
            return clsLicenseClassData.GetLicenseClassName((byte) LicenseClassID);
        }

        public static byte GetMinimumAllowedAge(byte LicenseClassID)
        {
            return clsLicenseClassData.GetMinimumAllowedAge(LicenseClassID);
        }

        public bool HasOldDataChangedFully()
        {
            return (_OldLicenseClassInfo.ClassName != ClassName && _OldLicenseClassInfo.ClassDescription != ClassDescription &&
                    _OldLicenseClassInfo.MinimumAllowedAge != MinimumAllowedAge && _OldLicenseClassInfo.DefaultValidityLength != DefaultValidityLength
                 && _OldLicenseClassInfo.ClassFees != ClassFees);
        }

        private bool _AddNewLicenseClass()
        {
            this.LicenseClassID = clsLicenseClassData.AddNewLicenseClass(ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);

            return (this.LicenseClassID != 0);
        }

        private bool _UpdateLicenseClassInfo()
        {
            return clsLicenseClassData.UpdateLicenseClassInfo(ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees, _OldLicenseClassInfo, HasOldDataChangedFully());
        }

        public bool Save()
        {
            switch(_CurrentMode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        _CurrentMode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLicenseClassInfo();
            }

            return false;
        }
    }
}
