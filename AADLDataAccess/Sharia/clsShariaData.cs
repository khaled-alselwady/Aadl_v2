using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AADL_DataAccess.HelperClasses;

namespace AADLDataAccess.Sharia
{
    public class clsShariaData
    {
   
        public static bool GetShariaInfoByPractitionerID(int InputPractitionerID, ref int ShariaID, ref string ShariaLicenseNumber
            , ref int PersonID, ref bool IsLawyer, ref DateTime IssueDate, ref int? LastEditByUserID,
            ref int SubscriptionTypeID, ref int SubscriptionWayID, ref int CreatedByUserID, ref bool IsActive,
           ref Dictionary<int, string> CasesShariaPracticesIDNameDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetShariaInfoByPractitionerID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PractitionerID", InputPractitionerID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Read first result set

                                if (reader.Read())
                                {
                                    // The record was found
                                    isFound = true;
                                    PersonID = (int)reader["PersonID"];
                                    IsLawyer = (bool)reader["IsLawyer"];
                                    SubscriptionTypeID = (int)reader["SubscriptionTypeID"];
                                    SubscriptionWayID = (int)reader["SubscriptionWayID"];
                                }

                                // Read second result set
                                reader.NextResult();
                                if (reader.Read())
                                {
                                    // The record was found
                                    isFound = true;
                                    ShariaID = (int)reader["ShariaID"];
                                    ShariaLicenseNumber = (string)reader["ShariaLicenseNumber"];
                                    IssueDate = (DateTime)reader["IssueDate"];
                                    LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID = (int)reader["LastEditByUserID"] : null;
                                    CreatedByUserID = (int)reader["CreatedByUserID"];
                                    IsActive = (bool)reader["IsActive"];
                                }

                                // Read second result set
                                reader.NextResult();
                                while (reader.Read())
                                {
                                    // Process the data from RegulatorsCasesPractice table
                                    int regulatoryCaseTypeId = reader.GetInt32(reader.GetOrdinal("ShariaCaseTypeID"));
                                    string regulatoryCaseTypeName = reader.GetString(reader.GetOrdinal("ShariaCaseTypeName"));

                                    CasesShariaPracticesIDNameDictionary.Add(regulatoryCaseTypeId, regulatoryCaseTypeName);
                                }
                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
                            }
                        }

                    }
                    catch (SqlException ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsShaira class, get info by PractitionerID():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                    catch (Exception ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsShaira class, get info by PractitionerID():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }

            }

            return isFound;
        }

        /// <summary>
        ///Can create a new Sharia profile in database, and verify if it has a practitioner profile  or not .
        ///by default it is lawyer=true 
        /// <returns> List of three New IDs ([0]NewRegulatorID, [1]NewPractitionerID)</returns>
        public static (int NewShariaID, int NewPractitionerID) AddNewSharia(int PersonID,string ShariaLicenseNumber, 
            int SubscriptionTypeID, int SubscriptionWayID, int CreatedByUserID, bool IsActive,
          Dictionary<int, string> CasesShariaPracticesIDNameDictionary)
        {
            int ShariaID = -1,PractitionerID=-1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewSharia", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@ShariaLicenseNumber", ShariaLicenseNumber);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);
                    command.Parameters.AddWithValue("@SubscriptionWayID", SubscriptionWayID);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);


                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var ShariaCasesTable = new DataTable();
                    ShariaCasesTable.Columns.Add("CaseID", typeof(int));
                    foreach (int ShariaCaseID in CasesShariaPracticesIDNameDictionary.Keys)
                    {
                        ShariaCasesTable.Rows.Add(ShariaCaseID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@ShariaCasesPracticesIds", ShariaCasesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name


                    SqlParameter outputIdParam = new SqlParameter("@NewShariaID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputIdParam2 = new SqlParameter("@OutputPractitionerID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
      

                    command.Parameters.Add(outputIdParam);
                    command.Parameters.Add(outputIdParam2);

                    try
                    {
                        connection.Open();

                        command.ExecuteNonQuery();
                        ShariaID = (int)command.Parameters["@NewShariaID"].Value;
                        PractitionerID = (int)command.Parameters["@OutputPractitionerID"].Value;

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("sharia data access class , add to database method()\nSQL EXCEPTION:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);

                        Console.WriteLine("SQL Error occurred: " + ex.Message);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("sharia data access class , add to database method()\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);

                        Console.WriteLine();
                    }
                }

            }

            return (ShariaID,PractitionerID);

        }
        /// <summary>
        /// I passed the only available  or allowed parameter for updating 
        /// </summary>
        /// <returns>Is it updated successfully or not </returns>
        public static bool UpdateSharia(int ShariaID,int PractitionerID, string ShariaLicenseNumber, 
            int SubscriptionTypeID, int SubscriptionWayID, bool IsActive,
           int? LastEditByUserID, Dictionary<int, string> CasesShariaPracticesIDNameDictionary)
        {

            int EffectedRows = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateSharia", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@ShariaID", ShariaID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@ShariaLicenseNumber", ShariaLicenseNumber);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);
                    command.Parameters.AddWithValue("@SubscriptionWayID", SubscriptionWayID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@LastEditByUserID", LastEditByUserID);


                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var CasesTable = new DataTable();
                    CasesTable.Columns.Add("CaseID", typeof(int));
                    foreach (int CaseID in CasesShariaPracticesIDNameDictionary.Keys)
                    {
                        CasesTable.Rows.Add(CaseID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@ShariaCasesPracticesIds", CasesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name


                    try
                    {
                        connection.Open();
                        EffectedRows= command.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("sharia data access class , update  method()\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        return false;
                    }
                }

            }

            return EffectedRows>0;

        }

        [Obsolete("Not Implemented yet.")]
        public async static Task<DataTable> GetAllShariasAsync()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {

                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of Regulators class , where data grid view load all people method dropped:\n"
                            + ex.Message, EventLogEntryType.Error);
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }

            }

            return dt;

        }

        public static bool DeletePermanently(int? ShariaID)
           => clsDataAccessHelper.Delete("SP_DeleteShariaPermanently", "ShariaID", ShariaID);

        public static bool DeleteSoftly(int? ShariaID)
        => clsDataAccessHelper.Delete("SP_DeleteShariaSoftly", "ShariaID", ShariaID);

        public static bool IsShariaExistByPersonID(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsShariaExistsByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    try
                    {
                        connection.Open();

                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();

                        isFound = (int)returnParameter.Value == 1;



                    }

                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("clsPerson Data access layer, is exists(RegulatorID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }
        public static bool IsShariaExistByShariaID(int ShariaID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsShariaExistsByShariaID", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ShariaID", ShariaID);
                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    try
                    {
                        connection.Open();

                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();

                        isFound = (int)returnParameter.Value == 1;



                    }

                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Regulator Data access layer, is exists(PersonID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }
        public static bool IsShariaExistByShariaLicenseNumber(string ShariaLicenseNumber)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsShariaExistsByShariaLicenseNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@ShariaLicenseNumber", ShariaLicenseNumber);
                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    try
                    {
                        connection.Open();

                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();

                        isFound = (int)returnParameter.Value == 1;



                    }

                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Regulator Data access layer, is exists(PersonID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }

        public static bool IsShariaExistByLawyerID(int LawyerID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsShariaExistsByLawyerID", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@LawyerID", LawyerID);
                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    try
                    {
                        connection.Open();

                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();

                        isFound = (int)returnParameter.Value == 1;



                    }

                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Regulator Data access layer, is exists(LawyerID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }

        public static bool IsShariaExistByPractitionerID(int PractitionerID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsShariaExistsByPractitionerID", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    try
                    {
                        connection.Open();

                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();

                        isFound = (int)returnParameter.Value == 1;



                    }

                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Regulator Data access layer, is exists(LawyerID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }


        /// <summary>
        /// When black list delete it , it has ability to handle lawyers profiles.
        /// </summary>
        /// <param name="ListTypeID"> just for white,closed type</param>
        /// <returns></returns>
        [Obsolete("Don't use it i am not sure from its results yet.")]
        public static bool UpdateRegulatorList(int RegulatorID, int ListID, int ListTypeID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateRegulatorList", connection))
                {

                    command.Parameters.AddWithValue("@RegulatorID", RegulatorID);
                    command.Parameters.AddWithValue("@ListID", ListID);
                    command.Parameters.AddWithValue("@ListTypeID", ListTypeID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Problem happened in regulator class while trying to cancel blackList ()\n" + ex.Message,
                            EventLogEntryType.Error);
                    }
                }
            }

            return (rowsAffected > 0);

        }

    }

}
