using System;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsLicenseClassesData
    {
        public static DataTable GetLicenseClassesNames()
        {
            DataTable dtLicenseClassesNames = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT ClassName FROM LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtLicenseClassesNames = new DataTable();
                    dtLicenseClassesNames.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtLicenseClassesNames;
        }
        
        public static int GetLicenseClassID(string LicenseClassName)
        {
            int LicenseClassID = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT LicenseClassID FROM LicenseClasses
                             WHERE ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", LicenseClassName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    LicenseClassID = Convert.ToInt32(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return LicenseClassID;
        }
        public static string GetLicenseClassName(int LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT ClassName FROM LicenseClasses
                             WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
