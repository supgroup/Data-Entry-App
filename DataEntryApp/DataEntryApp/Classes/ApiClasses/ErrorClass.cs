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
    public class ErrorClass
    {

        public int errorId { get; set; }
        public string num { get; set; }
        public string msg { get; set; }
        public string stackTrace { get; set; }
        public string targetSite { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<long> createUserId { get; set; }


        private string urimainpath = "errorController/";

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 

        public async Task<List<ErrorClass>> GetAll()
        {
            List<ErrorClass> list = new List<ErrorClass>();
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
                    list.Add(JsonConvert.DeserializeObject<ErrorClass>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }

        public async Task<decimal> Save(ErrorClass obj)
        {

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(obj);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);


        }

        //public async Task<ErrorClass> GetByID(int errorId)
        //{


        //    ErrorClass item = new ErrorClass();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("errorId", errorId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            item = JsonConvert.DeserializeObject<ErrorClass>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        //            break;
        //        }
        //    }
        //    return item;

        //    //ErrorClass obj = new ErrorClass();

        //    //HttpResponseMessage response = new HttpResponseMessage();
        //    //using (var client = new HttpClient())
        //    //{

        //    //    Uri uri = new Uri(Global.APIUri + urimainpath + "GetByID?errorId=" + errorId);

        //    //    response = await ApiConnect.ApiGetConnect(uri);
        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //       var jsonString = await response.Content.ReadAsStringAsync();
        //    //        obj = JsonConvert.DeserializeObject<ErrorClass>(jsonString);
        //    //        return obj;
        //    //    }

        //    //    return obj;
        //    //}
        //}

        //public async Task<decimal> Delete(int errorId, int userId, bool final)
        //{
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("errorId", errorId.ToString());
        //    parameters.Add("userId", userId.ToString());
        //    parameters.Add("final", final.ToString());

        //    string method = urimainpath + "Delete";
        //    return await APIResult.post(method, parameters);

        //    //HttpResponseMessage response = new HttpResponseMessage();
        //    //using (var client = new HttpClient())
        //    //{
        //    //    Uri uri = new Uri(Global.APIUri + urimainpath + "Delete?errorId=" + errorId + "&userId=" + userId + "&final=" + final);
        //    //    response = await ApiConnect.ApiPostConnect(uri);
        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //        var message = await response.Content.ReadAsStringAsync();
        //    //        message = JsonConvert.DeserializeObject<string>(message);
        //    //        return message;
        //    //    }
        //    //    return "";
        //    //}
        //}





    }
}
