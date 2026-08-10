using System;
using System.Data;
using System.Net;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsUser
    {
        public enum enMode {AddNew = 0 , Update = 1}
        enMode _CurrentMode;
        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            Salt = "";
            IsActive = false;
            _CurrentMode = enMode.AddNew;
        }

        private clsUser(int UserID , int PersonID,string UserName ,string Password,string Salt , bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.Salt = Salt;
            this.IsActive = IsActive;
            _CurrentMode = enMode.Update;
        }
        public static DataTable GetAllUsersInfo(byte WantedNumberOfRecords, int LastLowestBroughtUserID = -1)
        {
            return clsUsersData.GetAllUsersInfo(WantedNumberOfRecords,LastLowestBroughtUserID);
        }

        private bool _AddNewUser()
        {
            UserID = clsUsersData.AddNewUser(PersonID, UserName, Password, Salt, IsActive);

            return (UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(UserID, PersonID, UserName, Password, Salt, IsActive);
        }

        public bool Save()
        {
            switch(_CurrentMode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _CurrentMode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public static bool IsUserExists(int PersonID)
        {
            return clsUsersData.IsUserExists(PersonID);
        }

        public static bool IsUserAlreadyExists(string UserName)
        {
            return clsUsersData.IsUserAlreadyExists(UserName);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "" , Password = "" , Salt = "";
            bool IsActive = false;

            if (clsUsersData.Find(UserID, ref PersonID, ref UserName,ref Password , ref Salt , ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Salt, IsActive);
            }
            else
                return null;
        }

        public static void GetUserPasswordWithSalt(int UserID,ref string Password,ref byte[] Salt)
        {
            clsUsersData.GetUserPasswordWithSalt(UserID, ref Password, ref Salt);
        }
        public static bool GetLoginInfo(string UserName,ref int UserID, ref string Password, ref bool IsActive, ref byte[] Salt)
        {
            return clsUsersData.GetLoginInfo(UserName,ref UserID, ref Password,ref IsActive, ref Salt);
        }

        public static bool GetLoginInfo(int UserID,ref string UserName , ref string Password)
        {
            return clsUsersData.GetLoginInfo(UserID, ref UserName, ref Password);
        }

        public static bool ChangePassword(int UserID,string Password,string Salt)
        {
            return clsUsersData.ChangePassword(UserID, Password, Salt);
        }

        public static string GetUserName(int UserID)
        {
            return clsUsersData.GetUserName(UserID);
        }

        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null, int LastLowestbroughtUserID = -1)
        {
            return clsUsersData.GetFilteredData(WantedNumberOfRecords, ColumnNameToFilter, ValueToFilterBy, WildChar, LastLowestbroughtUserID);
        }

        public static uint GetTotalUsersCount()
        {
            return clsUsersData.GetTotalUsersCount();
        }

    }
}
