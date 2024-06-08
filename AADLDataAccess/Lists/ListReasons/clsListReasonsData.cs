using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AADLDataAccess
{
    [Obsolete("Add update process , not implemented yet.")]
    public class clsListReasonsData
    {
        public static DataTable GetAllBlackListReasons()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllBlackListReasons", connection))
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

                        clsDataAccessSettings.WriteEventToLogFile("Exception dropped from clsListReasonsData class" +
                            "at data access layer , with  GetAllBlackListReasons()\n"+ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        throw ex;
                    }
                }
            }


            return dt;

        }
        public static DataTable GetAllWhiteListReasons()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllWhiteListReasons", connection))
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

                        clsDataAccessSettings.WriteEventToLogFile("Exception dropped from clsListReasonsData class" +
                            "at data access layer , with  GetAllWhiteListReasons()\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        throw ex;
                    }
                }
            }


            return dt;

        }
        public static DataTable GetAllClosedListReasons()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllClosedListReasons", connection))
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

                        clsDataAccessSettings.WriteEventToLogFile("Exception dropped from clsListReasonsData class" +
                            "at data access layer , with  GetAllClosedListReasons()\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        throw ex;
                    }
                }
            }


            return dt;

        }
        public static bool GetBlackListReasonNameByID(int BlackListReasonID, 
            ref string BlackListReasonName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetBlackListReasonNameByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@BlackListReasonID", BlackListReasonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                BlackListReasonName = (string)reader["BlackListReasonName"];

                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
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

        [Obsolete("Not implemented yet.")]
        public static bool GetWhiteListReasonNameByID(int BlackListReasonID,
            ref string BlackListReasonName)
        {
            throw new ArgumentException("This function ain't implelement yet , you cannot use it ");
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetBlackListReasonNameByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@BlackListReasonID", BlackListReasonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                BlackListReasonName = (string)reader["BlackListReasonName"];

                            }
                            else
                            {
                                // The record was not found
                                isFound = false;
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
        //public static bool GetBlackListIDByName( string BlackListReasonName,
        //    ref int BlackListReasonID)
        //{
        //    bool isFound = false;

        //    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
        //    {

        //        using (SqlCommand command = new SqlCommand("SP_GetBlackListReasonIDByName", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@BlackListReasonName", BlackListReasonName);

        //            try
        //            {
        //                connection.Open();
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {


        //                    if (reader.Read())
        //                    {
        //                        // The record was found
        //                        isFound = true;
        //                        BlackListReasonID = (int)reader["BlackListReasonID"];
        //                    }
        //                    else
        //                    {
        //                        // The record was not found
        //                        isFound = false;
        //                    }


        //                }


        //            }
        //            catch (Exception ex)
        //            {
        //                clsDataAccessSettings.WriteEventToLogFile("Review your clsRegulatoryBlackListReasonsData class data " +
        //                    "access layer ,GetRegulatoryBlackListReasonNameByID()\n Exception:" +
        //                    ex.Message, EventLogEntryType.Error);
        //                isFound = false;
        //            }


        //        }

        //    }


        //    return isFound;
        //}



    }
}
