using System;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsTestData
    {
        public static int AddNewTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Tests VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID)
                             UPDATE TestAppointments SET IsLocked = 1 WHERE TestAppointmentID = @TestAppointmentID
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if(!string.IsNullOrEmpty(Notes))
            command.Parameters.AddWithValue("@Notes", Notes);
            else
            command.Parameters.AddWithValue("@Notes", DBNull.Value);
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

        public static bool HasPassedTheTest(int LDLApplicationID, byte TestTypeID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT PassedTest = 1 FROM LocalDrivingLicenseApplications INNER JOIN TestAppointments
                             ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                             INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                             WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLApplicationID AND TestTypeID = @TestTypeID AND TestResult = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
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

        public static byte GetTotalPassedTests(int LocalDrivingLicenseApplicationID)
        {
            byte TotalPassedTests = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT PassedTestsCount = COUNT(TestID) FROM Tests INNER JOIN TestAppointments
                              ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                              WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LDLAppID AND TestResult = 1";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    TotalPassedTests = Convert.ToByte(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return TotalPassedTests;
        }

        public static short GetTotalTrialsPerTestType(int LocalDrivingLicenseApplicationID, byte TestTypeID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT TotalTrials = COUNT(TestID) FROM LocalDrivingLicenseApplications INNER JOIN
                              TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivinglicenseApplicationID
                              INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                              WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static bool Find(int TestID, ref int TestAppointmentID, ref bool TestResult,
               ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT * FROM Tests WHERE TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        public static bool GetLastTestByTestType(int LocalDrivingLicenseApplicationID,byte TestTypeID,ref int TestID, ref int TestAppointmentID, ref bool TestResult,
               ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            
            string query = $@"SELECT TOP (1) Tests.* FROM Tests INNER JOIN TestAppointments
                              ON Test.TestAppointmentID = TestAppointments.TestAppointmentID
                              WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID AND TestAppointments.TestTypeID = @TestTypeID
                              ORDER BY TestID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID,byte LicenseClassID, byte TestTypeID
            , ref int TestID, ref int TestAppointmentID, ref bool TestResult,ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT TOP (1) Tests.* FROM LocalDrivingLicenseApplications INNER JOIN TestAppointments ON
                              LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.TestAppointmentID
                              INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID INNER JOIN Applications
                              ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID 
                              WHERE (Application.ApplicantPersonID = @PersonID) AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                              AND (TestAppointments.TestTypeID = @TestTypeID) ORDER BY Tests.TestAppointmentsID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (string)reader["Notes"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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
    }
}