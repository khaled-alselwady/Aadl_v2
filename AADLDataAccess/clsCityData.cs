using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace AADL_DataAccess
{
    public class clsCityData
    {

        public static bool GetCityInfoByID(int ID, ref string CityName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Cities WHERE CityID = @CityID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CityID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    CityName = (string)reader["CityName"];

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
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetCityInfoByName(string CityName, ref int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CityName = @CityName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CityName", CityName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    ID = (int)reader["CityID"];

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
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        [Obsolete ("this one is useless because it will brought all the cities in the world, also it might hang the program because " +
            "it slow one, you could use GetAllCitiesByCountryID() method it's better than this one .")]
        public static DataTable GetAllCities()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Cities order by CityName";

            SqlCommand command = new SqlCommand(query, connection);

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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }
        public async static Task<DataTable> GetAllCitiesByCountryIDAsync(int CountryID)
        {

            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {

                string query = "SELECT * FROM Cities  where CountryID=@CountryID ";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                    command.Parameters.AddWithValue("@CountryID", CountryID);
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
                        clsDataAccessSettings.WriteEventToLogFile("Exception from city class in data access layer:\n" + ex.Message, System.Diagnostics.EventLogEntryType.Error);    
                }
                }
            }


            return dt;

        }

    }
}
