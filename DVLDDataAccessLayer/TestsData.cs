using System;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsTestsData
    {
        public static int AddNewTest(int TestAppointmentID,bool TestResult,string Notes,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Tests VALUES (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if(Notes != null)
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
        public static bool HasPassedTheTest(int LDLApplicationID, int TestTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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

        public static sbyte GetTotalPassedTestsCount(int LDLApplicationID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT (CASE WHEN SUM(CAST(Tests.TestResult AS INT)) IS NOT NULL THEN SUM(CAST(Tests.TestResult AS INT)) ELSE 0 END) AS[Passed Tests]
                             FROM LocalDrivingLicenseApplications INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                             INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                             Where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLApplicationID";

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