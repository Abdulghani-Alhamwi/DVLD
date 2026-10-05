using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsLocalLicensesData
    {
        private static readonly string _PrimaryKeyColumnName = "LicenseID";

        private static readonly string _PrimaryKeyViewedColumnName = "Lic.ID";

        private static readonly string _FixedQueryPart =
          $@"{_PrimaryKeyColumnName} AS [Lic.ID],ApplicationID AS [App.ID],LicenseClasses.ClassName AS [Class Name],
            Format(IssueDate,'{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Issue Date],
            FORMAT(ExpirationDate,'{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Expiration Date],
            IsActive AS [Is Active] FROM LocalLicenses INNER JOIN LicenseClasses ON LocalLicenses.LicenseClassID = LicenseClasses.LicenseClassID
            WHERE DriverID = @DriverID";

        private static readonly string _QueryWithoutPagination = "SELECT TOP (@WantedNumOfRecords) " + _FixedQueryPart;

        private static readonly string _QueryForOffsetPagination = "SELECT " + _FixedQueryPart;

        private static readonly string _OffsetPaginationQueryPart = clsGeneralUtility.GetOffsetPaginationQueryPart();

        public static DataTable GetLocalLicenses(int DriverID,byte WantedNumOfRecords,int LastBroughtLicenseID = -1
            , string ColumnNameToOrderBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtLicensesData = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(LastBroughtLicenseID, ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
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
                    dtLicensesData = new DataTable();
                    dtLicensesData.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtLicensesData;
        }

        private static string _GetQueryForCursorPagination(int LastBroughtLicenseID, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;
            if (LastBroughtLicenseID != -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtLicenseID, true, true);
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

        public static int IssueLocalDrivingLicense(int ApplicationID ,int DriverID,byte LicenseClassID,DateTime IssueDate , DateTime ExpirationDate,string Notes,decimal PaidFees,bool IsActive,byte IssueReason,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LocalLicenses VALUES (@ApplicationID, @DriverID,@LicenseClassID,
                             @IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes != null)
            command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result);
            }

            catch (Exception ex){ Console.Write(ex.Message); }

            finally
            {
                connection.Close();
            }
            return -1;
        }

        public static bool Find(int LicenseID , ref int ApplicationID,ref int DriverID, ref byte LicenseClassID,
                            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT * FROM LocalLicenses WHERE {_PrimaryKeyColumnName} = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = Convert.ToByte(reader["LicenseClassID"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"]; ;

                    if (reader["Notes"] != DBNull.Value)
                        Notes = (string)reader["Notes"];
                    else
                        Notes = null;

                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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

        public static int GetLicenseID(int LDLApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $"SELECT {_PrimaryKeyColumnName} FROM LocalLicenses WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", LDLApplicationID);

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

        public static string GetLicenseNotes(int LocalLicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $"SELECT Notes FROM LocalLicenses WHERE {_PrimaryKeyColumnName} = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

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

        public static bool DeactivateLicense(int LocalLicenseID)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $@"UPDATE LocalLicenses SET IsActive = 0 WHERE {_PrimaryKeyColumnName} = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
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

        public static bool HasDriverRenewedLicense(int DriverID,int LicenseClassID,ref int RenewedLicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $@"SELECT {_PrimaryKeyColumnName} FROM LocalLicenses
                             WHERE DriverID = @DriverID AND LicenseClassID = @LicenseClassID AND IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    RenewedLicenseID = Convert.ToInt32(result);
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

        public static int GetDriverID(int LocalLicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = 
             $@"SELECT DriverID FROM LocalLicenses WHERE {_PrimaryKeyColumnName} = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

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

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy, string SortDirection)
        {
            string query = _QueryWithoutPagination;
            query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            return query;
        }

        public static DataTable GetSortedInfo(int DriverID,byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection)
        {
            SqlConnection Connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand Command = new SqlCommand(_GetDataSortingQuery(ColumnNameToOrderBy, SortDirection), Connection);
            Command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);
            Command.Parameters.AddWithValue("@DriverID", DriverID);

            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(Connection, Command);
        }

        public static int GetLocalLicenseIdByLicenseClass(int DriverID , byte LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query =
             $@"SELECT LicenseID FROM LocalLicenses WHERE DriverID = @DriverID AND LicenseClassID = @LicenseClassID AND IsActive = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
    }
}
