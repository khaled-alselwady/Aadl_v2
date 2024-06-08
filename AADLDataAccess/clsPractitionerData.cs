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
                string shariaLicenseNumber,
                int? subscriptionTypeId, // Nullable int for optional parameter
                int? subscriptionWayId,   // Nullable int for optional parameter
                string MemberShipNumber,
                bool? isRegulatorActive,    // Nullable int for optional parameter
                bool? isShariaActive,       // Nullable int for optional parameter
                string shariaCreatedByUserName,
                string regulatorCreatedByUserName,
                DateTime? regulatorIssueDate,  // Nullable DateTime for optional parameter
                DateTime? shariaIssueDate,     // Nullable DateTime for optional parameter
                DateTime? regulatorIssueDateFrom, // Nullable DateTime for optional parameter
                DateTime? regulatorIssueDateTo,  // Nullable DateTime for optional parameter
                DateTime? shariaIssueDateFrom, // Nullable DateTime for optional parameter
                DateTime? shariaIssueDateTo,  // Nullable DateTime for optional parameter
                bool? isInBlackList,        // Nullable bool for optional parameter
                bool? isInRegulatoryWhiteList, // Nullable bool for optional parameter
                bool? isInRegulatoryClosedList, // Nullable bool for optional parameter
                bool? isInShariaClosedList,  // Nullable bool for optional parameter
                bool? isInShariaWhiteList     // Nullable bool for optional parameter
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
                    command.Parameters.AddWithValue("@ShariaLicenseNumber", string.IsNullOrEmpty(shariaLicenseNumber) ? (object)DBNull.Value : shariaLicenseNumber);
                    command.Parameters.AddWithValue("@SubscriptionTypeID", subscriptionTypeId);
                    command.Parameters.AddWithValue("@SubscriptionWayID", subscriptionWayId);
                    command.Parameters.AddWithValue("@MemberShipNumber", string.IsNullOrEmpty(MemberShipNumber) ? (object)DBNull.Value : MemberShipNumber);
                    command.Parameters.AddWithValue("@IsRegulatorActive", isRegulatorActive);
                    command.Parameters.AddWithValue("@IsShariaActive", isShariaActive);
                    command.Parameters.AddWithValue("@ShariaCreatedByUserName", shariaCreatedByUserName);
                    command.Parameters.AddWithValue("@RegulatorCreatedByUserName", string.IsNullOrEmpty(regulatorCreatedByUserName) ? (object)DBNull.Value : regulatorCreatedByUserName);
                    command.Parameters.AddWithValue("@RegulatorIssueDate", regulatorIssueDate);
                    command.Parameters.AddWithValue("@RegulatorIssueDateFrom", regulatorIssueDateFrom);
                    command.Parameters.AddWithValue("@RegulatorIssueDateTo", regulatorIssueDateTo);
                    command.Parameters.AddWithValue("@ShariaIssueDate", shariaIssueDate);
                    command.Parameters.AddWithValue("@ShariaIssueDateFrom", shariaIssueDateFrom);
                    command.Parameters.AddWithValue("@ShariaIssueDateTo", shariaIssueDateTo);
                    command.Parameters.AddWithValue("@IsPractitionerInBlackList", isInBlackList);
                    command.Parameters.AddWithValue("@IsInRegulatoryWhiteList", isInRegulatoryWhiteList);
                    command.Parameters.AddWithValue("@IsInRegulatoryClosedList", isInRegulatoryClosedList);
                    command.Parameters.AddWithValue("@IsInShariaClosedList", isInShariaClosedList);
                    command.Parameters.AddWithValue("@IsInShariaWhiteList", isInShariaWhiteList);


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
