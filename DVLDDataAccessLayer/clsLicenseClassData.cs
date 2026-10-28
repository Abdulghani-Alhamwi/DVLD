using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsLicenseClassData
    {
        private static readonly string _PrimaryKeyColumnName = "LicenseClassID";

        public static DataTable GetLicenseClassesNames()
        {
            DataTable dtLicenseClassesNames = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT {_PrimaryKeyColumnName} FROM LicenseClasses WHERE ClassName = @ClassName";

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

        public static string GetLicenseClassName(byte LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT ClassName FROM LicenseClasses WHERE {_PrimaryKeyColumnName} = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static byte GetMinimumAllowedAge(byte LicenseClassID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT MinimumAllowedAge FROM LicenseClasses WHERE {_PrimaryKeyColumnName} = @LicenseClassID";

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

        public static bool GetLicenseClassInfoByID(byte LicenseClassID, ref string ClassName, ref string ClassDescription
            , ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees
                              FROM LicenseClasses WHERE {_PrimaryKeyColumnName} = @LicenseClassID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
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

        public static bool GetLicenseClassInfoByClassName(string ClassName, ref byte LicenseClassID, ref string ClassDescription
           , ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT LicenseClassID, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees
                              FROM LicenseClasses WHERE ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    LicenseClassID = (byte)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
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

        public static byte AddNewLicenseClass(string ClassName, string ClassDescription
        , byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO LicenseClasses (ClassName,ClassDescription,MinimumAllowedAge,DefaultValidityLength,ClassFees) 
                             VALUES (@ClassName,@ClassDescription,@MinimumAllowedAge,@DefaultValidityLength,@ClassFees);
                             SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);

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


        private enum _enUpdatableColumns : byte
        {
            ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees
        }

        public class clsOldLicenseClassInfo
        {
            public string ClassName;
            public string ClassDescription;
            public byte MinimumAllowedAge;
            public byte DefaultValidityLength;
            public float ClassFees;

            public clsOldLicenseClassInfo(string OldClassName, string OldClassDescription, byte OldMinimumAllowedAge, byte OldDefaultValidityLength, float OldClassFees)
            {
                ClassName = OldClassName;
                ClassDescription = OldClassDescription;
                MinimumAllowedAge = OldMinimumAllowedAge;
                DefaultValidityLength = OldDefaultValidityLength;
                ClassFees = OldClassFees;

            }
        }
        private static void _ResetChangedOldValues(string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees, clsOldLicenseClassInfo OldLicneseClassData)
        {
            if (ClassName != OldLicneseClassData.ClassName)
                OldLicneseClassData.ClassName = null;

            if (ClassDescription != OldLicneseClassData.ClassDescription)
                OldLicneseClassData.ClassDescription = null;

            if (MinimumAllowedAge != OldLicneseClassData.MinimumAllowedAge)
                OldLicneseClassData.MinimumAllowedAge = 0;

            if (DefaultValidityLength != OldLicneseClassData.DefaultValidityLength)
                OldLicneseClassData.DefaultValidityLength = 0;

            if (ClassFees != OldLicneseClassData.ClassFees)
                OldLicneseClassData.ClassFees = -1;
        }

        private static string _GetColumnValueSetPartForUpdate(_enUpdatableColumns UpdatableColumns)
        {
            switch (UpdatableColumns)
            {
                case _enUpdatableColumns.ClassName:
                    return " ClassName = @ClassName";

                case _enUpdatableColumns.ClassDescription:
                    return " ClassDescription = @ClassDescription";

                case _enUpdatableColumns.MinimumAllowedAge:
                    return " MinimumAllowedAge = @MinimumAllowedAge";

                case _enUpdatableColumns.DefaultValidityLength:
                    return " DefaultValidityLength = @DefaultValidityLength";

                case _enUpdatableColumns.ClassFees:
                    return " ClassFees = @ClassFees";
            }

            return null;
        }

        private static string _GetUpdateQuery(string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees, clsOldLicenseClassInfo OldLicenseClassInfo, bool HasOldInfoChangedFully)
        {
            string query = "UPDATE LicenseClasses SET";

            if (!HasOldInfoChangedFully)
            {
                if (ClassName != OldLicenseClassInfo.ClassName)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassName);

                    if (ClassDescription != OldLicenseClassInfo.ClassDescription)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassDescription);

                    if (MinimumAllowedAge != OldLicenseClassInfo.MinimumAllowedAge)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.MinimumAllowedAge);

                    if (DefaultValidityLength != OldLicenseClassInfo.DefaultValidityLength)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DefaultValidityLength);

                    if (ClassFees != OldLicenseClassInfo.ClassFees)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassFees);
                }

                else if (ClassDescription != OldLicenseClassInfo.ClassDescription)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassDescription);

                    if (MinimumAllowedAge != OldLicenseClassInfo.MinimumAllowedAge)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.MinimumAllowedAge);

                    if (DefaultValidityLength != OldLicenseClassInfo.DefaultValidityLength)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DefaultValidityLength);

                    if (ClassFees != OldLicenseClassInfo.ClassFees)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassFees);
                }

                else if (MinimumAllowedAge != OldLicenseClassInfo.MinimumAllowedAge)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.MinimumAllowedAge);

                    if (DefaultValidityLength != OldLicenseClassInfo.DefaultValidityLength)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DefaultValidityLength);

                    if (ClassFees != OldLicenseClassInfo.ClassFees)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassFees);
                }

                else if (DefaultValidityLength != OldLicenseClassInfo.DefaultValidityLength)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.DefaultValidityLength);

                    if (ClassFees != OldLicenseClassInfo.ClassFees)
                        query += "," + _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassFees);
                }

                else
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ClassFees);
                }
            }

            else
                query += @" ClassName = @ClassName, ClassDescription = @ClassDescription, MinimumAllowedAge = @MinimumAllowedAge, DefaultValidityLength = @DefaultValidityLength,
                           ClassFees = @ClassFees";

            query += $" WHERE {_PrimaryKeyColumnName} = @LicenseClassID";

            _ResetChangedOldValues(ClassName, ClassDescription,MinimumAllowedAge, DefaultValidityLength, ClassFees, OldLicenseClassInfo);

            return query;
        }


        public static bool UpdateLicenseClassInfo(string ClassName, string ClassDescription
               , byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees, clsOldLicenseClassInfo OldLicenseClassInfo, bool HasOldInfoChangedFully)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = _GetUpdateQuery(ClassName,ClassDescription,MinimumAllowedAge,DefaultValidityLength,ClassFees, OldLicenseClassInfo,HasOldInfoChangedFully);

            SqlCommand command = new SqlCommand(query, connection);

            if(OldLicenseClassInfo.ClassName == null)
            command.Parameters.AddWithValue("@ClassName", ClassName);

            if (OldLicenseClassInfo.ClassDescription == null)
                command.Parameters.AddWithValue("@ClassDescription", ClassDescription);

            if (OldLicenseClassInfo.MinimumAllowedAge == 0)
                command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);

            if (OldLicenseClassInfo.DefaultValidityLength == 0)
                command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);

            if (OldLicenseClassInfo.ClassFees == -1)
                command.Parameters.AddWithValue("@ClassFees", ClassFees);

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

    }
}
