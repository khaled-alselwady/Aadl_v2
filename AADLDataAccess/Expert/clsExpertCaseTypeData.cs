using AADL_DataAccess;
using AADL_DataAccess.HelperClasses;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AADLDataAccess.Expert
{
    public class clsExpertCaseTypeData
    {
        public static bool GetInfoByCaseTypeID(int? expertCaseTypeID, ref string expertCaseTypeName,
            ref int? createdByAdminID, ref int? lastEditByAdminID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExpertCaseTypeInfoByCaseTypeID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ExpertCaseTypeID", (object)expertCaseTypeID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                expertCaseTypeName = (string)reader["ExpertCaseTypeName"];
                                createdByAdminID = (reader["CreatedByAdminID"] != DBNull.Value) ? (int?)reader["CreatedByAdminID"] : null;
                                lastEditByAdminID = (reader["LastEditByAdminID"] != DBNull.Value) ? (int?)reader["LastEditByAdminID"] : null;
                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
                clsDataAccessHelper.HandleException(ex);
            }

            return isFound;
        }

        public static bool GetInfoByCaseTypeName(string expertCaseTypeName, ref int? expertCaseTypeID,
            ref int? createdByAdminID, ref int? lastEditByAdminID)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExpertCaseTypeInfoByCaseTypeName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ExpertCaseTypeName", (object)expertCaseTypeName ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                expertCaseTypeID = (reader["ExpertCaseTypeID"] != DBNull.Value) ? (int?)reader["ExpertCaseTypeID"] : null;
                                createdByAdminID = (reader["CreatedByAdminID"] != DBNull.Value) ? (int?)reader["CreatedByAdminID"] : null;
                                lastEditByAdminID = (reader["LastEditByAdminID"] != DBNull.Value) ? (int?)reader["LastEditByAdminID"] : null;
                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isFound = false;
                clsDataAccessHelper.HandleException(ex);
            }

            return isFound;
        }

        public static int? Add(string expertCaseTypeName,
            int createdByAdminID, int? lastEditByAdminID)
        {
            // This function will return the new person id if succeeded and null if not
            int? expertCaseTypeID = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_AddNewExpertCaseType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ExpertCaseTypeName", expertCaseTypeName);
                        command.Parameters.AddWithValue("@CreatedByAdminID", createdByAdminID);
                        command.Parameters.AddWithValue("@LastEditByAdminID", (object)lastEditByAdminID ?? DBNull.Value);

                        SqlParameter outputIdParam = new SqlParameter("@NewExpertCaseTypeID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdParam);

                        command.ExecuteNonQuery();

                        expertCaseTypeID = (int?)outputIdParam.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessHelper.HandleException(ex);
            }

            return expertCaseTypeID;
        }

        public static bool Update(int expertCaseTypeID,
            string expertCaseTypeName, int createdByAdminID, int? lastEditByAdminID)
        {
            int rowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_UpdateExpertCaseType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ExpertCaseTypeID", expertCaseTypeID);
                        command.Parameters.AddWithValue("@ExpertCaseTypeName", expertCaseTypeName);
                        command.Parameters.AddWithValue("@CreatedByAdminID", createdByAdminID);
                        command.Parameters.AddWithValue("@LastEditByAdminID", (object)lastEditByAdminID ?? DBNull.Value);

                        rowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                clsDataAccessHelper.HandleException(ex);
            }

            return (rowAffected > 0);
        }

        public static bool Delete(int? expertCaseTypeID)
        => clsDataAccessHelper.Delete("SP_DeleteExpertCaseType", "ExpertCaseTypeID", expertCaseTypeID);

        public static bool Exists(int? expertCaseTypeID)
        => clsDataAccessHelper.Exists("SP_DoesExpertCaseTypeExist", "ExpertCaseTypeID", expertCaseTypeID);

        public static DataTable All()
        => clsDataAccessHelper.All("SP_GetAllExpertCasesTypes");
    }
}