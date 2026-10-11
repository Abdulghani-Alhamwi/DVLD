using System;
using System.Data;
using DVLDDataAccessLayer;

namespace DVLDBusinessLayer
{
    public class clsCountries
    {
        public byte CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountries()
        {
            CountryID = 0;
            CountryName = "";
        }

        private clsCountries(byte CountryID,string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountriesData.GetAllCountries();
        }

        public static clsCountries Find(byte CountryID)
        {
            string CountryName = "";

            if (clsCountriesData.Find(CountryID, ref CountryName))
            {
                return new clsCountries(CountryID, CountryName);
            }
            else
                return null;
        }

        public static clsCountries Find(string CountryName)
        {
            byte CountryID = 0;

            if (clsCountriesData.Find(CountryName, ref CountryID))
            {
                return new clsCountries(CountryID, CountryName);
            }
            else
                return null;
        }
    }
}
