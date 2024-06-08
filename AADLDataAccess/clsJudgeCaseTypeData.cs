using AADL_DataAccess;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace AADLDataAccess.Judger
{
    public static class clsJudgeCaseTypeData
    {
        public static bool GetJudgeCaseTypeInfoByCaseTypeID(int JudgeCaseTypeID, ref string JudgeCaseTypeName, ref int CreatedByAdminID, ref int? LastEditByAdminID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetJudgeCaseTypeInfoByCaseTypeID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@JudgeCaseTypeID", JudgeCaseTypeID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                JudgeCaseTypeName = (string)reader["JudgeCaseTypeName"];
                                CreatedByAdminID = (int)reader["CreatedByAdminID"];
                                LastEditByAdminID = reader["LastEditByAdminID"] as int?;

                                isFound = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Review your clsJudgeCaseTypeData class data access layer ,get case type info by ID\n Exception:" +
                    ex.Message, EventLogEntryType.Error);

                isFound = false;
            }


            return isFound;
        }

        public static bool GetJudgeCaseTypeInfoByCaseTypeName(string JudgeCaseTypeName, ref int JudgeCaseTypeID, ref int CreatedByAdminID, ref int? LastEditByAdminID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetJudgeCaseTypeInfoByCaseTypeName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@JudgeCaseTypeName", JudgeCaseTypeName);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                JudgeCaseTypeID = (int)reader["JudgeCaseTypeID"];
                                CreatedByAdminID = (int)reader["CreatedByAdminID"];
                                LastEditByAdminID = reader["LastEditByAdminID"] as int?;

                                isFound = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Review your clsJudgeCaseTypeData class data access layer ,get case type info by ID\n Exception:" +
                    ex.Message, EventLogEntryType.Error);

                isFound = false;
            }

            return isFound;
        }

        public static int AddNewJudgeCaseType(string JudgeCaseTypeName, int CreatedByAdminID)
        {
            int NewJudgeCaseTypeID = -1;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_AddNewJudgeCaseType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@JudgeCaseTypeName", JudgeCaseTypeName);
                        command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);

                        SqlParameter outputIdParam = new SqlParameter("@NewJudgeCaseTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        connection.Open();
                        command.ExecuteNonQuery();

                        NewJudgeCaseTypeID = (int)command.Parameters["@NewJudgeCaseTypeID"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsJudgeCaseTypeData class DataAccess add new Case type method:\t" + ex.Message,
                    EventLogEntryType.Error);

                NewJudgeCaseTypeID = -1;
            }

            return NewJudgeCaseTypeID;
        }

        public static bool UpdateJudgeCaseType(int? JudgeCaseTypeID, string JudgeCaseTypeName, int LastEditByAdminID)
        {
            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_UpdateJudgeCaseType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@JudgeCaseTypeName", JudgeCaseTypeName);
                        command.Parameters.AddWithValue("@JudgeCaseTypeID", JudgeCaseTypeID);
                        command.Parameters.AddWithValue("@LastEditByAdminID", LastEditByAdminID);

                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Exception Message From JudgeCaseType dataAccess  class update method :\t" + ex.Message,
                    EventLogEntryType.Error);

                rowsAffected = 0;
            }

            return rowsAffected > 0;
        }

        public static bool DeleteJudgeCaseType(int JudgeCaseTypeID)
        {

            int rowsAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_DeleteJudgeCaseType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@JudgeCaseTypeID", JudgeCaseTypeID);

                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Exception Message From JudgeCaseType DataAccess  class delete method :\t" + ex.Message,
                    EventLogEntryType.Error);

                rowsAffected = 0;
            }


            return (rowsAffected > 0);

        }

        public static DataTable GetAllJudgeCaseTypes()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllJudgeCaseTypes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                                dt.Load(reader);
                        }
                    }
                }
            }

            catch (SqlException ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of JudgeCaseTypes class , where data grid view load all JudgeCaseTypes method dropped:\n"
                    + ex.Message, EventLogEntryType.Error);
            }

            return dt;
        }
    }
}
