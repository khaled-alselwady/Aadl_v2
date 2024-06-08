using AADL_DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AADLDataAccess
{
    public class clsAdminData
    {

        public static bool GetAdminInfoByAdminID(int? AdminID, ref string UserName,
         ref string Password, ref bool IsActive, ref DateTime IssueDate, ref int? CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                string query = "SELECT * FROM Admins WHERE AdminID= @AdminID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@AdminID", AdminID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                IsActive = (bool)reader["IsActive"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                CreatedByAdminID =reader["CreatedByAdminID"]!=DBNull.Value?CreatedByAdminID= (int)reader["CreatedByAdminID"]:null;

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your Admin class data access layer ,get user info by ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }


                }

            }


            return isFound;
        }

        public static bool GetAdminInfoByUsernameAndPassword(string UserName, string Password,
            ref int? AdminID, ref bool IsActive, ref DateTime IssueDate, ref int? CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM Admins WHERE Username = @Username and Password=@Password;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@Username", UserName);
                    command.Parameters.AddWithValue("@Password", Password);


                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            // The record was found
                            isFound = true;
                            AdminID = (int)reader["AdminID"];
                            UserName = (string)reader["UserName"];
                            Password = (string)reader["Password"];
                            IsActive = (bool)reader["IsActive"];
                            IssueDate = (DateTime)reader["IssueDate"];
                            CreatedByAdminID = reader["CreatedByAdminID"] != DBNull.Value ? CreatedByAdminID = (int)reader["CreatedByAdminID"] : null;
                           

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your  Admin data access layer ,get user info by UsernameAndPassword Method() \n Exception:" +
                                                 ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }
            return isFound;
        }
        public static int? AddNewAdmin(string UserName, string Password, bool IsActive,DateTime IssueDate,int? CreatedByAdminID)
        {
            int? NewAdminId = null;
            string Query = "INSERT INTO [dbo].[Admins] ([UserName],[Password],[IsActive],[IssueDate],[CreatedByAdminID])" +
                "VALUES (@UserName,@Password,@IsActive,@IssueDate,@CreatedByAdminID) SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    if (CreatedByAdminID != null)
                    {

                        command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CreatedByAdminID", DBNull.Value);

                    }
                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            NewAdminId = insertedID;
                        }

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From admin class DataAccess add admin method:\t" + ex.Message,
                            EventLogEntryType.Error);
                        throw ex;
                    }

                }

                return NewAdminId;

            }
        }

        public static bool UpdateAdmin(int? AdminID, string UserName, string Password, bool IsActive, DateTime IssueDate, int? CreatedByAdminID)
        {
            int rowsAffected = 0;
            string query = @" 
                                 UPDATE [dbo].[Admins]
                                    SET [UserName] = @UserName
                                       ,[Password] = @Password
                                       ,[IsActive] = @IsActive
                                       ,[IssueDate]=@IssueDate
                                       ,[CreatedByAdminID]=@CreatedByAdminID
                                  WHERE AdminID=@AdminID; ";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdminID", AdminID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    if (CreatedByAdminID != null)
                    {

                        command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@CreatedByAdminID", DBNull.Value);

                    }
                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From admin dataAccess  class update method :\t" + ex.Message,
                            EventLogEntryType.Error);
                        throw ex;
                    }
                    return rowsAffected > 0;

                }

            }
        }
        public async static Task<DataTable> GetAllAdmins()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "select * from Admins";

                using (SqlCommand command = new SqlCommand(query, connection))
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
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of Admin class , where data grid view load all admins method dropped:\n"
                            + ex.Message, EventLogEntryType.Error);
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }

            }

            return dt;

        }
        public static bool DeleteAdmin(int? AdminID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = @"Delete Admins
                                where AdminID = @AdminID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@AdminID", AdminID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
                }
            }


            return (rowsAffected > 0);

        }

        [Obsolete("You must create view and sp , using T-sql")]
        public async static Task<DataTable> GetAllLawyersAsync()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "select * from lawyers order by issueDate";

                using (SqlCommand command = new SqlCommand(query, connection))
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
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of lawyer class , where data grid view load all people method dropped:\n"
                            + ex.Message, EventLogEntryType.Error);
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }

            }

            return dt;

        }


    }

}