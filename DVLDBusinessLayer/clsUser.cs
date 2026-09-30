using System;
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
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
        public sbyte UserPermissions { get; set; }

        private clsUsersData.clsOldUserData _OldUserData;

        public clsUser(int PersonID, string UserName, string Password, string Salt, bool IsActive, sbyte Permissions)
        {
            this.UserID = -1;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.Salt = Salt;
            this.IsActive = IsActive;
            this.UserPermissions = Permissions;
        }

        private clsUser(int UserID, int PersonID, string UserName, string Password, string Salt, bool IsActive, sbyte Permissions)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.Salt = Salt;
            this.IsActive = IsActive;
            this.UserPermissions = Permissions;
            _CurrentMode = enMode.Update;

            _OldUserData = new clsUsersData.clsOldUserData(PersonID, UserName, Password
                           , Salt, IsActive, Permissions);
        }

        public static DataTable GetUsersInfo(byte WantedNumOfRecords)
        {
            return clsUsersData.GetUsersInfo(WantedNumOfRecords);
        }

        public static DataTable GetUsersInfo(byte WantedNumOfRecords, int LastBroughtUserID
            , string LastColumnNameDataOrderedBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            return clsUsersData.GetUsersInfo(WantedNumOfRecords, LastBroughtUserID, LastColumnNameDataOrderedBy
                , NumberOfRowsToOffset, SortDirection);
        }

        private bool _AddNewUser()
        {
            UserID = clsUsersData.AddNewUser(PersonID, UserName, Password, Salt, IsActive, UserPermissions);

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
            return HasUserPermission(RequiredPermission, UserPermissions);
        }

        public static bool IsAdminUser(sbyte UserPermissions)
        {
            return (UserPermissions == -1);
        }

        public bool IsAdminUser()
        {
            return IsAdminUser(UserPermissions);
        }

        public bool AreAllFieldsOldValuesNotChanged()
        {
            return (_OldUserData.PersonID == PersonID && _OldUserData.UserName == UserName &&
                    _OldUserData.Password == Password && _OldUserData.OldSalt == Salt &&
                    _OldUserData.IsActiveCase == IsActive && _OldUserData.Permissions == UserPermissions);
        }

        private bool _HasOldDataChangedFully()
        {
            return (_OldUserData.PersonID != PersonID && _OldUserData.UserName != UserName &&
                    _OldUserData.Password != Password && _OldUserData.OldSalt != Salt &&
                    _OldUserData.IsActiveCase != IsActive && _OldUserData.Permissions != UserPermissions);
        }

        private bool _UpdateUser()
        {
            return clsUsersData.UpdateUser(UserID, PersonID, UserName, Password, Salt, IsActive, UserPermissions
                , _OldUserData, _HasOldDataChangedFully());
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
            string UserName = "", Password = "", Salt = "";
            bool IsActive = false;
            sbyte Permissions = 0;

            if (clsUsersData.Find(UserID, ref PersonID, ref UserName, ref Password, ref Salt, ref IsActive, ref Permissions))
            {
                return new clsUser(UserID, PersonID, UserName, Password, Salt, IsActive, Permissions);
            }
            else
                return null;
        }

        public static void GetUserPasswordWithSalt(int UserID, ref string Password, ref byte[] Salt)
        {
            clsUsersData.GetUserPasswordWithSalt(UserID, ref Password, ref Salt);
        }

        public static bool GetLoginInfo(string UserName, ref int UserID, ref string Password, ref byte[] Salt, ref bool IsActive, ref sbyte Permissions)
        {
            return clsUsersData.GetLoginInfo(UserName, ref UserID, ref Password, ref Salt, ref IsActive, ref Permissions);
        }

        public static bool GetLoginInfo(int UserID, ref string UserName, ref string Password)
        {
            return clsUsersData.GetLoginInfo(UserID, ref UserName, ref Password);
        }

        public static bool ChangePassword(int UserID, string Password, string Salt)
        {
            return clsUsersData.ChangePassword(UserID, Password, Salt);
        }

        public static string GetUserName(int UserID)
        {
            return clsUsersData.GetUserName(UserID);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string LastColumnNameDataOrderedBy, string SortDirection, char? WildChar = null)
        {
            return clsUsersData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection, -1, -1, WildChar);
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, string LastColumnNameDataOrderedBy, string SortDirection
            , int LastBroughtUserID, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            return clsUsersData.GetFilteredData(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, LastColumnNameDataOrderedBy, SortDirection, LastBroughtUserID, NumberOfRowsToOffset, WildChar);
        }

        public static int GetTotalUsersCount()
        {
            return clsUsersData.GetTotalUsersCount();
        }

        public static DataTable GetColumnsNamesForView()
        {
            return clsUsersData.GetColumnsNamesForView();
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection
            , string ColumnNameToFilterBy = null, string valueToFilterBy = null, char? WildChar = null)
        {
            return clsUsersData.GetSortedInfo(WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,
                ColumnNameToFilterBy, valueToFilterBy, WildChar);
        }
    }
}
