using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsUsersData
    {
        private static readonly string _PrimaryKeyColumnName = "UserID"; 

        private static string _query =
         $@"SELECT TOP (@WantedNumOfRecords) {_PrimaryKeyColumnName} AS [User ID],Users.PersonID AS [Person ID] ,
           People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName AS [Full Name],
           UserName,IsActive AS [Is Active] From Users INNER JOIN People ON Users.PersonID = People.PersonID";

        public static DataTable GetUsersInfo(byte WantedNumOfRecords, int LastLowestBroughtUserID = -1)
        {
            DataTable dtUsers = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _query;

            if (LastLowestBroughtUserID != -1)
                query += $" WHERE {_PrimaryKeyColumnName} < {LastLowestBroughtUserID}";
                            
                query += $" ORDER BY {_PrimaryKeyColumnName} DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

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

        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _query;

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

        public static int AddNewUser(int PersonID , string UserName,string Password ,string Salt,bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users VALUES (@PersonID, @UserName, @Password, @Salt, @IsActive);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Salt", Salt);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result!=null)
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

        public static bool UpdateUser(int UserID,int PersonID, string UserName, string Password,string Salt, bool IsActive)
        {
            byte AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"UPDATE USERS SET PersonID = @PersonID, UserName = @UserName,
                             Password = @Password, Salt = @Salt, IsActive = @IsActive
                             WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID",UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Salt", Salt);
            command.Parameters.AddWithValue("@IsActive", IsActive);

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

        public static bool IsUserAlreadyExists(string UserName)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName",UserName);

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

        public static bool Find(int UserID, ref int PersonID, ref string UserName, ref string Password, ref string Salt, ref bool IsActive)
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
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    Salt = (string)reader["Salt"];
                    IsActive = (bool)reader["IsActive"];

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

        public static bool GetLoginInfo(string UserName, ref int UserID, ref string Password, ref bool IsActive, ref byte[] Salt)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT {_PrimaryKeyColumnName}, Password, Salt, IsActive FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader[_PrimaryKeyColumnName];
                    Password = (string)reader["Password"];
                    Salt = Convert.FromBase64String((string)reader["Salt"]);
                    IsActive = (bool)reader["IsActive"];

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

        public static bool GetLoginInfo(int UserID, ref string UserName, ref string Password)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT UserName, Password FROM Users WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserName = (string)reader["UserName"];
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

        public static string GetUserName(int UserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $"SELECT UserName FROM Users WHERE {_PrimaryKeyColumnName} = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return result.ToString();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return null;
        }

        private static string _GetOriginalColumnName(string SendedColumnName)
        {
            switch(SendedColumnName)
            {
                case "User ID":
                    return _PrimaryKeyColumnName;

                case "Person ID":
                    return "Users.PersonID";

                case "Full Name":
                    return "People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName";

                case "Is Active":
                    return "IsActive";

                default:
                    return "";
            }
        }

        private static string _GetDataFilteringQuery(byte WantedNumOfRecords, string ColumnNameToFilterBy, ref string ValueToFilterBy,
            string ColumnNameToOrderBy,string SortDirection, int LastLowestbroughtUserID = -1, char? WildChar = null)
        {
            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyColumnName;

                string query = _query;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "IsActive" && ValueToFilterBy == "All"))
            {
                query += clsGeneralUtility.GetLastFilterQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, false, LastLowestbroughtUserID);
            }

            else
            {
                if (ColumnNameToFilterBy != "UserName")
                    ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

                if (ColumnNameToFilterBy == "IsActive")
                {
                    if(ValueToFilterBy != "All")
                    ValueToFilterBy = clsGeneralUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }

                query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);

                query += clsGeneralUtility.GetLastFilterQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, true, LastLowestbroughtUserID);
            }

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilterBy, string ValueToFilterBy, string ColumnNameToOrderBy,string SortDirection,
            int LastLowestbroughtUserID = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilterBy, ref ValueToFilterBy,
                            ColumnNameToOrderBy,SortDirection, LastLowestbroughtUserID, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (ValueToFilterBy != null)
                command.Parameters.AddWithValue("@Value", ValueToFilterBy);

            if (WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

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

            catch  { }

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
            string query = _query;

            if (ColumnNameToFilterBy != "UserName")
                ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "IsActive" && ValueToFilterBy == "All"))
            {
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                if (ColumnNameToFilterBy == "IsActive")
                {
                    if(ValueToFilterBy != "All")
                    ValueToFilterBy = clsGeneralUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }

                query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }
                
            return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }
    }
}