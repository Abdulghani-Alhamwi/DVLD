using System;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsDriver
    {
        private enum _enMode : byte { AddNew = 1 , Update = 2}

        private _enMode _CurrentMode;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set;}
        public DateTime CreationDate { get; set; }

        public clsDriver(int PersonID,int CreatedByUserID,DateTime CreationDate)
        {
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreationDate = CreationDate;
        }
        private bool _AddNewDriver()
        {
            DriverID = clsDriversData.AddNewDriver(PersonID, CreatedByUserID, CreationDate);

            return (DriverID != -1);
        }

        public bool Save()
        {
            if (_CurrentMode == _enMode.Update)
                return false;

            if (_AddNewDriver())
            {
                _CurrentMode = _enMode.Update;
                return true;
            }
            else
                return false;
        }
        public static bool IsPersonAlreadyADriver(int PersonID)
        {
            return clsDriversData.IsPersonAlreadyADriver(PersonID);
        }

        public static int GetDriverID(int PersonID)
        {
            return clsDriversData.GetDriverID(PersonID);
        }
        public static int GetDriverPersonID(int DriverID)
        {
            return clsDriversData.GetDriverPersonID(DriverID);
        }
    }
}
