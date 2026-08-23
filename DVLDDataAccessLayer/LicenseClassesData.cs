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
        
        public static byte GetLicenseClassID(string LicenseClassName)
        {
            byte LicenseClassID = 0;
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
                    LicenseClassID = Convert.ToByte(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return LicenseClassID;
        }
        public static byte GetLicenseValidityLength(byte LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT DefaultValidityLength FROM LicenseClasses
                             WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static decimal GetLicenseClassFees(byte LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT ClassFees FROM LicenseClasses
                             WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToDecimal(result);
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return -1;
        }
        public static byte GetMinimumAllowedAge(byte LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT MinimumAllowedAge FROM LicenseClasses
                             WHERE LicenseClassID = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
