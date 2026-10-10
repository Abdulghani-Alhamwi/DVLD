using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLDDataAccessLayer
{
    public class clsApplicationTypesData
    {
        private static readonly string _PrimaryKeyColumnName = "ApplicationTypeID";

        private enum _enUpdatableColumns : byte { ApplicationTypeTitle, ApplicationTypeFees }

        public class clsOldApplicationTypeData
        {
            public string ApplicationTypeTitle;
            public decimal ApplicationTypeFees;

            public clsOldApplicationTypeData(string ApplicationTypeTitle, decimal ApplicationTypeFees)
            {
                this.ApplicationTypeTitle = ApplicationTypeTitle;
                this.ApplicationTypeFees = ApplicationTypeFees;
            }
        }

        public static DataTable GetApplicationTypes()
        {
            DataTable dtApplicationTypes = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes_View";

            SqlCommand command = new SqlCommand(query,connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.HasRows)
                {
                    dtApplicationTypes = new DataTable();
                    dtApplicationTypes.Load(reader);
                }
                reader.Close();
            }

            catch { }

            finally
            {
                connection.Close();
            }
            return dtApplicationTypes;
        }

        private static void _ResetChangedOldValues(string ApplicationTypeTitle, decimal ApplicationTypeFees, clsOldApplicationTypeData OldApplicationTypeData)
        {
            if (ApplicationTypeTitle != OldApplicationTypeData.ApplicationTypeTitle)
                OldApplicationTypeData.ApplicationTypeTitle = null;

            if (ApplicationTypeFees != OldApplicationTypeData.ApplicationTypeFees)
                OldApplicationTypeData.ApplicationTypeFees = -1;
        }

        private static string _GetColumnValueSetPartForUpdate(_enUpdatableColumns UpdatableColumn)
        {
            switch (UpdatableColumn)
            {
                case _enUpdatableColumns.ApplicationTypeTitle:
                    return " ApplicationTypeTitle = @Title";

                case _enUpdatableColumns.ApplicationTypeFees:
                    return " ApplicationFees = @Fees";
            }

            return null;
        }

        private static string _GetUpdateQuery(string ApplicationTypeTitle, decimal ApplicationTypeFees, clsOldApplicationTypeData OldApplicationTypeData, bool HasOldDataChangedFully)
        {
            string query = "UPDATE ApplicationTypes SET";

            if (!HasOldDataChangedFully)
            {
                if (ApplicationTypeTitle != OldApplicationTypeData.ApplicationTypeTitle)
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ApplicationTypeTitle);
                }

                else
                {
                    query += _GetColumnValueSetPartForUpdate(_enUpdatableColumns.ApplicationTypeFees);
                }

            }

            else
                query += @" ApplicationTypeTitle = @Title, ApplicationFees = @Fees";

            query += $" WHERE {_PrimaryKeyColumnName} = @ID";

            _ResetChangedOldValues(ApplicationTypeTitle, ApplicationTypeFees, OldApplicationTypeData);

            return query;
        }

        public static bool UpdateApplicationType(byte ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationTypeFees, clsOldApplicationTypeData OldApplicationTypeData, bool HasOldDataChangedFully)
        {
            byte AffectedRows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = _GetUpdateQuery(ApplicationTypeTitle, ApplicationTypeFees, OldApplicationTypeData, HasOldDataChangedFully);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ApplicationTypeID);

            if (OldApplicationTypeData.ApplicationTypeTitle == null)
                command.Parameters.AddWithValue("@Title", ApplicationTypeTitle);

            if (OldApplicationTypeData.ApplicationTypeFees == -1)
                command.Parameters.AddWithValue("@Fees", ApplicationTypeFees);

            try
            {
                connection.Open();
                AffectedRows = Convert.ToByte(command.ExecuteNonQuery());
            }

            catch (Exception ex) { Console.Write(ex.Message); }

            finally
            {
                connection.Close();
            }

            return (AffectedRows > 0);
        }

        public static decimal GetApplicationTypeFees(byte ApplicationTypeID)
        {
            decimal ApplicationTypeFees = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT ApplicationFees FROM ApplicationTypes
                              WHERE {_PrimaryKeyColumnName} = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    ApplicationTypeFees = Convert.ToDecimal(result);

            }

            catch { }

            finally
            {
                connection.Close();
            }
            return ApplicationTypeFees;
        }

        public static string GetApplicationTypeTitle(byte ApplicationTypeID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = $@"SELECT ApplicationTypeTitle FROM ApplicationTypes
                              WHERE {_PrimaryKeyColumnName} = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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
    }
}
