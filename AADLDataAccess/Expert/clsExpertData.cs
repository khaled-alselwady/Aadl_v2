using AADL_DataAccess;
using AADL_DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AADLDataAccess.Expert
{
    public class clsExpertData
    {
        public static bool GetInfoByExpertID(int? expertID, ref int? practitionerID, ref int? personID, ref bool isLawyer,
            ref int? createdByUserID, ref DateTime issueDate, ref int? subscriptionTypeID, ref int? subscriptionWayID,
            ref bool isActive, ref int? lastEditByUserID,
            ref Dictionary<int, string> casesExpertPracticesIDNameDictionary)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExpertInfoByExpertID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ExpertID", (object)expertID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Read first result set

                                if (reader.Read())
                                {
                                    // The record was found
                                    isFound = true;
                                    isLawyer = (bool)reader["IsLawyer"];
                                    practitionerID = (reader["PractitionerID"] != DBNull.Value) ? (int?)reader["PractitionerID"] : null;
                                    subscriptionTypeID = (reader["SubscriptionTypeID"] != DBNull.Value) ? (int?)reader["SubscriptionTypeID"] : null;
                                    subscriptionWayID = (reader["SubscriptionWayID"] != DBNull.Value) ? (int?)reader["SubscriptionWayID"] : null;
                                }

                                // Read second result set
                                reader.NextResult();
                                if (reader.Read())
                                {
                                    isFound = true;
                                    personID = (reader["PersonID"] != DBNull.Value) ? (int?)reader["PersonID"] : null;
                                    createdByUserID = (reader["CreatedByUserID"] != DBNull.Value) ? (int?)reader["CreatedByUserID"] : null;
                                    issueDate = (DateTime)reader["IssueDate"];
                                    isActive = (bool)reader["IsActive"];
                                    lastEditByUserID = (reader["LastEditByUserID"] != DBNull.Value) ? (int?)reader["LastEditByUserID"] : null;
                                }

                                // Read second result set
                                reader.NextResult();
                                while (reader.Read())
                                {
                                    // Process the data from ExpertCasesPractice table
                                    int expertCaseTypeId = reader.GetInt32(reader.GetOrdinal("ExpertCaseTypeID"));
                                    string expertCaseTypeName = reader.GetString(reader.GetOrdinal("ExpertCaseTypeName"));

                                    casesExpertPracticesIDNameDictionary.Add(expertCaseTypeId, expertCaseTypeName);
                                }
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

        public static bool GetInfoByPersonID(int? personID, ref int? practitionerID, ref int? expertID, ref bool isLawyer,
           ref int? createdByUserID, ref DateTime issueDate, ref int? subscriptionTypeID, ref int? subscriptionWayID,
           ref bool isActive, ref int? lastEditByUserID,
           ref Dictionary<int, string> casesExpertPracticesIDNameDictionary)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExpertInfoByPersonID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PersonID", (object)personID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Read first result set

                                if (reader.Read())
                                {
                                    // The record was found
                                    isFound = true;
                                    isLawyer = (bool)reader["IsLawyer"];
                                    practitionerID = (reader["PractitionerID"] != DBNull.Value) ? (int?)reader["PractitionerID"] : null;
                                    subscriptionTypeID = (reader["SubscriptionTypeID"] != DBNull.Value) ? (int?)reader["SubscriptionTypeID"] : null;
                                    subscriptionWayID = (reader["SubscriptionWayID"] != DBNull.Value) ? (int?)reader["SubscriptionWayID"] : null;
                                }

                                // Read second result set
                                reader.NextResult();
                                if (reader.Read())
                                {
                                    isFound = true;
                                    expertID = (reader["ExpertID"] != DBNull.Value) ? (int?)reader["ExpertID"] : null;
                                    createdByUserID = (reader["CreatedByUserID"] != DBNull.Value) ? (int?)reader["CreatedByUserID"] : null;
                                    issueDate = (DateTime)reader["IssueDate"];
                                    isActive = (bool)reader["IsActive"];
                                    lastEditByUserID = (reader["LastEditByUserID"] != DBNull.Value) ? (int?)reader["LastEditByUserID"] : null;
                                }

                                // Read second result set
                                reader.NextResult();
                                while (reader.Read())
                                {
                                    // Process the data from ExpertCasesPractice table
                                    int expertCaseTypeId = reader.GetInt32(reader.GetOrdinal("ExpertCaseTypeID"));
                                    string expertCaseTypeName = reader.GetString(reader.GetOrdinal("ExpertCaseTypeName"));

                                    casesExpertPracticesIDNameDictionary.Add(expertCaseTypeId, expertCaseTypeName);
                                }
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

        public static bool GetInfoByPractitionerID(int? practitionerID, ref int? expertID, ref int? personID, ref bool isLawyer,
            ref int? createdByUserID, ref DateTime issueDate, ref int? subscriptionTypeID, ref int? subscriptionWayID,
            ref bool isActive, ref int? lastEditByUserID,
            ref Dictionary<int, string> casesExpertPracticesIDNameDictionary)
        {
            bool isFound = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetExpertInfoByPractitionerID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@PractitionerID", (object)practitionerID ?? DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Read first result set

                                if (reader.Read())
                                {
                                    // The record was found
                                    isFound = true;
                                    personID = (reader["PersonID"] != DBNull.Value) ? (int?)reader["PersonID"] : null;
                                    isLawyer = (bool)reader["IsLawyer"];
                                    subscriptionTypeID = (reader["SubscriptionTypeID"] != DBNull.Value) ? (int?)reader["SubscriptionTypeID"] : null;
                                    subscriptionWayID = (reader["SubscriptionWayID"] != DBNull.Value) ? (int?)reader["SubscriptionWayID"] : null;
                                }

                                // Read second result set
                                reader.NextResult();
                                if (reader.Read())
                                {
                                    isFound = true;
                                    expertID = (reader["ExpertID"] != DBNull.Value) ? (int?)reader["ExpertID"] : null;
                                    createdByUserID = (reader["CreatedByUserID"] != DBNull.Value) ? (int?)reader["CreatedByUserID"] : null;
                                    issueDate = (DateTime)reader["IssueDate"];
                                    isActive = (bool)reader["IsActive"];
                                    lastEditByUserID = (reader["LastEditByUserID"] != DBNull.Value) ? (int?)reader["LastEditByUserID"] : null;
                                }

                                // Read second result set
                                reader.NextResult();
                                while (reader.Read())
                                {
                                    // Process the data from ExpertCasesPractice table
                                    int expertCaseTypeId = reader.GetInt32(reader.GetOrdinal("ExpertCaseTypeID"));
                                    string expertCaseTypeName = reader.GetString(reader.GetOrdinal("ExpertCaseTypeName"));

                                    casesExpertPracticesIDNameDictionary.Add(expertCaseTypeId, expertCaseTypeName);
                                }
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

        public static (int NewExpertID, int NewPractitionerID) Add(int PersonID,
            int SubscriptionTypeID, int SubscriptionWayID, int CreatedByUserID, bool IsActive,
            Dictionary<int, string> CasesExpertPracticesIDNameDictionary)
        {
            int ExpertID = -1, PractitionerID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewExpert", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);
                    command.Parameters.AddWithValue("@SubscriptionWayID", SubscriptionWayID);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var CasesTable = new DataTable();
                    CasesTable.Columns.Add("CaseID", typeof(int));
                    foreach (int CaseID in CasesExpertPracticesIDNameDictionary.Keys)
                    {
                        CasesTable.Rows.Add(CaseID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@ExpertCasesPracticesIds", CasesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name


                    SqlParameter outputIdParam = new SqlParameter("@NewExpertID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);
                    SqlParameter outputIdParam2 = new SqlParameter("@OUTPUTPractitionerID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam2);

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();
                        ExpertID = (int)command.Parameters["@NewExpertID"].Value;
                        PractitionerID = (int)command.Parameters["@OUTPUTPractitionerID"].Value;

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessHelper.HandleException(ex);
                    }
                }

            }

            return (ExpertID, PractitionerID);
        }

        public static bool Update(int expertID, int PractitionerID,
          int? LastEditByUserID, int SubscriptionTypeID, int SubscriptionWayID, bool IsActive,
           Dictionary<int, string> CasesRegulatorPracticesIDNameDictionary)
        {

            int TotalEffectedRows = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateExpert", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ExpertID", expertID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);
                    command.Parameters.AddWithValue("@SubscriptionWayID", SubscriptionWayID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@LastEditByUserID", (object)LastEditByUserID ?? DBNull.Value);

                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var CasesTable = new DataTable();
                    CasesTable.Columns.Add("CaseID", typeof(int));
                    foreach (int CaseID in CasesRegulatorPracticesIDNameDictionary.Keys)
                    {
                        CasesTable.Rows.Add(CaseID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@ExpertCasesPracticesIds", CasesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name


                    try
                    {
                        connection.Open();
                        TotalEffectedRows = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        clsDataAccessHelper.HandleException(ex);
                    }
                }

            }

            return TotalEffectedRows > 0;

        }

        public static bool DeletePermanently(int? expertID)
        => clsDataAccessHelper.Delete("SP_DeleteExpertPermanently", "ExpertID", expertID);

        public static bool DeleteSoftly(int? expertID)
        => clsDataAccessHelper.Delete("SP_DeleteExpertSoftly", "ExpertID", expertID);

        public static bool ExistsByExpertID(int? expertID)
        => clsDataAccessHelper.Exists("SP_DoesExpertExistByExpertID", "ExpertID", expertID);

        public static bool ExistsByPersonID(int? personID)
        => clsDataAccessHelper.Exists("SP_DoesExpertExistByPersonID", "PersonID", personID);

        public static bool ExistsByPractitionerID(int? practitionerID)
        => clsDataAccessHelper.Exists("SP_DoesExpertExistByPractitionerID", "PractitionerID", practitionerID);

        public static DataTable All()
        => clsDataAccessHelper.All("SP_GetAllExperts");
    }
}