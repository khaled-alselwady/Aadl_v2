using AADL_DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADLDataAccess
{
    public class clsSubscriptionWayData
    {
        public static bool GetSubscriptionInfoByID(int SubscriptionWayID, ref string SubscriptionName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM SubscriptionWays WHERE SubscriptionWayID = @SubscriptionWayID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@SubscriptionWayID", SubscriptionWayID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {



                            if (reader.Read())
                            {

                                // The record was found
                                isFound = true;

                                SubscriptionName = (string)reader["SubscriptionName"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Exception was dropped from SubscriptionType data access class,get by id method:\n" +
                            ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }

        public static bool GetSubscriptionInfoByName(string SubscriptionName, ref int SubscriptionWayID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM SubscriptionWays WHERE SubscriptionName = @SubscriptionName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {



                    command.Parameters.AddWithValue("@SubscriptionName", SubscriptionName);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {

                            // The record was found
                            isFound = true;

                            SubscriptionWayID = (int)reader["SubscriptionWayID"];

                        }
                        else
                        {
                            // The record was not found
                            isFound = false;
                        }

                        reader.Close();


                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception was dropped from SubscriptionType data access class,get by name method:\n" +
                                          ex.Message, System.Diagnostics.EventLogEntryType.Error); isFound = false;
                    }
                }
            }
            return isFound;
        }

        public static DataTable GetAllSubscriptionWays()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {



                using (SqlCommand command = new SqlCommand("SP_GetAllSubscriptionWays", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    try
                    {
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)

                        {
                            dt.Load(reader);
                        }

                        reader.Close();


                    }

                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception was dropped from SubscriptionType data access class,getAllSubscriptionTypes():\n" +
                                                                 ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    }
                }
            }


            return dt;

        }
    }
}
