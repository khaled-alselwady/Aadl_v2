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
    public class clsPractitionerData
    {

        public static bool accessPractitionerInfoByPractitionerID(int InputpractitionerID, ref int PersonID, ref int SubscriptionTypeID,
            ref int SubscriptionWayID, ref bool IsLawyer)
        {

            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetPractitionerInfoByPractitionerID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@PractitionerID", InputpractitionerID);

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
                                SubscriptionTypeID = (int)reader["SubscriptionTypeID"];
                                SubscriptionWayID = (int)reader["SubscriptionWayID"];
                                IsLawyer = (bool)reader["IsActive"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your practitioner class data access layer ,get user info by practitioner ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }


                }

            }


            return isFound;

        }

        public static bool accessPractitionerInfoByPersonID(int InputPersonID, ref int practitionerID, ref int SubscriptionTypeID,
            ref int SubscriptionWayID, ref bool IsLawyer)

        {

            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {


                using (SqlCommand command = new SqlCommand("SP_GetPractitionerInfoByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    command.Parameters.AddWithValue("@PersonID", InputPersonID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            if (reader.Read())
                            {
                                // The record was found
                                isFound = true;

                                practitionerID = (int)reader["practitionerID"];
                                SubscriptionTypeID = (int)reader["SubscriptionTypeID"];
                                SubscriptionWayID = (int)reader["SubscriptionWayID"];
                                IsLawyer = (bool)reader["IsActive"];

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
                        clsDataAccessSettings.WriteEventToLogFile("Review your practitioner class data access layer ,get user info by practitioner ID\n Exception:" +
                            ex.Message, EventLogEntryType.Error);
                        isFound = false;
                    }


                }

            }


            return isFound;

        }

        public static bool IsPractitionerExistByPractitionerID(int PractitionerID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsPractitionerExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PractitionerID", PractitionerID);
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
                        clsDataAccessSettings.WriteEventToLogFile("Practitioner  Data access layer, is exists(PractitionerID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }

        public static bool IsPractitionerExistByPersonID(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_IsPractitionerExistsByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);
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
                        clsDataAccessSettings.WriteEventToLogFile("Practitioner  Data access layer, is exists(PersonID) method\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                        isFound = false;
                    }
                }
            }


            return isFound;
        }

        public static DataTable GetAllPractitioners()
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("SP_GetAllPractitionersInfo_View", connection))
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
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of Practitioners class , where data grid view load all people method dropped:\n"
                            + ex.Message, EventLogEntryType.Error);
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }

            }

            return dt;

        }

        public static DataTable GetAllPractitionersFitAdvanceProperties(
                string fullName,
                string phoneNumber,
                string email,
                bool? IsLawyer,
                int? subscriptionTypeId, // Nullable int for optional parameter
                int? subscriptionWayId,   // Nullable int for optional parameter
                string MemberShipNumber,
                string shariaLicenseNumber,
                bool? IsRegulatorActive,    // Nullable int for optional parameter
                bool? IsShariaActive,       
                bool? IsJudgerActive,       
                bool? IsExpertActive,       
                string regulatorCreatedByUserName,
                string shariaCreatedByUserName,
                string JudgerCreatedByUserName,
                string ExpertCreatedByUserName,
                DateTime? regulatorIssueDate,  
                DateTime? shariaIssueDate,     
                DateTime? JudgerIssueDate,     
                DateTime? ExpertIssueDate,     
                DateTime? regulatorIssueDateFrom,
                DateTime? regulatorIssueDateTo,  
                DateTime? shariaIssueDateFrom, 
                DateTime? shariaIssueDateTo,
                DateTime? JudgerIssueDateFrom,
                DateTime? JudgerIssueDateTo,
                DateTime? ExpertIssueDateFrom,
                DateTime? ExpertIssueDateTo,
                bool? isInBlackList,        
                bool? isInRegulatoryWhiteList, 
                bool? isInRegulatoryClosedList,
                bool? isInShariaClosedList,  
                bool? isInShariaWhiteList,
                bool? isInJudgerClosedList,
                bool? isInJudgerWhiteList,
                bool? isInExpertClosedList,
                bool? isInExpertWhiteList
            )
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                using (SqlCommand command = new SqlCommand("dbo.Sp_PractitionerSearch", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters with proper data type and null handling
                    command.Parameters.AddWithValue("@FullName", string.IsNullOrEmpty(fullName) ? (object)DBNull.Value : fullName);
                    command.Parameters.AddWithValue("@PhoneNumber", string.IsNullOrEmpty(phoneNumber) ? (object)DBNull.Value : phoneNumber);
                    command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);
                    command.Parameters.AddWithValue("@IsLawyer", IsLawyer);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", subscriptionTypeId);
                    command.Parameters.AddWithValue("@SubscriptionWayID", subscriptionWayId);
                    command.Parameters.AddWithValue("@MemberShipNumber", string.IsNullOrEmpty(MemberShipNumber) ? (object)DBNull.Value : MemberShipNumber);
                    command.Parameters.AddWithValue("@ShariaLicenseNumber", string.IsNullOrEmpty(shariaLicenseNumber) ? (object)DBNull.Value : shariaLicenseNumber);
                    
                    command.Parameters.AddWithValue("@IsRegulatorActive", IsRegulatorActive);
                    command.Parameters.AddWithValue("@IsShariaActive", IsShariaActive);
                    command.Parameters.AddWithValue("@IsJudgerActive", IsJudgerActive);
                    command.Parameters.AddWithValue("@IsExpertActive", IsExpertActive);
                   
                    command.Parameters.AddWithValue("@RegulatorCreatedByUserName", string.IsNullOrEmpty(regulatorCreatedByUserName) ? (object)DBNull.Value : regulatorCreatedByUserName);
                    command.Parameters.AddWithValue("@ShariaCreatedByUserName", shariaCreatedByUserName);
                    command.Parameters.AddWithValue("@JudgerCreatedByUserName", JudgerCreatedByUserName);
                    command.Parameters.AddWithValue("@ExpertCreatedByUserName", ExpertCreatedByUserName);

                    command.Parameters.AddWithValue("@RegulatorIssueDate", regulatorIssueDate);
                    command.Parameters.AddWithValue("@RegulatorIssueDateFrom", regulatorIssueDateFrom);
                    command.Parameters.AddWithValue("@RegulatorIssueDateTo", regulatorIssueDateTo);
                    
                    command.Parameters.AddWithValue("@ShariaIssueDate", shariaIssueDate);
                    command.Parameters.AddWithValue("@ShariaIssueDateFrom", shariaIssueDateFrom);
                    command.Parameters.AddWithValue("@ShariaIssueDateTo", shariaIssueDateTo);

                    command.Parameters.AddWithValue("@JudgerIssueDate", shariaIssueDate);
                    command.Parameters.AddWithValue("@JudgerIssueDateFrom", shariaIssueDateFrom);
                    command.Parameters.AddWithValue("@JudgerIssueDateTo", shariaIssueDateTo);

                    command.Parameters.AddWithValue("@ExpertIssueDate", shariaIssueDate);
                    command.Parameters.AddWithValue("@ExpertIssueDateFrom", shariaIssueDateFrom);
                    command.Parameters.AddWithValue("@ExpertIssueDateTo", shariaIssueDateTo);

                    command.Parameters.AddWithValue("@IsPractitionerInBlackList", isInBlackList);
                   
                    command.Parameters.AddWithValue("@IsRegulatoryInWhiteList", isInRegulatoryWhiteList);
                    command.Parameters.AddWithValue("@IsRegulatoryInClosedList", isInRegulatoryClosedList);
                  
                    command.Parameters.AddWithValue("@IsShariaInClosedList", isInShariaClosedList);
                    command.Parameters.AddWithValue("@IsShariaInWhiteList", isInShariaWhiteList);


                    command.Parameters.AddWithValue("@IsJudgerInClosedList", isInShariaClosedList);
                    command.Parameters.AddWithValue("@IsJudgerInWhiteList", isInShariaWhiteList);


                    command.Parameters.AddWithValue("@IsExpertInClosedList", isInShariaClosedList);
                    command.Parameters.AddWithValue("@IsExpertInWhiteList", isInShariaWhiteList);

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
                        clsDataAccessSettings.WriteEventToLogFile("Exception comes from data access layer of Practitioners class , where data grid view load all people method dropped:\n"
                            + ex.Message, EventLogEntryType.Error);
                        Console.WriteLine("Error: " + ex.Message);
                    }


                    return dt;
                }
            }
        }
    }
}
