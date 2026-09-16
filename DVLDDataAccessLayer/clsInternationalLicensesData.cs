using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsInternationalLicensesData
    {
        private static string _query =
         $@"SELECT TOP (@WantedNumOfRecords) InternationalLicenseID AS [Int.License ID],ApplicationID AS [Application ID],DriverID AS [Driver ID],
           IssuedUsingLocalLicenseID AS [L.License ID],Format(IssueDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Issue Date],
           Format(ExpirationDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Expiration Date],IsActive AS [Is Active]
           FROM InternationalLicenses";

        private static string _PrimaryKeyColumnName = "InternationalLicenseID";

        public static DataTable GetDriverInternationalLicenses(int DriverID, byte WantedNumOfRecords, int LowstBroughtIntLicID = -1)
        {
            DataTable dtIntLicenses = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _query + @" WHERE DriverID = @DriverID";

            if (LowstBroughtIntLicID != -1)
                query += " AND DriverID < @LowstBroughtIntLicID";

            query += " ORDER BY InternationalLicenseID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            if (LowstBroughtIntLicID != -1)
                command.Parameters.AddWithValue("@LowstBroughtIntLicID", LowstBroughtIntLicID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtIntLicenses = new DataTable();
                    dtIntLicenses.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtIntLicenses;
        }

        public static DataTable GetAllInternationalLicenses(byte WantedNumOfRecords, int LowstBroughtIntLicID = -1)
        {
            DataTable dtIntLicenses = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _query;

            if (LowstBroughtIntLicID != -1)
                query += " AND DriverID < @LowstBroughtIntLicID";

            query += " ORDER BY InternationalLicenseID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (LowstBroughtIntLicID != -1)
                command.Parameters.AddWithValue("@LowstBroughtIntLicID", LowstBroughtIntLicID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtIntLicenses = new DataTable();
                    dtIntLicenses.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtIntLicenses;
        }

        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(_query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", 0);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable dtLDLApplications = new DataTable();
                dtLDLApplications.Load(reader);
                reader.Close();

                return dtLDLApplications;
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return null;
        }

        public static int IssueDriverLicense(int ApplicationID ,int DriverID,int LocalLicenseID,
            DateTime IssueDate,DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses VALUES (@ApplicationID,@DriverID,@LocalLicenseID,
                             @IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool Find(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int LocalLicenseID,
           ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        private static string _GetOriginalColumnName(string SendedColumnName)
        {
            
            switch(SendedColumnName)
            {
                case "Int.License ID":
                    return "InternationalLicenseID";

                case "Application ID":
                    return "ApplicationID";

                case "Driver ID":
                    return "DriverID";

                case "L.License ID":
                    return "IssuedUsingLocalLicenseID";

                case "Is Active":
                    return "IsActive";

                default:
                    return "";
            }
        }
        private static string _GetDataFilteringQuery(byte WantedNumOfRecords, string ColumnNameToFilterBy, ref string ValueToFilterBy,
                string ColumnNameToOrderBy, string SortDirection, int LastLowestbroughtIntAppID = -1, char? WildChar = null)
        {
            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyColumnName;

            ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            string query = _query;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                  || (ColumnNameToFilterBy == "IsActive" && ValueToFilterBy == "All"))
            {
                query += clsUtility.GetLastFilterQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, false, LastLowestbroughtIntAppID);
            }
            else
            {
                if (ColumnNameToFilterBy == "IsActive")
                {
                    if (ValueToFilterBy != "All")
                        ValueToFilterBy = clsUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }

                query += clsUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                query += clsUtility.GetLastFilterQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, true, LastLowestbroughtIntAppID);
            }

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilterBy, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                          int LastLowestbroughtLDLAppID = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilterBy, ref ValueToFilterBy,
                            ColumnNameToOrderBy, SortDirection, LastLowestbroughtLDLAppID, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if(! string.IsNullOrEmpty(ValueToFilterBy))
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

        public static int GetTotalCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Count(InternationalLicenseID) FROM InternationalLicenses";

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

        public static bool HasDriverActiveInternationalLicense(int LocalLicenseID,ref int InternationalLicenseID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenseID FROM InternationalLicenses
                             WHERE IssuedUsingLocalLicenseID = @LocalLicenseID AND IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    InternationalLicenseID = Convert.ToInt32(result);
                    IsFound = true;
                }

            }

            catch { }

            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static int GetLicenseID(int InternationalLicenseAppID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT InternationalLicenseID FROM InternationalLicenses WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", InternationalLicenseAppID);

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

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy, string SortDirection, string ColumnNameToFilterBy, ref string ValueToFilterBy, char? WildChar = null)
        {
            string query = _query;

            ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "IsActive" && ValueToFilterBy == "All"))
            {
                query += clsUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                if (ColumnNameToFilterBy == "IsActive")
                {
                    if(ValueToFilterBy !="All")
                    ValueToFilterBy = clsUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }

                query += clsUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                query += clsUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }
            return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }
    }
}