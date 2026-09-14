using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsDetainedLicensesData
    {
        private static string _query =
         $@"SELECT TOP(@WantedNumOfRecords) DetainID AS [D.ID],DetainedLicenses.LicenseID AS [L.ID],FORMAT(DetainDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [D.Date],IsReleased AS [Is Released],
           FineFees AS [Fine Fees],FORMAT(ReleaseDate,'{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.DateTimeCustomFormat)}') AS [Release Date],People.NationalNo AS [N.No.],
           People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName AS [Full Name],
           DetainedLicenses.ReleaseApplicationID AS [Release App.ID] FROM DetainedLicenses INNER JOIN LocalLicenses ON DetainedLicenses.LicenseID = LocalLicenses.LicenseID
           INNER JOIN Applications ON LocalLicenses.ApplicationID = Applications.ApplicationID INNER JOIN People ON Applications.ApplicantPersonID = People.PersonID";

        public static DataTable GetDetainedLicensesInfo(byte WantedNumOfRecords, int LastLowestBroughtDetainID = -1)
        {
            DataTable dtDetainedLicenses = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _query;

            if (LastLowestBroughtDetainID != -1)
                query += " WHERE DetainID < @LastLowestBroughtDetainID";

            query += " ORDER BY DetainID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if(LastLowestBroughtDetainID != -1)
            command.Parameters.AddWithValue("@LastLowestBroughtDetainID", LastLowestBroughtDetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtDetainedLicenses = new DataTable();
                    dtDetainedLicenses.Load(reader);
                }

                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtDetainedLicenses;
        }
        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(_query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", 0);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable dtLDLApplications = new DataTable();
                dtLDLApplications.Load(reader);
                reader.Close();

                return dtLDLApplications;
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return null;
        }


        public static bool IsDetainedLicense(int LocalLicenseID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT Detained = 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

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

        public static int AddNewDetainedLicense(int LocalLicenseID,DateTime DetainDate,decimal FineFees,int CreatedByUserID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses VALUES (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,0,null,null,null);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
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

        public static bool ReleaseDetainedLicense(int LocalLicenseID, DateTime ReleaseDate,int ReleasedByUserID,int ReleaseApplicationID)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses SET IsReleased = 1, ReleaseDate = @ReleaseDate,
                             ReleasedByUserID = @ReleasedByUserID, ReleaseApplicationID = @ReleaseApplicationID
                             WHERE LicenseID = @LicenseID";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

            try
            {
                connection.Open();
                AffectedRows = Convert.ToByte(command.ExecuteNonQuery());
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return (AffectedRows != 0);
        }

        public static bool Find(int LocalLicenseID, ref int DetainID, ref DateTime DetainDate, ref decimal FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LocalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] != DBNull.Value)
                        ReleaseDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

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
        
        private static string _GetOriginalColumnNameToFilterBy(string SendedColumnName)
        {
            switch (SendedColumnName)
            {
                case "Detain ID":
                    return "DetainID";

                case "Is Released":
                    return "IsReleased";

                case "National No.":
                    return "NationalNo";

                case "Full Name":
                    return "People.FirstName + ' ' + People.SecondName + CASE WHEN People.ThirdName IS NULL THEN '' ELSE ' ' + People.ThirdName END + ' '+ People.LastName";

                case "Release Application ID":
                    return "ReleaseApplicationID";

                default:
                    return null;
            }
        }

        private static string _GetOriginalColumnNameToOrderBy(string SendedColumnName)
        {
            switch (SendedColumnName)
            {
                case "D.ID":
                    return "DetainID";

                case "L.ID":
                    return "DetainedLicenses.LicenseID";

                case "D.Date":
                    return "DetainDate";

                case "Is Released":
                    return "IsReleased";

                case "Fine Fees":
                    return "FineFees";

                case "Release Date":
                    return "ReleaseDate";

                case "N.No.":
                    return "NationalNo";

                case "Full Name":
                    return _GetOriginalColumnNameToFilterBy("Full Name");

                case "Release App.ID":
                    return "ReleaseApplicationID";

                default:
                    return null;
            }
        }

        private static string _GetDataFilteringQuery(byte WantedNumOfRecords, string ColumnNameToFilterBy, ref string ValueToFilterBy,
                    string ColumnNameToOrderBy, string SortDirection, int LastLowestbroughtDetainID = -1, char? WildChar = null)
        {
            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = "DetainID";
            else
                ColumnNameToOrderBy = _GetOriginalColumnNameToOrderBy(ColumnNameToOrderBy);

                string query = _query;

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                query += clsUtility.GetLastFilterQueryPart(ColumnNameToOrderBy, SortDirection);
                return query;
            }

            ColumnNameToFilterBy = _GetOriginalColumnNameToFilterBy(ColumnNameToFilterBy);

            if (ColumnNameToFilterBy == "IsReleased")
            {
                if (ValueToFilterBy == "All")
                {
                    query += clsUtility.GetLastFilterQueryPart(ColumnNameToFilterBy, ColumnNameToOrderBy,
                        SortDirection, false, LastLowestbroughtDetainID);

                    return query;
                }

                else
                    ValueToFilterBy = clsUtility.GetYesNoValueAsNumericString(ValueToFilterBy);
            }

            query += clsUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);

            query += clsUtility.GetLastFilterQueryPart(ColumnNameToFilterBy, ColumnNameToOrderBy, SortDirection, true, LastLowestbroughtDetainID);

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords, string ColumnNameToFilterBy, string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                    int LastLowestbroughtDetainID = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords, ColumnNameToFilterBy, ref ValueToFilterBy,
                ColumnNameToOrderBy,SortDirection, LastLowestbroughtDetainID, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            command.Parameters.AddWithValue("@Value", ValueToFilterBy);

            if (WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtFilteredData = new DataTable();
                    dtFilteredData.Load(reader);
                }

                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtFilteredData;
        }

        public static int GetTotalCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Count(DetainID) FROM DetainedLicenses";

            SqlCommand command = new SqlCommand(query, connection);

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

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy, string SortDirection, string ColumnNameToFilterBy, ref string ValueToFilterBy, char? WildChar = null)
        {
            string query = _query;
            
            ColumnNameToFilterBy = _GetOriginalColumnNameToOrderBy(ColumnNameToFilterBy);

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                query += clsUtility.GetLastFilterQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                if (ValueToFilterBy == "All")
                {
                    query += clsUtility.GetLastFilterQueryPart(ColumnNameToOrderBy, SortDirection);

                    return query;
                }

                else
                    ValueToFilterBy = clsUtility.GetYesNoValueAsNumericString(ValueToFilterBy);

                query += clsUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                query += clsUtility.GetLastFilterQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }
    }
}