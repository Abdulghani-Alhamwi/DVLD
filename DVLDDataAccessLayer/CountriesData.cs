using System;
using System.Data.SqlClient;
using System.Data;

namespace DVLDDataAccessLayer
{
    public class clsCountriesData
    {
        public static DataTable GetAllCountries()
        {
            DataTable dtCountries = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

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
        public static int GetCountryID(string CountryName)
        {
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT CountryID FROM Countries
                             WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if(result!=null)
                CountryID = Convert.ToInt32(result);
                
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return CountryID;
        }
        public static string GetCountryName(int NationalityCountryID)
        {
            string CountryName = "";

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT CountryName FROM Countries
                             WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", NationalityCountryID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    CountryName = result.ToString();

            }

            catch { }

            finally
            {
                connection.Close();
            }
            return CountryName;
        }
    }
}
