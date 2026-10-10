using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsDetainedLicensesData
    {
        private static readonly string _PrimaryKeyColumnName = "DetainID";

        private static readonly string _PrimaryKeyViewedColumnName = "D.ID";

        private static readonly string _FixedQueryPart = "* FROM DetainedLicenses_View";
        
        private static readonly string _QueryWithoutPagination = "SELECT TOP (@WantedNumOfRecords) " + _FixedQueryPart;

        private static readonly string _QueryForOffsetPagination = "SELECT " + _FixedQueryPart;

        private static readonly string _OffsetPaginationQueryPart = clsGeneralUtility.GetOffsetPaginationQueryPart();

        public static DataTable GetDetainedLicensesInfo(byte WantedNumOfRecords, int LastBroughtDetainID = -1
             , string ColumnNameToOrderBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtDetainedLicenses = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(LastBroughtDetainID, ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
            }


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (NumberOfRowsToOffset != -1 && ColumnNameToOrderBy != _PrimaryKeyViewedColumnName)
                command.Parameters.AddWithValue("@NumberOfRowsToOffset", NumberOfRowsToOffset);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtDetainedLicenses = new DataTable();
                    dtDetainedLicenses.Load(reader);
                }

                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtDetainedLicenses;
        }

        private static string _GetQueryForCursorPagination(int LastBroughtIntLicenseID, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;
            if (LastBroughtIntLicenseID != -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyViewedColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtIntLicenseID, true, true, true);
            }

            else
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
            }

            return query;
        }

        private static string _GetQueryForOffsetPagination(int NumberOfRowsToOffset, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;

            if (NumberOfRowsToOffset == -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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

        public static bool IsDetainedLicense(int LocalLicenseID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT Detained = 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

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

        public static int AddNewDetainedLicense(int LocalLicenseID,DateTime DetainDate,decimal FineFees,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses VALUES (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,0,null,null,null);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
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

        public static bool ReleaseDetainedLicense(int LocalLicenseID, DateTime ReleaseDate,int ReleasedByUserID,int ReleaseApplicationID)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses SET IsReleased = 1, ReleaseDate = @ReleaseDate,
                             ReleasedByUserID = @ReleasedByUserID, ReleaseApplicationID = @ReleaseApplicationID
                             WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

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
            return (AffectedRows != 0);
        }

        public static bool Find(int LocalLicenseID, ref int DetainID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

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
                string ColumnNameToOrderBy, string SortDirection, int LastBroughtDetainID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            string query;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Is Released" && ValueToFilterBy == "All"))
            {
                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _GetQueryForCursorPagination(LastBroughtDetainID, ColumnNameToOrderBy, SortDirection);
                }

                else
                {
                    query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
                }
            }

            else
            {
                if (ColumnNameToFilterBy == "Is Released")
                {
                    if (ValueToFilterBy != "All")
                    ValueToFilterBy = clsGeneralUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
                }

                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _QueryWithoutPagination;
                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar, true);
                    query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyViewedColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtDetainID, true, true, true);
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
               int LastBroughtDetainID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilterBy, ref ValueToFilterBy,
                ColumnNameToOrderBy, SortDirection, LastBroughtDetainID, NumberOfRowsToOffset, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            command.Parameters.AddWithValue("@Value", ValueToFilterBy);

            if (WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

            if (NumberOfRowsToOffset != -1 && ColumnNameToOrderBy != _PrimaryKeyViewedColumnName)
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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT Count({_PrimaryKeyColumnName}) FROM DetainedLicenses";

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

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy, string SortDirection, string ColumnNameToFilterBy, ref string ValueToFilterBy, char? WildChar = null)
        {
            string query = _QueryWithoutPagination;

            if (string.IsNullOrEmpty(ValueToFilterBy) 
                || (ColumnNameToFilterBy == "Is Released" && ValueToFilterBy == "All"))
            {
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection,true);
            }

            else
            {
                if (ColumnNameToFilterBy == "Is Released")
                {
                    if (ValueToFilterBy != "All")
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
            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(clsDataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }
    }
}