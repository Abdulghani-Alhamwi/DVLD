using System;
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

        public static bool UpdateApplicationType(int ApplicationTypeID,string Title,double Fees)
        {
            return clsApplicationTypesData.UpdateApplicationType(ApplicationTypeID, Title, Fees);
        }
    }
}
