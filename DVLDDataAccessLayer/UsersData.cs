using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsUsersData
    {
        private static string ColumnNamesQuery = @"SELECT TOP (@WantedNumberOfRecords) UserID AS [User ID] , Users.PersonID AS [Person ID] ,
                                                   People.FirstName + ' ' + People.SecondName
                                                  + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName AS [Full Name] , UserName,IsActive AS [Is Active]
                                                   From Users INNER JOIN People ON Users.PersonID = People.PersonID";

        public static DataTable GetUsersInfo(byte WantedNumberOfRecords, int LastLowestBroughtUserID = -1)
        {
            DataTable dtUsers = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"SELECT TOP (@WantedNumberOfRecords) UserID AS [User ID] , Users.PersonID AS [Person ID] ,
                              People.FirstName + ' ' + People.SecondName
                             + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName AS [Full Name] , UserName,IsActive AS [Is Active]
                              From Users INNER JOIN People ON Users.PersonID = People.PersonID";

            if (LastLowestBroughtUserID != -1)
                query += " WHERE UserID < @LastLowestBroughtUserID";
                            
                query += " ORDER BY UserID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", WantedNumberOfRecords);
            command.Parameters.AddWithValue("@LastLowestBroughtUserID", LastLowestBroughtUserID);


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
            string query = ColumnNamesQuery;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", 0);

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

            string query = @"INSERT INTO Users VALUES
                             (@PersonID, @UserName, @Password,@Salt, @IsActive) ;
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
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE USERS SET
                             PersonID = @PersonID , UserName = @UserName ,
                             Password = @Password ,Salt = @Salt, IsActive = @IsActive
                             WHERE UserID = @UserID";

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
                AffectedRows = command.ExecuteNonQuery();
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
            int AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"DELETE FROM Users
                             WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID" , UserID);

            try
            {
                connection.Open();

                AffectedRows = command.ExecuteNonQuery();
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

            string query = @"SELECT Found = 1
                             FROM Users WHERE PersonID = @PersonID";

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

            string query = @"SELECT Found = 1 FROM Users 
                             WHERE UserName = @UserName";

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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT UserID,PersonID,UserName,Password,Salt
                             ,IsActive FROM Users 
                             WHERE UserID = @UserID";

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

        public static void GetUserPasswordWithSalt(int UserID , ref string Password, ref byte[] Salt)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Password , Salt FROM Users
                             WHERE UserID = @UserID";

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

            string query = @"SELECT UserID , Password , Salt , IsActive
                             FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
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

            string query = @"SELECT UserName , Password FROM Users
                             WHERE UserID = @UserID";

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
            int AffectedRows = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE Users SET Password = @Password , Salt = @Salt
                             WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Salt",Salt);

            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
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

            string query = "SELECT UserName FROM Users WHERE UserID = @UserID";

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
                    return "UserID";

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

        private static string _GetDataFilteringQuery(byte WantedNumberOfRecords, string ColumnNameToFilter,ref string ValueToFilterBy, char? WildChar = null, int LastLowestbroughtUserID = -1)
        {
            string query = ColumnNamesQuery;

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                query += " ORDER BY UserID DESC";
                return query;
            }

            if (ColumnNameToFilter != "UserName")
                ColumnNameToFilter = _GetOriginalColumnName(ColumnNameToFilter);

            if (ColumnNameToFilter == "IsActive")
            {
                if (ValueToFilterBy == "All")
                {

                    if (LastLowestbroughtUserID != -1)
                        query += @" WHERE UserID < @LastLowestbroughtUserID
                                    ORDER BY UserID DESC";
                    
                    else 
                        query += " ORDER BY UserID DESC";

                    return query;
                }

                else
                    ValueToFilterBy = (ValueToFilterBy == "Yes") ? "1" : "0";
            }

                if (WildChar == null)
                    query += $" WHERE {ColumnNameToFilter} = @Value";
                else
                    query += $" WHERE {ColumnNameToFilter} Like @Value + @WildChar";
            

                if (LastLowestbroughtUserID == -1)
                    query += " ORDER BY UserID DESC";

                else
                    query += @" AND UserID < @LastLowestbroughtUserID
                           ORDER BY UserID DESC";

                return query;
        }

        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null, int LastLowestbroughtUserID = -1)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumberOfRecords, ColumnNameToFilter,ref ValueToFilterBy, WildChar, LastLowestbroughtUserID);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", WantedNumberOfRecords);

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

        public static uint GetTotalUsersCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Count(UserID) FROM Users";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToUInt32(result);

            }

            catch { }

            finally
            {
                connection.Close();
            }
            return 0;
        }

    }
}
