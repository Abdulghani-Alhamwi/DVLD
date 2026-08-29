using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsPeopleData
    {
        private static string _query =
         $@"SELECT TOP (@WantedNumberOfRecords) PersonID As [Person ID], NationalNo AS [National No.],
           FirstName AS [First Name], SecondName AS [Second Name] , ThirdName AS [Third Name], LastName AS [Last Name],
           Gendor = Case When Gendor = 0 Then 'Male' ELSE 'Female' END,
           FORMAT(DateOfBirth , '{clsUtility.GetCustomDateFormat(clsUtility.enCustomDateFormat.NumericFormat)}') AS [Date Of Birth] ,Countries.CountryName AS Nationality, Phone, Email
           FROM People INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID";

        public static int AddNewPerson(string NationalNo, string FirstName,
                       string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People VALUES (@NationalNo , @FirstName ,
                             @SecondName , @ThirdName , @LastName , @DateOfBirth , @Gendor,
                             @Address , @Phone , @Email , @NationalityCountryID, @ImagePath);
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (!String.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (!String.IsNullOrEmpty(Email))
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            
            if (!String.IsNullOrEmpty(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

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

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName,
                       string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int AffectedRows = -1;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"UPDATE PEOPLE SET NationalNo = @NationalNo ,FirstName = @FirstName ,SecondName = @SecondName ,
                             ThirdName = @ThirdName ,LastName = @LastName ,DateOfBirth = @DateOfBirth ,
                             Gendor = @Gendor ,Address = @Address ,Phone = @Phone ,Email = @Email ,
                             NationalityCountryID = @NationalityCountryID ,ImagePath = @ImagePath 
                             WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (!String.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (!String.IsNullOrEmpty(Email))
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (!String.IsNullOrEmpty(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();

            }

            catch { }

            finally
            {
                connection.Close();
            }
            return (AffectedRows > 0);
        }

        public static bool DeletePerson(int PersonID)
        {
            int AffectedRows = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"DELETE FROM People
                           WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                AffectedRows = command.ExecuteNonQuery();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return (AffectedRows > 0);
        }
        public static DataTable GetPeopleInfo(byte WantedNumberOfRecords,int LastLowestbroughtPersonID = -1)
        {
            DataTable dtPeople = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _query;
            
            if (LastLowestbroughtPersonID != -1)
                query += @" WHERE PersonID < @LastLowestbroughtPersonID";

                query += " ORDER BY PersonID DESC";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", WantedNumberOfRecords);

            if(LastLowestbroughtPersonID != -1)
                command.Parameters.AddWithValue("@LastLowestbroughtPersonID", LastLowestbroughtPersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dtPeople = new DataTable();
                    dtPeople.Load(reader);
                }

                    reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtPeople;
        }

        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _query;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumberOfRecords", 0);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                DataTable dtPeople = new DataTable();
                dtPeople.Load(reader);
                reader.Close();

                return dtPeople;
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return null;
        }

        public static bool SearchForNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM People
                             WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null)
                {
                    return true;
                }
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return false;
        }
        public static bool Find(int PersonID,ref string NationalNo,ref string FirstName,
                       ref string SecondName,ref string ThirdName,ref string LastName,ref DateTime DateOfBirth,ref byte Gendor,ref string Address,ref string Phone,ref string Email,ref int NationalityCountryID,ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT NationalNo,FirstName, SecondName,ThirdName, LastName, Gendor, DateOfBirth,
                             Address, Phone, Email , NationalityCountryID ,ImagePath FROM People
                             WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    NationalNo =(string) reader["NationalNo"];
                    FirstName = (string) reader["FirstName"];
                    SecondName = (string) reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];
                    else
                        ThirdName = null;

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime) reader["DateOfBirth"];
                    Gendor = (byte) reader["Gendor"];
                    Address = (string) reader["Address"];
                    Phone = (string) reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];
                    else
                        Email = null;                        

                        NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = null;

                        return true;
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return false;
        }

        public static bool Find(string NationalNo,ref int PersonID, ref string FirstName,
                      ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT PersonID,FirstName,SecondName,ThirdName,LastName,Gendor,DateOfBirth,
                             Address,Phone,Email,NationalityCountryID,ImagePath FROM People
                             WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    if (reader["ThirdName"] != DBNull.Value)
                        ThirdName = (string)reader["ThirdName"];

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];

                    NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];

                    return true;
                }
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return false;
        }

        public static int GetTotalPeopleCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Count(PersonID) FROM People";

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
            return 0;
        }

        private static string _GetOriginalColumnName(string SendedColumnName)
        {
            switch(SendedColumnName)
            {
                case "Person ID":
                    return "PersonID";

                case "National No.":
                    return "NationalNo";

                case "First Name":
                    return "FirstName";

                case "Second Name":
                    return "SecondName";

                case "Third Name":
                    return "ThirdName";

                case "Last Name":
                    return "LastName";

                case "Nationality":
                    return "Countries.CountryName";

                default:
                    return "";
            }
        }

        private static string _GetValueForGendorColumn(string Value)
        {
            switch(Value.Length)
            {
                case 1:
                    if (Value.ToUpper() == "M")
                        return "0";
                    else if (Value.ToUpper() == "F")
                        return "1";

                    break;

                case 2:
                    if (Value.ToUpper() == "MA")
                        return "0";
                    else if (Value.ToUpper() == "FA")
                        return "1";

                        break;

                case 3:
                    if (Value.ToUpper() == "MAL")
                        return "0";
                    else if (Value.ToUpper() == "FEM")
                        return "1";

                    break;

                case 4:
                    if (Value.ToUpper() == "MALE")
                        return "0";
                    else if (Value.ToUpper() == "FEMA")
                        return "1";

                    break;

                case 5:
                    if (Value.ToUpper() == "FEMAL")
                        return "1";

                    break;

                case 7:
                    if (Value.ToUpper() == "FEMALE")
                        return "1";

                    break;
            }
            return "2";
        }

        private static string _GetDataFilteringQuery(byte WantedNumberOfRecords, string ColumnNameToFilter,ref string ValueToFilterBy, char? WildChar = null, int LastLowestbroughtPersonID = -1)
        {
            string query = _query;

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                if (LastLowestbroughtPersonID != -1)
                query += @" WHERE PersonID < @LastLowestbroughtPersonID
                           ORDER BY PersonID DESC";
                else
                    query += " ORDER BY PersonID DESC";
                return query;
            }

            if (ColumnNameToFilter != "Gendor" && ColumnNameToFilter != "Phone" && ColumnNameToFilter != "Email")
                ColumnNameToFilter = _GetOriginalColumnName(ColumnNameToFilter);
            else
            {
                if (ColumnNameToFilter == "Gendor")
                {
                    ValueToFilterBy = _GetValueForGendorColumn(ValueToFilterBy);
                }
            }

                if (WildChar == null)
                    query += $" WHERE {ColumnNameToFilter} = @Value";
                else
                    query += $" WHERE {ColumnNameToFilter} LIKE @Value + @WildChar";

            if (ColumnNameToFilter == "Gendor")
            {
                if (LastLowestbroughtPersonID == -1)
                    query += " ORDER BY [Person ID] DESC";

                else
                    query += @" AND [Person ID] < @LastLowestbroughtPersonID
                           ORDER BY [Person ID] DESC";
            }

            else
            {
                if (LastLowestbroughtPersonID == -1)
                    query += " ORDER BY PersonID DESC";

                else
                    query += @" AND PersonID < @LastLowestbroughtPersonID
                           ORDER BY PersonID DESC";
            }

                return query;
        }

        public static DataTable GetFilteredData(byte WantedNumberOfRecords,string ColumnNameToFilter,string ValueToFilterBy, int LastLowestbroughtPersonID = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumberOfRecords,ColumnNameToFilter,ref ValueToFilterBy,WildChar,LastLowestbroughtPersonID);

            SqlCommand command = new SqlCommand(query, connection);
             command.Parameters.AddWithValue("@WantedNumberOfRecords", WantedNumberOfRecords);

            if (!string.IsNullOrEmpty(ValueToFilterBy))
                command.Parameters.AddWithValue("@Value", ValueToFilterBy);

                if(WildChar != null)
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

        public static string GetPersonFullName(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT FirstName + ' ' + SecondName + ' ' + CASE WHEN ThirdName IS NULL THEN LastName ELSE ThirdName + ' ' + LastName END
                             FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static string GetNationalNumber(int PersonID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT NationalNo FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
        public static int GetPersonID(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT PersonID FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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