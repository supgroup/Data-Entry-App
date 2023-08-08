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
    public class PayOp
    {
        public int payOpId { get; set; }
        public string code { get; set; }
        public Nullable<decimal> cash { get; set; }
        public string opType { get; set; }
        public string side { get; set; }
        public Nullable<int> serviceId { get; set; }
        public string opStatus { get; set; }
        public Nullable<System.DateTime> opDate { get; set; }
        public string notes { get; set; }
        public Nullable<int> officeId { get; set; }
        public Nullable<int> passengerId { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<PayOp>> GetAll()
        {

            List<PayOp> List = new List<PayOp>();
            bool canDelete = false;
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    List = (from S in entity.payOp
                            select new PayOp()
                            {
                                payOpId = S.payOpId,
                                code = S.code,
                                cash = S.cash,
                                opType = S.opType,
                                side = S.side,
                                serviceId = S.serviceId,
                                opStatus = S.opStatus,
                                opDate = S.opDate,
                                notes = S.notes,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                officeId = S.officeId,
                                passengerId = S.passengerId,
                                userId = S.userId,

                            }).ToList();

                    return List;
                }

            }
            catch
            {
                return List;
            }

        }

        public async Task<decimal> Save(PayOp newitem)
        {
            payOp newObject = new payOp();
            newObject = JsonConvert.DeserializeObject<payOp>(JsonConvert.SerializeObject(newitem));

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
                if (newObject.serviceId == 0 || newObject.serviceId == null)
                {
                    Nullable<int> id = null;
                    newObject.serviceId = id;
                }

                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        var locationEntity = entity.Set<payOp>();
                        if (newObject.serviceId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;


                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.payOpId;
                        }
                        else
                        {
                            var tmpObject = entity.payOp.Where(p => p.payOpId == newObject.payOpId).FirstOrDefault();

                            tmpObject.updateDate = DateTime.Now;
                            //   tmpObject.payOpId = newObject.payOpId;
                            tmpObject.code = newObject.code;
                            tmpObject.cash = newObject.cash;
                            tmpObject.opType = newObject.opType;
                            tmpObject.side = newObject.side;
                            tmpObject.serviceId = newObject.serviceId;
                            tmpObject.opStatus = newObject.opStatus;
                            tmpObject.opDate = newObject.opDate;
                            tmpObject.notes = newObject.notes;
                            tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            // tmpObject.createDate = newObject.createDate;
                      
                            tmpObject.officeId = newObject.officeId;
                            tmpObject.passengerId = newObject.passengerId;
                            tmpObject.userId = newObject.userId;

                            entity.SaveChanges();

                            message = tmpObject.payOpId;
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

        public async Task<PayOp> GetByID(int payOpId)
        {

            string message = "";

            PayOp row = new PayOp();
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    var list = entity.payOp.ToList();
                    row = list.Where(u => u.payOpId == payOpId)
                      .Select(S => new PayOp()
                      {
                          payOpId = S.payOpId,
                          code = S.code,
                          cash = S.cash,
                          opType = S.opType,
                          side = S.side,
                          serviceId = S.serviceId,
                          opStatus = S.opStatus,
                          opDate = S.opDate,
                          notes = S.notes,
                          createUserId = S.createUserId,
                          updateUserId = S.updateUserId,
                          createDate = S.createDate,
                          updateDate = S.updateDate,
                          officeId = S.officeId,
                          passengerId = S.passengerId,
                          userId = S.userId,


                      }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new PayOp();
                //userrow.name = ex.ToString();
                return row;
            }

        }

        public async Task<PayOp> getLastPayOp(int packageUserId)
        {
            PayOp item = new PayOp();

            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("packageUserId", packageUserId.ToString());
            ////#################
            //IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "getLastPayOp", parameters);

            //foreach (Claim c in claims)
            //{
            //    if (c.Type == "scopes")
            //    {
            //        item = JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            //        break;
            //    }
            //}

            return item;


        }

        public async Task<List<PayOp>> GetByCustomerId(int customerId)
        {
            List<PayOp> list = new List<PayOp>();
            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //parameters.Add("customerId", customerId.ToString());
            ////#################
            //IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByCustomerId", parameters);

            //foreach (Claim c in claims)
            //{
            //    if (c.Type == "scopes")
            //    {
            //        list.Add(JsonConvert.DeserializeObject<PayOp>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
            //    }
            //}
            return list;

        }
        public async Task<List<PayOp>> GetByOfficeId(int officeId)
        {
            List<PayOp> list = new List<PayOp>();
            using (dedbEntities entity = new dedbEntities())
            {
                list = (from S in entity.payOp
                        where (S.serviceData.officeId == officeId)
                        select new PayOp()
                        {
                            payOpId = S.payOpId,
                            code = S.code,
                            cash = S.cash,
                            opType = S.opType,
                            side = S.side,
                            serviceId = S.serviceId,
                            opStatus = S.opStatus,
                            opDate = S.opDate,
                            notes = S.notes,
                            createUserId = S.createUserId,
                            updateUserId = S.updateUserId,
                            createDate = S.createDate,
                            updateDate = S.updateDate,


                        }).ToList();
            }
            return list;

        }

        public async Task<decimal> Delete(int id, int userId, bool final)
        {
            decimal message = 0;
            if (final)
            {
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        payOp objectDelete = entity.payOp.Find(id);

                        entity.payOp.Remove(objectDelete);
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
            //            payOp objectDelete = entity.payOp.Find(userId);

            //            objectDelete.isActive = 0;
            //            objectDelete.updateUserId = signuserId;
            //            objectDelete.updateDate = DateTime.Now;
            //            message = entity.SaveChanges();

            //            return message;
            //        }
            //    }
            //    catch
            //    {
            //        return 0;
            //    }
            //}

        }

        public async Task<int> GetLastNum(string payOpCode)
        {
           

            List<string> numberList;
            int lastNum = 0;
            using (dedbEntities entity = new dedbEntities())
            {
                numberList = entity.payOp.Where(b => b.code.Contains(payOpCode + "-")).Select(b => b.code).ToList();

                for (int i = 0; i < numberList.Count; i++)
                {
                    string code = numberList[i];
                    string s = code.Substring(code.LastIndexOf("-") + 1);
                    numberList[i] = s;
                }
                if (numberList.Count > 0)
                {
                    numberList.Sort();
                    lastNum = int.Parse(numberList[numberList.Count - 1]);
                }
            }
            return lastNum;
        }

           
       

    public async Task<string> generateNumber(string payOpCode)
    {
        int sequence = await GetLastNum(payOpCode);
        sequence++;
        string strSeq = sequence.ToString();
        if (sequence <= 999999)
            strSeq = sequence.ToString().PadLeft(6, '0');
        string payOpNum = payOpCode + "-" + strSeq;
        return payOpNum;
    }


    /// ////



}
}
