using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsLocalDrivingLicenseApplications
    {
        private enum _enMode { AddNew = 0 , Update = 1};
        private _enMode _CurrentMode;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplications()
        {
            LocalDrivingLicenseApplicationID = -1;
            ApplicationID = -1;
            LicenseClassID = -1;
            _CurrentMode = _enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplications(int LocalDrivingLicenseApplicationID,int ApplicationID,int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;
            _CurrentMode = _enMode.Update;
        }

        public static DataTable GetLocalDrivingLicenseApplications()
        {
            return null;
        }

        public static clsLocalDrivingLicenseApplications Find(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationsData.Find(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                return new clsLocalDrivingLicenseApplications(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
            }
            else
                return null;
        }

        public bool _AddLocalDrivingLicenseApplication()
        {
           LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddLocalDrivingLicenseApplication(ApplicationID, LicenseClassID);

            return (LocalDrivingLicenseApplicationID != -1);
        }

        private bool UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID,ApplicationID, LicenseClassID);
        }

        public bool Save()
        {
            switch (_CurrentMode)
            {
                case _enMode.AddNew:
                    if (_AddLocalDrivingLicenseApplication())
                    {
                        _CurrentMode = _enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case _enMode.Update:
                    return UpdateLocalDrivingLicenseApplication();
            }
            return false;
        }
    }
}
