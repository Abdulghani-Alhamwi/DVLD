using System;
using System.Data;
using System.Data.SqlClient;

namespace Utility_Library
{
    public class clsSqlQueryBuilder
    {
        /// <summary>
        /// Returns a datatable contains sorted info based on your query and sended arguments, the query must be designed to bring the sorted info , this method is for structure only in order to avoid repeating code and the sorted query must be sended from you.
        /// </summary>
        public static DataTable GetSortedInfoFromYourQueryAndArgs(string ConnectionString, string Query, byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection,
           string ColumnNameToFilterBy, string ValueToFilterBy, char? WildChar = null, SqlConnection CustomConnection = null, SqlCommand CustomCommand = null)
        {
            DataTable dtSortedData = null;

            SqlConnection Connection;
            SqlCommand Command = null;

            if (CustomCommand == null)
            {
                Connection = new SqlConnection(ConnectionString);

                Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@WantedNumOfRecords", WantedNumOfRecords);

                if (ValueToFilterBy != null)
                    Command.Parameters.AddWithValue("@Value", ValueToFilterBy);

                if (WildChar != null)
                    Command.Parameters.AddWithValue("@WildChar", WildChar);
            }

            else
                Connection = CustomConnection;

            try
            {
                Connection.Open();

                SqlDataReader reader;
                if (CustomCommand == null)
                    reader = Command.ExecuteReader();
                else
                    reader = CustomCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    dtSortedData = new DataTable();
                    dtSortedData.Load(reader);
                }

                reader.Close();
            }

            catch { }

            finally
            {
                Connection.Close();
            }

            return dtSortedData;
        }

        /// <summary>
        /// Returns a datatable contains sorted info based on your query and sended arguments, the query must be designed to bring the sorted info , this method is for structure only in order to avoid repeating code and the sorted query must be sended from you.
        /// </summary>
        public static DataTable GetSortedInfoFromYourQueryAndArgs(SqlConnection CustomConnection, SqlCommand CustomCommand)
        {
            return GetSortedInfoFromYourQueryAndArgs(null, null, 0, null, null, null, null, null, CustomConnection, CustomCommand);
        }

        /// <summary>
        /// Returns a datatable contains sorted info based on your query and sended arguments, the query must be designed to bring the sorted info , this method is for structure only in order to avoid repeating code and the sorted query must be sended from you.
        /// </summary>
        public static DataTable GetSortedInfoFromYourQueryAndArgs(string ConnectionString, string Query, byte WantedNumOfRecords, string ColumnNameToOrderBy, string SortDirection, string ValueToFilterBy = null)
        {
            return GetSortedInfoFromYourQueryAndArgs(ConnectionString, Query, WantedNumOfRecords, ColumnNameToOrderBy, SortDirection, null, ValueToFilterBy, null);
        }

