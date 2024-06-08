using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLDataAccess.Lists.White
{
    /// <summary>
    /// This class does represents all info we need to know about practitioners who are in whiteLists table,
    /// with there differences from regulator to sharia to expert to judger, all are handled through this class
    /// exists check, deletion ,adding ,updating ,and accessing .
    /// </summary>
    public static class clsWhiteListData
    {

        /// <summary>
        /// Able to return full info of white-list of the practitioner and its reasons that led him to enter it.
        /// </summary>
        /// <param name="PractitionerID"> allows to detect the right white-list info</param>
        /// <param name="practitionerTypeID">in order to distinguish between different types of practitioners in reasons table</param>
        /// <param name="WhiteListReasonsIDNamesDictionary"> Ids and names of reasons that made him in white list</param>
        /// <returns></returns>
        public static bool AccessWhiteListInfoByWhiteListID(int InputWhiteListID, ref int ListID, ref string Notes, ref DateTime AddedToListDate,
            ref int CreatedByUserID, ref int? LastEditByUserID, ref int PractitionerID,
            ref int practitionerTypeID, ref Dictionary<int, string> WhiteListReasonsIDNamesDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetWhiteListByWhiteListID", connection))
                {
                    command.Parameters.AddWithValue("@WhiteListID", InputWhiteListID);
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
                                Notes = (string)reader["Notes"];
                                AddedToListDate = (DateTime)reader["AddedToListDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID = (int)reader["LastEditByUserID"]
                                    : null;

                                // Move to the 2nd result set

                                reader.NextResult();

                                if (reader.Read())//From White-Lists table
                                {
                                    PractitionerID = (int)reader["PractitionerID"];
                                    practitionerTypeID = (int)reader["practitionerTypeID"];

                                }

                                else
                                {
                                    return false;
                                }

                                // Move to the 3rd result set (White-Reasons)
                                reader.NextResult();
                                while (reader.Read())//From  white practitioner reasons table
                                {
                                    int WhiteListReasonID = (int)reader["WhiteListReasonID"];
                                    string WhiteReasonName = (string)reader["WhiteListReasonName"];

                                    WhiteListReasonsIDNamesDictionary.Add(WhiteListReasonID, WhiteReasonName);
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
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsWhiteListReasonsData class data " +
                            "access layer ,GetWhiteList(ID)\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }


                }

            }


            return isFound;
        }
     
        /// <summary>
        /// Able to return full info of white-list of the practitioner and its reasons that led him to enter it.
        /// </summary>
        /// <param name="PractitionerID"> allows to detect the right white-list info</param>
        /// <param name="practitionerTypeID">in order to distinguish between different types of practitioners in reasons table</param>
        /// <param name="WhiteListReasonsIDNamesDictionary"> Ids and names of reasons that made him in white list</param>
        /// <returns></returns>
        public static bool AccessWhiteListInfoByPractitionerID(int InputPractitionerID,int InputPractitionerTypeID, ref int ListID, ref string Notes, ref DateTime AddedToListDate,
            ref int CreatedByUserID, ref int? LastEditByUserID,ref int WhiteListID 
             ,ref Dictionary<int, string> WhiteListReasonsIDNamesDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetWhiteListByPractitionerID", connection))
                {
                    command.Parameters.AddWithValue("@PractitionerID", InputPractitionerID);
                    command.Parameters.AddWithValue("@PractitionerTypeID", InputPractitionerTypeID);

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
                                Notes = (string)reader["Notes"];
                                AddedToListDate = (DateTime)reader["AddedToListDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID = (int)reader["LastEditByUserID"]
                                    : null;

                                // Move to the 2nd result set

                                reader.NextResult();

                                if (reader.Read())//From black-Lists table
                                {
                                    WhiteListID = (int)reader["WhiteListID"];

                                }

                                else
                                {
                                    return false;
                                }

                                // Move to the 3rd result set
                                reader.NextResult();
                                while (reader.Read())//From  white practitioner reasons table
                                {
                                    int WhiteListReasonID = (int)reader["WhiteListReasonID"];
                                    string WhiteReasonName = (string)reader["WhiteListReasonName"];

                                    WhiteListReasonsIDNamesDictionary.Add(WhiteListReasonID, WhiteReasonName);
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
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsWhiteListData class  " +
                            "access layer ,GetWhiteList by (practitionerID)\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }




                }

            }


            return isFound;
        }


        /// <summary>
        /// Create new White-List,(base)list ,and add White list's  reasons that led practitioner to enter white-list 
        /// </summary>
        /// <returns>return new white list ID , and General List ID</returns>
        /// <exception cref="Exception"></exception>
        public static (int? NewListID, int? NewWhiteListID) AddNewWhiteList(string Notes, DateTime AddedToListDate, int CreatedByUserID
            , int PractitionerID,int PractitionerTypeID, Dictionary<int, string> WhiteListPractitionerReasonsIDNamesDictionary)
        {
            int ?NewListID = null
            ,NewWhiteListID = null  ;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewWhiteList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AddedToListDate", AddedToListDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@PractitionerTypeID", PractitionerTypeID);

                    // Create table-valued parameter for reasons IDs
                    // Iterate over reasons and insert into  table-valued parameter
                    var ReasonsTable = new DataTable();
                    ReasonsTable.Columns.Add("WhiteListReasonID", typeof(int));
                    foreach (int ReasonID in WhiteListPractitionerReasonsIDNamesDictionary.Keys)
                    {
                        ReasonsTable.Rows.Add(ReasonID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@WhiteReasonsIds", ReasonsTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name

                    SqlParameter newListIDParam = new SqlParameter("@NewListID", SqlDbType.Int);
                    newListIDParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(newListIDParam);

                    SqlParameter newWhiteListIDParam = new SqlParameter("@NewWhiteListID", SqlDbType.Int);
                    newWhiteListIDParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(newWhiteListIDParam);


                    if (Notes == "")
                    {
                        command.Parameters.AddWithValue("@Notes", System.DBNull.Value);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Notes", Notes);

                    }

                    try
                    {

                        connection.Open();
                        command.ExecuteNonQuery();
                        // Retrieve output parameter values
                        NewWhiteListID = command.Parameters["@NewWhiteListID"].Value as int?;
                        NewListID = command.Parameters["@NewListID"].Value as int?;

                        Console.WriteLine($"New List ID: {NewListID}");

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsWhiteListData class DataAccess AddNewWhiteList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);
                        return (null, null);
                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsWhiteListData class DataAccess AddNewWhiteList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);
                        return (null, null);
                    }
                  

                }


            }

            return (NewListID, NewWhiteListID);

        }

        /// <summary>
        /// Update Whitelist general info , plus update the reasons that made practitioner to enter White-list.
        /// </summary>
        /// <returns>boolean value indicate weather update happen successfully or not . </returns>
        /// <exception cref="Exception"></exception>
        public static bool UpdateWhiteList(int? ListID, string Notes,
          int? LastEditByUserID, int ?WhiteListID, int ?PractitionerID,
          int PractitionerTypeID,Dictionary<int, string> WhiteListPractitionerReasonsIDNamesDictionary)
        {
            bool IsSuccess = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateWhiteList", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                   
                    command.Parameters.AddWithValue("@ListID", ListID);
                    command.Parameters.AddWithValue("@WhiteListID", WhiteListID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@PractitionerTypeID", PractitionerTypeID);


                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var ReasonsTable = new DataTable();
                    ReasonsTable.Columns.Add("WhiteListReasonID", typeof(int));
                    foreach (int ReasonID in WhiteListPractitionerReasonsIDNamesDictionary.Keys)
                    {
                        ReasonsTable.Rows.Add(ReasonID);
                    }

                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@WhiteReasonsIds", ReasonsTable);
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
                        IsSuccess = (bool)command.Parameters["@IsSuccess"].Value;

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsListData class DataAccess UpdateBlackList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine("ex:" + ex.Message);
                    }

                }


            }

            return IsSuccess;

        }

        /// <summary>
        /// Method ,that capable to delete Base-List,WhiteList,and reasons led practitioner to enter it , 
        /// </summary>
        /// <param WhiteListID="Refers to the white list table in database targeted to delete"></param>
        /// <returns>boolean either false or true , indications of success of the process</returns>
        public static bool DeleteWhiteList(int WhiteListID)
        {

            bool IsSuccess = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_DeleteWhiteList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@WhiteListID", WhiteListID);

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
                        Console.WriteLine("Exception:\t" + ex.Message);
                        clsDataAccessSettings.WriteEventToLogFile("Exception from whiteList data access layer delete method(),\n" + ex.Message, EventLogEntryType.Error);
                        throw ex;

                    }
                }
            }


            return IsSuccess;

        }

        /// <summary>
        /// Can handle practitioners types in white list , check just if a practitioner based on his type 
        /// in white list or not 
        /// </summary>
        /// <param name="PractitionerID">FK key of Practitioner in whitelist table.</param>
        /// <param name="PractitionerTypeID">Used to distinguish between different type of Practitioners</param>
        /// <returns> weather it's existed in white list or not </returns>
        public static bool IsPractitionerInWhiteList(int PractitionerID,int PractitionerTypeID)
        {

            bool IsSuccess = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsPractitionerInWhiteList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@PractitionerTypeID", PractitionerTypeID);

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
                        Console.WriteLine("Exception;" + ex.Message);
                        clsDataAccessSettings.WriteEventToLogFile("Exception from whitelist data access layer IsExistsInWhiteList(PractitionerID),\n" + ex.Message, EventLogEntryType.Error);
                        throw ex;

                    }
                }
            }


            return IsSuccess;

        }

        public static DataTable GetAllWhiteListReasons()
        {
            return clsListReasonsData.GetAllWhiteListReasons();
        }

    }

}
