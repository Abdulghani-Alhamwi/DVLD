using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsUsersData
    {
        private static readonly string _PrimaryKeyColumnName = "UserID";
        private static readonly string _PrimaryKeyViewedColumnName = "User ID";

        private static readonly string _FixedQueryPart = "* FROM Users_View";

        private static readonly string _UsernameLookupQueryPart = "Users_View.*  FROM Users inner join Users_View ON Users.UserID = Users_View.[User ID]";

        private static readonly string _QueryWithoutPagination = "SELECT TOP (@WantedNumOfRecords) " + _FixedQueryPart;

        private static readonly string _QueryForOffsetPagination = "SELECT " + _FixedQueryPart;

        private static readonly string _OffsetPaginationQueryPart = clsGeneralUtility.GetOffsetPaginationQueryPart();

        private enum _enUpdatableColumns : byte
        {
            PersonID, Username, Password, IsActive,Permissions
        }

        public class clsOldUserData
        {
            public int PersonID;
            public string Username;
            public string Password;
            public bool? IsActiveCase;
            public sbyte? Permissions;

            public clsOldUserData(int OldPersonID, string OldUsername, string OldPassword, bool OldIsActiveCase,sbyte? OldPermissions)
            {
                this.PersonID = OldPersonID;
                this.Username = OldUsername;
                this.Password = OldPassword;
                this.IsActiveCase = OldIsActiveCase;
                this.Permissions = OldPermissions;
            }
        }

        public static DataTable GetUsersInfo(byte WantedNumOfRecords, int LastBroughtUserID = -1, string ColumnNameToOrderBy = null
            , int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtUsers = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(LastBroughtUserID, ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
            }

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (NumberOfRowsToOffset != -1)
                command.Parameters.AddWithValue("@NumberOfRowsToOffset", NumberOfRowsToOffset);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtUsers = new DataTable();
                    dtUsers.Load(reader);
                }

                 reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtUsers;
        }

        private static string _GetQueryForCursorPagination(int LastBroughtUserID, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;
            if (LastBroughtUserID != -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyViewedColumnName, ColumnNameToOrderBy, SortDirection, false, LastBroughtUserID, true, true, true);
            }

            else
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            return query;
        }

        private static string _GetQueryForOffsetPagination(int NumberOfRowsToOffset, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;

            if (NumberOfRowsToOffset == -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _QueryForOffsetPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
                query += _OffsetPaginationQueryPart;
            }

            return query;
        }

        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _QueryWithoutPagination;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", 0);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable dtUsers = new DataTable();
                dtUsers.Load(reader);
                reader.Close();

                return dtUsers;
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return null;
        }

        public static int AddNewUser(int PersonID, string Username, string UsernameIV, string UsernameHash, string Password, string Salt, bool IsActive, sbyte Permissions)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users VALUES (@PersonID, @Username, @UsernameIV, @UsernameHash, @Password, @Salt, @IsActive, @Permissions);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@Username", Username);
            command.Parameters.AddWithValue("@UsernameIV", UsernameIV);
            command.Parameters.AddWithValue("@UsernameHash", UsernameHash);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Salt", Salt);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@Permissions", Convert.ToInt16(Permissions));

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    UserID = Convert.ToInt32(result);
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return UserID;
        }

        private static void _ResetChangedOldValues(int PersonID, string Username, string Password, bool IsActive, sbyte Permissions, clsOldUserData OldUserData)
        {
            if (PersonID != OldUserData.PersonID)
                OldUserData.PersonID = -1;

            if (Username != OldUserData.Username)
                OldUserData.Username = null;

            if (Password != OldUserData.Password)
                OldUserData.Password = null;

            if (IsActive != OldUserData.IsActiveCase)
                OldUserData.IsActiveCase = null;

            if (Permissions != OldUserData.Permissions)
                OldUserData.Permissions = null;
        }

        private static string _GetColumnValueSetPartForUpdate(_enUpdatableColumns UpdatableColumns)
        {
            switch(UpdatableColumns)
            {
                case _enUpdatableColumns.PersonID:
                    return " PersonID = @PersonID";

                case _enUpdatableColumns.Username:
                    return " Username = @Username, UsernameIV = @UsernameIV, UsernameHash = @UsernameHash";

                case _enUpdatableColumns.Password:
                    return " Password = @Password, Salt = @Salt";

                case _enUpdatableColumns.IsActive:
                    return " IsActive = @IsActive";

                case _enUpdatableColumns.Permissions:
                    return " Permissions = @Permissions";
            }

            return null;
        }

        private static string _GetUpdateQuery(int PersonID, string Username, string Password,
            bool IsActive,sbyte Permissions, clsOldUserData OldUserData, bool HasOldDataChangedFully)
        {
            string query = "UPDATE Users SET";

            if (!HasOldDataChangedFully)
            {
                if (PersonID != OldUserData.PersonID)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.PersonID);

                    if (Username != OldUserData.Username)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Username);

                    if (Password != OldUserData.Password)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Password);

                    if (IsActive != OldUserData.IsActiveCase)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.IsActive);

                    if (Permissions != OldUserData.Permissions)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Permissions);
                }

                else if (Username != OldUserData.Username)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Username);

                    if (Password != OldUserData.Password)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Password);

                    if (IsActive != OldUserData.IsActiveCase)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.IsActive);

                    if (Permissions != OldUserData.Permissions)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Permissions);
                }

                else if (Password != OldUserData.Password)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Password);

                    if (IsActive != OldUserData.IsActiveCase)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.IsActive);

                    if (Permissions != OldUserData.Permissions)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Permissions);
                }

                else if (IsActive != OldUserData.IsActiveCase)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.IsActive);

                    if (Permissions != OldUserData.Permissions)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Permissions);
                }

                else
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Permissions);
                }
            }

            else
                query += @" PersonID = @PersonID, Username = @Username, UsernameIV = @UsernameIV, UsernameHash = @UsernameHash,
                           Password = @Password, Salt = @Salt, IsActive = @IsActive, Permissions = @Permissions";

            query += $" WHERE {_PrimaryKeyColumnName} = @UserID";

            _ResetChangedOldValues(PersonID, Username, Password, IsActive, Permissions, OldUserData);

            return query;
        }

        public static bool UpdateUser(int UserID, int PersonID, string Username, string UsernameIV, string UsernameHash, string Password, string Salt, bool IsActive, sbyte Permissions, clsOldUserData OldUserData, bool HasOldDataChangedFully)
        {
            byte AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetUpdateQuery(PersonID, Username, Password, IsActive,Permissions, OldUserData, HasOldDataChangedFully);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID",UserID);

            if (OldUserData.PersonID == -1)
                command.Parameters.AddWithValue("@PersonID", PersonID);

            if (OldUserData.Username == null)
            {
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@UsernameIV", UsernameIV);
                command.Parameters.AddWithValue("@UsernameHash", UsernameHash);
            }

            if (OldUserData.Password == null)
            {
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@Salt", Salt);
            }
                
            if(OldUserData.IsActiveCase == null)
            command.Parameters.AddWithValue("@IsActive", IsActive);

            if (OldUserData.Permissions == null)
                command.Parameters.AddWithValue("@Permissions", Convert.ToInt16(Permissions));

            try
            {
                connection.Open();
                AffectedRows = Convert.ToByte(command.ExecuteNonQuery());
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return (AffectedRows > 0);
        }

        public static bool DeleteUser(int UserID)
        {
            byte AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $@"DELETE FROM Users WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID" , UserID);

            try
            {
                connection.Open();

                AffectedRows = Convert.ToByte(command.ExecuteNonQuery());
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return (AffectedRows > 0);
        }

        public static bool IsUserExists(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return true;
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return false;
        }

        public static bool IsUserAlreadyExists(string UsernameHash)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Users WHERE UsernameHash = @UsernameHash";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UsernameHash", UsernameHash);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return true;
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return false;
        }

        public static bool Find(int UserID, ref int PersonID, ref string Username,ref string UsernameIV,ref string UsernameHash,
           ref string Password, ref string Salt, ref bool IsActive,ref sbyte Permissions)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT * FROM Users WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    PersonID = (int) reader["PersonID"];
                    Username = (string)reader["Username"];
                    UsernameIV = (string)reader["UsernameIV"];
                    UsernameHash = (string)reader["UsernameHash"];
                    Password = (string)reader["Password"];
                    Salt = (string)reader["Salt"];
                    IsActive = (bool)reader["IsActive"];
                    Permissions = Convert.ToSByte(reader["Permissions"]);

                    IsFound = true;
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static void GetUserPasswordWithSalt(int UserID , ref string Password, ref byte[] Salt)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT Password, Salt FROM Users WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    Password = (string)reader["Password"];
                    Salt = Convert.FromBase64String((string)reader["Salt"]);
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }
        }

        public static bool GetLoginInfo(string UsernameHash, ref int UserID, ref string Username, ref string UsernameIV, ref string Password, ref byte[] Salt, ref bool IsActive, ref sbyte Permissions)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT UserID, Username, UsernameIV, Password, Salt, IsActive, Permissions FROM Users WHERE UsernameHash = @UsernameHash";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UsernameHash", UsernameHash);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader[_PrimaryKeyColumnName];
                    Username = (string)reader["Username"];
                    UsernameIV = (string)reader["UsernameIV"];
                    Password = (string)reader["Password"];
                    Salt = Convert.FromBase64String((string)reader["Salt"]);
                    IsActive = (bool)reader["IsActive"];
                    Permissions = Convert.ToSByte(reader["Permissions"]);

                    return true;
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return false;
        }

        public static bool GetLoginInfo(int UserID, ref string Username, ref string UsernameIV, ref string Password)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT Username, UsernameIV, Password FROM Users WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Username = (string)reader["Username"];
                    UsernameIV = (string)reader["UsernameIV"];
                    Password = (string)reader["Password"];

                    return true;
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return false;
        }

        public static bool ChangePassword(int UserID , string Password ,string Salt)
        {
            byte AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"UPDATE Users SET Password = @Password, Salt = @Salt WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Salt",Salt);

            try
            {
                connection.Open();
                AffectedRows = Convert.ToByte(command.ExecuteNonQuery());
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return (AffectedRows > 0);
        }

        public static bool GetUserName(int UserID, ref string Username, ref string UsernameIV)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $"SELECT UserName, UsernameIV FROM Users WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    Username = (string)reader["Username"];
                    UsernameIV = (string)reader["UsernameIV"];

                    return true;
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return false;
        }

        private static string _GetDataFilteringQuery(byte WantedNumOfRecords, string ColumnNameToFilterBy, ref string ValueToFilterBy,
            string ColumnNameToOrderBy, string SortDirection, int LastBroughtUserID = -1,int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Is Active" && ValueToFilterBy == "All"))
            {
                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _GetQueryForCursorPagination(LastBroughtUserID, ColumnNameToOrderBy, SortDirection);
                }

                else
                {
                    query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
                }
            }

            else
            {
                if (ColumnNameToFilterBy == "Is Active")
                {
                    if(ValueToFilterBy != "All")
                    ValueToFilterBy = clsGeneralUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }


                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {

                    if (ColumnNameToFilterBy == "Username")
                    {
                        ColumnNameToFilterBy = "UsernameHash";
                        query = "SELECT TOP (@WantedNumOfRecords) " + _UsernameLookupQueryPart;
                    }

                    else
                    query = _QueryWithoutPagination;

                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar, true);
                    query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyViewedColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtUserID, true, true, true);
                }

                else
                {
                    if (ColumnNameToFilterBy == "Username")
                    {
                        ColumnNameToFilterBy = "UsernameHash";
                        query = "SELECT " + _UsernameLookupQueryPart;
                    }

                    else
                        query = _QueryForOffsetPagination;

                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar, true);
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                    query += _OffsetPaginationQueryPart;
                }
            }

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilterBy, string ValueToFilterBy, string ColumnNameToOrderBy,string SortDirection,
            int LastBroughtUserID = -1,int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilterBy, ref ValueToFilterBy,
                            ColumnNameToOrderBy, SortDirection, LastBroughtUserID, NumberOfRowsToOffset, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (ValueToFilterBy != null)
                command.Parameters.AddWithValue("@Value", ValueToFilterBy);

            if (WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

            if (NumberOfRowsToOffset != -1)
                command.Parameters.AddWithValue("@NumberOfRowsToOffset", NumberOfRowsToOffset);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtFilteredData = new DataTable();
                    dtFilteredData.Load(reader);
                }

                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtFilteredData;
        }

        public static int GetTotalUsersCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT Count({_PrimaryKeyColumnName}) FROM Users";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return -1;
        }

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy,string SortDirection, string ColumnNameToFilterBy ,ref string ValueToFilterBy, char? WildChar = null)
        {
            string query = _QueryWithoutPagination;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Is Active" && ValueToFilterBy == "All"))
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection,true);
            }

            else
            {
                if (ColumnNameToFilterBy == "Username")
                {
                    ColumnNameToFilterBy = "UsernameHash";
                    query = "SELECT TOP (@WantedNumOfRecords) " + _UsernameLookupQueryPart;
                }
                else
                    query = _QueryWithoutPagination;

                if (ColumnNameToFilterBy == "Is Active")
                {
                    if(ValueToFilterBy != "All")
                    ValueToFilterBy = clsGeneralUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }

                query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar, true);
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection,true);
            }
                
            return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }

        public static Dictionary<int, string> GetUsersIDsWithIVs()
        {
            Dictionary<int, string> UsersIDsWithIVs = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT UserID, UsernameIV FROM Users";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    UsersIDsWithIVs = new Dictionary<int, string>();

                    while (reader.Read())
                    {
                        UsersIDsWithIVs.Add((int)reader["UserID"], (string)reader["UsernameIV"]);
                    }
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return UsersIDsWithIVs;
        }
    }
}