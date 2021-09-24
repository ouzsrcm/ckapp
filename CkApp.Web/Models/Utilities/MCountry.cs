using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Library;

namespace CkApp.Web.Models.Utilities
{
    public class MCountry : MBase
    {

        public IList<Country> Countries;
        public IList<City> Cities;
        public IList<District> Districts;

        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }

        public void All()
        {
            this.LoadCountry();
            this.LoadCity();
            this.LoadDistrict();
        }

        public void LoadCountry()
        {
            this.Countries = db.Countries.ToList();
        }

        public void LoadCity(int countryid = 0)
        {
            if (countryid != 0)
            {
                this.Cities = db.Cities.Where(x => x.CountryId == countryid).ToList();
            }
            else
            {
                this.Cities = db.Cities.ToList();
            }
        }

        public void LoadDistrict(int cityid = 0)
        {
            if (cityid != 0)
            {
                this.Districts = db.Districts.Where(x => x.CityId == cityid).ToList();
            }
            else
            {
                this.Districts = db.Districts.ToList();
            }
        }

        public void Selected(int districtid)
        {
            var _district = db.Districts.Where(x => x.DistrictId == districtid).FirstOrDefault();
            this.DistrictId = _district.DistrictId;
            this.CityId = (int)_district.CityId;
            this.CountryId = (int)_district.City.CountryId;
        }

    }
}