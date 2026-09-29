using System;
using System.Data;
using System.Data.SqlClient;
using Utility_Library;

namespace DVLDDataAccessLayer
{
    public class clsPeopleData
    {
        private static readonly string _PrimaryKeyColumnName = "PersonID";
        private static readonly string _PrimaryKeyViewedColumnName = "Person ID";

        private static readonly string _FixedQueryPart =
         $@"{_PrimaryKeyColumnName} As [Person ID], NationalNo AS [National No.],
           FirstName AS [First Name], SecondName AS [Second Name] , ThirdName AS [Third Name], LastName AS [Last Name],
           Gendor = Case When Gendor = 0 Then 'Male' ELSE 'Female' END,
           FORMAT(DateOfBirth,'{clsGeneralUtility.GetCustomDateFormat(clsGeneralUtility.enCustomDateFormat.NumericFormat)}') AS [Date Of Birth],
           Countries.CountryName AS Nationality, Phone, Email FROM People INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID";

        private static readonly string _QueryWithoutPagination = "SELECT TOP (@WantedNumOfRecords) " + _FixedQueryPart;

        private static readonly string _QueryForOffsetPagination = "SELECT " + _FixedQueryPart;

        private static readonly string _OffsetPaginationQueryPart = clsGeneralUtility.GetOffsetPaginationQueryPart();

        private enum _enUpdatableColumns : byte
        {
            NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor,
            Address, Phone, Email, NationalityCountryID, ImagePath
        }

        public class clsOldPersonData
        {
            public string NationalNo, FirstName, SecondName, ThirdName, LastName;
            public DateTime? DateOfBirth;
            public byte? Gendor;
            public string Address, Phone, Email;
            public int NationalityCountryID;
            public string ImagePath;

            public clsOldPersonData(string NationalNo, string FirstName,string SecondName, string ThirdName, string LastName
                , DateTime DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
            {
                this.NationalNo = NationalNo;
                this.FirstName = FirstName;
                this.SecondName = SecondName;
                this.ThirdName = ThirdName;
                this.LastName = LastName;
                this.DateOfBirth = DateOfBirth;
                this.Gendor = Gendor;
                this.Address = Address;
                this.Phone = Phone;
                this.Email = Email;
                this.NationalityCountryID = NationalityCountryID;
                this.ImagePath = ImagePath;
            }
        }

        public static DataTable GetPeopleInfo(byte WantedNumOfRecords, int LastBroughtPersonID = -1,string ColumnNameToOrderBy = null
            ,int NumberOfRowsToOffset = -1, string SortDirection = "DESC")
        {
            DataTable dtPeople = null;

            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if(ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
            {
                query = _GetQueryForCursorPagination(LastBroughtPersonID,ColumnNameToOrderBy,SortDirection);
            }

            else
            {
                query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
            }

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (NumberOfRowsToOffset != -1)
            command.Parameters.AddWithValue("@NumberOfRowsToOffset", NumberOfRowsToOffset);

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

        private static string _GetQueryForCursorPagination(int LastBroughtPersonID, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;
            if (LastBroughtPersonID != -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetLastQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, false, LastBroughtPersonID,true,true);
            }

            else
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            return query;
        }

        private static string _GetQueryForOffsetPagination(int NumberOfRowsToOffset, string ColumnNameToOrderBy, string SortDirection)
        {
            string query;

            if (NumberOfRowsToOffset == -1)
            {
                query = _QueryWithoutPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                query = _QueryForOffsetPagination;
                query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy, SortDirection);
                query += _OffsetPaginationQueryPart;
            }

            return query;
        }

        public static DataTable GetColumnsNamesForView()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = _QueryWithoutPagination;

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@WantedNumOfRecords", 0);

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

