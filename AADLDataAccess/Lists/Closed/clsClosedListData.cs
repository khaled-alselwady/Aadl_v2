using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AADLDataAccess.Lists.Closed
{
    public static  class clsClosedListData
    {
        /// <summary>
        /// Able to return full info of Closed-list of the practitioner and its reasons that led him to enter it.
        /// </summary>
        /// <param name="PractitionerID"> allows to detect the right Closed-list info</param>
        /// <param name="practitionerTypeID">in order to distinguish between different types of practitioners in reasons table</param>
        /// <param name="closedListReasonsIDNamesDictionary"> Ids and names of reasons that made him in closed list</param>
        /// <returns></returns>
        public static bool AccessClosedListInfoByClosedListID(int InputClosedListID, ref int ListID, ref string Notes, ref DateTime AddedToListDate,
            ref int CreatedByUserID, ref int? LastEditByUserID, ref int PractitionerID,
            ref int practitionerTypeID, ref Dictionary<int, string> closedListReasonsIDNamesDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetClosedListByClosedListID", connection))
                {
                    command.Parameters.AddWithValue("@ClosedListID", InputClosedListID);
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
                                Notes = reader["Notes"] != DBNull.Value ? Notes = (string)reader["Notes"]:null;
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
                                    int WhiteListReasonID = (int)reader["ClosedListReasonID"];
                                    string WhiteReasonName = (string)reader["ClosedListReasonName"];

                                    closedListReasonsIDNamesDictionary.Add(WhiteListReasonID, WhiteReasonName);
                                }

                            }

                            else
                            {
                                // The record was not found
                                return false;
                            }

                        }


                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsClosedListData class data " +
                            "access layer ,GetClosedList(ID)\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }


                }

            }


            return isFound;
        }

        /// <summary>
        /// Able to return full info of closed-list of the practitioner and its reasons that led him to enter it.
        /// </summary>
        /// <param name="PractitionerID"> allows to detect the right white-list info</param>
        /// <param name="practitionerTypeID">in order to distinguish between different types of practitioners in reasons table</param>
        /// <param name="WhiteListReasonsIDNamesDictionary"> Ids and names of reasons that made him in closed list</param>
        /// <returns></returns>
        public static bool AccessClosedListInfoByPractitionerID(int InputPractitionerID, int InputPractitionerTypeID, ref int ListID, ref string Notes, ref DateTime AddedToListDate,
            ref int CreatedByUserID, ref int? LastEditByUserID, ref int ClosedListID,
            ref Dictionary<int, string> ClosedListReasonsIDNamesDictionary)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetClosedListByPractitionerID", connection))
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
                                Notes = reader["Notes"] != DBNull.Value ? Notes = (string)reader["Notes"] : null;
                                AddedToListDate = (DateTime)reader["AddedToListDate"];
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                LastEditByUserID = reader["LastEditByUserID"] != DBNull.Value ? LastEditByUserID = (int)reader["LastEditByUserID"]
                                    : null;

                                // Move to the 2nd result set

                                reader.NextResult();

                                if (reader.Read())//From black-Lists table
                                {
                                    ClosedListID = (int)reader["ClosedListID"];
                                }

                                else
                                {
                                    return false;
                                }

                                // Move to the 3rd result set
                                reader.NextResult();
                                while (reader.Read())//From  white practitioner reasons table
                                {
                                    int ClosedListReasonID = (int)reader["ClosedListReasonID"];
                                    string ClosedReasonName = (string)reader["ClosedListReasonName"];

                                    ClosedListReasonsIDNamesDictionary.Add(ClosedListReasonID, ClosedReasonName);
                                }

                            }

                            else
                            {
                                // The record was not found
                                return false;
                            }

                        }


                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsClosedListData class  " +
                            "access layer ,GetClosedList by (practitionerID)\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsClosedListData class  " +
                            "access layer ,GetClosedList by (practitionerID)\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }




                }

            }


            return isFound;
        }
        public static DataTable GetAllClosedListReasons()
        {
            return clsListReasonsData.GetAllClosedListReasons();
        }


        /// <summary>
        /// Create new Closed-List,(base)list ,and add closed list's  reasons that led practitioner to enter closed-list 
        /// </summary>
        /// <returns>return new closed list ID , and General List ID</returns>
        /// <exception cref="Exception"></exception>
        public static (int? NewListID, int? NewClosedListID) AddNewClosedList(string Notes, DateTime AddedToListDate, int CreatedByUserID
            , int PractitionerID, int PractitionerTypeID, Dictionary<int, string> ClosedListPractitionerReasonsIDNamesDictionary)
        {
            int? NewListID = null
            , NewClosedListID = null;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewClosedList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AddedToListDate", AddedToListDate);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@PractitionerTypeID", PractitionerTypeID);

                    // Create table-valued parameter for reasons IDs
                    // Iterate over reasons and insert into  table-valued parameter
                    var ReasonsTable = new DataTable();
                    ReasonsTable.Columns.Add("ClosedListReasonID", typeof(int));
                    foreach (int ReasonID in ClosedListPractitionerReasonsIDNamesDictionary.Keys)
                    {
                        ReasonsTable.Rows.Add(ReasonID);
                    }
                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@ClosedReasonsIds", ReasonsTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.HashSetOfInt";// Replace "YourHashSetType" with the actual SQL Server type name

                    SqlParameter newListIDParam = new SqlParameter("@NewListID", SqlDbType.Int);
                    newListIDParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(newListIDParam);

                    SqlParameter newWhiteListIDParam = new SqlParameter("@NewClosedListID", SqlDbType.Int);
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
                        NewClosedListID = command.Parameters["@NewClosedListID"].Value as int?;
                        NewListID = command.Parameters["@NewListID"].Value as int?;

                        Console.WriteLine($"New List ID: {NewListID}");

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsClosedListData class DataAccess AddNewClosedList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);
                        return (null, null);
                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsClosedListData class DataAccess AddNewClosedList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);
                        return (null, null);
                    }


                }


            }

            return (NewListID, NewClosedListID);

        }


        /// <summary>
        /// Update ClosedList general info , plus update the reasons that made practitioner to enter Closed-list.
        /// </summary>
        /// <returns>boolean value indicate weather update happen successfully or not . </returns>
        /// <exception cref="Exception"></exception>
        public static bool UpdateClosedList(int? ListID, string Notes,
          int? LastEditByUserID, int? ClosedListID, int? PractitionerID,
          int PractitionerTypeID, Dictionary<int, string> ClosedListPractitionerReasonsIDNamesDictionary)
        {
            bool IsSuccess = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateClosedList", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ListID", ListID);
                    command.Parameters.AddWithValue("@ClosedListID", ClosedListID);
                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
                    command.Parameters.AddWithValue("@PractitionerTypeID", PractitionerTypeID);


                    // Create table-valued parameter for cases IDs
                    // Iterate over cases and insert into  table-valued parameter
                    var ReasonsTable = new DataTable();
                    ReasonsTable.Columns.Add("ClosedListID", typeof(int));
                    foreach (int ReasonID in ClosedListPractitionerReasonsIDNamesDictionary.Keys)
                    {
                        ReasonsTable.Rows.Add(ReasonID);
                    }

                    // Add table-valued parameter
                    SqlParameter parameter = command.Parameters.AddWithValue("@ClosedReasonsIds", ReasonsTable);
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
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsListData class DataAccess UpdateClosedList():\t" + ex.Message,
                            EventLogEntryType.Error);
                        Console.WriteLine("ex:" + ex.Message);
                    }

                }


            }

            return IsSuccess;

        }

        public static bool DeleteClosedList(int ClosedListID)
        {

            bool IsSuccess = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_DeleteClosedList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@ClosedListID", ClosedListID);

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
                        clsDataAccessSettings.WriteEventToLogFile("Exception from ClosedList data access layer delete method(),\n" + ex.Message, EventLogEntryType.Error);
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
        public static bool IsPractitionerInClosedList(int PractitionerID, int PractitionerTypeID)
        {

            bool IsSuccess = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsPractitionerInClosedList", connection))
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


    }
}
