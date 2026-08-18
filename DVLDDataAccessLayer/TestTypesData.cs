using System;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsTestTypesData
    {
        public static DataTable GetTestTypes()
        {
            DataTable dtTestTypes = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT TestTypeID AS ID , TestTypeTitle AS Title ,
                             TestTypeDescription AS Description , TestTypeFees AS Fees 
                             FROM TestTypes";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtTestTypes = new DataTable();
                    dtTestTypes.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtTestTypes;
        }

        public static bool UpdateTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,decimal TestTypeFees)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE TestTypes SET
                             TestTypeTitle = @Title,
                             TestTypeDescription = @Description,
                             TestTypeFees = @Fees
                             WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@Title", TestTypeTitle);
            command.Parameters.AddWithValue("@Description", TestTypeDescription);
            command.Parameters.AddWithValue("@Fees", TestTypeFees);

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

        public static float GetTestTypeFees(byte TestTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT TestTypeFees FROM TestTypes
                             WHERE TestTypeID = @TestTypeId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToSingle(result);
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
