using AADL_DataAccess;
using AADL_DataAccess.HelperClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace AADLDataAccess
{
    /// <summary>
    /// To Stored_Procedure not ,yet.
    /// </summary>
    public class clsRegulatorData
    {

        public static bool GetRegulatorInfoByRegulatorID(int RegulatorID,ref int PersonID,ref string MemberShipNumber,
            ref bool IsLawyer,ref int PractitionerID, ref DateTime IssueDate,ref int? LastEditByUserID,
            ref int SubscriptionTypeID,ref int SubscriptionWayID,ref int CreatedByUserID,ref bool IsActive,
           ref Dictionary<int, string> CasesRegulatorPracticesIDNameDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetRegulatorInfoByRegulatorID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                  
                    command.Parameters.AddWithValue("@RegulatorID", RegulatorID);

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
                                    IsLawyer = (bool)reader["IsLawyer"];
                                    PractitionerID = (int)reader["PractitionerID"];
                                    SubscriptionTypeID = (int)reader["SubscriptionTypeID"];
                                    SubscriptionWayID = (int)reader["SubscriptionWayID"];
                                }

                                // Read second result set
                                reader.NextResult();
                                if (reader.Read())
                                {
                                    // The record was found
                                    isFound = true;
                                    RegulatorID = (int)reader["RegulatorID"];
                                    MemberShipNumber = (string)reader["MemberShipNumber"];
                                    IssueDate = (DateTime)reader["IssueDate"];
                                    LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID=(int)reader["LastEditByUserID"] : null;
                                    CreatedByUserID = (int)reader["CreatedByUserID"];
                                    IsActive = (bool)reader["IsActive"];
                                    PersonID = (int)reader["PersonID"];
                                }

                                // Read second result set
                                reader.NextResult();
                                while (reader.Read())
                                {
                                    // Process the data from RegulatorsCasesPractice table
                                    int regulatoryCaseTypeId = reader.GetInt32(reader.GetOrdinal("RegulatoryCaseTypeID"));
                                    string regulatoryCaseTypeName = reader.GetString(reader.GetOrdinal("RegulatoryCaseTypeName"));

                                    CasesRegulatorPracticesIDNameDictionary.Add(regulatoryCaseTypeId, regulatoryCaseTypeName);
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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by id():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                    catch (Exception ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by id():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }

            }

            return isFound;
        }

        [Obsolete ("Don't use it ain't ready for use.")]
        public static bool GetRegulatorInfoByPersonID(int PersonID, ref int RegulatorID, ref string MemberShipNumber,
            ref bool IsLawyer, ref int PractitionerID, ref DateTime IssueDate,ref  int? LastEditByUserID, 
            ref int SubscriptionTypeID, ref int SubscriptionWayID, ref int CreatedByUserID, ref bool IsActive,
            ref Dictionary<int, string> CasesRegulatorPracticesIDNameDictionary)

        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetRegulatorInfoByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);

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
                                    PractitionerID = (int)reader["PractitionerID"];
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
                                    RegulatorID = (int)reader["RegulatorID"];
                                    IssueDate = (DateTime)reader["IssueDate"];
                                    MemberShipNumber = (string)reader["MemberShipNumber"];
                                    LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID = (int)reader["LastEditByUserID"] : null;
                                    CreatedByUserID = (int)reader["CreatedByUserID"];
                                    IsActive = (bool)reader["IsActive"];
                                }

                                // Read second result set
                                reader.NextResult();
                                while (reader.Read())
                                {
                                    // Process the data from RegulatorsCasesPractice table
                                    int regulatoryCaseTypeId = reader.GetInt32(reader.GetOrdinal("RegulatoryCaseTypeID"));
                                    string regulatoryCaseTypeName = reader.GetString(reader.GetOrdinal("RegulatoryCaseTypeName"));

                                    CasesRegulatorPracticesIDNameDictionary.Add(regulatoryCaseTypeId, regulatoryCaseTypeName);
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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by person():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                    catch (Exception ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by preson():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }

                }

            }

            return isFound;
        }

        public static bool GetRegulatorInfoByMemberShipNumber(string MemberShipNumber, ref int PersonID, ref int RegulatorID, ref int PractitionerID, 
            ref bool IsLawyer,ref DateTime IssueDate, ref int? LastEditByUserID,  
            ref int SubscriptionTypeID, ref int SubscriptionWayID,ref int CreatedByUserID, ref bool IsActive,
             ref Dictionary<int,string> CasesRegulatorPracticesIDNameDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {



                using (SqlCommand command = new SqlCommand("SP_GetRegulatorInfoByMemberShipNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MemberShipNumber", MemberShipNumber);

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
                                    PractitionerID = (int)reader["PractitionerID"];
                                    IsLawyer = (bool)reader["IsLawyer"];
                                    PersonID = (int)reader["PersonID"];
                                    SubscriptionTypeID = (int)reader["SubscriptionTypeID"];
                                    SubscriptionWayID = (int)reader["SubscriptionWayID"];
                                }

                                // Read second result set
                                reader.NextResult();
                                if (reader.Read())
                                {
                                    // The record was found
                                    isFound = true;
                                    RegulatorID = (int)reader["RegulatorID"];
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
                                    int regulatoryCaseTypeId = reader.GetInt32(reader.GetOrdinal("RegulatoryCaseTypeID"));
                                    string regulatoryCaseTypeName = reader.GetString(reader.GetOrdinal("RegulatoryCaseTypeName"));

                                    CasesRegulatorPracticesIDNameDictionary.Add(regulatoryCaseTypeId, regulatoryCaseTypeName);
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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by mebmer():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                    catch (Exception ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by mebmer():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }


                }

            }

            return isFound;
        }

        public static bool GetRegulatorInfoByPractitionerID(int InputPractitionerID, ref int PersonID, ref int RegulatorID,
            ref string MemberShipNumber, ref bool IsLawyer, ref DateTime IssueDate, ref int? LastEditByUserID, 
            ref int SubscriptionTypeID, ref int SubscriptionWayID, ref int CreatedByUserID, ref bool IsActive,
           ref Dictionary<int, string> CasesRegulatorPracticesIDNameDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {



                using (SqlCommand command = new SqlCommand("SP_GetRegulatorInfoByPractitionerID", connection))
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
                                    RegulatorID = (int)reader["RegulatorID"];
                                    MemberShipNumber = (string)reader["MemberShipNumber"];
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
                                    int regulatoryCaseTypeId = reader.GetInt32(reader.GetOrdinal("RegulatoryCaseTypeID"));
                                    string regulatoryCaseTypeName = reader.GetString(reader.GetOrdinal("RegulatoryCaseTypeName"));

                                    CasesRegulatorPracticesIDNameDictionary.Add(regulatoryCaseTypeId, regulatoryCaseTypeName);
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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by pracititonerID():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                    catch (Exception ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsRegulator class, get info by PRactitinoerID():\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }


                }

            }

            return isFound;
        }

        /// <summary>
        ///Can create a new regulator profile in database, and verify if it has a practitioner profile or not .
        ///Note: I don't supply the method with  IsLawyer boolean , or practitioner ID , because it will be either created or selected in T-SQL.
        ///and Regulator is standard for IsLawyer =true
        /// <returns> List of three New IDs ([0]RegulatorID,[1]PractitionerID)</returns>
        public static (int NewRegulatorID, int NewPractitionerID) AddNewRegulator(int? PersonID,  string MemberShipNumber,
            int SubscriptionTypeID,int SubscriptionWayID,  int CreatedByUserID,bool IsActive,
            Dictionary<int, string> CasesRegulatorPracticesIDNameDictionary)
        {
            int RegulatorID = -1, PractitionerID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewRegulator", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@MemberShipNumber", MemberShipNumber);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);
                    command.Parameters.AddWithValue("@SubscriptionWayID", SubscriptionWayID);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var CasesTable = new DataTable();
                    CasesTable.Columns.Add("CaseID", typeof(int));
                    foreach (int CaseID in CasesRegulatorPracticesIDNameDictionary.Keys)
                    {
                        CasesTable.Rows.Add(CaseID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@RegulatoryCasesPracticesIds", CasesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name


                    SqlParameter outputIdParam = new SqlParameter("@NewRegulatorID", SqlDbType.Int)
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
                        RegulatorID = (int)command.Parameters["@NewRegulatorID"].Value;
                        PractitionerID = (int)command.Parameters["@OUTPUTPractitionerID"].Value;

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("regulator data access class , add to database method()\nSQL EXCEPTION:" + ex.Message, System.Diagnostics.EventLogEntryType.Error);

                        Console.WriteLine("SQL Error occurred: " + ex.Message);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("regulator data access class , add to database method()\n"+ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);
                    }
                }

            }

            return (RegulatorID, PractitionerID);

        }

        /// <summary>
        /// It accepts only the parameter that can be real changes, other properties ain't available to re-change after was created.
        /// </summary>
        /// <returns>Is it updated successfully or not</returns>
        public static bool UpdateRegulator(int? RegulatorID,int PractitionerID, string MemberShipNumber,
          int LastEditByUserID, int SubscriptionTypeID, int SubscriptionWayID, bool IsActive,
           Dictionary<int, string> CasesRegulatorPracticesIDNameDictionary)
        {

            int TotalEffectedRows = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateRegulator", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RegulatorID", RegulatorID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@MemberShipNumber", MemberShipNumber);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);
                    command.Parameters.AddWithValue("@SubscriptionWayID", SubscriptionWayID);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@LastEditByUserID", LastEditByUserID);
                        
                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var CasesTable = new DataTable();
                    CasesTable.Columns.Add("CaseID", typeof(int));
                    foreach (int CaseID in CasesRegulatorPracticesIDNameDictionary.Keys)
                    {
                        CasesTable.Rows.Add(CaseID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@RegulatoryCasesPracticesIds", CasesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name


                    try
                    {
                        connection.Open();
                        TotalEffectedRows = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("regulator data access class , update  method()\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        return false;
                    }
                }

            }

            return TotalEffectedRows>0;

        }
       
        public async static Task<DataTable> GetAllRegulatorsAsync()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllRegulatorsInfo_View", connection))
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
  
        public  static DataTable GetAllRegulators()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllRegulatorsInfo_View", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader()) 
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

        /// <summary>
        /// Able to handle deleting process for both regulator info , and its cases practices.
        /// Also,it handles it relationship with Lawyer Profile , and Practitioner profile
        /// </summary>
        /// <param name="RegulatorID"></param>
        /// <returns>Weather it was deleted successfully or not.</returns>
        public static bool DeletePermanently(int? RegulatorID)
               => clsDataAccessHelper.Delete("SP_DeleteRegulatorPermanently", "RegulatorID", RegulatorID);

        public static bool DeleteSoftly(int? RegulatorID)
        => clsDataAccessHelper.Delete("SP_DeleteRegulatorSoftly", "RegulatorID", RegulatorID); 
        
        public static bool IsRegulatorExistByPersonID(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsRegulatorExistsByPersonID", connection))
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
        public static bool IsRegulatorExistByRegulatorID(int RegulatorID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsRegulatorExistsByRegulatorID", connection))
                {
                    command.Parameters.AddWithValue("@RegulatorID", RegulatorID);

                    command.CommandType = CommandType.StoredProcedure;
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
        public static bool IsRegulatorExistByMemberShipNumber(string MemberShipNumber)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsRegulatorExistsByMemberShipNumber", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@MemberShipNumber", MemberShipNumber);
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
        public static bool IsRegulatorExistByPractitionerID(int PractitionerID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsRegulatorExistsByPractitionerID", connection))
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
                  
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Regulator Data access layer, is exists(PractitionerID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
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
        public static bool UpdateRegulatorList(int RegulatorID,int ListID,int ListTypeID)
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
