using System;
using System.Collections.Generic;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsUser
    {
        public enum enMode : byte { AddNew = 0, Update = 1 }
        private enMode _CurrentMode;

        public enum enUserPermissions : sbyte
        {
            All = -1, UsersManagement = 1, PeopleManagement = 2,
            ApplicationsManagement = 4, DriversView = 8
        }

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonalInfo { get; set; }
        public string Username { get; set; }
        public string UsernameIV { get; set; }
        public string UsernameHash { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
        public sbyte Permissions { get; set; }

        private clsUserData.clsOldUserData _OldUserData;

        public clsUser(int PersonID, string Username, string UsernameIV, string UsernameHash, string Password, string Salt, bool IsActive, sbyte Permissions)
        {
            this.UserID = -1;
            this.PersonID = PersonID;
            PersonalInfo = clsPerson.Find(PersonID);
            this.Username = Username;
            this.UsernameIV = UsernameIV;
            this.UsernameHash = UsernameHash;
            this.Password = Password;
            this.Salt = Salt;
            this.IsActive = IsActive;
            this.Permissions = Permissions;

            _OldUserData = new clsUserData.clsOldUserData(PersonID, Username, Password, IsActive, Permissions);
        }

        private clsUser(int UserID, int PersonID, string Username, string UsernameIV, string UsernameHash, string Password, string Salt, bool IsActive, sbyte Permissions)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            PersonalInfo = clsPerson.Find(PersonID);
            this.Username = Username;
            this.UsernameIV = UsernameIV;
            this.UsernameHash = UsernameHash;
            this.Password = Password;
            this.Salt = Salt;
            this.IsActive = IsActive;
            this.Permissions = Permissions;
            _CurrentMode = enMode.Update;

            _OldUserData = new clsUserData.clsOldUserData(PersonID, Username, Password, IsActive, Permissions);
        }

        public static DataTable GetUsersInfo(byte WantedNumOfRecords)
        {
            return clsUserData.GetUsersInfo(WantedNumOfRecords);
        }

        public static DataTable GetUsersInfo(byte WantedNumOfRecords, int LastBroughtUserID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsUserData.GetUsersInfo(WantedNumOfRecords, LastBroughtUserID, LastColumnNameDataOrderedBy
                , NumberOfRowsToOffset, SortDirection);
        }

        private bool _AddNewUser()
        {
            UserID = clsUserData.AddNewUser(PersonID, Username, UsernameIV, UsernameHash, Password, Salt, IsActive, Permissions);

            return (UserID != -1);
        }

        private static sbyte _GetPermissionNumericValue(enUserPermissions Permission)
        {
            switch (Permission)
            {
                case enUserPermissions.UsersManagement:
                    return 1;

                case enUserPermissions.PeopleManagement:
                    return 2;

                case enUserPermissions.ApplicationsManagement:
                    return 4;

                case enUserPermissions.DriversView:
                    return 8;
            }
            return 0;
        }

        public static bool HasUserPermission(enUserPermissions RequiredPermission, sbyte UserPermissions)
        {
            if (UserPermissions == -1)
                return true;

            sbyte Permission = _GetPermissionNumericValue(RequiredPermission);

            return (Permission & UserPermissions) == Permission;
        }

        public bool HasUserPermission(enUserPermissions RequiredPermission)
        {
            return HasUserPermission(RequiredPermission, Permissions);
        }

        public static bool IsAdminUser(sbyte UserPermissions)
        {
            return (UserPermissions == -1);
        }

        public bool IsAdminUser()
        {
            return IsAdminUser(Permissions);
        }

        public bool AreAllFieldsOldValuesNotChanged()
        {
            return (_OldUserData.PersonID == PersonID && !HasUsernameChanged() &&
                    _OldUserData.Password == Password &&_OldUserData.IsActiveCase == IsActive
                 && _OldUserData.Permissions == Permissions);
        }

        public bool HasUsernameChanged()
        {
            return (_OldUserData.Username != Username);
        }

        private bool _HasOldDataChangedFully()
        {
            return (_OldUserData.PersonID != PersonID && HasUsernameChanged() &&
                    _OldUserData.Password != Password && _OldUserData.IsActiveCase != IsActive
                 && _OldUserData.Permissions != Permissions);
        }

        private bool _UpdateUser()
        {
            if(clsUserData.UpdateUser(UserID, PersonID, Username, UsernameIV, UsernameHash, Password, Salt, IsActive, Permissions
                , _OldUserData, _HasOldDataChangedFully()))
            {
                _OldUserData = new clsUserData.clsOldUserData(PersonID, Username, Password, IsActive, Permissions);
                return true;
            }

            return false;
        }

        public bool Save()
        {
            switch (_CurrentMode)
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
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExists(int PersonID)
        {
            return clsUserData.IsUserExists(PersonID);
        }

        public static bool IsUserAlreadyExists(string UsernameHash)
        {
            return clsUserData.IsUserAlreadyExists(UsernameHash);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string Username = "", UsernameIV = "", UsernameHash = "", Password = "", Salt = "";
            bool IsActive = false;
            sbyte Permissions = 0;

            if (clsUserData.Find(UserID, ref PersonID, ref Username, ref UsernameIV, ref UsernameHash, ref Password, ref Salt, ref IsActive, ref Permissions))
            {
                return new clsUser(UserID, PersonID, Username, UsernameIV, UsernameHash, Password, Salt, IsActive, Permissions);
            }
            else
                return null;
        }

        public static void GetUserPasswordWithSalt(int UserID, ref string Password, ref byte[] Salt)
        {
            clsUserData.GetUserPasswordWithSalt(UserID, ref Password, ref Salt);
        }

        public static bool GetLoginInfo(string UsernameHash, ref int UserID, ref string Username, ref string UsernameIV, ref string Password, ref byte[] Salt, ref bool IsActive, ref sbyte Permissions)
        {
            return clsUserData.GetLoginInfo(UsernameHash, ref UserID, ref Username, ref UsernameIV, ref Password, ref Salt, ref IsActive, ref Permissions);
        }

        public static bool GetLoginInfo(int UserID, ref string Username, ref string UsernameIV, ref string Password)
        {
            return clsUserData.GetLoginInfo(UserID, ref Username, ref UsernameIV, ref Password);
        }

        public static bool ChangePassword(int UserID, string Password, string Salt)
        {
            return clsUserData.ChangePassword(UserID, Password, Salt);
        }

        public static bool GetUserName(int UserID, ref string Username, ref string UsernameIV)
        {
            return clsUserData.GetUserName(UserID, ref Username, ref UsernameIV);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string LastColumnNameDataOrderedBy, string SortDirection, char? WildChar = null)
        {
            return clsUserData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection, -1, -1, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string LastColumnNameDataOrderedBy, string SortDirection
            , int LastBroughtUserID, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            return clsUserData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection, LastBroughtUserID, NumberOfRowsToOffset, WildChar);
        }

        public static int GetTotalUsersCount()
        {
            return clsUserData.GetTotalUsersCount();
        }

        public static DataTable GetColumnsNamesForView()
        {
            return clsUserData.GetColumnsNamesForView();
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection
            , string ColumnNameToFilterBy = null, string valueToFilterBy = null, char? WildChar = null)
        {
            return clsUserData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,
                ColumnNameToFilterBy, valueToFilterBy, WildChar);
        }

        public static Dictionary<int , string> GetUsersIDsWithIVs()
        {
            return clsUserData.GetUsersIDsWithIVs();
        }
    }
}
