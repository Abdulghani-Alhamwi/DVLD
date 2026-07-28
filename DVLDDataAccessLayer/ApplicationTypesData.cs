using System;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsApplicationTypesData
    {
        public static DataTable GetApplicationTypes()
        {
            DataTable dtApplicationTypes = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT ApplicationTypeID AS ID , ApplicationTypeTitle AS Title ,
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

        public static bool UpdateApplicationType(int ApplicationTypeID , string ApplicationTitle,double ApplicationFees)
        {
            int AffectedRows = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE ApplicationTypes SET
                             ApplicationTypeTitle = @Title , 
                             ApplicationFees = @Fees
                             WHERE ApplicationTypeID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ApplicationTypeID);
            command.Parameters.AddWithValue("@Title", ApplicationTitle);
            command.Parameters.AddWithValue("@Fees", ApplicationFees);

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
