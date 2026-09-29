using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsTestAppointmentsData
    {
        private static readonly string _PrimaryKeyColumnName = "TestAppointmentID";
        private static readonly string _PrimaryKeyViewedColumnName = "Appointment ID";

        private static readonly string _FixedQueryPart =
          $@"{_PrimaryKeyColumnName} AS [Appointment ID],
          FORMAT(AppointmentDate,'{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Appointment Date] ,
          PaidFees AS [Paid Fees],IsLocked AS [Is Locked] FROM TestAppointments";

        private static readonly string _QueryWithoutPagination = "SELECT TOP (@WantedNumOfRecords) " + _FixedQueryPart;

        private static readonly string _QueryConditionPart = "TestTypeID = @TestTypeID AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID";

        private static readonly string _QueryForOffsetPagination = "SELECT " + _FixedQueryPart;

        private static readonly string _OffsetPaginationQueryPart = clsGeneralUtility.GetOffsetPaginationQueryPart();

        private enum _enUpdatableColumns : byte { AppointmentDate, IsLocked }

        public class clsOldAppointmentData
        {
            public DateTime? AppointmentDate;
            public bool? IsLocked;

            public clsOldAppointmentData(DateTime AppointmentDate, bool IsLocked)
            {
                this.AppointmentDate = AppointmentDate;
                this.IsLocked = IsLocked;
            }
        }

        public static DataTable GetTestAppointments(byte WantedNumOfRecords , byte TestTypeID,int LocalDrivingLicenseAppID, int _LastBroughtAppointmentID = -1
            , string ColumnNameToOrderBy = null, int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtTestAppointments = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(_LastBroughtAppointmentID, ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
            }

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);

            if(_LastBroughtAppointmentID != -1)
            command.Parameters.AddWithValue("@LastBroughtAppointmentID", _LastBroughtAppointmentID);

            if (NumberOfRowsToOffset != -1)
            command.Parameters.AddWithValue("@NumberOfRowsToOffset", NumberOfRowsToOffset);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtTestAppointments = new DataTable();
                    dtTestAppointments.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtTestAppointments;
    }

        private static string _GetQueryForCursorPagination(int _LastBroughtAppointmentID,string ColumnNameToOrderBy, string SortDirection)
        {
            string query;
            if (_LastBroughtAppointmentID != -1)
            {
                query = _QueryWithoutPagination;
                query += $@" WHERE {_PrimaryKeyColumnName} < @LowestBroughtAppointmentID AND {_QueryConditionPart}";
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _QueryWithoutPagination;
                query += $" WHERE {_QueryConditionPart}";
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
                query += $" WHERE {_QueryConditionPart}";
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _QueryForOffsetPagination;
                query += $" WHERE {_QueryConditionPart}";
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
                query += _OffsetPaginationQueryPart;
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

                DataTable dtTestAppointments = new DataTable();
                dtTestAppointments.Load(reader);
                reader.Close();

                return dtTestAppointments;
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return null;
        }

        public static int AddNewAppointment(int TestTypeID , int LocalDrivingLicenseAppID, DateTime AppointmentDate , decimal PaidFees , int CreatedByUserID , bool IsLocked,int RetakeTestAppID = -1)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments VALUES (@TestTypeID,@LocalDrivingLicenseAppID,
                             @AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked,@RetakeTestAppID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if(RetakeTestAppID == -1)
                command.Parameters.AddWithValue("@RetakeTestAppID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestAppID", RetakeTestAppID);

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

        private static void _ResetChangedOldValues(DateTime AppointmentDate, bool IsLocked, clsOldAppointmentData OldAppointmentData)
        {
            if (AppointmentDate != OldAppointmentData.AppointmentDate)
                OldAppointmentData.AppointmentDate = null;

            if (IsLocked != OldAppointmentData.IsLocked)
                OldAppointmentData.IsLocked = null;
        }

        private static string _GetColumnValueSetPartForUpdate(_enUpdatableColumns UpdatableColumn)
        {
            switch (UpdatableColumn)
            {
                case _enUpdatableColumns.AppointmentDate:
                    return " AppointmentDate = @AppointmentDate";

                case _enUpdatableColumns.IsLocked:
                    return " IsLocked = @IsLocked";
            }

            return null;
        }

        private static string _GetUpdateQuery(DateTime AppointmentDate, bool IsLocked, clsOldAppointmentData OldAppointmentData, bool HasOldDataChangedFully)
        {
            string query = "UPDATE TestAppointments SET";

            if (!HasOldDataChangedFully)
            {
                if (AppointmentDate != OldAppointmentData.AppointmentDate)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.AppointmentDate);
                }

                else if (IsLocked != OldAppointmentData.IsLocked)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.IsLocked);
                }

            }

            else
                query += @" AppointmentDate = @AppointmentDate, IsLocked = @IsLocked";

            query += $" WHERE {_PrimaryKeyColumnName} = @TestAppointmentID";

            _ResetChangedOldValues(AppointmentDate, IsLocked, OldAppointmentData);

            return query;
        }

        public static bool UpdateAppointment(int TestAppointmentID,DateTime AppointmentDate,bool IsLocked, clsOldAppointmentData OldAppointmentData, bool HasOldDataChangedFully)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetUpdateQuery(AppointmentDate, IsLocked, OldAppointmentData, HasOldDataChangedFully);


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            if(OldAppointmentData.AppointmentDate == null)
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

            if(OldAppointmentData.IsLocked == null)
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

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

        public static bool Find(int TestAppointmentID,ref byte TestTypeID,ref int LocalDrivingLicenseAppID, ref DateTime AppointmentDate,ref decimal PaidFees,ref int CreatedByUserID,ref bool IsLocked,ref int RetakeTestAppID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $"SELECT * FROM TestAppointments WHERE {_PrimaryKeyColumnName} = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    TestTypeID = Convert.ToByte(reader["TestTypeID"]);
                    LocalDrivingLicenseAppID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if(reader["RetakeTestApplicationID"]!=DBNull.Value)
                    RetakeTestAppID = (int)reader["RetakeTestApplicationID"];

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

        public static int GetTotalAppointmentsCount(int LocalDrivingLicenseAppID, byte TestTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT Count({_PrimaryKeyColumnName}) FROM TestAppointments
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID AND TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToUInt16(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return -1;
        }

        public static bool IsAppointmentSchedulingAvailable(int LocalDrivingLicenseAppID, byte TestTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"SELECT Found = 1 FROM TestAppointments
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID AND TestTypeID = @TestTypeID AND IsLocked = 0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy, string SortDirection, int LDLApplicationID, byte TestTypeID)
        {
            string query = _QueryWithoutPagination;

            query += clsGeneralUtility.GetFilterQueryPart_ValueCondition("TestTypeID");
            query += $" AND LocalDrivingLicenseApplicationID = {LDLApplicationID}";
            query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);

            return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection, int LDLApplicationID, byte TestTypeID)
        {
            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection,LDLApplicationID,TestTypeID),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection,TestTypeID.ToString());
        }
    }
}
