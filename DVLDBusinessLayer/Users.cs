using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsUser
    {
        public static DataTable GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUsersData.DeleteUser(UserID);
        }

        public static bool IsUserExists(int PersonID)
        {
            return clsUsersData.IsUserExists(PersonID);
        }

        public static bool IsUserExists(string PersonNationalNo)
        {
            return clsUsersData.IsUserExists(PersonNationalNo);
        }
    }
}
