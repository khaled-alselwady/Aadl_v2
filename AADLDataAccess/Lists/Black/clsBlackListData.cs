using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLDataAccess.Lists.Black
{
    public class clsBlackListData
    {
        public static bool AccessBlackListInfoByBlackListID(int InputBlackListID,ref int ListID,ref string Notes,ref DateTime AddedToListDate,
              ref int CreatedByUserID ,ref int? LastEditByUserID,ref int PractitionerID,ref Dictionary<int,string> BlackListReasonsIDNamesDictionary)
            {
                bool isFound = false;

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    using (SqlCommand command = new SqlCommand("SP_GetBlackListByBlackListID", connection))
                    {
                    command.Parameters.AddWithValue("@BlackListID", InputBlackListID);
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                            connection.Open();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;
                                ListID = (int)reader["ListID"];
                                Notes = reader["Notes"] != DBNull.Value ? Notes = (string)reader["Notes"] : null;
                                AddedToListDate = (DateTime)reader["AddedToListDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID = (int)reader["LastEditByUserID"] 
                                    : null;

                                // Move to the 2nd result set

                                reader.NextResult();

                                if(reader.Read())//From black-Lists table
                                {
                                    PractitionerID = (int)reader["PractitionerID"];
                                }

                                else
                                {
                                    return false;
                                }

                                // Move to the 3rd result set
                                reader.NextResult();
                                while (reader.Read())//From  black lawyer reasons table
                                {
                                    int BlackListReasonID = (int)reader["BlackListReasonID"];
                                    string blackReasonName=(string) reader["BlackListReasonName"];

                                    BlackListReasonsIDNamesDictionary.Add(BlackListReasonID, blackReasonName);
                                }

                            }
                        
                            else
                                {
                                // The record was not found
                                return false;
                                }

                            }


                        }
                        catch (Exception ex)
                        {
                            clsDataAccessSettings.WriteEventToLogFile("Review your clsRegulatoryBlackListReasonsData class data " +
                                "access layer ,GetRegulatoryBlackListReasonNameByID()\n Exception:" +
                                ex.Message, EventLogEntryType.Error);
                            isFound = false;
                        }


                    }

                }


                return isFound;
            }

        public static bool AccessBlackListInfoByPractitionerID(int InputPractitionerID, ref int BlackListID, ref int ListID, ref string Notes, ref DateTime AddedToListDate,
            ref int CreatedByUserID,  ref int? LastEditByUserID, ref Dictionary<int, string> BlackListReasonsIDNamesDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetBlackListByPractitionerID", connection))
                {
                    command.Parameters.AddWithValue("@PractitionerID", InputPractitionerID);
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;
                                ListID = (int)reader["ListID"];
                                Notes = reader["Notes"] != DBNull.Value ? Notes=(string)reader["Notes"]:null;
                                AddedToListDate = (DateTime)reader["AddedToListDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID = (int)reader["LastEditByUserID"]
                                    : null;

                                // Move to the 2nd result set

                                reader.NextResult();

                                if (reader.Read())//From black-Lists table
                                {
                                    BlackListID = (int)reader["BlackListID"];
                                }

                                else
                                {
                                    return false;
                                }

                                // Move to the 3rd result set
                                reader.NextResult();
                                while (reader.Read())//From  black lawyer reasons table
                                {
                                    int blackListReasonID = (int)reader["BlackListReasonID"];
                                    string blackReasonName = (string)reader["BlackListReasonName"];

                                    BlackListReasonsIDNamesDictionary.Add(blackListReasonID, blackReasonName);
                                }

                            }

                            else
                            {
                                // The record was not found
                                return false;
                            }

                        }


                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsRegulatoryBlackListReasonsData class data " +
                            "access layer ,GetRegulatoryBlackListReasonNameByID()\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }




            }

            }


            return isFound;
        }


        /// <summary>
        /// Create new blackList,(base)list ,and add black list's  reasons that led lawyer to enter black-list 
        /// </summary>
        /// <returns>return new black list ID , and General List ID </returns>
        /// <exception cref="Exception"></exception>
        public static (int NewListID, int NewBlackListID) AddNewBlackList(string Notes,DateTime AddedToListDate,int CreatedByUserID
            ,int PractitionerID, Dictionary<int, string> BlackListPractitionerReasonsIDNamesDictionary)
        {
            int NewListID = -1
            , NewBlackListID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewBlackList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AddedToListDate", AddedToListDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var ReasonsTable = new DataTable();
                    ReasonsTable.Columns.Add("BlackListReasonID", typeof(int));
                    foreach (int ReasonID in BlackListPractitionerReasonsIDNamesDictionary.Keys)
                    {
                        ReasonsTable.Rows.Add(ReasonID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@BlackReasonsIds", ReasonsTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name


                    SqlParameter outputIdParam = new SqlParameter("@NewListID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    SqlParameter outputIdParam2 = new SqlParameter("@NewBlackListID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
  
                    if (Notes == "")
                    {
                        command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Notes", Notes);

                    }

                    // @ReturnVal could be any name

                    command.Parameters.Add(outputIdParam);
                    command.Parameters.Add(outputIdParam2);

                    try
                    {

                        connection.Open();
                        command.ExecuteNonQuery();
                        // Retrieve output parameter values
                        NewListID = (int)command.Parameters["@NewListID"].Value;
                        NewBlackListID = (int)command.Parameters["@NewBlackListID"].Value;

                        Console.WriteLine($"New List ID: {NewListID}");

                    }
                    catch(Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsBlackListData class DataAccess AddNewBlackList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine(ex.Message );
                    }
                  

                }


            }
              
            return  (NewListID,NewBlackListID);

        }
       
        /// <summary>
        /// Update blackList general info , plus update the reasons that made lawyer to enter black-list.
        /// </summary>
        /// <returns>boolean value indicate weather update happen successfully or not . </returns>
        /// <exception cref="Exception"></exception>
        public static bool UpdateBlackList(int BlackListID,string Notes,int PractitionerID,
          int? LastEditByUserID, Dictionary<int, string> BlackListPractitionerReasonsIDNamesDictionary)
        {
            bool IsSuccess = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateBlackList", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BlackListID", BlackListID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);

                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var ReasonsTable = new DataTable();
                    ReasonsTable.Columns.Add("BlackListReasonID", typeof(int));
                    foreach (int ReasonID in BlackListPractitionerReasonsIDNamesDictionary.Keys)
                    {
                        ReasonsTable.Rows.Add(ReasonID);
                    }

                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@BlackReasonsIds", ReasonsTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name

                    if (Notes == "")
                    {
                        command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Notes", Notes);

                    }


                    if (LastEditByUserID == null)
                    {
                        command.Parameters.AddWithValue("@LastEditByUserID", System.DBNull.Value);


                    }

                    else
                    {
                        command.Parameters.AddWithValue("@LastEditByUserID", LastEditByUserID);

                    }
                    SqlParameter outputIdParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    try
                    {

                        connection.Open();
                        command.ExecuteNonQuery();
                        IsSuccess= (bool)command.Parameters["@IsSuccess"].Value;

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsListData class DataAccess UpdateBlackList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine("ex:" + ex.Message);
                    }

                }


            }

            return IsSuccess ;

        }
  
        /// <summary>
        /// Method ,that capable to delete Base-BlackList,BlackList,reasons led lawyer to enter it , 
        /// and also update lawyer info profile .
        /// </summary>
        /// <param BlackListId="Refers to the blackList targeted to delete"></param>
        /// <returns>boolean either false or true , indications of success of the process</returns>
        public static bool DeleteBlackList(int BlackListID)
        {

            bool IsSuccess = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_DeleteBlackList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@BlackListID", BlackListID);

                    SqlParameter outputIdParam = new SqlParameter("@IsSuccess", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    try
                    {

                        connection.Open();

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                         IsSuccess = Convert.ToBoolean(command.Parameters["@IsSuccess"].Value);


                    }
                    catch (SqlException ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from BlackList data access layer delete (),\n" + ex.Message, EventLogEntryType.Error);
                        throw ex;

                    }
                }
            }


            return IsSuccess;

        }
        public static bool IsPractitionerInBlackList(int PractitionerID)
        {

            bool IsSuccess = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsPractitionerInBlackList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);

                    SqlParameter outputIdParam = new SqlParameter("@IsExists", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    try
                    {

                        connection.Open();

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Retrieve the output parameter value
                         IsSuccess = Convert.ToBoolean(command.Parameters["@IsExists"].Value);


                    }
                    catch (SqlException ex)
                    {

                        clsDataAccessSettings.WriteEventToLogFile("Exception from BlackList data access layer IsExistsInBlackList (LawyerID),\n" + ex.Message, EventLogEntryType.Error);
                        throw ex;

                    }
                }
            }


            return IsSuccess;

        }
        public static DataTable GetAllBlackListReasons()
        {
           return clsListReasonsData.GetAllBlackListReasons();
        }

    }
}
