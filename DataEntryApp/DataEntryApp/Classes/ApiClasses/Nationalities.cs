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
    public class Nationalities
    {
        public long nationalityId { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<long> createUserId { get; set; }
        public Nullable<long> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }

        public bool canDelete { get; set; }



        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Nationalities>> GetAll()
        {

            List<Nationalities> List = new List<Nationalities>();
            bool canDelete = false;
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    List = (from S in entity.nationalities
                            select new Nationalities()
                            {
                                nationalityId = S.nationalityId,
                                name = S.name,
                                notes = S.notes,
                                isActive = S.isActive,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                createDate = S.createDate,
                                updateDate = S.updateDate,


                                canDelete = true,

                            }).ToList();

                    //if (List.Count > 0)
                    //{
                    //    for (int i = 0; i < List.Count; i++)
                    //    {
                    //        if (List[i].isActive == 1)
                    //        {
                    //            int userId = (int)List[i].userId;
                    //            var itemsI = entity.packageUser.Where(x => x.userId == userId).Select(b => new { b.userId }).FirstOrDefault();

                    //            if ((itemsI is null))
                    //                canDelete = true;
                    //        }
                    //        List[i].canDelete = canDelete;
                    //    }
                    //}
                    return List;
                }

            }
            catch
            {
                return List;
            }
        }

        public async Task<decimal> Save(Nationalities newitem)
        {
            nationalities newObject = new nationalities();
            newObject = JsonConvert.DeserializeObject<nationalities>(JsonConvert.SerializeObject(newitem));

            decimal message = 0;
            if (newObject != null)
            {
                if (newObject.updateUserId == 0 || newObject.updateUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.updateUserId = id;
                }
                if (newObject.createUserId == 0 || newObject.createUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.createUserId = id;
                }
           


                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        var locationEntity = entity.Set<nationalities>();
                        if (newObject.nationalityId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.nationalityId;
                        }
                        else
                        {
                            var tmpObject = entity.nationalities.Where(p => p.nationalityId == newObject.nationalityId).FirstOrDefault();
                            newObject.updateDate = DateTime.Now;
                            //  tmpObject.nationalityId = newObject.nationalityId;
                            tmpObject.nationalityId = newObject.nationalityId;
                            tmpObject.name = newObject.name;
                            tmpObject.notes = newObject.notes;
                            tmpObject.isActive = newObject.isActive;
                           // tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            //tmpObject.createDate = newObject.createDate;
                            //tmpObject.updateDate = newObject.updateDate;


                            entity.SaveChanges();

                            message = tmpObject.nationalityId;
                        }
                    }
                    return message;
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public async Task<long> FindorSave(Nationalities newitem)
        {
            nationalities newObject = new nationalities();
            newObject = JsonConvert.DeserializeObject<nationalities>(JsonConvert.SerializeObject(newitem));
            long message = 0;
            if (newObject != null)
            {
                if (newObject.updateUserId == 0 || newObject.updateUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.updateUserId = id;
                }
                if (newObject.createUserId == 0 || newObject.createUserId == null)
                {
                    Nullable<int> id = null;
                    newObject.createUserId = id;
                }
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        var locationEntity = entity.Set<nationalities>();
                        string serchval = newitem.name == null ? "" : newitem.name.Trim();
                            var tmpObject = entity.nationalities.Where(p => p.name == serchval).FirstOrDefault();
                            if (tmpObject == null)
                            {
                                //add
                                newObject.createDate = DateTime.Now;
                                newObject.updateDate = newObject.createDate;
                                newObject.updateUserId = newObject.createUserId;
                                newObject.name = newitem.name.Trim();
                            newObject.isActive = true;
                                locationEntity.Add(newObject);
                                entity.SaveChanges();
                                message = newObject.nationalityId;
                            }
                            else
                            {
                                message = tmpObject.nationalityId;
                            }
                    }
                    return message;
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public async Task<Nationalities> GetByID(int itemId)
        {


            Nationalities item = new Nationalities();
           

            Nationalities row = new Nationalities();
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    var list = entity.nationalities.ToList();
                    row = list.Where(u => u.nationalityId == itemId)
                     .Select(S => new Nationalities()
                     {
                         nationalityId = S.nationalityId,
                         name = S.name,
                         notes = S.notes,
                         isActive = S.isActive,
                         createUserId = S.createUserId,
                         updateUserId = S.updateUserId,
                         createDate = S.createDate,
                         updateDate = S.updateDate,


                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Nationalities();
                //userrow.name = ex.ToString();
                return row;
            }
        }
        public async Task<decimal> Delete(int id, int signuserId, bool final)
        {

            decimal message = 0;
            if (final)
            {
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        nationalities objectDelete = entity.nationalities.Find(id);

                        entity.nationalities.Remove(objectDelete);
                        message = entity.SaveChanges();
                        return message;

                    }
                }
                catch
                {
                    return 0;

                }
            }
            return message;
            //else
            //{
            //    try
            //    {
            //        using (dedbEntities entity = new dedbEntities())
            //        {
            //            nationalities objectDelete = entity.nationalities.Find(userId);

            //            objectDelete.isActive = 0;
            //            objectDelete.updateUserId = signuserId;
            //        objectDelete.updateDate = DateTime.Now;
            //            message = entity.SaveChanges() ;

            //            return message;
            //        }
            //    }
            //    catch
            //    {
            //        return 0;
            //    }
            //}

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
        //            numberList = entity.nationalities.Where(b => b.nu.Contains(type + "-")).Select(b => b.serviceNum).ToList();

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
