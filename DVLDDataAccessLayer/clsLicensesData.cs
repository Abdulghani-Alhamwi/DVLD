using System;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsLicensesData
    {
        public static int IssueDrivingLicense(int ApplicationID ,int DriverID,byte LicenseClassID,DateTime IssueDate , DateTime ExpirationDate,string Notes,decimal PaidFees,bool IsActive,byte IssueReason,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Licenses VALUES (@ApplicationID, @DriverID,@LicenseClassID,
                             @IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (Notes != null)
            command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    return Convert.ToInt32(result);
            }

            catch (Exception ex){ Console.Write(ex.Message); }

            finally
            {
                connection.Close();
            }
            return -1;
        }

    public static bool Find(int LDLAppID , ref int LicenseID,ref int ApplicationID,ref int DriverID, ref byte LicenseClassID,
                            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {

            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.* FROM Licenses INNER JOIN Applications ON License.ApplicationID = Applications.ApplicationID
                             INNER JOIN LocalDrivingLicenseApplications ON Applications.LocalDrivingLicenseApplicationID = @LDLAppID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = Convert.ToByte(reader["LicenseClassID"]);
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"]; ;
                    Notes = (string)reader["Notes"];
                    PaidFees = (decimal)reader["Notes"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsFound = true;
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return IsFound;
        }

    }
}
