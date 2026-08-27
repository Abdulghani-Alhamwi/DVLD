using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsDriversData
    {

        private static string Query =
         @"SELECT TOP (@WantedNumberOfRecords) Drivers.DriverID AS [Driver ID] , Drivers.PersonID AS [Person ID] ,People.NationalNo AS [National No.],
           People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName AS [Full Name],
           Format(CreatedDate,'dd/MM/yyyy h:MM tt') AS [Date Created],SUM(CAST(LocalLicenses.IsActive AS tinyINT)) + SUM(CAST(InternationalLicenses.IsActive AS tinyINT)) AS [Active Licenses]
           From Drivers INNER JOIN People ON Drivers.PersonID = People.PersonID INNER JOIN LocalLicenses ON Drivers.DriverID = LocalLicenses.DriverID
           INNER JOIN InternationalLicenses ON Drivers.DriverID = InternationalLicenses.DriverID
           GROUP BY Drivers.DriverID,Drivers.PersonID,People.NationalNo,
           People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName,
           Format(CreatedDate,'dd/MM/yyyy h:MM tt')";

        public static DataTable GetDriversInfo(byte WantedNumberOfRecords, int LastLowestBroughtDriverID = -1)
        {
            DataTable dtDrivers = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = Query;

            if (LastLowestBroughtDriverID != -1)
                query += " WHERE DriverID < @LastLowestBroughtUserID";

            query += " ORDER BY DriverID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", WantedNumberOfRecords);
            command.Parameters.AddWithValue("@LastLowestBroughtUserID", LastLowestBroughtDriverID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtDrivers = new DataTable();
                    dtDrivers.Load(reader);
                }

                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtDrivers;
        }
        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers VALUES (@PersonID,@CreatedByUserID,@CreationDate);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreationDate", CreatedDate);

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
        public static bool IsPersonAlreadyADriver(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Drivers WHERE PersonID = @PersonID";

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
        public static int GetDriverID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT DriverID FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
        public static int GetDriverPersonID(int DriverID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT PersonID FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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
        public static int GetTotalDriversCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Count(DriverID) FROM Drivers";

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
            return 0;
        }
        private static string _GetOriginalColumnName(string SendedColumnName)
        {
            switch (SendedColumnName)
            {
                case "Driver ID":
                    return "DriverID";

                case "Person ID":
                    return "PersonID";

                case "National No.":
                    return "NationalNo";

                case "Full Name":
                    return "FullName";

                case "Date Created":
                    return "CreatedDate";

                case "Active Licenses":
                    return "SUM(CAST(LocalLicenses.IsActive AS tinyINT)) + SUM(CAST(InternationalLicenses.IsActive AS tinyINT))";

                default:
                    return "";
            }
        }
        private static string _GetDataFilteringQuery(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null, int LastLowestBroughtDriverID = -1)
        {
            string query = Query;

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                if (LastLowestBroughtDriverID != -1)
                    query += @" WHERE DriverID < @LastLowestbroughtPersonID
                           ORDER BY DriverID DESC";
                else
                    query += " ORDER BY DriverID DESC";
                return query;
            }

                ColumnNameToFilter = _GetOriginalColumnName(ColumnNameToFilter);

            if (WildChar == null)
                query += $" HAVING {ColumnNameToFilter} = @Value";
            else
                query += $" HAVING {ColumnNameToFilter} LIKE @Value + @WildChar";

                if (LastLowestBroughtDriverID == -1)
                    query += " ORDER BY DriverID DESC";

                else
                    query += @" AND DriverID < @LastLowestbroughtPersonID
                           ORDER BY DriverID DESC";

            return query;
        }
        public static DataTable GetFilteredData(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null, int LastLowestBroughtDriverID = -1)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumberOfRecords, ColumnNameToFilter, ValueToFilterBy, WildChar, LastLowestBroughtDriverID);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", WantedNumberOfRecords);

            if (!string.IsNullOrEmpty(ValueToFilterBy))
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

            catch { }

            finally
            {
                connection.Close();
            }

            return dtFilteredData;
        }
    }
}
