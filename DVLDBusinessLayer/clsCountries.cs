using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsCountries
    {
        public static DataTable GetAllCountries()
        {
            return clsCountriesData.GetAllCountries();
        }

        public static int GetCountryID(string CountryName)
        {
            return clsCountriesData.GetCountryID(CountryName);
        }
    }
}
