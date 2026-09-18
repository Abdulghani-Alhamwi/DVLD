using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsLocalDrivingLicenseAppData
    {
        private static string _PrimaryKeyColumnName = "LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID";

        private static string _query =
         $@"SELECT TOP (@WantedNumOfRecords) {_PrimaryKeyColumnName} AS [L.D.L.AppID] , LicenseClasses.ClassName AS [Driving Class] ,
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

            private static string _GroupByQueryPart=
            $@" GROUP BY {_PrimaryKeyColumnName}, LicenseClasses.ClassName, People.NationalNo,
                (CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName ELSE People.FirstName + ' ' + People.SecondName + ' ' + People.LastName END),
                FORMAT(ApplicationDate , '{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.DateTimeCustomFormat)}'),
                (CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' ELSE 'Completed' END)";

        public static DataTable GetLDLApplications(byte WantedNumOfRecords, int LastLowestBroughtLDLAppID = -1)
        {
            DataTable dtLDLApplications = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _query + _GroupByQueryPart;

            if (LastLowestBroughtLDLAppID == -1)
                query += $" ORDER BY {_PrimaryKeyColumnName} DESC";
            else
                query += $@" HAVING {_PrimaryKeyColumnName} < {LastLowestBroughtLDLAppID}
                           ORDER BY {_PrimaryKeyColumnName} DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

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

        public static int AddLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int LDLApplicationID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
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

        public static bool UpdateLDLApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $@"UPDATE LocalDrivingLicenseApplications SET
                             ApplicationID = @ApplicationID , LicenseClassID = @LicenseClassID
                             WHERE {_PrimaryKeyColumnName} = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
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
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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
             string ColumnNameToOrderBy, string SortDirection, int LastLowestbroughtLDLAppID = -1, char? WildChar = null)
        {
            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyColumnName;

            ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            string query = _query;

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Applications.ApplicationStatus" && ValueToFilterBy == "All"))
            {
                query += _GroupByQueryPart;
                query += clsGeneralUtility.GetLastFilterQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, false, LastLowestbroughtLDLAppID);
            }
            else
            {

                if (ColumnNameToFilterBy == "Applications.ApplicationStatus")
                {
                    if (ValueToFilterBy != "All")
                        ValueToFilterBy = _GetStatusNumericValue(ValueToFilterBy);
                }

                query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                query += _GroupByQueryPart;
                query += clsGeneralUtility.GetLastFilterQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, true, LastLowestbroughtLDLAppID);
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

        public static sbyte GetPassedTests(int LDLApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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
            string query = _query;

            ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            if (string.IsNullOrEmpty(ValueToFilterBy)
                || (ColumnNameToFilterBy == "Applications.ApplicationStatus" && ValueToFilterBy == "All"))
            {
                query += _GroupByQueryPart;
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
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
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }
                return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }
    }
}