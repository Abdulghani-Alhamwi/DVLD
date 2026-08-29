using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsInternationalLicensesData
    {
        private static string _query =
         $@"SELECT TOP (@WantedNumOfRecords) InternationalLicenseID AS [Int.License ID],ApplicationID AS [Application ID],DriverID AS [Driver ID],
           IssuedUsingLocalLicenseID AS [Issued Using L.License ID],Format(IssueDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Issue Date],
           Format(ExpirationDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Expiration Date],IsActive AS [Is Active]
           FROM InternationalLicenses";

        public static DataTable GetInternationalLicenses(int DriverID, byte WantedNumOfRecords, int LowstBroughtIntLicID = -1)
        {
            DataTable dtIntLicenses = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _query + @"INNER JOIN Drivers ON InternationalLicenses.DriverID = Drivers.DriverID WHERE
                           DriverID = @DriverID";

            if (LowstBroughtIntLicID != -1)
                query += " AND DriverID < @LowstBroughtIntLicID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            if (LowstBroughtIntLicID != -1)
                command.Parameters.AddWithValue("@LowstBroughtIntLicID", LowstBroughtIntLicID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtIntLicenses = new DataTable();
                    dtIntLicenses.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtIntLicenses;
        }

        public static int IssueDriverLicense(int ApplicationID ,int DriverID,int LocalLicenseID,
            DateTime IssueDate,DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses VALUES (@ApplicationID,@DriverID,@LocalLicenseID,
                             @IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result);
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
