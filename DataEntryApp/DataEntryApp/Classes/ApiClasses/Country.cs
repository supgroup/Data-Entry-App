using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;
using Newtonsoft.Json.Converters;

namespace DataEntryApp.ApiClasses
{
    public class Country
    {

        public int countryId { get; set; }
        public string code { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public byte isDefault { get; set; }

        private string urimainpath = "Country/";
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Country>> GetAll()
        {

            List<Country> list = new List<Country>();
            //  Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("mainBranchId", mainBranchId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("date", date.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAll");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Country>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;

        }

        public async Task<List<Country>> GetAllRegion()
        {
            List<Country> list = new List<Country>();
            //  Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("mainBranchId", mainBranchId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("date", date.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAllRegion");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Country>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }

       
        //public async Task<decimal> UpdateIsdefault(int countryId)
        //{
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("countryId", countryId.ToString());

        //    //parameters.Add("userId", userId.ToString());
        //    //parameters.Add("final", final.ToString());

        //    string method = urimainpath+ "UpdateIsdefault";
        //    //  return await APIResult.post(method, parameters);
        //    return await APIResult.post(method, parameters);

         
        //}
        //public async Task<TimeSpan> GetOffsetTime(int countryId)
        //{
        //    TimeSpan item = new TimeSpan();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("countryId", countryId.ToString());

        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList("Country/GetOffsetTime", parameters);

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            item = TimeSpan.Parse(c.Value);
        //            break;
        //        }
        //    }
        //    return item;


        //}

    }
}
