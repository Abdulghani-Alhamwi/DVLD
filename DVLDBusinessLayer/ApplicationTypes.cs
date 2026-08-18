using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Remoting.Messaging;
using DVLDDataAccessLayer;
using Microsoft.SqlServer.Server;

namespace DVLDBusinessLayer
{
    public class clsApplicationTypes
    {
        public static DataTable GetApplicationTypes()
        {
            return clsApplicationTypesData.GetApplicationTypes();
        }

        public static bool UpdateApplicationType(int ApplicationTypeID,string ApplicationTypeTitle,double ApplicationTypeFees)
        {
            return clsApplicationTypesData.UpdateApplicationType(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFees);
        }

        public static double GetApplicationTypeFees(int ApplicationTypeID)
        {
            return clsApplicationTypesData.GetApplicationTypeFees(ApplicationTypeID);
        }
        public static string GetApplicationTypeTitle(int ApplicationTypeID)
        {
            return clsApplicationTypesData.GetApplicationTypeTitle(ApplicationTypeID);
        }
    }
}
