using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsUser
    {
        enum _enMode {AddNew = 0 , Update = 1}
        _enMode _CurrentMode;
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
            _CurrentMode = _enMode.AddNew;
        }

        private clsUser(int UserID , int PersonID,string UserName ,string Password,string Salt,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.Salt = Salt;
            this.IsActive = IsActive;
            _CurrentMode = _enMode.Update;
        }
        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
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
                case _enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _CurrentMode = _enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case _enMode.Update:
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
            string UserName = "", Password = "" , Salt = "";
            bool IsActive = false;

            if (clsUsersData.Find(UserID, ref PersonID, ref UserName, ref Password,ref Salt, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Salt, IsActive);
            }
            else
                return null;
        }
    }
}
