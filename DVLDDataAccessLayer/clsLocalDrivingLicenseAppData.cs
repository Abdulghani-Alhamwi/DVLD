using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsLocalDrivingLicenseAppData
    {
        private static string _query =
         $@"SELECT TOP (@WantedNumOfRecords) LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS [L.D.L.AppID] , LicenseClasses.ClassName AS [Driving Class] ,
           People.NationalNo As [National No.] ,(CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName +' '+ People.SecondName +' '+ People.ThirdName + ' ' + People.LastName
           ELSE People.FirstName +' '+ People.SecondName +' '+ People.LastName END) AS [Full Name] , FORMAT(ApplicationDate , '{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Application Date] ,
           (CASE WHEN SUM(CAST(Tests.TestResult AS tinyINT)) IS NOT NULL THEN SUM(CAST(Tests.TestResult AS tinyINT)) ELSE 0 END) AS [Passed Tests] ,
           (CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' ELSE 'Completed' END) AS Status
           FROM LocalDrivingLicenseApplications INNER JOIN LicenseClasses
           ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 
           INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
           INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
           LEFT JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
           LEFT JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID";

            private static string _groupByQueryPart=
            $@" GROUP BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, LicenseClasses.ClassName,
                People.NationalNo,(CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName + ' ' + People.SecondName + ' ' + People.ThirdName + ' ' + People.LastName
                ELSE People.FirstName + ' ' + People.SecondName + ' ' + People.LastName END), FORMAT(ApplicationDate , '{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') , (CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' ELSE 'Completed' END)";

        public static DataTable GetLDLApplications(byte WantedNumOfRecords, int LastLowestBroughtLDLAppID = -1)
        {
            DataTable dtLDLApplications = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _query + _groupByQueryPart;

            if (LastLowestBroughtLDLAppID == -1)
                query += " ORDER BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID DESC";
            else
                query += @" HAVING LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID < @LastLowestBroughtLDLAppID 
                           ORDER BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (LastLowestBroughtLDLAppID != -1)
                command.Parameters.AddWithValue("@LastLowestBroughtLDLAppID", LastLowestBroughtLDLAppID);

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

            string query = _query;

            SqlCommand command = new SqlCommand(query, connection);
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

            catch (Exception ex) { Console.Write(ex.Message); }

            finally
            {
                connection.Close();
            }

            return null;
        }
        public static int AddLDLApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"INSERT INTO LocalDrivingLicenseApplications VALUES
                            (@ApplicationID,@LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;
        }

        public static bool UpdateLDLApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"UPDATE LocalDrivingLicenseApplications SET
                            ApplicationID = @ApplicationID , LicenseClassID = @LicenseClassID
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return (AffectedRows > 0);
        }

        public static bool Find(int LDLApplicationID, ref byte ApplicationID, ref byte LicenseClassID , ref string LicenseClassName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"SELECT LocalDrivingLicenseApplications.* , LicenseClasses.ClassName FROM LocalDrivingLicenseApplications
                             INNER JOIN LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDLApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    ApplicationID = Convert.ToByte(reader["ApplicationID"]);
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
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"DELETE FROM LocalDrivingLicenseApplications
                             WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);

            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
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

            string query = @"SELECT ApplicationID FROM LocalDrivingLicenseApplications
                             WHERE LocalDrivingLicenseApplicationID = @LDLApplicationID";

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
        public static bool HasPersonApplied(int ApplicantPersonID, byte LicenseClassID, out byte ApplicationStatus)
        {
            ApplicationStatus = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Applications.ApplicationStatus FROM Applications INNER JOIN LocalDrivingLicenseApplications
                             ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                             WHERE Applications.ApplicantPersonID = @ApplicantPersonID AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID";

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
                    return false;
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return true;
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
            string query = @"SELECT LocalDrivingLicenseApplicationID FROM LocalDrivingLicenseApplications INNER JOIN Applications
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

            string query = @"SELECT Count(LocalDrivingLicenseApplicationID) FROM LocalDrivingLicenseApplications";

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
                    return "LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID";

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

        private static string _GetDataFilteringQuery(byte WantedNumOfRecords,ref string ColumnNameToFilter,ref string ValueToFilterBy, char? WildChar = null, int LastLowestBroughtLDLAppID = -1)
        {
            string query = _query;

            if (!string.IsNullOrEmpty(ValueToFilterBy))
            {
                if (ColumnNameToFilter != "None")
                {
                    ColumnNameToFilter = _GetOriginalColumnName(ColumnNameToFilter);

                    if (ColumnNameToFilter == "Applications.ApplicationStatus")
                    {
                        if (ValueToFilterBy != "All")
                        {
                            switch (ValueToFilterBy)
                            {
                                case "New":
                                    ValueToFilterBy = "1";
                                    break;

                                case "Canceled":
                                    ValueToFilterBy = "2";
                                    break;

                                case "Completed":
                                    ValueToFilterBy = "3";
                                    break;
                            }
                        }
                     }

                    if (ValueToFilterBy != "All")
                    {
                        if (WildChar == null)
                            query += $" WHERE {ColumnNameToFilter} = @Value";
                        else
                            query += $" WHERE {ColumnNameToFilter} LIKE @Value + @WildChar";
                    }
                }
            }

            if (LastLowestBroughtLDLAppID == -1)
            {
                query += _groupByQueryPart + " ORDER BY [L.D.L.AppID] DESC";
            }

            else
            {
                if (string.IsNullOrEmpty(ValueToFilterBy) || ColumnNameToFilter == "None" || ValueToFilterBy == "All")
                    query += " WHERE";
                else
                    query += " AND";

                query += " LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID < @LastLowestBroughtLDLAppID" + _groupByQueryPart
                            + " ORDER BY [L.D.L.AppID] DESC";
            }

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilter, string ValueToFilterBy, int LastLowestBroughtLDLAppID = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords,ref ColumnNameToFilter,ref ValueToFilterBy, WildChar,LastLowestBroughtLDLAppID);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

             if(ColumnNameToFilter == "Applications.ApplicationStatus" && ValueToFilterBy != "All")
                command.Parameters.AddWithValue("@Value", Convert.ToByte(ValueToFilterBy));

            else if (!(string.IsNullOrEmpty(ValueToFilterBy) || ColumnNameToFilter == "None" || ValueToFilterBy == "All"))
                command.Parameters.AddWithValue("@Value", ValueToFilterBy);


            if (WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

            if(LastLowestBroughtLDLAppID != -1)
                command.Parameters.AddWithValue("@LastLowestBroughtLDLAppID", LastLowestBroughtLDLAppID);

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

            string query = @"SELECT (CASE WHEN SUM(CAST(Tests.TestResult AS INT)) IS NOT NULL THEN SUM(CAST(Tests.TestResult AS INT)) ELSE 0 END) AS [Passed Tests]
                             FROM LocalDrivingLicenseApplications
                             INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                             INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                             WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLApplicationID";

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
    }
}