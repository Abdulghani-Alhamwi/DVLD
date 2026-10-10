using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsLocalDrivingLicenseAppsData
    {
        private static readonly string _PrimaryKeyColumnName = "LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID";

        private static readonly string _PrimaryKeyViewedColumnName = "L.D.L.AppID";

        private static readonly string _FixedQueryPart =
            $@"{_PrimaryKeyColumnName} AS [L.D.L.AppID] , LicenseClasses.ClassName AS [Driving Class] ,
           People.NationalNo As [National No.] ,(CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName +' '+ People.SecondName +' '+ People.ThirdName + ' ' + People.LastName
           ELSE People.FirstName +' '+ People.SecondName +' '+ People.LastName END) AS [Full Name] , FORMAT(ApplicationDate , '{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Application Date] ,
           (CASE WHEN SUM(CAST(Tests.TestResult AS tinyINT)) IS NOT NULL THEN SUM(CAST(Tests.TestResult AS tinyINT)) ELSE 0 END) AS [Passed Tests] ,
           (CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' ELSE 'Completed' END) AS Status
           FROM LocalDrivingLicenseApplications INNER JOIN LicenseClasses
           ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 
           INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
           INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
           LEFT JOIN TestAppointments ON {_PrimaryKeyColumnName} = TestAppointments.LocalDrivingLicenseApplicationID 
           LEFT JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID";

        private static readonly string _QueryWithoutPagination = "SELECT TOP (@WantedNumOfRecords) " + _FixedQueryPart;

        private static readonly string _QueryForOffsetPagination = "SELECT " + _FixedQueryPart;

        private static readonly string _GroupByQueryPart=
            $@" GROUP BY {_PrimaryKeyColumnName}, LicenseClasses.ClassName, People.NationalNo,
                (CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName ELSE People.FirstName + ' ' + People.SecondName + ' ' + People.LastName END),
                FORMAT(ApplicationDate , '{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}'),
                (CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' ELSE 'Completed' END)";

        private static readonly string _OffsetPaginationQueryPart = clsGeneralUtility.GetOffsetPaginationQueryPart();

        public static DataTable GetLDLApplications(byte WantedNumOfRecords, int LastBroughtLDLAppID = -1, string ColumnNameToOrderBy = null
             , int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtLDLApplications = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(LastBroughtLDLAppID, ColumnNameToOrderBy, SortDirection);
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
                    dtLDLApplications = new DataTable();
                    dtLDLApplications.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtLDLApplications;
        }

        private static string _GetQueryForCursorPagination(int LastBroughtLDLAppID, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;
            if (LastBroughtLDLAppID != -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, false, LastBroughtLDLAppID, true, false);
                query += _GroupByQueryPart;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
            }

            else
            {
                query = _QueryWithoutPagination;
                query += _GroupByQueryPart;
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
                query += _GroupByQueryPart;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
            }

            else
            {
                query = _QueryForOffsetPagination;
                query += _GroupByQueryPart;
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

        public static int AddLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int LDLApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"INSERT INTO LocalDrivingLicenseApplications VALUES
                             (@ApplicationID,@LicenseClassID); SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    LDLApplicationID = Convert.ToInt32(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return LDLApplicationID;
        }

        private static void _ResetChangedOldValues(int LicenseClassID,ref int _OldLicenseClassID)
        {
            if (LicenseClassID != _OldLicenseClassID)
                _OldLicenseClassID = -1;
        }

        private static string _GetUpdateQuery(int LicenseClassID,ref int OldLicenseClassID)
        {
            string query = "UPDATE LocalDrivingLicenseApplications SET";

            if (LicenseClassID != OldLicenseClassID)
            {
                query += " LicenseClassID = @LicenseClassID";
            }

            query += $" WHERE {_PrimaryKeyColumnName} = @LocalDrivingLicenseApplicationID";

            _ResetChangedOldValues(LicenseClassID,ref OldLicenseClassID);

            return query;
        }

        public static bool UpdateLDLApplication(int LocalDrivingLicenseApplicationID,int LicenseClassID,int OldLicenseClassID)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = _GetUpdateQuery(LicenseClassID,ref OldLicenseClassID);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            if (OldLicenseClassID == -1)
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool Find(int LDLApplicationID, ref int ApplicationID, ref byte LicenseClassID , ref string LicenseClassName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $@"SELECT LocalDrivingLicenseApplications.* , LicenseClasses.ClassName FROM LocalDrivingLicenseApplications
                             INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
                             WHERE {_PrimaryKeyColumnName} = @LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = Convert.ToByte(reader["LicenseClassID"]);
                    LicenseClassName = (string)reader["ClassName"];

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

        public static bool DeleteLDLApplication(int LDLApplicationID)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"DELETE FROM LocalDrivingLicenseApplications
                             WHERE {_PrimaryKeyColumnName} = @LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

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

        public static int GetApplicationID(int LDLApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT ApplicationID FROM LocalDrivingLicenseApplications
                             WHERE {_PrimaryKeyColumnName} = @LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return -1;
        }

        public static bool HasPersonApplied(int ApplicantPersonID, byte LicenseClassID, ref byte ApplicationStatus)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TOP (1) Applications.ApplicationStatus FROM Applications INNER JOIN LocalDrivingLicenseApplications
                             ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                             WHERE Applications.ApplicantPersonID = @ApplicantPersonID AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                             ORDER BY Applications.ApplicationID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    ApplicationStatus = Convert.ToByte(result);
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

        public static bool IsPersonAgeAppropriate(int PersonID , byte LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Valid = 1 WHERE (SELECT DATEDIFF(Year,DateOfBirth,GetDate()) FROM People WHERE PersonID = @PersonID)
                             >= (SELECT MinimumAllowedAge FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static int GetLDLApplicationID(int ApplicantPersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = $@"SELECT {_PrimaryKeyColumnName} FROM LocalDrivingLicenseApplications INNER JOIN Applications
                             ON Applications.ApplicantPersonID = @ApplicantPersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

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

        public static int GetTotalLDLApplicationsCount()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT Count({_PrimaryKeyColumnName}) FROM LocalDrivingLicenseApplications";

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
            switch(SendedColumnName)
            {
                case "L.D.L.AppID":
                    return _PrimaryKeyColumnName;

                case "Full Name":
                    return "CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName +' '+ People.SecondName +' '+ People.ThirdName + ' ' + People.LastName\r\n                             ELSE People.FirstName +' '+ People.SecondName +' '+ People.LastName END";

                case "National No.":
                    return "People.NationalNo";

                case "Status":
                    return "Applications.ApplicationStatus";

                default:
                    return null;
            }
        }

        private static string _GetStatusNumericValue(string FitlerValue)
        {
                switch (FitlerValue)
                {
                case "New":
                    return "1";

                case "Canceled":
                    return "2";

                case "Completed":
                    return "3";

                default:
                    return null;
                }
        }

        private static string _GetDataFilteringQuery(byte WantedNumOfRecords, string ColumnNameToFilterBy, ref string ValueToFilterBy,
             string ColumnNameToOrderBy, string SortDirection, int LastBroughtLDLAppID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            string query;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Applications.ApplicationStatus" && ValueToFilterBy == "All"))
            {
                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _GetQueryForCursorPagination(LastBroughtLDLAppID, ColumnNameToOrderBy, SortDirection);
                }

                else
                {
                    query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
                }
            }
            else
            {

                if (ColumnNameToFilterBy == "Applications.ApplicationStatus")
                {
                    if (ValueToFilterBy != "All")
                        ValueToFilterBy = _GetStatusNumericValue(ValueToFilterBy);
                }

                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _QueryWithoutPagination;
                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                    query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtLDLAppID, true, false);
                    query += _GroupByQueryPart;
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                }

                else
                {
                    query = _QueryForOffsetPagination;
                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                    query += _GroupByQueryPart;
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
                    query += _OffsetPaginationQueryPart;
                }
            }

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilterBy, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection,
               int LastBroughtLDLAppID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

             
            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilterBy, ref ValueToFilterBy,
                            ColumnNameToOrderBy, SortDirection, LastBroughtLDLAppID,NumberOfRowsToOffset, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (!string.IsNullOrEmpty(ValueToFilterBy))
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

        public static sbyte GetPassedTests(int LDLApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT (CASE WHEN SUM(CAST(Tests.TestResult AS INT)) IS NOT NULL THEN SUM(CAST(Tests.TestResult AS INT)) ELSE 0 END) AS [Passed Tests]
                             FROM LocalDrivingLicenseApplications
                             INNER JOIN TestAppointments ON {_PrimaryKeyColumnName} = TestAppointments.LocalDrivingLicenseApplicationID 
                             INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                             WHERE {_PrimaryKeyColumnName} = @LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToSByte(result);
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

            ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Applications.ApplicationStatus" && ValueToFilterBy == "All"))
            {
                query += _GroupByQueryPart;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
            }

            else
            {
                if (ColumnNameToFilterBy == "Applications.ApplicationStatus")
                {
                    if(ValueToFilterBy != "All")
                    ValueToFilterBy = _GetStatusNumericValue(ValueToFilterBy);
                }

                query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                query += _GroupByQueryPart;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection, true);
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