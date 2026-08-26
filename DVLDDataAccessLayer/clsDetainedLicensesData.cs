using System;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsDetainedLicensesData
    {
        public static bool IsDetainedLicense(int LicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT Found = 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

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
