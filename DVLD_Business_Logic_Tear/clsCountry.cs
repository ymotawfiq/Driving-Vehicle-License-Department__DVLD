using DVLD_Data_Access_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Logic_Tear
{
    public class clsCountry
    {

        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountry()
        {
            CountryID = -1;
            CountryName = "";
        }

        private clsCountry(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryAccessData.GetAllCountries();
        }

        public static clsCountry FindCountryByName(string countryName)
        {
            int countryID = -1;
            clsCountryAccessData.FindCountryByName(countryName, ref countryID);
            return new clsCountry(countryID, countryName);
        }

        public static clsCountry FindCountryByID(int countryID)
        {
            string countryName = "";
            clsCountryAccessData.FindCountryByID(countryID, ref countryName);
            return new clsCountry(countryID, countryName);
        }

        public static bool ExistsByCountryNameForFilter(string countryName)
        {
            return clsCountryAccessData.ExistsByCountryNameForFilter(countryName);
        }

    }
}
