using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsApplicationType
    {
        public byte ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationTypeFees { get; set; }

        public clsApplicationTypeData.clsOldApplicationTypeData _OldApplicationTypeData;

        public clsApplicationType(byte ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationTypeFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationTypeFees = ApplicationTypeFees;

            _OldApplicationTypeData = new clsApplicationTypeData.clsOldApplicationTypeData(
                                      ApplicationTypeTitle, ApplicationTypeFees);
        }

        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypeData.GetApplicationTypes();
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
            if (clsApplicationTypeData.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees,
                _OldApplicationTypeData, _HasOldDataChangedFully()))
            {
                _OldApplicationTypeData = new clsApplicationTypeData.clsOldApplicationTypeData(
                                    ApplicationTypeTitle, ApplicationTypeFees);

                return true;
            }

            return false;
        }

        public static decimal GetApplicationTypeFees(clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationTypeData.GetApplicationTypeFees((byte) ApplicationTypeID);
        }

        public static string GetApplicationTypeTitle(byte ApplicationTypeID)
        {
            return clsApplicationTypeData.GetApplicationTypeTitle(ApplicationTypeID);
        }

        public static byte GetApplicationTypeID(clsApplication.enApplicationType ApplicationType)
        {
            return (byte)ApplicationType;
        }

        public bool Save()
        {
            return _UpdateApplicationType();
        }
    }
}
