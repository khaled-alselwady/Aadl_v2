using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AADL_DataAccess
{
    public class clsPersonData
    {
        public enum PersonColumn
        {
            PersonID,
            NationalNo,
            PassportNo,
            FullName,
            WhatsApp,
            Phone,
            Email
        }

        public enum enPhoneType {Standard,WhatsApp};
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonID"> this parameter is used to search for person info in people table in database </param>

        /// <returns> person true or false based on the result weather person is found or not </returns>
        public static bool GetPersonInfoByID(int? PersonID,
            ref string NationalNo, ref string PassportNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref short? Gender, ref DateTime? DateOfBirth,
            ref string Address, ref string Phone, ref string WhatsApp, ref string Email,
            ref string ImagePath, ref int CreatedByUserID, ref int CountryID, ref int CityID,ref DateTime IssueDate,
            ref bool IsActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM People WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                NationalNo = reader["NationalNo"] != DBNull.Value ? NationalNo = (string)reader["NationalNo"] : "";
                                PassportNo = reader["PassportNo"] != DBNull.Value ? PassportNo = (string)reader["PassportNo"] : "";
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                Gender = reader["Gender"] != DBNull.Value ? Gender = (byte)reader["Gender"] : null;
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOfBirth = (DateTime)reader["DateOfBirth"] : null;
                                Address = reader["Address"] != DBNull.Value ? Address = (string)reader["Address"] : "";
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                                CountryID = (int)reader["CountryID"];
                                ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                                WhatsApp = reader["WhatsApp"] != DBNull.Value ? WhatsApp = (string)reader["WhatsApp"] : "";
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CityID = (int)reader["CityID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                              
                                IsActive = (bool)reader["IsActive"];

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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsPerson class:\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
                }
                }

            }

            return isFound;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NationalNo"> this parameter is used to search for person info in people table in database </param>

        /// <returns> person true or false based on the result weather person is found or not </returns>
        public static bool GetPersonInfoByNationalNo(string NationalNo,
            ref int? PersonID, ref string PassportNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref short? Gender, ref DateTime? DateOfBirth,
            ref string Address, ref string Phone, ref string WhatsApp, ref string Email,
            ref string ImagePath, ref int CreatedByUserID, ref int CountryID ,ref int CityID,ref DateTime IssueDate
            , ref bool IsActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM People WHERE NationalNo = @NationalNo and IsActive=1   ";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                NationalNo = reader["NationalNo"] != DBNull.Value ? NationalNo = (string)reader["NationalNo"] : "";
                                PassportNo = reader["PassportNo"] != DBNull.Value ? PassportNo = (string)reader["PassportNo"] : "";
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                Gender = reader["Gender"] != DBNull.Value ? Gender = (byte)reader["Gender"] : null;
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOfBirth = (DateTime)reader["DateOfBirth"] : null;
                                Address = reader["Address"] != DBNull.Value ? Address = (string)reader["Address"] : "";
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                                CountryID = (int)reader["CountryID"];
                                ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                                WhatsApp = reader["WhatsApp"] != DBNull.Value ? WhatsApp = (string)reader["WhatsApp"] : "";
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CityID = (int)reader["CityID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                IsActive = (bool)reader["IsActive"];

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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsPerson class, " +
                            " GetPersonInfoByNationalNo method :\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }

            }

            return isFound;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PassportNo"> this parameter is used to search for person info in people table in database </param>

        /// <returns> person true or false based on the result weather person is found or not </returns>
        public static bool GetPersonInfoByPassportNo(string PassportNo,
         ref int? PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
         ref string ThirdName, ref string LastName, ref short? Gender, ref DateTime? DateOfBirth,
         ref string Address, ref string Phone, ref string WhatsApp, ref string Email,
         ref string ImagePath, ref int CreatedByUserID, ref int CountryID, ref int CityID,ref DateTime IssueDate,
          ref bool IsActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM People WHERE PassportNo = @PassportNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@PassportNo", PassportNo);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                NationalNo = reader["NationalNo"] != DBNull.Value ? NationalNo = (string)reader["NationalNo"] : "";
                                PassportNo = reader["PassportNo"] != DBNull.Value ? PassportNo = (string)reader["PassportNo"] : "";
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                Gender = reader["Gender"] != DBNull.Value ? Gender = (byte)reader["Gender"] : null;
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOfBirth = (DateTime)reader["DateOfBirth"] : null;
                                Address = reader["Address"] != DBNull.Value ? Address = (string)reader["Address"] : "";
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                                CountryID = (int)reader["CountryID"];
                                ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                                WhatsApp = reader["WhatsApp"] != DBNull.Value ? WhatsApp = (string)reader["WhatsApp"] : "";
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CityID = (int)reader["CityID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                IsActive = (bool)reader["IsActive"];

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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsPerson class:\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }

            }

            return isFound;
        }

        /// <summary>
        /// Will search in database based on the name content , as long as the name consists 4 parts,which is the standard approach. 
        /// otherwise , it will handle special  case where name might exceed 4 parts , the mechanism will be differ,
        /// </summary>
        /// <param name="IsCompound"> refers to full name is a compound name or standard (4 parts) only.</param>
        /// <returns>weather the person is found with this name or not </returns>
        public static bool GetPersonInfoByFullName(string FirstName, string SecondName,
         string ThirdName, string LastName, ref int? PersonID, ref string PassportNo,
         ref string NationalNo,  ref short? Gender, ref DateTime? DateOfBirth,
        ref string Address, ref string Phone, ref string WhatsApp, ref string Email,
        ref string ImagePath, ref int CreatedByUserID, ref int CountryID, ref int CityID,ref DateTime IssueDate,
            ref bool IsActive)
        {
            
            bool isFound = false;
            StringBuilder fullNameBuilder = new StringBuilder();
            // Append first ,second,third and last name.
            

            fullNameBuilder.Append(FirstName);
            fullNameBuilder.Append(" ").Append(SecondName);
            fullNameBuilder.Append(" ").Append(ThirdName);
            fullNameBuilder.Append(" ").Append(LastName);
 
               string FullName = fullNameBuilder.ToString();
            
     

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM People WHERE  FirstName+ ' '+ SecondName + ' '+ ThirdName + ' ' +LastName =@FullName " +
                               "and isActive=1;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@FullName", FullName);

                    try
                    {
                       
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                NationalNo = reader["NationalNo"] != DBNull.Value ? NationalNo = (string)reader["NationalNo"] : "";
                                PassportNo = reader["PassportNo"] != DBNull.Value ? PassportNo = (string)reader["PassportNo"] : "";
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                Gender = reader["Gender"] != DBNull.Value ? Gender = (byte)reader["Gender"] : null;
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOfBirth = (DateTime)reader["DateOfBirth"] : null;
                                Address = reader["Address"] != DBNull.Value ? Address = (string)reader["Address"] : "";
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                                CountryID = (int)reader["CountryID"];
                                ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                                WhatsApp = reader["WhatsApp"] != DBNull.Value ? WhatsApp = (string)reader["WhatsApp"] : "";
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CityID = (int)reader["CityID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                IsActive = (bool)reader["IsActive"];

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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsPerson class:\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        throw ex;
                    }
                }

            }

            return isFound;
        }

        public static bool GetPersonInfoByCompoundName(string CompoundName,ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref int? PersonID, ref string PassportNo,
        ref string NationalNo, ref short? Gender, ref DateTime ?DateOfBirth,
        ref string Address, ref string Phone, ref string WhatsApp, ref string Email,
        ref string ImagePath, ref int CreatedByUserID, ref int CountryID, ref int CityID, ref DateTime IssueDate,
        ref bool IsActive )
        {

            bool isFound = false;
            StringBuilder fullNameBuilder = new StringBuilder();
            // Append first ,second,third and last name.
          

       
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "SELECT * FROM People WHERE  FirstName+ ' '+ SecondName + ' '+ ThirdName + ' ' +LastName =@CompoundName " +
                               "and isActive=1;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@CompoundName", CompoundName);

                    try
                    {

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                NationalNo = reader["NationalNo"] != DBNull.Value ? NationalNo = (string)reader["NationalNo"] : "";
                                PassportNo = reader["PassportNo"] != DBNull.Value ? PassportNo = (string)reader["PassportNo"] : "";
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                Gender = reader["Gender"] != DBNull.Value ? Gender = (byte)reader["Gender"] : null;
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOfBirth = (DateTime)reader["DateOfBirth"] : null;
                                Address = reader["Address"] != DBNull.Value ? Address = (string)reader["Address"] : "";
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                                CountryID = (int)reader["CountryID"];
                                ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                                WhatsApp = reader["WhatsApp"] != DBNull.Value ? WhatsApp = (string)reader["WhatsApp"] : "";
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CityID = (int)reader["CityID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                IsActive = (bool)reader["IsActive"];

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

                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsPerson class:\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }

            }

            return isFound;
        }

        public static bool GetPersonInfoByPhone(ref string Phone, ref int? PersonID, ref string PassportNo,
        ref string NationalNo, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref short? Gender, ref DateTime? DateOfBirth,
        ref string Address, ref string WhatsApp, ref string Email,
        ref string ImagePath, ref int CreatedByUserID, ref int CountryID, ref int CityID,ref DateTime IssueDate,
         ref bool IsActive , enPhoneType phoneType)
        {
            bool isFound = false;




            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "";


                if (phoneType == enPhoneType.Standard)
                {
                    query = "SELECT * FROM People WHERE Phone=@Phone " +
                                   "and isActive=1 ;";
                }
                else
                {
                    query = "SELECT * FROM People WHERE WhatsApp=@WhatsApp " +
                                  "and isActive=1;";


                }
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (phoneType == enPhoneType.Standard)
                    {
                        command.Parameters.AddWithValue("@Phone", Phone);
                    }
                    else
                    {

                        command.Parameters.AddWithValue("@WhatsApp", WhatsApp);
                    }

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                NationalNo = reader["NationalNo"] != DBNull.Value ? NationalNo = (string)reader["NationalNo"] : "";
                                PassportNo = reader["PassportNo"] != DBNull.Value ? PassportNo = (string)reader["PassportNo"] : "";
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                Gender = reader["Gender"] != DBNull.Value ? Gender = (byte)reader["Gender"] : null;
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOfBirth = (DateTime)reader["DateOfBirth"] : null;
                                Address = reader["Address"] != DBNull.Value ? Address = (string)reader["Address"] : "";
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                                CountryID = (int)reader["CountryID"];
                                ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                                WhatsApp = reader["WhatsApp"] != DBNull.Value ? WhatsApp = (string)reader["WhatsApp"] : "";
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CityID = (int)reader["CityID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                IsActive = (bool)reader["IsActive"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsPerson class:\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }
            }
            return isFound;
            }
        public static bool GetPersonInfoByEmail(string Email, ref int? PersonID, ref string PassportNo,
         ref string NationalNo, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref short? Gender, ref DateTime? DateOfBirth,
        ref string Address, ref string WhatsApp, ref string Phone,
        ref string ImagePath, ref int CreatedByUserID, ref int CountryID, ref int CityID,ref DateTime IssueDate,
          ref bool IsActive)
        {
            bool isFound = false;

            string query = "SELECT * FROM People WHERE Email=@Email " +
                               "and isActive=1 ";


            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

            
              
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                   
                        command.Parameters.AddWithValue("@Email", Email);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                PersonID = (int)reader["PersonID"];
                                NationalNo = reader["NationalNo"] != DBNull.Value ? NationalNo = (string)reader["NationalNo"] : "";
                                PassportNo = reader["PassportNo"] != DBNull.Value ? PassportNo = (string)reader["PassportNo"] : "";
                                FirstName = (string)reader["FirstName"];
                                SecondName = (string)reader["SecondName"];
                                ThirdName = (string)reader["ThirdName"];
                                LastName = (string)reader["LastName"];
                                Gender = reader["Gender"] != DBNull.Value ? Gender = (byte)reader["Gender"] : null;
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOfBirth = (DateTime)reader["DateOfBirth"] : null;
                                Address = reader["Address"] != DBNull.Value ? Address = (string)reader["Address"] : "";
                                Phone = (string)reader["Phone"];
                                Email = reader["Email"] != DBNull.Value ? Email = (string)reader["Email"] : "";
                                CountryID = (int)reader["CountryID"];
                                ImagePath = reader["ImagePath"] != DBNull.Value ? ImagePath = (string)reader["ImagePath"] : "";
                                WhatsApp = reader["WhatsApp"] != DBNull.Value ? WhatsApp = (string)reader["WhatsApp"] : "";
                                CreatedByUserID = (int)reader["CreatedByUserID"];
                                CityID = (int)reader["CityID"];
                                IssueDate = (DateTime)reader["IssueDate"];
                                IsActive = (bool)reader["IsActive"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Exception from Data Access layer clsPerson class:\n" + ex.Message,
                            EventLogEntryType.Error);
                        //Console.WriteLine("Error: " + ex.Message);
                        isFound = false;
                    }
                }
            }
            return isFound;
        }


        /// <summary>
        ///  Function responsible to add a new person to the database People table 
        /// </summary>
        /// <param name="NationalNo"> this value is use to show National number in person ID </param>
        /// <param name="PassportNo"> this value represents the passoprt uniuqe ID number </param>

        /// <returns>return either new ID or failed operation.
        ///  in turn function will return a new id or null value .</returns>
        public static int? AddNewPerson(string NationalNo, string PassportNo,
            string FirstName, string SecondName,
           string ThirdName, string LastName, short? Gender, DateTime? DateOfBirth,
           string Address, string Phone, string WhatsApp, string Email,
            string ImagePath, int ?CreatedByUserID, int ?CountryID, int? CityID,DateTime IssueDate,  bool IsActive)
        {
            int? PersonID = null;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_AddNewPerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@CountryID", CountryID);
                    command.Parameters.AddWithValue("@CityID", CityID);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@IsActive", IsActive);
                    SqlParameter outputIdParam = new SqlParameter("@NewPersonID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputIdParam);

                    if (NationalNo == "")
                    {
                        command.Parameters.AddWithValue("@NationalNo", System.DBNull.Value);


                    }

                    else
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    }
                    if (Email == "")
                    {
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Email", Email);

                    }

                    if (PassportNo != "")
                    {
                        command.Parameters.AddWithValue("@PassportNo", PassportNo);


                    }

                    else
                    {
                        command.Parameters.AddWithValue("@PassportNo", System.DBNull.Value);


                    }

                    if (Gender != null)
                    {

                        command.Parameters.AddWithValue("@Gender", Gender);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Gender", System.DBNull.Value);
                    }

                    if (DateOfBirth != null)
                    {

                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@DateOfBirth", System.DBNull.Value);
                    }


                    if (Address != "")
                    {
                        command.Parameters.AddWithValue("@Address", Address);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Address", System.DBNull.Value);
                    }


                    if (WhatsApp != "")
                    {
                        command.Parameters.AddWithValue("@WhatsApp", WhatsApp);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@WhatsApp", System.DBNull.Value);
                    }


                    if (ImagePath != "")
                    {

                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    }

                    else
                    {

                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                    }




                    try
                    {
                        connection.Open();

                       command.ExecuteNonQuery();
                        PersonID = (int)command.Parameters["@NewPersonID"].Value;

                       
                    }
                    catch (SqlException ex)
                    {
                     
                          
                            clsDataAccessSettings.WriteEventToLogFile(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                            return null; 


                    }
                }
                
            }
          
            return PersonID;

        }

        public static bool UpdatePerson(int? PersonID,string NationalNo, string PassportNo,
            string FirstName, string SecondName,
           string ThirdName, string LastName, short? Gender, DateTime? DateOfBirth,
           string Address, string Phone, string WhatsApp, string Email,
            string ImagePath, int? CreatedByUserID, int? CountryID, int? CityID,DateTime IssueDate, bool IsActive)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@SecondName", SecondName);
                    command.Parameters.AddWithValue("@ThirdName", ThirdName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                    command.Parameters.AddWithValue("@CountryID", CountryID);
                    command.Parameters.AddWithValue("@CityID", CityID);
                    command.Parameters.AddWithValue("@IssueDate", IssueDate);
                    command.Parameters.AddWithValue("@IsActive", IsActive);

                    if (NationalNo == "")
                    {
                        command.Parameters.AddWithValue("@NationalNo", System.DBNull.Value);


                    }

                    else
                    {
                        command.Parameters.AddWithValue("@NationalNo", NationalNo);

                    }
                    if (Email == "")
                    {
                        command.Parameters.AddWithValue("@Email", System.DBNull.Value);


                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Email", Email);

                    }

                    if (PassportNo != "")
                    {
                        command.Parameters.AddWithValue("@PassportNo", PassportNo);


                    }

                    else
                    {
                        command.Parameters.AddWithValue("@PassportNo", System.DBNull.Value);


                    }

                    if (Gender != null)
                    {

                        command.Parameters.AddWithValue("@Gender", Gender);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Gender", System.DBNull.Value);
                    }

                    if (DateOfBirth != null)
                    {

                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@DateOfBirth", System.DBNull.Value);
                    }


                    if (Address != "")
                    {
                        command.Parameters.AddWithValue("@Address", Address);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@Address", System.DBNull.Value);
                    }


                    if (WhatsApp != "")
                    {
                        command.Parameters.AddWithValue("@WhatsApp", WhatsApp);

                    }

                    else
                    {
                        command.Parameters.AddWithValue("@WhatsApp", System.DBNull.Value);
                    }


                    if (ImagePath != "")
                    {

                        command.Parameters.AddWithValue("@ImagePath", ImagePath);
                    }

                    else
                    {

                        command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                    }

                    try
                    {
                       connection.Open();
                       rowsAffected = command.ExecuteNonQuery();
            
                    }
                   catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Exception Message From person class DataAccess:\t" + ex.Message,
                            EventLogEntryType.Error);
                        return false;
                    }

                   }
                   
            }
            
            
                   return (rowsAffected > 0);
        
        }
            

        public async static Task < DataTable > GetAllPeople()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query ="select * from FullPersonInfo";

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
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of person class , where data grid view load all people method dropped:\n"
                            + ex.Message, EventLogEntryType.Error);
                         Console.WriteLine("Error: " + ex.Message);
                    }
                }

            }

            return dt;

        }

        public async static Task<DataTable> GetAllPeopleByFullName(string FullName)
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "select * from FullPersonInfo where FullPersonInfo.[الاسم الكامل]= @FullName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@FullName", FullName);
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
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of person class ," +
                            " where data grid view load all people duplicated names  method dropped:\n"
                                                    + ex.Message, EventLogEntryType.Error);
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }

            }

            return dt;

        }

        public static bool DeletePerson(int? PersonID)
        {

            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_DeletePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
               
                        connection.Open();
                       rowsAffected = command.ExecuteNonQuery();
            

            }
            catch (Exception ex)
            {
            
                        clsDataAccessSettings.WriteEventToLogFile("Exception from person data access layer delete (),\n"+ex.Message, EventLogEntryType.Error);
                        throw ex;
                    
                    }
                }
            }


            return (rowsAffected > 0);

        }

       
        /// <summary>
        /// this function might be used with both national no , passport no . 
        /// </summary>
        /// <param name="NationalNo"></param>
        /// <param name="PassportNo"></param>
        /// <returns></returns>
       
        public static bool IsPersonFullNameDuplicated(string FullName)
        {
            bool isDuplicated= false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                string query = "select case" +
                               " when COUNT(*)<=1 then 0 " +
                               " when COUNT(*)>1 then 1 " +
                               " end as IsDuplicated " +
                               " from People " +
                               " where People.FirstName+ ' '+ secondName + ' ' +ThirdName+ ' ' +lastName =@FullName " +
                               " and IsActive=1 ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {


                    command.Parameters.AddWithValue("@FullName", FullName);

                    try
                    {
                        connection.Open();
                        int result = (int)command.ExecuteScalar();
                        isDuplicated = result == 1; // Convert the integer result to a boolean

                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile("clsPerson Data access layer, is Duplicated(FullName) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isDuplicated = false;
                    }
                }
            }

            return isDuplicated;
        }
   
        public static bool IsPersonExist(PersonColumn column, string columnValue)
        {
            string columnName = ""
            ,StoredProcedure = "";
            switch (column)
            {
                case PersonColumn.PersonID:
                    columnName = "PersonID";
                    StoredProcedure = "SP_CheckPersonExistsByPersonID";
                    break;
                case PersonColumn.NationalNo:
                    
                    columnName = "NationalNo";
                    StoredProcedure = "SP_CheckPersonExistsByNationalNo";
                    break;
                case PersonColumn.PassportNo:
                    columnName = "PassportNo";
                    StoredProcedure = "SP_CheckPersonExistsByPassportNo";
                    break;
                case PersonColumn.FullName:
                     columnName = "FirstName+' '+ SecondName +' '+ ThirdName + ' ' + LastName";
                    StoredProcedure = "SP_CheckPersonExistsByFullName";

                    break;
                case PersonColumn.Phone:
                    columnName = "Phone";
                    StoredProcedure = "SP_CheckPersonExistsByPhone";

                    break;
                case PersonColumn.WhatsApp:
                    columnName = "WhatsApp";
                    StoredProcedure = "SP_CheckPersonExistsByWhatsApp";
                    break;
                case PersonColumn.Email:
                    columnName = "Email";
                    StoredProcedure = "SP_CheckPersonExistsByEmail";

                    break;
                default:
                    {
                        clsDataAccessSettings.WriteEventToLogFile("Invalid column type", EventLogEntryType.Warning);
                    throw new ArgumentException("Invalid column type");
                    }
            }

            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                
                using (SqlCommand command = new SqlCommand(StoredProcedure, connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    if (column == PersonColumn.FullName)
                    {

                    command.Parameters.AddWithValue($"@FullName", (object)columnValue ?? DBNull.Value);
                    }
                    else
                    {
                    command.Parameters.AddWithValue($"@{columnName}", (object)columnValue ?? DBNull.Value);
                    }

                    SqlParameter returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    try
                    {
                        connection.Open();
                        
                        command.Parameters.Add(returnParameter);
                        command.ExecuteNonQuery();

                        isFound = (int)returnParameter.Value == 1;



                    }
                    catch (Exception ex)
                    {
                        clsDataAccessSettings.WriteEventToLogFile($"clsPerson Data access layer, is exists({columnName}) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

    }
}