        public static int AddNewPerson(string NationalNo, string FirstName,
                       string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"INSERT INTO People VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName,
                             @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
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

        private static void _ResetChangedOldValues(string NationalNo, string FirstName,string SecondName
            , string ThirdName, string LastName, DateTime DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, clsOldPersonData OldPersonData)
        {
            if (NationalNo != OldPersonData.NationalNo)
                OldPersonData.NationalNo = null;

            if (FirstName != OldPersonData.FirstName)
                OldPersonData.FirstName = null;

            if (SecondName != OldPersonData.SecondName)
                OldPersonData.SecondName = null;

            if (ThirdName != OldPersonData.ThirdName)
                OldPersonData.ThirdName = null;

            if (LastName != OldPersonData.LastName)
                OldPersonData.LastName = null;

            if (DateOfBirth != OldPersonData.DateOfBirth)
                OldPersonData.DateOfBirth = null;

            if (Gendor != OldPersonData.Gendor)
                OldPersonData.Gendor = null;

            if (Address != OldPersonData.Address)
                OldPersonData.Address = null;

            if (Phone != OldPersonData.Phone)
                OldPersonData.Phone = null;

            if (Email != OldPersonData.Email)
                OldPersonData.Email = null;

            if (NationalityCountryID != OldPersonData.NationalityCountryID)
                OldPersonData.NationalityCountryID = -1;

            if (ImagePath != OldPersonData.ImagePath)
                OldPersonData.ImagePath = null;
        }

        private static string _GetColumnValueSetPartForUpdate(_enUpdatableColumns UpdatableColumn)
        {
            switch (UpdatableColumn)
            {
                case _enUpdatableColumns.NationalNo:
                    return " NationalNo = @NationalNo";

                case _enUpdatableColumns.FirstName:
                    return " FirstName = @FirstName";

                case _enUpdatableColumns.SecondName:
                    return " SecondName = @SecondName";

                case _enUpdatableColumns.ThirdName:
                    return " ThirdName = @ThirdName";

                case _enUpdatableColumns.LastName:
                    return " LastName = @LastName";

                case _enUpdatableColumns.DateOfBirth:
                    return " DateOfBirth = @DateOfBirth";

                case _enUpdatableColumns.Gendor:
                    return " Gendor = @Gendor";

                case _enUpdatableColumns.Address:
                    return " Address = @Address";

                case _enUpdatableColumns.Phone:
                    return " Phone = @Phone";

                case _enUpdatableColumns.Email:
                    return " Email = @Email";

                case _enUpdatableColumns.NationalityCountryID:
                    return " NationalityCountryID = @NationalityCountryID";

                case _enUpdatableColumns.ImagePath:
                    return " ImagePath = @ImagePath";
            }

            return null;
        }
     
        private static string _GetUpdateQuery(string NationalNo, string FirstName,string SecondName, string ThirdName
            , string LastName, DateTime DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath, clsOldPersonData OldPersonData, bool HasOldDataChangedFully)
        {
            string query = "UPDATE People SET";

            if (!HasOldDataChangedFully)
            {
                if (NationalNo != OldPersonData.NationalNo)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalNo);

                    if (FirstName != OldPersonData.FirstName)
                        query += ","+ _GetColumnValueSetPartForUpdate(_enUpdatableColumns.FirstName);

                    if (SecondName != OldPersonData.SecondName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.SecondName);

                    if (ThirdName != OldPersonData.ThirdName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ThirdName);

                    if (LastName != OldPersonData.LastName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.LastName);

                    if (DateOfBirth != OldPersonData.DateOfBirth)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DateOfBirth);

                    if (Gendor != OldPersonData.Gendor)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Gendor);

                    if (Address != OldPersonData.Address)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (FirstName != OldPersonData.FirstName)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.FirstName);
                    if (SecondName != OldPersonData.SecondName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.SecondName);

                    if (ThirdName != OldPersonData.ThirdName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ThirdName);

                    if (LastName != OldPersonData.LastName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.LastName);

                    if (DateOfBirth != OldPersonData.DateOfBirth)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DateOfBirth);

