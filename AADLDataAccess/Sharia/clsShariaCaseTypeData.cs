using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLDataAccess.Sharia
{
    public class clsShariaCaseTypeData
    {
        public static bool GetShariaCaseTypeInfoByCaseTypeID(int InputShariaCaseTypeID, ref string ShariaCaseTypeName
            , ref int CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetShariaCaseTypeInfoByCaseTypeID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShariaCaseTypeID", InputShariaCaseTypeID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                ShariaCaseTypeName = (string)reader["ShariaCaseTypeName"];
                                CreatedByAdminID = (int)reader["CreatedByAdminID"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,get case type info by ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,get case type info by ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }


                }

            }


            return isFound;
        }

        public static bool GetShariaCaseTypeInfoByCaseTypeName(string InputShariaCaseTypeName, ref int ShariaCaseTypeID
          , ref int CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetShariaCaseTypeInfoByCaseTypeName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShariaCaseTypeName", InputShariaCaseTypeName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                ShariaCaseTypeID = (int)reader["RegulatoryCaseTypeID"];
                                CreatedByAdminID = (int)reader["CreatedByAdminID"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,get case type info by name\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,get case type info by name\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }


                }

            }


            return isFound;
        }
        public static int AddNewShariaCaseType(string ShariaCaseTypeName, int CreatedByAdminID)
        {
            int NewShariaCaseTypeID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_AddNewShariaCaseType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShariaCaseTypeName", ShariaCaseTypeName);
                    command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);
                    SqlParameter outputIdParam = new SqlParameter("@NewShariaCaseTypeID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);
                    try
                    {
                        connection.Open();

                        NewShariaCaseTypeID = (int)command.Parameters["@NewShariaCaseTypeID"].Value;


                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,AddNewShariaCaseType\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer, AddNewShariaCaseType\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }

                }

                return NewShariaCaseTypeID;

            }

        }
        public static bool UpdateShariaCaseType(int ShariaCaseTypeID, string ShariaCaseTypeName, int CreatedByAdminID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdateShariaCaseType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ShariaCaseTypeID", ShariaCaseTypeID);
                    command.Parameters.AddWithValue("@ShariaCaseTypeName", ShariaCaseTypeName);
                    command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,UpdateShariaCaseType\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,UpdateShariaCaseType\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }

                }

            }

            return rowsAffected > 0;

        }
        public static bool DeleteShariaCaseType(int? ShariaCaseTypeID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_DeleteShariaCaseType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ShariaCaseTypeID", ShariaCaseTypeID);

                    try
                    {
                        connection.Open();

                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From ShariaCaseType DataAccess  class delete method :\t" + ex.Message,
                            EventLogEntryType.Error);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,delete\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }

                }
            }


            return (rowsAffected > 0);

            //}
            //public static DataTable GetAllRegulatoryCaseTypes()
            //{

            //    DataTable dt = new DataTable();
            //    using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            //    {

            //        using (SqlCommand command = new SqlCommand("SP_GetAllRegulatoryCaseTypes", connection))
            //        {
            //            command.CommandType = CommandType.StoredProcedure;

            //            try
            //            {
            //                connection.Open();

            //                using (SqlDataReader reader = command.ExecuteReader())
            //                {

            //                    if (reader.HasRows)
            //                    {
            //                        dt.Load(reader);
            //                    }
            //                }
            //            }

            //            catch (SqlException ex)
            //            {
            //                clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of RegulatoryCaseTypes class , where data grid view load all RegulatoryCaseTypes method dropped:\n"
            //                    + ex.Message, EventLogEntryType.Error);
            //                Console.WriteLine("Error: " + ex.Message);
            //                throw new Exception(ex.Message + "\n" + ex.StackTrace);
            //            }
            //        }

            //    }

            //    return dt;

            //}

        }
        public static DataTable GetAllShariaCaseTypes()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllShariaCaseTypes", connection))
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

                    catch (SqlException ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From ShariaCaseType DataAccess  class get all cases of sharia method :\t" + ex.Message,
                            EventLogEntryType.Error);

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Review your clsShariaCaseTypeData class data access layer ,class get all cases of sharia method\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        Console.WriteLine(ex.Message);

                    }
                }

            }

            return dt;

        }

    }
}

