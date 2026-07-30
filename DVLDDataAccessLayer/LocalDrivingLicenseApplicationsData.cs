using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace DVLDDataAccessLayer
{
    public class clsLocalDrivingLicenseApplicationsData
    {
        public static DataTable GetLocalDrivingLicenseApplications()
        {
            DataTable dtLDLApplications = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS [L.D.LAppID] , LicenseClasses.ClassName AS [Driving Class] ,
                             People.NationalNo As [National No.] ,(CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName +' '+ People.SecondName +' '+ People.ThirdName + ' ' + People.LastName
                             ELSE People.FirstName +' '+ People.SecondName +' '+ People.LastName END) AS [Full Name] , ApplicationDate AS [Application Date] ,
                             COUNT(Tests.TestResult) AS [Passed Tests] 
                             FROM LocalDrivingLicenseApplications INNER JOIN LicenseClasses
                             ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID 
                             INNER JOIN Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                             INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID
                             INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                             INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                             GROUP BY LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID , LicenseClasses.ClassName,
                             People.NationalNo,(CASE WHEN People.ThirdName IS NOT NULL THEN People.FirstName +' '+ People.SecondName +' '+ People.ThirdName + ' ' + People.LastName
                             ELSE People.FirstName +' '+ People.SecondName +' '+ People.LastName END), ApplicationDate,
                             Tests.TestResult";

            return dtLDLApplications;
        }
        public static int AddLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
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

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID ,int ApplicationID,int LicenseClassID)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"UPDATE LocalDrivingLicenseApplications SET
                            (ApplicationID = @ApplicationID , LicenseClassID = @LicenseClassID)
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
    }
}
