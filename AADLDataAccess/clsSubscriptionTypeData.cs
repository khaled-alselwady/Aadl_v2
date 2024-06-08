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
    public class clsSubscriptionTypeData
    {
        public static bool GetSubscriptionInfoByID(int SubscriptionTypeID, ref string SubscriptionName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM SubscriptionTypes WHERE SubscriptionTypeID = @SubscriptionTypeID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {


                command.Parameters.AddWithValue("@SubscriptionTypeID", SubscriptionTypeID);

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
                clsDataAccessSettings.WriteEventToLogFile("Exception was dropped from SubscriptionType data access class,get by id method:\n"+
                    ex.Message,System.Diagnostics.EventLogEntryType.Error);
                isFound = false;
            }
                }
            }


            return isFound;
        }

        public static bool GetSubscriptionInfoByName(string SubscriptionName, ref int SubscriptionTypeID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM SubscriptionTypes WHERE SubscriptionName = @SubscriptionName";

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

                            SubscriptionTypeID = (int)reader["SubscriptionTypeID"];

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

        public static DataTable GetAllSubscriptionTypes()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {



                using (SqlCommand command = new SqlCommand("SP_GetAllSubscriptionTypes", connection))
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
