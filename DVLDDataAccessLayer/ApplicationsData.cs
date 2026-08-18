using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using static System.Net.Mime.MediaTypeNames;

namespace DVLDDataAccessLayer
{
    public class clsApplicationsData
    {
        public static int AddApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Applications VALUES
                     (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                      @ApplicationStatus,@LastStatusDate,@PaidApplicationFees,@CreatedByUserID);
                       SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidApplicationFees", PaidApplicationFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    ApplicationID = Convert.ToInt32(result);
            }

            catch { }
            
            finally
            {
                connection.Close();
            }
            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE Applications SET
                             ApplicantPersonID = @ApplicantPersonID,ApplicationDate = @ApplicationDate,ApplicationTypeID = @ApplicationTypeID,
                             ApplicationStatus = @ApplicationStatus,LastStatusDate = @LastStatusDate,PaidFees = @PaidApplicationFees,CreatedByUserID = @CreatedByUserID
                             WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidApplicationFees", PaidApplicationFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool Find(int ApplicationID , ref int ApplicantPersonID,ref DateTime ApplicationDate, ref int ApplicationTypeID,ref byte ApplicationStatus ,ref DateTime LastStatusDate,ref decimal PaidApplicationFees,ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM Applications
                             WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidApplicationFees = (Decimal)(reader["PaidFees"]);
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

        public static bool DeleteApplication(int ApplicationID)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"DELETE FROM Applications
                             WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool ChangeApplicationStatus(int ApplicationID,byte TheNewStatus)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE Applications SET ApplicationStatus = @TheNewStatus
                             WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@TheNewStatus", TheNewStatus);

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
    }
}
