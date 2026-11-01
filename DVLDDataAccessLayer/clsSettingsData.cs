using System;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsSettingsData
    {
        public static byte GetInternationalLicenseValidityLength()
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT InternationalLicenseValidityLength FROM Settings";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToByte(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return 0;
        }
    }
}