                    if (Gendor != OldPersonData.Gendor)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Gendor);

                    if (Address != OldPersonData.Address)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (SecondName != OldPersonData.SecondName)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.SecondName);

                    query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ThirdName);

                    if (LastName != OldPersonData.LastName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.LastName);

                    if (DateOfBirth != OldPersonData.DateOfBirth)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DateOfBirth);

                    if (Gendor != OldPersonData.Gendor)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Gendor);

                    if (Address != OldPersonData.Address)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (ThirdName != OldPersonData.ThirdName)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ThirdName);
                    if (LastName != OldPersonData.LastName)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.LastName);

                    if (DateOfBirth != OldPersonData.DateOfBirth)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DateOfBirth);

                    if (Gendor != OldPersonData.Gendor)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Gendor);

                    if (Address != OldPersonData.Address)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (LastName != OldPersonData.LastName)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.LastName);

                    if (DateOfBirth != OldPersonData.DateOfBirth)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DateOfBirth);

                    if (Gendor != OldPersonData.Gendor)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Gendor);

                    if (Address != OldPersonData.Address)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (DateOfBirth != OldPersonData.DateOfBirth)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DateOfBirth);

                    if (Gendor != OldPersonData.Gendor)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Gendor);

                    if (Address != OldPersonData.Address)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (Gendor != OldPersonData.Gendor)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Gendor);

                    if (Address != OldPersonData.Address)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (Address != OldPersonData.Address)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Address);

                    if (Phone != OldPersonData.Phone)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (Phone != OldPersonData.Phone)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Phone);

                    if (Email != OldPersonData.Email)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (Email != OldPersonData.Email)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.Email);

                    if (NationalityCountryID != OldPersonData.NationalityCountryID)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else if (NationalityCountryID != OldPersonData.NationalityCountryID)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.NationalityCountryID);

                    if (ImagePath != OldPersonData.ImagePath)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
                }

                else
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ImagePath);
            }

            else
                query += @" NationalNo = @NationalNo ,FirstName = @FirstName ,SecondName = @SecondName,
                            ThirdName = @ThirdName ,LastName = @LastName ,DateOfBirth = @DateOfBirth,
                            Gendor = @Gendor ,Address = @Address ,Phone = @Phone ,Email = @Email,
                            NationalityCountryID = @NationalityCountryID, ImagePath = @ImagePath";

            query += $" WHERE {_PrimaryKeyColumnName} = @PersonID";

            _ResetChangedOldValues(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address,
                    Phone, Email, NationalityCountryID, ImagePath, OldPersonData);

            return query;
        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName,string SecondName, string ThirdName, string LastName
            , DateTime DateOfBirth, byte? Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath,clsOldPersonData OldPersonDate,bool HasOldDataChangedFully)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetUpdateQuery(NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,
                Address,Phone,Email,NationalityCountryID,ImagePath,OldPersonDate,HasOldDataChangedFully);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            if (OldPersonDate.NationalNo == null)
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            if (OldPersonDate.FirstName == null)
                command.Parameters.AddWithValue("@FirstName", FirstName);

            if (OldPersonDate.SecondName == null)
                command.Parameters.AddWithValue("@SecondName", SecondName);

            if (OldPersonDate.ThirdName == null)
            {
                if (!String.IsNullOrEmpty(ThirdName))
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                else
                    command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            }

            if (OldPersonDate.LastName == null)
                command.Parameters.AddWithValue("@LastName", LastName);

            if (OldPersonDate.DateOfBirth == null)
                command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            if (OldPersonDate.Gendor == null)
                command.Parameters.AddWithValue("@Gendor", Gendor);

            if (OldPersonDate.Address == null)
                command.Parameters.AddWithValue("@Address", Address);

            if (OldPersonDate.Phone == null)
                command.Parameters.AddWithValue("@Phone", Phone);

            if (OldPersonDate.Email == null)
            {
                if (!String.IsNullOrEmpty(Email))
                    command.Parameters.AddWithValue("@Email", Email);
                else
                    command.Parameters.AddWithValue("@Email", DBNull.Value);
            }

            if (OldPersonDate.NationalityCountryID == -1)
                command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (OldPersonDate.ImagePath == null)
            {
                if (!String.IsNullOrEmpty(ImagePath))
                    command.Parameters.AddWithValue("@ImagePath", ImagePath);
                else
                    command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }

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
            return (AffectedRows > 0);
        }

        public static bool DeletePerson(int PersonID)
        {
            byte AffectedRows = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = $@"DELETE FROM People WHERE {_PrimaryKeyColumnName} = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
            return (AffectedRows > 0);
        }

        public static bool SearchForNationalNo(string NationalNo)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM People WHERE NationalNo = @NationalNo";

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
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT NationalNo,FirstName, SecondName,ThirdName, LastName, Gendor, DateOfBirth,
                             Address, Phone, Email , NationalityCountryID ,ImagePath FROM People
                             WHERE {_PrimaryKeyColumnName} = @PersonID";

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

                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime) reader["DateOfBirth"];
                    Gendor = (byte) reader["Gendor"];
                    Address = (string) reader["Address"];
                    Phone = (string) reader["Phone"];

                    if (reader["Email"] != DBNull.Value)
                        Email = (string)reader["Email"];

                        NationalityCountryID = (int)reader["NationalityCountryID"];

                    if (reader["ImagePath"] != DBNull.Value)
                        ImagePath = (string)reader["ImagePath"];
                    else
                        ImagePath = null;

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

        public static bool Find(string NationalNo,ref int PersonID, ref string FirstName,
                      ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT {_PrimaryKeyColumnName},FirstName,SecondName,ThirdName,LastName,Gendor,DateOfBirth,
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

        public static int GetTotalPeopleCount()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT Count({_PrimaryKeyColumnName}) FROM People";

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

        private static string _GetOriginalColumnName(string SendedColumnName)
        {
            switch(SendedColumnName)
            {
                case "Person ID":
                    return _PrimaryKeyColumnName;

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

                case "Date Of Birth":
                    return "DateOfBirth";

                default:
                    return "";
            }
        }

        private static string _GetValueForGendorColumn(string Value)
        {
            switch (Value.ToUpper())
            {
                case "M":
                case "MA":
                case "MAL":
                case "MALE":
                    return "0";

                case "F":
                case "FE":
                case "FEM":
                case "FEMA":
                case "FEMAL":
                case "FEMALE":
                    return "1";
            }
            return null;
        }

        private static bool _IsOriginalColumnName(string ColumnNameToFilterBy)
        {
            return (ColumnNameToFilterBy == "Gendor" || ColumnNameToFilterBy == "Phone" || ColumnNameToFilterBy == "Email");
        }

        private static string _GetDataFilteringQuery(byte WantedNumOfRecords, string ColumnNameToFilterBy, ref string ValueToFilterBy
                    , string ColumnNameToOrderBy, string SortDirection, int LastBroughtPersonID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            if (ColumnNameToOrderBy == null)
                ColumnNameToOrderBy = _PrimaryKeyViewedColumnName;

            string query;

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _GetQueryForCursorPagination(LastBroughtPersonID, ColumnNameToOrderBy, SortDirection);
                }

                else
                {
                    query = _GetQueryForOffsetPagination(NumberOfRowsToOffset, ColumnNameToOrderBy, SortDirection);
                }
            }

            else
            {
                if (!_IsOriginalColumnName(ColumnNameToFilterBy))
                    ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

                else
                {
                    if (ColumnNameToFilterBy == "Gendor")
                    {
                        ValueToFilterBy = _GetValueForGendorColumn(ValueToFilterBy);
                    }
                }

                if (ColumnNameToOrderBy == _PrimaryKeyViewedColumnName)
                {
                    query = _QueryWithoutPagination;
                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                    query += clsGeneralUtility.GetLastFilterQueryPart(_PrimaryKeyColumnName, ColumnNameToOrderBy, SortDirection, true, LastBroughtPersonID,true,true);
                }

                else
                {
                    query = _QueryForOffsetPagination;
                    query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                    query += clsGeneralUtility.GetOrderByQueryPart(ColumnNameToOrderBy,SortDirection,true);
                    query += _OffsetPaginationQueryPart;
                }
            }

            return query;
        }

        public static DataTable GetFilteredData(byte WantedNumOfRecords,string ColumnNameToFilter,string ValueToFilterBy, string ColumnNameToOrderBy, string SortDirection,
            int LastBroughtPersonID = -1, int NumberOfRowsToOffset = -1, char? WildChar = null)
        {
            DataTable dtFilteredData = null;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetDataFilteringQuery(WantedNumOfRecords,ColumnNameToFilter,ref ValueToFilterBy,
                            ColumnNameToOrderBy,SortDirection, LastBroughtPersonID,NumberOfRowsToOffset, WildChar);

            SqlCommand command = new SqlCommand(query, connection);
             command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

            if (!string.IsNullOrEmpty(ValueToFilterBy))
                command.Parameters.AddWithValue("@Value", ValueToFilterBy);

                if(WildChar != null)
                command.Parameters.AddWithValue("@WildChar", WildChar);

            if (NumberOfRowsToOffset != -1)
                command.Parameters.AddWithValue("@NumberOfRowsToOffset", NumberOfRowsToOffset);

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

            string query = $@"SELECT FirstName + ' ' + SecondName + ' ' + CASE WHEN ThirdName IS NULL THEN LastName ELSE ThirdName + ' ' + LastName END
                             FROM People WHERE {_PrimaryKeyColumnName} = @PersonID";

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

            string query = $"SELECT NationalNo FROM People WHERE {_PrimaryKeyColumnName} = @PersonID";

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

            string query = $"SELECT {_PrimaryKeyColumnName} FROM People WHERE NationalNo = @NationalNo";

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

        private static string _GetDataSortingQuery(string ColumnNameToOrderBy, string SortDirection, string ColumnNameToFilterBy, ref string ValueToFilterBy, char? WildChar = null)
        {
            string query = _QueryWithoutPagination;

            if (!_IsOriginalColumnName(ColumnNameToFilterBy))
                ColumnNameToFilterBy = _GetOriginalColumnName(ColumnNameToFilterBy);

            if (string.IsNullOrEmpty(ValueToFilterBy))
            {
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            else
            {
                if (ColumnNameToFilterBy == "Gendor")
                {
                    ValueToFilterBy = _GetValueForGendorColumn(ValueToFilterBy);
                }

                query += clsGeneralUtility.GetFilterQueryPart_ValueCondition(ColumnNameToFilterBy, WildChar);
                query += clsGeneralUtility.GetLastSortQueryPart(ColumnNameToOrderBy, SortDirection);
            }

            return query;
        }

        public static DataTable GetSortedInfo(byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
            string ColumnNameToFilterBy = null, string ValueToFilterBy = null, char? WildChar = null)
        {
            return clsGeneralUtility.GetSortedInfoFromYourQueryAndArgs(DataAccessSettings.ConnectionString, _GetDataSortingQuery(ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ref ValueToFilterBy, WildChar),
                WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, ColumnNameToFilterBy, ValueToFilterBy, WildChar);
        }
    }
}