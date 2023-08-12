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
using System.Security.Claims;

using DataEntryApp.ApiClasses;

namespace DataEntryApp.Classes
{
    public class SettingCls
    {
        public int settingId { get; set; }
        public string name { get; set; }
        public string trName { get; set; }
        public string notes { get; set; }
        public async Task<List<SettingCls>> GetAll()
        {

            List<SettingCls> list = new List<SettingCls>();
            //  Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("mainBranchId", mainBranchId.ToString());
            //parameters.Add("userId", userId.ToString());
            //parameters.Add("date", date.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("setting/Get");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<SettingCls>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }
            public async Task<List<SettingCls>> GetByNotes(string notes)
        {
            List<SettingCls> list = new List<SettingCls>();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("notes", notes);
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("setting/GetByNotes", parameters);



            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<SettingCls>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }

        public async Task<decimal> Save(SettingCls newitem)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = "setting/Save";

            var myContent = JsonConvert.SerializeObject(newitem);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);

        }


        public async Task<SettingCls> GetByID(int settingId)
        {

            SettingCls item = new SettingCls();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("id", settingId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList("setting/GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<SettingCls>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                    break;
                }
            }
            return item;

        }

        public async Task<decimal> Delete(int Id, int userId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Id", Id.ToString());
            parameters.Add("userId", userId.ToString());

            string method = "setting/Delete";
            return await APIResult.post(method, parameters);
        }
    }
}

