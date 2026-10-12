using System;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsCountryData
    {
        private static readonly string _PrimaryKeyColumnName = "CountryID";

        public static DataTable GetAllCountries()
        {
            DataTable dtCountries = null;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT CountryName FROM Countries";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtCountries = new DataTable();
                    dtCountries.Load(reader);
                }

                    reader.Close();
            }

            catch { }
            
            finally
            {
                connection.Close();
            }
            return dtCountries;
        }

        public static bool Find(byte CountryID,ref string CountryName)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT CountryName FROM Countries WHERE {_PrimaryKeyColumnName} = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                    CountryName = result.ToString();
                }

            }

            catch
            {
                IsFound = false;
            }

            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static bool Find(string CountryName,ref byte CountryID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT {_PrimaryKeyColumnName} FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
                    CountryID = Convert.ToByte(result);
                }

            }

            catch
            {
                IsFound = false;
            }

            finally
            {
                connection.Close();
            }
            return IsFound;
        }
    }
}