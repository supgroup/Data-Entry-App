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
        private string urimainpath = "city/";

        public async Task<List<City>> Get()
        {


            List<City> list = new List<City>();
            //  Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("mainBranchId", mainBranchId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("date", date.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "Get");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<City>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }
    }
}

