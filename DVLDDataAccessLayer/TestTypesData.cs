using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsTestTypesData
    {
        private static readonly string _PrimaryKeyColumnName = "TestTypeID";

        private enum enUpdatableColumns : byte
        {
            TestTypeTitle, TestTypeDescription, TestTypeFees
        }

        public class clsOldTestTypeData
        {
            public string TestTypeTitle;
            public string TestTypeDescription;
            public decimal TestTypeFees;

            public clsOldTestTypeData(string OldTestTypeTitle,string OldTestTypeDescription,
            decimal OldTestTypeFees)
            {
                this.TestTypeTitle = OldTestTypeTitle;
                this.TestTypeDescription = OldTestTypeDescription;
                this.TestTypeFees = OldTestTypeFees;
            }
        }

        public static DataTable GetTestTypes()
        {
            DataTable dtTestTypes = null;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT {_PrimaryKeyColumnName} AS ID, TestTypeTitle AS Title,
                              TestTypeDescription AS Description, TestTypeFees AS Fees FROM TestTypes";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtTestTypes = new DataTable();
                    dtTestTypes.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }

            return dtTestTypes;
        }

        private static void _ResetChangedOldValues(string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees, clsOldTestTypeData OldTestTypeData)
        {
            if (TestTypeTitle != OldTestTypeData.TestTypeTitle)
                OldTestTypeData.TestTypeTitle = null;

            if (TestTypeDescription != OldTestTypeData.TestTypeDescription)
                OldTestTypeData.TestTypeDescription = null;

            if (TestTypeFees != OldTestTypeData.TestTypeFees)
                OldTestTypeData.TestTypeFees = -1;
        }

        private static string _GetColumnValueSetPartForUpdate(enUpdatableColumns UpdatableColumn)
        {
            switch(UpdatableColumn)
            {
                case enUpdatableColumns.TestTypeTitle:
                    return " TestTypeTitle = @Title";

                case enUpdatableColumns.TestTypeDescription:
                    return " TestTypeDescription = @Description";

                case enUpdatableColumns.TestTypeFees:
                    return " TestTypeFees = @Fees";
            }

            return null;
        }

        private static string _GetUpdateQuery(string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees, clsOldTestTypeData OldTestTypeData,bool HasOldDataChangedFully)
        {
            string query = "UPDATE TestTypes SET";

            if (!HasOldDataChangedFully)
            {
                if (TestTypeTitle != OldTestTypeData.TestTypeTitle)
                {
                    query += _GetColumnValueSetPartForUpdate(enUpdatableColumns.TestTypeTitle);

                    if (TestTypeDescription != OldTestTypeData.TestTypeDescription)
                        query += "," + _GetColumnValueSetPartForUpdate(enUpdatableColumns.TestTypeDescription);

                    if (TestTypeFees != OldTestTypeData.TestTypeFees)
                        query += "," + _GetColumnValueSetPartForUpdate(enUpdatableColumns.TestTypeFees);
                }

                else if (TestTypeDescription != OldTestTypeData.TestTypeDescription)
                {
                    if (TestTypeDescription != OldTestTypeData.TestTypeDescription)
                        query += _GetColumnValueSetPartForUpdate(enUpdatableColumns.TestTypeDescription);

                    if (TestTypeFees != OldTestTypeData.TestTypeFees)
                        query += "," + _GetColumnValueSetPartForUpdate(enUpdatableColumns.TestTypeFees);
                }

                else
                    query += _GetColumnValueSetPartForUpdate(enUpdatableColumns.TestTypeFees);

            }

            else
                query += @" TestTypeTitle = @Title, TestTypeDescription = @Description, TestTypeFees = @Fees";

            query += $" WHERE {_PrimaryKeyColumnName} = @TestTypeID";

            _ResetChangedOldValues(TestTypeTitle, TestTypeDescription, TestTypeFees, OldTestTypeData);

            return query;
        }

        public static bool UpdateTestType(int TestTypeID,string TestTypeTitle,string TestTypeDescription,decimal TestTypeFees, clsOldTestTypeData OldTestTypeData,bool HasOldDataChangedFully)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = _GetUpdateQuery(TestTypeTitle, TestTypeDescription, TestTypeFees, OldTestTypeData, HasOldDataChangedFully);

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            if (OldTestTypeData.TestTypeTitle == null)
                command.Parameters.AddWithValue("@Title", TestTypeTitle);

            if (OldTestTypeData.TestTypeDescription == null)
                command.Parameters.AddWithValue("@Description", TestTypeDescription);

            if (OldTestTypeData.TestTypeFees == -1)
                command.Parameters.AddWithValue("@Fees", TestTypeFees);

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

        public static decimal GetTestTypeFees(byte TestTypeID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = $@"SELECT TestTypeFees FROM TestTypes WHERE {_PrimaryKeyColumnName} = @TestTypeId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

    }
}