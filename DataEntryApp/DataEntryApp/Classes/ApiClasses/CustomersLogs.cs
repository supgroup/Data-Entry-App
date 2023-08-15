using DataEntryApp.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public class CustomersLogs
    {
        public long logId { get; set; }
        public Nullable<System.DateTime> sInDate { get; set; }
        public Nullable<System.DateTime> sOutDate { get; set; }
        public Nullable<long> posId { get; set; }
        public Nullable<long> custId { get; set; }
        public string custname { get; set; }
        public string department { get; set; }
        public string Nationality { get; set; }
        public Nullable<long> nationalityId { get; set; }
        public Nullable<long> departmentId { get; set; }
        private string urimainpath = "customersLogs/";


        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<CustomersLogs>> GetAll()
        {

            List<CustomersLogs> list = new List<CustomersLogs>();

            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAll");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<CustomersLogs>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }

        public async Task<decimal> Save(CustomersLogs newitem)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(newitem);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);
        }
        public async Task<decimal> savelog(string barcode, string type)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "savelog";
            parameters.Add("barcode", barcode);
            parameters.Add("type", type);
          
            return await APIResult.post(method, parameters);
        }

        public async Task<CustomersLogs> GetByID(long itemId)
        {

            CustomersLogs item = new CustomersLogs();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("itemId", itemId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<CustomersLogs>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }


            return item;


        }
        public async Task<decimal> Delete(long id, long signuserId, bool final)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("userId", id.ToString());
            parameters.Add("signuserId", signuserId.ToString());
            parameters.Add("final", final.ToString());

            string method = urimainpath + "Delete";
            return await APIResult.post(method, parameters);
        }
     
        

   

    }
}
