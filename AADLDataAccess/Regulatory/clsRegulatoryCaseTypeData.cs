using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLDataAccess
{
    public class clsRegulatoryCaseTypeData
    {
        public static bool GetRegulatoryCaseTypeInfoByCaseTypeID(int RegulatoryCaseTypeID ,ref string RegulatoryCaseTypeName
            ,ref int CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetRegulatoryCaseTypeInfoByCaseTypeID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RegulatoryCaseTypeID", RegulatoryCaseTypeID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                RegulatoryCaseTypeID = (int)reader["RegulatoryCaseTypeID"];
                                RegulatoryCaseTypeName = (string)reader["RegulatoryCaseTypeName"];
                                CreatedByAdminID = (int)reader["CreatedByAdminID"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsRegulatoryCaseTypeData class data access layer ,get case type info by ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        throw new Exception(ex.Message);

                    }


                }

            }


            return isFound;
        }
        public static bool GetRegulatoryCaseTypeInfoByCaseTypeName(string RegulatoryCaseTypeName,ref int RegulatoryCaseTypeID
          , ref int CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetRegulatoryCaseTypeInfoByCaseTypeName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RegulatoryCaseTypeName", RegulatoryCaseTypeName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                RegulatoryCaseTypeID = (int)reader["RegulatoryCaseTypeID"];
                                RegulatoryCaseTypeName = (string)reader["RegulatoryCaseTypeName"];
                                CreatedByAdminID = (int)reader["CreatedByAdminID"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsRegulatoryCaseTypeData class data access layer ,get case type info by ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        throw new Exception(ex.Message);

                    }


                }

            }


            return isFound;
        }
        public static int  AddNewRegulatoryCaseType(string RegulatoryCaseTypeName, int CreatedByAdminID)
        {
            int NewRegulatoryCaseTypeID = -1;
          
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewRegulatoryCaseType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RegulatoryCaseTypeName", RegulatoryCaseTypeName);
                    command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);
                    SqlParameter outputIdParam = new SqlParameter("@NewRegulatoryCaseTypeID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    try
                    {
                        connection.Open();

                        NewRegulatoryCaseTypeID = (int)command.Parameters["@NewRegulatoryCaseTypeID"].Value;


                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From clsRegulatoryCaseTypeData class DataAccess add new Case type method:\t" + ex.Message,
                            EventLogEntryType.Error);
                        throw new Exception(ex.Message);
                    }

                }

                return NewRegulatoryCaseTypeID;

            }

        }
        public static bool UpdateRegulatoryCaseType(int? RegulatoryCaseTypeID,  string RegulatoryCaseTypeName, int CreatedByAdminID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateRegulatoryCaseType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RegulatoryCaseTypeName", RegulatoryCaseTypeName);
                    command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);
                    command.Parameters.AddWithValue("@RegulatoryCaseTypeID", RegulatoryCaseTypeID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From RegulatoryCaseType dataAccess  class update method :\t" + ex.Message,
                            EventLogEntryType.Error);
                        throw new Exception(ex.Message);
                    }

                }

            }
           
            return rowsAffected > 0;

        }
        public static bool DeleteRegulatoryCaseType(int? RegulatoryCaseTypeID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_DeleteRegulatoryCaseType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegulatoryCaseTypeID", RegulatoryCaseTypeID);

                    try
                    {
                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From RegulatoryCaseType DataAccess  class delete method :\t" + ex.Message,
                            EventLogEntryType.Error);
                        throw new Exception(ex.Message);

                    }
                }
            }


            return (rowsAffected > 0);

        }
        public  static DataTable GetAllRegulatoryCaseTypes()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllRegulatoryCaseTypes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader =  command.ExecuteReader())
                        {

                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }

                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of RegulatoryCaseTypes class , where data grid view load all RegulatoryCaseTypes method dropped:\n"
                            + ex.Message, EventLogEntryType.Error);
                        Console.WriteLine("Error: " + ex.Message);
                        throw new Exception(ex.Message + "\n" + ex.StackTrace);
                    }
                }

            }

            return dt;

        }

    }

}
