﻿using DataEntryApp.Classes;
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
    public class Customers
    {
        public long custId { get; set; }
        public string custname { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string department { get; set; }
        public string barcode { get; set; }
        public Nullable<System.DateTime> printDate { get; set; }
        public string image { get; set; }
        public string notes { get; set; }
        public Nullable<long> createUserId { get; set; }
        public Nullable<long> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<long> nationalityId { get; set; }
        public string Nationality { get; set; }
        public Nullable<long> departmentId { get; set; }
        public bool canDelete { get; set; }

        private string urimainpath = "customers/";

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Customers>> GetAll()
        {

            List<Customers> list = new List<Customers>();

            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAll");

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    list.Add(JsonConvert.DeserializeObject<Customers>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
                }
            }
            return list;
        }

        public async Task<decimal> Save(Customers newitem)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string method = urimainpath + "Save";

            var myContent = JsonConvert.SerializeObject(newitem);
            parameters.Add("Object", myContent);
            return await APIResult.post(method, parameters);
        }
        //public async Task<long?> savenationality(customers newObject,Customers newitem)
        //{
            
        //        Nationalities natmodel = new Nationalities();
        //        natmodel.createDate = newObject.createDate;
        //        natmodel.createUserId = newObject.createUserId;
        //        natmodel.updateUserId = newObject.updateUserId;
        //        natmodel.name = newitem.Nationality;
        //        natmodel.nationalityId = newitem.nationalityId==null?0:(long) newitem.nationalityId;
        //        long nid = await natmodel.FindorSave(natmodel);
        //        if (nid > 0)
        //        {
        //            newObject.nationalityId = nid;
        //        }
        //        else
        //        {
        //            newObject.nationalityId = null;
        //        }
            
        //    return newObject.nationalityId;
        //}
        //public async Task<long?> savedepartment(customers newObject, Customers newitem)
        //{

        //    Departments departmentmodel = new Departments();

        //    departmentmodel.name = newitem.department;
        //    departmentmodel.departmentId = newitem.departmentId == null ? 0 : (long)newitem.departmentId;
        //    long depid = await departmentmodel.FindorSave(departmentmodel);
        //    if (depid > 0)
        //    {
        //        newObject.departmentId = depid;
        //    }
        //    else
        //    {
        //        newObject.departmentId = null;
        //    }

        //    return newObject.departmentId;
        //}
        public async Task<Customers> GetByID(long itemId)
        {

            Customers item = new Customers();
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("itemId", itemId.ToString());
            //#################
            IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByID", parameters);

            foreach (Claim c in claims)
            {
                if (c.Type == "scopes")
                {
                    item = JsonConvert.DeserializeObject<Customers>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
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

        //public async Task<string> generateCodeNumber(string type)
        //{
        //    int sequence = await GetLastNumOfCode(type);
        //    sequence++;
        //    string strSeq = sequence.ToString();
        //    if (sequence <= 999999)
        //        strSeq = sequence.ToString().PadLeft(6, '0');
        //    string transNum = type.ToUpper() + "-" + strSeq;
        //    return transNum;
        //}

        //public async Task<int> GetLastNumOfCode(string type)
        //{

        //    try
        //    {
        //        List<string> numberList;
        //        int lastNum = 0;
        //        using (dedbEntities entity = new dedbEntities())
        //        {
        //            numberList = entity.customers.Where(b => b.nu.Contains(type + "-")).Select(b => b.serviceNum).ToList();

        //            for (int i = 0; i < numberList.Count; i++)
        //            {
        //                string code = numberList[i];
        //                string s = code.Substring(code.LastIndexOf("-") + 1);
        //                numberList[i] = s;
        //            }
        //            if (numberList.Count > 0)
        //            {
        //                numberList.Sort();
        //                lastNum = int.Parse(numberList[numberList.Count - 1]);
        //            }
        //        }

        //        return lastNum;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

    }
}
