using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsDriversData
    {
        private static readonly string _PrimaryKeyColumnName = "Drivers.DriverID";

        private static string _query =
         $@"SELECT TOP (@WantedNumOfRecords) {_PrimaryKeyColumnName} AS [Driver ID] , Drivers.PersonID AS [Person ID] ,People.NationalNo AS [National No.],
            People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName AS [Full Name],Format(CreatedDate,'{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Date Created],
            SUM(CAST(LocalLicenses.IsActive AS TINYINT)) + (CASE WHEN InternationalLicenses.IsActive IS NULL THEN 0 ELSE 1 END) AS [Active Licenses]
            From Drivers INNER JOIN People ON Drivers.PersonID = People.PersonID INNER JOIN LocalLicenses ON {_PrimaryKeyColumnName} = LocalLicenses.DriverID
            LEFT JOIN InternationalLicenses ON {_PrimaryKeyColumnName} = InternationalLicenses.DriverID";

        private static string _groupByPartOfQuery =
         $@" GROUP BY {_PrimaryKeyColumnName}, Drivers.PersonID, People.NationalNo,
            People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName,
            Format(CreatedDate,'{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}'), InternationalLicenses.IsActive
            ORDER BY {_PrimaryKeyColumnName} DESC";

        public static DataTable GetDriversInfo(byte WantedNumOfRecords, int LastLowestBroughtDriverID = -1)
        {
            DataTable dtDrivers = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _query;

            if (LastLowestBroughtDriverID != -1)
                query += $" WHERE {_PrimaryKeyColumnName} < {LastLowestBroughtDriverID}" + _groupByPartOfQuery;

            else
                query += _groupByPartOfQuery;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

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

            string query = $@"SELECT {_PrimaryKeyColumnName} FROM Drivers WHERE PersonID = @PersonID";

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

            string query = $@"SELECT PersonID FROM Drivers WHERE {_PrimaryKeyColumnName} = @DriverID";

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

            string query = $@"SELECT Count({_PrimaryKeyColumnName}) FROM Drivers";

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

        private static string _GetOriginalColumnName(string SendedColumnName)
        {
            switch (SendedColumnName)
            {
                case "Driver ID":
                    return _PrimaryKeyColumnName;

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

        private static string _GetDataFilteringQuery(byte WantedNumberOfRecords, string ColumnNameToFilterBy, string ValueToFilterBy, char? WildChar = null, int LastLowestBroughtDriverID = -1)
        {
            string query = _query;

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                if (LastLowestBroughtDriverID != -1)
                    query += $" WHERE {_PrimaryKeyColumnName} < {LastLowestBroughtDriverID}" + _groupByPartOfQuery;

                else
                    query += _groupByPartOfQuery;

                return query;
            }

            ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);

            if (LastLowestBroughtDriverID != -1)
                query += $" AND {_PrimaryKeyColumnName} < {LastLowestBroughtDriverID}" + _groupByPartOfQuery;

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

        public static bool HasActiveLicenseFromClass(int DriverID,int LicenseClassID, out int LicenseID)
        {
            LicenseID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT LicenseID FROM LocalLicenses WHERE DriverID = @DriverID
                             AND LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    LicenseID = Convert.ToInt32(result);
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

        public static short GetDriverLocalLicensesCount(int DriverID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT Count(LicenseID) FROM LocalLicenses WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt16(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return -1;
        }

        public static short GetDriverInternationalLicensesCount(int DriverID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT Count(InternationalLicenseID) FROM InternationalLicenses WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt16(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return -1;
        }
    }
}