        private static string _GetLastQueryPartWithOrderByPart(string PrimaryKeyToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                bool PreviousConditionMayExists, int LastBroughtID, bool HasOrderColumnNameWhiteSpaces, char ComparisonOperator, bool HasPrimaryKeyColumnWhiteSpaces)
        {
            if (HasPrimaryKeyColumnWhiteSpaces)
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}
                                  ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }

                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}
                                 ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}
                     ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                  ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                }
            }

            else
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                  ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }

                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                 ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";

                        else
                            return $" ORDER BY [{ColumnNameToOrderBy}] {SortDirection}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                     ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}
                                  ORDER BY {ColumnNameToOrderBy} {SortDirection}";

                        else
                            return $" ORDER BY {ColumnNameToOrderBy} {SortDirection}";
                    }
                }
            }
        }

        private static string _GetQueryPartWithWhereClauseOnly(string PrimaryKeyToFilterBy, string ColumnNameToOrderBy, string SortDirection,
             bool PreviousConditionMayExists, int LastBroughtID, bool HasOrderColumnNameWhiteSpaces, char ComparisonOperator, bool HasPrimaryKeyColumnWhiteSpaces)
        {
            if (HasPrimaryKeyColumnWhiteSpaces)
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND [{PrimaryKeyToFilterBy}] {ComparisonOperator} {LastBroughtID}";
                    }
                }
            }

            else
            {
                if (HasOrderColumnNameWhiteSpaces)
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                }

                else
                {
                    if (!PreviousConditionMayExists)
                    {
                        if (LastBroughtID != -1)
                            return $@" WHERE {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                    else
                    {
                        if (LastBroughtID != -1)
                            return $@" AND {PrimaryKeyToFilterBy} {ComparisonOperator} {LastBroughtID}";
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Returns last query part with condition if there was brought id then order by sended column or directly if there was no last brought id then send -1 instead and by that it returns the last query part order by sended column
        /// </summary>
        public static string GetLastQueryPart(string PrimaryKeyToFilterBy, string ColumnNameToOrderBy, string SortDirection,
                bool PreviousConditionMayExists, int LastBroughtID = -1, bool HasOrderColumnNameWhiteSpaces = true, bool ResultWithOrderByClause = false, bool HasPrimaryKeyColumnWhiteSpaces = false)
        {
            char ComparisonOperator = (SortDirection == "Desc") ? '<' : '>';

            if (ResultWithOrderByClause)
            {
                return _GetLastQueryPartWithOrderByPart(PrimaryKeyToFilterBy, ColumnNameToOrderBy, SortDirection, PreviousConditionMayExists,
                    LastBroughtID, HasOrderColumnNameWhiteSpaces, ComparisonOperator, HasPrimaryKeyColumnWhiteSpaces);
            }

            else
            {
                return _GetQueryPartWithWhereClauseOnly(PrimaryKeyToFilterBy, ColumnNameToOrderBy, SortDirection, PreviousConditionMayExists,
                     LastBroughtID, HasOrderColumnNameWhiteSpaces, ComparisonOperator, HasPrimaryKeyColumnWhiteSpaces);
            }
        }

        /// <summary>
        /// Returns last query part with condition if there was brought id then order by sended column or directly if there was no last brought id then send -1 instead and by that it returns the last query part order by sended column
        /// </summary
        public static string GetLastSortQueryPart(string ColumnNameToOrderBy, string SortDirection, bool HasOrderColumnNameWhiteSpaces = true)
        {
            return GetLastQueryPart(null, ColumnNameToOrderBy, SortDirection, false, -1, HasOrderColumnNameWhiteSpaces, true);
        }

        /// <summary>
        /// Returns last part of the query that is used to bring table data and that part is the order by part.
        /// </summary
        public static string GetOrderByQueryPart(string LastColumnNameDataOrderedBy, string SortDirection, bool HasOrderColumnNameWhiteSpaces = true)
        {
            return GetLastQueryPart(null, LastColumnNameDataOrderedBy, SortDirection, false, -1, HasOrderColumnNameWhiteSpaces, true);
        }

        /// <summary>
        /// Returns last query part which is the where clause part for filteration based on a value , the WildChar if sended it will be used after the value to bring specified pattern.
        /// </summary
        public static string GetFilterQueryPart_ValueCondition(string ColumnNameToFilterBy, char? WildChar = null, bool HasColumnWhiteSpaces = false)
        {
            if (HasColumnWhiteSpaces)
            {
                if (WildChar == null)
                    return $" WHERE [{ColumnNameToFilterBy}] = @Value";
                else
                    return $" WHERE [{ColumnNameToFilterBy}] Like @Value + @WildChar";
            }

            else
            {
                if (WildChar == null)
                    return $" WHERE {ColumnNameToFilterBy} = @Value";
                else
                    return $" WHERE {ColumnNameToFilterBy} Like @Value + @WildChar";
            }
        }

        public static string GetYesNoValueAsNumericString(string FilterValue)
        {
            return (FilterValue == "Yes") ? "1" : "0";
        }

        /// <summary>
        /// Returns offset pagination query part to add to your main query , the returned part is : " OFFSET @NumberOfRowsToOffset ROWS FETCH NEXT @WantedNumOfRecords ROWS ONLY"
        /// </summary
        public static string GetOffsetPaginationQueryPart()
        {
            return " OFFSET @NumberOfRowsToOffset ROWS FETCH NEXT @WantedNumOfRecords ROWS ONLY";
        }
    }
}
