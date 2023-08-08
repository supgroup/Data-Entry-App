using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using DataEntryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataEntryApp.Classes;

using System.Security.Claims;

namespace DataEntryApp.ApiClasses
{
    public class City
    {
        public int cityId { get; set; }
        public string cityCode { get; set; }
        public Nullable<int> countryId { get; set; }

        public async Task<List<City>> Get()
        {

            List<City> list = new List<City>();
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {

                    list = entity.cities
              .Select(c => new City
              {
                  cityId = c.cityId,
                  cityCode = c.cityCode,
                  countryId = c.countryId
              })
              .ToList();

                    return list;
                }

            }
            catch
            {
                return list;
            }
        }
    }
}

