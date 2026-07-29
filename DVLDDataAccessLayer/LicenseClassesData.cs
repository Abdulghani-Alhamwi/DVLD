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

    }
}
