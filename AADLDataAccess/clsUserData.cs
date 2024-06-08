using AADL_DataAccess;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AADLBusiness
{
    public class clsUserData
    {
       
        public static bool GetUserInfoByUserID(int? UserID, ref string UserName,
            ref string Password, ref bool IsActive, ref DateTime IssueDate,ref int CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                string query = "SELECT * FROM Users WHERE UserID = @UserID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {


                command.Parameters.AddWithValue("@UserID", UserID);

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,get user info by ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                isFound = false;
            }
           
   
                }

            }


            return isFound;
        }

        public static bool GetUserInfoByUsernameAndPassword(string UserName,  string Password, 
            ref int UserID, ref bool IsActive, ref DateTime IssueDate, ref int CreatedByAdminID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";

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
                            UserID = (int)reader["UserID"];
                            UserName = (string)reader["UserName"];
                            Password = (string)reader["Password"];
                            IsActive = (bool)reader["IsActive"];
                            IssueDate = (DateTime)reader["IssueDate"];
                            CreatedByAdminID = (int)reader["CreatedByAdminID"];


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
                        clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,get user info by UsernameAndPassword Method() \n Exception:" +
                                                 ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }
            return isFound;
        }

        public static int? AddNewUser(  string UserName,
             string Password,  bool IsActive, DateTime IssueDate,  int CreatedByAdminID)
        {
            //this function will return the new person id if succeeded and -1 if not.
            int ?UserID = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Users (UserName,Password,IsActive,IssueDate,CreatedByAdminID)
                             VALUES (@UserName, @Password,@IsActive,@IssueDate,@CreatedByAdminID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    UserID = insertedID;
                }
            }

            catch (Exception ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,AddNewUser Method() \n Exception:" +
                                               ex.Message, EventLogEntryType.Error);
            }

            finally
            {
                connection.Close();
            }

            return UserID;
        }


        public static bool UpdateUser(int? UserID, string UserName,
             string Password, bool IsActive, DateTime IssueDate, int CreatedByAdminID)
        {

            int rowsAffected = 0;
         
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = @"Update  Users  
                            set 
                                UserName = @UserName,
                                Password = @Password,
                                IsActive = @IsActive,
                                IssueDate = @IssueDate,
                                CreatedByAdminID = @CreatedByAdminID
                                where UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@IsActive", IsActive);
                command.Parameters.AddWithValue("@IssueDate", IssueDate);
                command.Parameters.AddWithValue("@CreatedByAdminID", CreatedByAdminID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,Update user Method() \n Exception:" +
                                                    ex.Message, EventLogEntryType.Error);
                    return false;
                }
            }
        }

            return (rowsAffected > 0);

        }


        public async static Task<DataTable> GetAllUsers()
        {

            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = @"select * from fullUserInfo_view;";

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,GetAllUsers Method() \n Exception:" +
                                                                           ex.Message, EventLogEntryType.Error);
                    }
                }
            }

            return dt;

        }

        public static bool DeleteUser(int UserID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                string query = @"Delete Users 
                                where UserID = @UserID";

            using (SqlCommand command = new SqlCommand(query, connection))
            {


                command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,DeleteUser Method() \n Exception:" +
                                                                   ex.Message, EventLogEntryType.Error);
            }
                }
            }


            return (rowsAffected > 0);

        }

        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                    connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            isFound = reader.HasRows;
                        }

                
            }
            catch (Exception ex)
            {
                        clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,IsUserExist Method(UserID) \n Exception:" +
                                                                                      ex.Message, EventLogEntryType.Error); isFound = false;
            }
                }
            }


            return isFound;
        }

        public static bool IsUserExist(string UserName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT Found=1 FROM Users WHERE UserName = @UserName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            isFound = reader.HasRows;

                        }
            }
            catch (Exception ex)
            {
                        clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,IsUserExist Method(UserName) \n Exception:" +
                                                                                                             ex.Message, EventLogEntryType.Error); isFound = false; isFound = false;
            }
                }
            }

            return isFound;
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            isFound = reader.HasRows;

                        }
            }
            catch (Exception ex)
            {
                        clsDataAccessSettings.WriteEventToLogFile("Review your user class data access layer ,IsUserExist Method(UserName) \n Exception:" +
                    
                            ex.Message, EventLogEntryType.Error); 
                        isFound = false; 

                    
                    }
                }
            }


            return isFound;
        }

        public static bool DoesPersonHaveUser44(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Users  
                            set Password = @Password
                            where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

    }
}
