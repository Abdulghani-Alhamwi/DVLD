using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsInternationalLicensesData
    {
        private static readonly string _PrimaryKeyColumnName = "InternationalLicenseID";

        private static readonly string _PrimaryKeyViewedColumnName = "Int.License ID";

        private static readonly string _FixedQueryPart = "* FROM InternationalLicenses_View";
        
        private static readonly string _QueryWithoutPagination = "SELECT TOP (@WantedNumOfRecords) " + _FixedQueryPart;

        private static readonly string _QueryForOffsetPagination = "SELECT " + _FixedQueryPart;

        private static readonly string _OffsetPaginationQueryPart = clsGeneralUtility.GetOffsetPaginationQueryPart();

        public static DataTable GetDriverInternationalLicenses(int DriverID, byte WantedNumOfRecords, int LastBroughtIntLicenseID = -1
              , string ColumnNameToOrderBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtIntLicenses = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(LastBroughtIntLicenseID, ColumnNameToOrderBy, SortDirection,true);
            }

            else
            {
                query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection,true);
            }

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static DataTable GetAllInternationalLicensesData(byte WantedNumOfRecords, int LastBroughtIntLicenseID = -1
             , string ColumnNameToOrderBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtIntLicenses = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(LastBroughtIntLicenseID, ColumnNameToOrderBy, SortDirection);
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

        private static string _GetQueryForCursorPagination(int LastBroughtIntLicenseID, string ColumnNameToOrderBy, string SortDirection,bool FilterByTheDriver = false)
        {
            string query;
            if (LastBroughtIntLicenseID != -1)
            {
                if (FilterByTheDriver)
                {
                    query = _QueryWithoutPagination + " WHERE DriverID = @DriverID";
                    query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyViewedColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtIntLicenseID, true, true, true);
                }

                else
                {
                    query = _QueryWithoutPagination;
                    query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyViewedColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtIntLicenseID, true, true, true);
                }
            }

            else
            {
                if (FilterByTheDriver)
                {
                    query = _QueryWithoutPagination + " WHERE DriverID = @DriverID";
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                }

                else
                {
                    query = _QueryWithoutPagination;
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                }
            }

            return query;
        }

        private static string _GetQueryForOffsetPagination(int NumberOfRowsToOffset, string ColumnNameToOrderBy, string SortDirection, bool FilterByTheDriver = false)
        {
            string query;

            if (NumberOfRowsToOffset == -1)
            {
                if (FilterByTheDriver)
                {
                    query = _QueryWithoutPagination + " WHERE DriverID = @DriverID";
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                }

                else
                {
                    query = _QueryWithoutPagination;
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                }
            }

            else
            {
                if (FilterByTheDriver)
                {
                    query = _QueryForOffsetPagination + " WHERE DriverID = @DriverID";
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
                    query += _OffsetPaginationQueryPart;
                }

                else
                {
                    query = _QueryForOffsetPagination;
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
                    query += _OffsetPaginationQueryPart;
                }
            }

            return query;
        }

        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(_QueryWithoutPagination, connection);
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

            string query = $"SELECT * FROM InternationalLicenses WHERE {_PrimaryKeyColumnName} = @InternationalLicenseID";

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

        private static string _GetDataFilteringQuery(byte WantedNumOfRecords, string ColumnNameToFilterBy, ref string ValueToFilterBy,
                string ColumnNameToOrderBy, string SortDirection, int LastBroughtIntLicenseID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            string query;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                  || (ColumnNameToFilterBy == "Is Active" && ValueToFilterBy == "All"))
            {
                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _GetQueryForCursorPagination(LastBroughtIntLicenseID, ColumnNameToOrderBy, SortDirection);
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
                    if (ValueToFilterBy != "All")
                        ValueToFilterBy = clsGeneralUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }


                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _QueryWithoutPagination;
                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar, true);
                    query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyViewedColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtIntLicenseID, true, true, true);
                }

                else
                {
                    query = _QueryForOffsetPagination;
                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar, true);
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                    query += _OffsetPaginationQueryPart;
                }
            }

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilterBy, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection,
               int LastBroughtIntLicenseID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilterBy, ref ValueToFilterBy,
                            ColumnNameToOrderBy, SortDirection, LastBroughtIntLicenseID, NumberOfRowsToOffset, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if(!string.IsNullOrEmpty(ValueToFilterBy))
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

        public static int GetTotalCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT Count({_PrimaryKeyColumnName}) FROM InternationalLicenses";

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

            string query = $@"SELECT {_PrimaryKeyColumnName} FROM InternationalLicenses
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

        public static bool HasDriverActiveInternationalLicense(int DriverID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM InternationalLicenses
                             WHERE DriverID = @DriverID AND IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;                }

            }

            catch { }

            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static bool LinkWithNewLocalLicense(int DriverID, int NewLocalLicenseID)
        {
            byte AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE InternationalLicenses SET IssuedUsingLocalLicenseID = @NewLocalLicenseID
                             WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NewLocalLicenseID", NewLocalLicenseID);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static int GetLicenseID(int InternationalLicenseAppID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $"SELECT {_PrimaryKeyColumnName} FROM InternationalLicenses WHERE ApplicationID = @ApplicationID";

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

        public static int GetLocalLicenseID(int InternationalLicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $"SELECT IssuedUsingLocalLicenseID FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", InternationalLicenseID);

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

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy, string SortDirection, string ColumnNameToFilterBy,
            ref string ValueToFilterBy, char? WildChar = null)
        {
            string query = _QueryWithoutPagination;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Is Active" && ValueToFilterBy == "All"))
            {
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                if (ColumnNameToFilterBy == "Is Active")
                {
                    if(ValueToFilterBy !="All")
                    ValueToFilterBy = clsGeneralUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }

                query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar, true);
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }
            return query;
        }

        private static string _GetDataSortingQueryByDriver(string ColumnNameToOrderBy, string SortDirection)
        {
            string query = _QueryWithoutPagination;

            query += " WHERE DriverID = @DriverID";
            query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);

            return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }

        public static DataTable GetSortedInfo(int DriverID,byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _GetDataSortingQueryByDriver(ColumnNameToOrderBy, SortDirection);

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(Connection, Command);
        }
    }
}