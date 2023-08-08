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
    public class Office
    {
        public int officeId { get; set; }
        public string name { get; set; }
        public string agentName { get; set; }
        public Nullable<System.DateTime> joinDate { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string userName { get; set; }
        public string passwordSY { get; set; }
        public string PasswordSoto { get; set; }
        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }

        public bool canDelete { get; set; }



        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Office>> GetAll()
        {

            List<Office> List = new List<Office>();
            bool canDelete = false;
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    List = (from S in entity.office
                            select new Office()
                            {
                                officeId = S.officeId,
                                name = S.name,
                                agentName = S.agentName,
                                joinDate = S.joinDate,
                                mobile = S.mobile,
                                address = S.address,
                                userName = S.userName,
                                passwordSY = S.passwordSY,
                                PasswordSoto = S.PasswordSoto,
                                notes = S.notes,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,

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

        public async Task<decimal> Save(Office newitem)
        {
            office newObject = new office();
            newObject = JsonConvert.DeserializeObject<office>(JsonConvert.SerializeObject(newitem));

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
                        var locationEntity = entity.Set<office>();
                        if (newObject.officeId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.officeId;
                        }
                        else
                        {
                            var tmpObject = entity.office.Where(p => p.officeId == newObject.officeId).FirstOrDefault();

                            tmpObject.updateDate = DateTime.Now;
                         
                            tmpObject.name = newObject.name;
                            tmpObject.agentName = newObject.agentName;
                            tmpObject.joinDate = newObject.joinDate;
                            tmpObject.mobile = newObject.mobile;
                            tmpObject.address = newObject.address;
                            tmpObject.userName = newObject.userName;
                            tmpObject.passwordSY = newObject.passwordSY;
                            tmpObject.PasswordSoto = newObject.PasswordSoto;
                            tmpObject.notes = newObject.notes;
                         
                            tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;

                            entity.SaveChanges();

                            message = tmpObject.officeId;
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
        public async Task<Office> GetByID(int itemId)
        {


            Office item = new Office();
           

            Office row = new Office();
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    var list = entity.office.ToList();
                    row = list.Where(u => u.officeId == itemId)
                     .Select(S => new Office()
                     {
                         officeId = S.officeId,
                         name = S.name,
                         agentName = S.agentName,
                         joinDate = S.joinDate,
                         mobile = S.mobile,
                         address = S.address,
                         userName = S.userName,
                         passwordSY = S.passwordSY,
                         PasswordSoto = S.PasswordSoto,
                         notes = S.notes,
                         createDate = S.createDate,
                         updateDate = S.updateDate,
                         createUserId = S.createUserId,
                         updateUserId = S.updateUserId,

                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Office();
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
                        office objectDelete = entity.office.Find(id);

                        entity.office.Remove(objectDelete);
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
            //            office objectDelete = entity.office.Find(userId);

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
        //            numberList = entity.office.Where(b => b.nu.Contains(type + "-")).Select(b => b.serviceNum).ToList();

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
