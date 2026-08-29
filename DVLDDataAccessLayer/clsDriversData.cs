using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsDriversData
    {
        private static string _query =
         $@"SELECT TOP (@WantedNumOfRecords) Drivers.DriverID AS [Driver ID] , Drivers.PersonID AS [Person ID] ,People.NationalNo AS [National No.],
           People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName AS [Full Name],Format(CreatedDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Date Created],
           SUM(CAST(LocalLicenses.IsActive AS TINYINT)) + (CASE WHEN InternationalLicenses.IsActive IS NULL THEN 0 ELSE 1 END) AS [Active Licenses]
           From Drivers INNER JOIN People ON Drivers.PersonID = People.PersonID INNER JOIN LocalLicenses ON Drivers.DriverID = LocalLicenses.DriverID
           LEFT JOIN InternationalLicenses ON Drivers.DriverID = InternationalLicenses.DriverID";

        private static string _groupByPartOfQuery =
         $@" GROUP BY Drivers.DriverID,Drivers.PersonID,People.NationalNo,
           People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName,
           Format(CreatedDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}'),InternationalLicenses.IsActive ORDER BY Drivers.DriverID DESC";
        public static DataTable GetDriversInfo(byte WantedNumOfRecords, int LastLowestBroughtDriverID = -1)
        {
            DataTable dtDrivers = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _query;

            if (LastLowestBroughtDriverID != -1)
                query += " WHERE DriverID < @LastLowestBroughtDriverID" + _groupByPartOfQuery;

            else
                query += _groupByPartOfQuery;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (LastLowestBroughtDriverID != -1) 
            command.Parameters.AddWithValue("@LastLowestBroughtDriverID", LastLowestBroughtDriverID);

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
                    return "Drivers.DriverID";

                case "Person ID":
                    return "Drivers.PersonID";

                case "National No.":
                    return "People.NationalNo";

                case "Full Name":
                    return "People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName";

                default:
                    return "";
            }
        }
        private static string _GetDataFilteringQuery(byte WantedNumberOfRecords, string ColumnNameToFilter, string ValueToFilterBy, char? WildChar = null, int LastLowestBroughtDriverID = -1)
        {
            string query = clsDriversData._query;

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                if (LastLowestBroughtDriverID != -1)
                    query += " WHERE DriverID < @LastLowestBroughtDriverID" + _groupByPartOfQuery;

                else
                    query += _groupByPartOfQuery;

                return query;
            }

                ColumnNameToFilter = _GetOriginalColumnName(ColumnNameToFilter);

            if (WildChar == null)
                query += $" WHERE {ColumnNameToFilter} = @Value";
            else
                query += $" WHERE {ColumnNameToFilter} LIKE @Value + @WildChar";

            if (LastLowestBroughtDriverID != -1)
                query += " AND DriverID < @LastLowestBroughtDriverID" + _groupByPartOfQuery;

            else
                query += _groupByPartOfQuery;

            return query;
        }
        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, int LastLowestBroughtDriverID = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilter, ValueToFilterBy, WildChar, LastLowestBroughtDriverID);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (!string.IsNullOrEmpty(ValueToFilterBy))
                command.Parameters.AddWithValue("@Value", ValueToFilterBy);

            if (WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

            if(LastLowestBroughtDriverID!=-1)
                command.Parameters.AddWithValue("@LastLowestBroughtDriverID", LastLowestBroughtDriverID);

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
