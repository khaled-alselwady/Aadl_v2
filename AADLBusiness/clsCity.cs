using AADL_DataAccess;
using System.Data;
using System.Threading.Tasks;

namespace AADLBusiness
{
    public class clsCity
    {

        public int ID { set; get; }
        public string CityName { set; get; }

        public clsCity()
        {
            this.ID = -1;
            this.CityName = "";

        }

        private clsCity(int ID, string CityName)

        {
            this.ID = ID;
            this.CityName = CityName;
        }

        public static clsCity Find(int ID)
        {
            string CityName = "";

            if (clsCityData.GetCityInfoByID(ID, ref CityName))

                return new clsCity(ID, CityName);
            else
                return null;

        }

        public static clsCity Find(string CityName)
        {

            int ID = -1;

            if (clsCityData.GetCityInfoByName(CityName, ref ID))

                return new clsCity(ID, CityName);
            else
                return null;

        }

        public static DataTable GetAllCities()
        {
            return clsCityData.GetAllCities();

        }

        public async static Task<DataTable> GetAllCitiesByCountryIDAsync(int CountryID)
        {
            return await clsCityData.GetAllCitiesByCountryIDAsync(CountryID);

        }


    }
}

