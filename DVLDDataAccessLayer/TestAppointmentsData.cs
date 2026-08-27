using System;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsTestAppointmentsData
    {

        private static string Query = @"SELECT TOP (@WantedNumberOfRecords) TestAppointmentID AS [Appointment ID] , FORMAT(AppointmentDate,'dd/MM/yyyy h:mm tt') AS [Appointment Date] ,
                             PaidFees AS [Paid Fees] , IsLocked  AS [Is Locked] FROM TestAppointments";

        public static DataTable GetTestAppointments(byte WantedNumberOfRecords , byte TestTypeID,int LocalDrivingLicenseAppID, int LowestBroughtAppointmentID = -1,string DateFormat = null)
    {
            DataTable dtTestAppointments = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = Query;

            if (LowestBroughtAppointmentID != -1)
                query += @" WHERE TestAppointmentID < @LowestBroughtAppointmentID AND TestTypeID = @TestTypeID AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID
                            ORDER BY TestAppointmentID DESC";
            else
                query += @" WHERE TestTypeID = @TestTypeID AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID
                            ORDER BY TestAppointmentID DESC";


                SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords",WantedNumberOfRecords);
            
            if(LowestBroughtAppointmentID != -1)
                command.Parameters.AddWithValue("@LowestBroughtAppointmentID", LowestBroughtAppointmentID);

                command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);

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
        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", 0);

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

        public static int AddNewAppointment(int TestTypeID , int LocalDrivingLicenseAppID, DateTime AppointmentDate , decimal PaidFees , int CreatedByUserID , bool IsLocked)
        {
            int AppointmentID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments VALUES (@TestTypeID,@LocalDrivingLicenseAppID,
                             @AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    AppointmentID = Convert.ToInt32(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return AppointmentID;
        }
        public static bool UpdateAppointment(int TestAppointmentID,int TestTypeID, int LocalDrivingLicenseAppID, DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE TestAppointments SET TestTypeID = @TestTypeID,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseAppID,
                             AppointmentDate = @AppointmentDate,PaidFees = @PaidFees,CreatedByUserID = @CreatedByUserID,
                             IsLocked = @IsLocked WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseAppID", LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

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

        public static bool  Find(int TestAppointmentID,ref byte TestTypeID,ref int LocalDrivingLicenseAppID, ref DateTime AppointmentDate,ref decimal PaidFees,ref int CreatedByUserID,ref bool IsLocked)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

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

        public static ushort GetTotalAppointmentsCount(int LocalDrivingLicenseAppID, byte TestTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Count(TestAppointmentID) FROM TestAppointments
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

            return 0;
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
            
    }
}
