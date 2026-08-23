using System;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsDriversData
    {
        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreationDate)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Drivers VALUES (@PersonID,@CreatedByUserID,@CreationDate);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreationDate", CreationDate);

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
        public static bool IsPersonAlreadyADriver(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
        public static int GetDriverID(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT DriverID FROM Drivers WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
