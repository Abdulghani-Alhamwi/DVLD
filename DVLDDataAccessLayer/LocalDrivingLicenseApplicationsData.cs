using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace DVLDDataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationsData
    {
        public static DataTable GetLDLApplications()
        {
            DataTable dtLDLApplications = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS [L.D.L.AppID] , LicenseClasses.ClassName AS [Driving Class] ,
                             People.NationalNo As [National No.] ,(CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName +' '+ People.SecondName +' '+ People.ThirdName + ' ' + People.LastName
                             ELSE People.FirstName +' '+ People.SecondName +' '+ People.LastName END) AS [Full Name] , ApplicationDate AS [Application Date] ,
                             (CASE WHEN SUM(CAST(Tests.TestResult AS INT)) IS NOT NULL THEN SUM(CAST(Tests.TestResult AS INT)) ELSE 0 END) AS [Passed Tests] ,
                             (CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' ELSE 'Completed' END) AS Status
                             FROM LocalDrivingLicenseApplications INNER JOIN LicenseClasses
                             ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 
                             INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                             INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
                             LEFT JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                             LEFT JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                             GROUP BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID, LicenseClasses.ClassName,
                             People.NationalNo,(CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName +' '+ People.SecondName +' '+ People.ThirdName + ' ' + People.LastName
                             ELSE People.FirstName +' '+ People.SecondName +' '+ People.LastName END), ApplicationDate , (CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' ELSE 'Completed' END) ";

            SqlCommand command = new SqlCommand(query, connection);
            
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

        public static bool UpdateLDLApplication(int LocalDrivingLicenseApplicationID ,int ApplicationID,int LicenseClassID)
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
                AffectedRows  = command.ExecuteNonQuery();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return (AffectedRows > 0);
        }

        public static bool Find(int LocalDrivingLicenseApplicationID,ref int ApplicationID,ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"SELECT * FROM LocalDrivingLicenseApplications
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

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

            SqlCommand command = new SqlCommand(query,connection);
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
        public static bool CanPersonApply(int ApplicantPersonID,int LicenseClassID , out byte ApplicationStatus)
        {
            ApplicationStatus = 2;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Applications.ApplicationStatus FROM LocalDrivingLicenseApplications INNER JOIN Applications
                             ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                             WHERE Applications.ApplicantPersonID = @ApplicantPersonID AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                             AND Applications.ApplicationStatus IN (1,3)";

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
    }
}
