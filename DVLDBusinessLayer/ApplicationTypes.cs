using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplicationTypes
    {
        public enum enApplicationType : byte { NewLDL = 1,RenewDL = 2,ReplacementForLostDL = 3 ,ReplacementForDamagedDL = 4 ,ReleaseDetainedDL = 5,NewInternationlLicense = 6,ReTakeTest = 7}
        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypesData.GetApplicationTypes();
        }
        public static bool UpdateApplicationType(byte ApplicationTypeID,string ApplicationTypeTitle,double ApplicationTypeFees)
        {
            return clsApplicationTypesData.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees);
        }

        public static decimal GetApplicationTypeFees(byte ApplicationTypeID)
        {
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
                case enApplicationType.NewLDL:
                    return 1;

                case enApplicationType.RenewDL:
                    return 2;

                case enApplicationType.ReplacementForLostDL:
                    return 3;

                case enApplicationType.ReplacementForDamagedDL:
                    return 4;

                case enApplicationType.ReleaseDetainedDL:
                    return 5;

                case enApplicationType.NewInternationlLicense:
                    return 6;

                case enApplicationType.ReTakeTest:
                    return 7;
            }
            return 0;
        }
    }
}
