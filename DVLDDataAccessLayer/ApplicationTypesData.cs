using System;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsApplicationTypesData
    {
        private static string _PrimaryKeyColumnName = "ApplicationTypeID";

        public static DataTable GetApplicationTypes()
        {
            DataTable dtApplicationTypes = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT {_PrimaryKeyColumnName} AS ID, ApplicationTypeTitle AS Title,
                              ApplicationFees AS Fees FROM ApplicationTypes";

            SqlCommand command = new SqlCommand(query,connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtApplicationTypes = new DataTable();
                    dtApplicationTypes.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtApplicationTypes;
        }

        public static bool UpdateApplicationType(byte ApplicationTypeID , string ApplicationTypeTitle,decimal ApplicationTypeFees)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"UPDATE ApplicationTypes SET ApplicationTypeTitle = @Title,
                              ApplicationFees = @Fees WHERE {_PrimaryKeyColumnName} = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ApplicationTypeID);
            command.Parameters.AddWithValue("@Title", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@Fees", ApplicationTypeFees);

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

        public static decimal GetApplicationTypeFees(byte ApplicationTypeID)
        {
            decimal ApplicationTypeFees = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT ApplicationFees FROM ApplicationTypes
                              WHERE {_PrimaryKeyColumnName} = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    ApplicationTypeFees = Convert.ToDecimal(result);

            }

            catch { }

            finally
            {
                connection.Close();
            }
            return ApplicationTypeFees;
        }

        public static string GetApplicationTypeTitle(byte ApplicationTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT ApplicationTypeTitle FROM ApplicationTypes
                              WHERE {_PrimaryKeyColumnName} = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return result.ToString();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return null;
        }
    }
}
