using System;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsApplicationsData
    {
        private static string _PrimaryKeyColumnName = "ApplicationID";

        private enum _enUpdatableColumns : byte { ApplicantPersonID, ApplicationStatus, LastStatusDate }

        public class clsOldApplicationData
        {
            public int ApplicantPersonID;
            public short ApplicationStatus;

            public clsOldApplicationData(int ApplicantPersonID, short ApplicationStatus)
            {
                this.ApplicantPersonID = ApplicantPersonID;
                this.ApplicationStatus = ApplicationStatus;
        }
        }

        public static int AddApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, decimal PaidApplicationFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Applications VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID,
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

        private static void _ResetChangedOldValues(int ApplicantPersonID, short ApplicationStatus, DateTime LastStatusDate,clsOldApplicationData oldApplicationData)
        {
            if (ApplicantPersonID != oldApplicationData.ApplicantPersonID)
                oldApplicationData.ApplicantPersonID = -1;

            if (ApplicationStatus != oldApplicationData.ApplicationStatus)
            {
                oldApplicationData.ApplicationStatus = -1;
            }
        }

        private static string _GetColumnValueSetPartForUpdate(_enUpdatableColumns UpdatableColumn)
        {
            switch (UpdatableColumn)
            {
                case _enUpdatableColumns.ApplicantPersonID:
                    return " ApplicantPersonID = @ApplicantPersonID";

                case _enUpdatableColumns.ApplicationStatus:
                    return " ApplicationStatus = @Status";

                case _enUpdatableColumns.LastStatusDate:
                    return ",LastStatusDate = @LastStatusDate";
            }

            return null;
        }

        private static string _GetUpdateQuery(int ApplicantPersonID, short ApplicationStatus, DateTime LastStatusDate, clsOldApplicationData OldApplicationData)
        {
            string query = "UPDATE Applications SET";

                if (ApplicantPersonID != OldApplicationData.ApplicantPersonID)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ApplicantPersonID);
                }

                else
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ApplicationStatus)
                           + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.LastStatusDate);
                }

            query += $" WHERE {_PrimaryKeyColumnName} = @ApplicationID";

            _ResetChangedOldValues(ApplicantPersonID, ApplicationStatus, LastStatusDate, OldApplicationData);

            return query;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, short ApplicationStatus, DateTime LastStatusDate, clsOldApplicationData OldApplicationData)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetUpdateQuery(ApplicantPersonID,ApplicationStatus,LastStatusDate,OldApplicationData);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID",ApplicationID);

            if(OldApplicationData.ApplicantPersonID == -1)
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);

            if (OldApplicationData.ApplicationStatus == -1)
            {
                command.Parameters.AddWithValue("@Status", ApplicationStatus);
                command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            }
            
            try
            {
                connection.Open();
                AffectedRows = Convert.ToByte(command.ExecuteNonQuery());
            }

            catch (Exception ex){ Console.Write(ex.Message); }

            finally
            {
                connection.Close();
            }
            return (AffectedRows > 0);
        }

        public static bool Find(int ApplicationID , ref int ApplicantPersonID,ref DateTime ApplicationDate, ref byte ApplicationTypeID,ref byte ApplicationStatus ,ref DateTime LastStatusDate,ref decimal PaidApplicationFees,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT * FROM Applications WHERE {_PrimaryKeyColumnName} = @ApplicationID";

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
                    ApplicationTypeID = Convert.ToByte(reader["ApplicationTypeID"]);
                    ApplicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
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
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"DELETE FROM Applications WHERE {_PrimaryKeyColumnName} = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool ChangeApplicationStatus(int ApplicationID,byte TheNewStatus)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"UPDATE Applications SET ApplicationStatus = @TheNewStatus
                             WHERE {_PrimaryKeyColumnName} = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@TheNewStatus", TheNewStatus);

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
    }
}