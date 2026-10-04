using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplicationType
    {
        public enum enApplicationType : byte { NewLocalDrivingLicense = 1,RenewLicense = 2,ReplacementForLostLicense = 3 ,ReplacementForDamagedLicense = 4 ,ReleaseDetainedLicense = 5,NewInternationlLicense = 6,ReTakeTest = 7}
        public byte ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationTypeFees { get; set; }

        public clsApplicationTypesData.clsOldApplicationTypeData _OldApplicationTypeData;

        public clsApplicationType(byte ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationTypeFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationTypeFees = ApplicationTypeFees;

            _OldApplicationTypeData = new clsApplicationTypesData.clsOldApplicationTypeData(
                                      ApplicationTypeTitle, ApplicationTypeFees);
        }

        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypesData.GetApplicationTypes();
        }

        public bool AreAllFieldsOldValuesNotChanged()
        {
            return (ApplicationTypeTitle == _OldApplicationTypeData.ApplicationTypeTitle && ApplicationTypeFees == _OldApplicationTypeData.ApplicationTypeFees);
        }

        private bool _HasOldDataChangedFully()
        {
            return (ApplicationTypeTitle != _OldApplicationTypeData.ApplicationTypeTitle && ApplicationTypeFees != _OldApplicationTypeData.ApplicationTypeFees);                   
        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesData.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees,
                _OldApplicationTypeData, _HasOldDataChangedFully());
        }

        public static decimal GetApplicationTypeFees(enApplicationType ApplicationType)
        {
            byte ApplicationTypeID = GetApplicationTypeID(ApplicationType);
            return clsApplicationTypesData.GetApplicationTypeFees(ApplicationTypeID);
        }

        public static string GetApplicationTypeTitle(byte ApplicationTypeID)
        {
            return clsApplicationTypesData.GetApplicationTypeTitle(ApplicationTypeID);
        }

        public static byte GetApplicationTypeID(enApplicationType ApplicationType)
        {
            switch(ApplicationType)
            {
                case enApplicationType.NewLocalDrivingLicense:
                    return 1;

                case enApplicationType.RenewLicense:
                    return 2;

                case enApplicationType.ReplacementForLostLicense:
                    return 3;

                case enApplicationType.ReplacementForDamagedLicense:
                    return 4;

                case enApplicationType.ReleaseDetainedLicense:
                    return 5;

                case enApplicationType.NewInternationlLicense:
                    return 6;

                case enApplicationType.ReTakeTest:
                    return 7;
            }
            return 0;
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }
    }
}
