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
    public class ServiceData
    {
        public int serviceId { get; set; }
        public string serviceNum { get; set; }
        public string type { get; set; }
        public string passenger { get; set; }
        public string ticketNum { get; set; }
        public string airline { get; set; }
        public Nullable<int> officeId { get; set; }
        public Nullable<System.DateTime> serviceDate { get; set; }
        public string pnr { get; set; }
        public Nullable<decimal> ticketvalueSP { get; set; }
        public Nullable<decimal> ticketvalueDollar { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> priceBeforTax { get; set; }
        public Nullable<decimal> commitionRatio { get; set; }
        public Nullable<decimal> commitionValue { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> saleValue { get; set; }
        public Nullable<decimal> paid { get; set; }
        public Nullable<decimal> profit { get; set; }
        public string notes { get; set; }
        public string state { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUserId { get; set; }
        public Nullable<int> updateUserId { get; set; }
        public Nullable<int> passengerId { get; set; }
        public Nullable<int> flightId { get; set; }
        public Nullable<int> operationId { get; set; }
        public bool canDelete { get; set; }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<ServiceData>> GetAll()
        {

            List<ServiceData> List = new List<ServiceData>();
            bool canDelete = false;
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                    List = (from S in entity.serviceData
                            select new ServiceData()
                            {
                                serviceId = S.serviceId,
                                serviceNum = S.serviceNum,
                                type = S.type,
                                passenger = S.passenger,
                                ticketNum = S.ticketNum,
                                airline = S.airline,
                                officeId = S.officeId,
                                serviceDate = S.serviceDate,
                                pnr = S.pnr,
                                ticketvalueSP = S.ticketvalueSP,
                                ticketvalueDollar = S.ticketvalueDollar,
                                total = S.total,
                                priceBeforTax = S.priceBeforTax,
                                commitionRatio = S.commitionRatio,
                                commitionValue = S.commitionValue,
                                cost = S.cost,
                                saleValue = S.saleValue,
                                paid = S.paid,
                                profit = S.profit,
                                notes = S.notes,
                                state = S.state,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                passengerId = S.passengerId,
                                flightId = S.flightId,
                                operationId = S.operationId,

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
    
        public async Task<decimal> Save(ServiceData newitem)
        {
            serviceData newObject = new serviceData();
            newObject = JsonConvert.DeserializeObject<serviceData>(JsonConvert.SerializeObject(newitem));

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
                if (newObject.officeId == 0 || newObject.officeId == null)
                {
                    Nullable<int> id = null;
                    newObject.officeId = id;
                }
                

                    try
                    {
                        using (dedbEntities entity = new dedbEntities())
                        {
                            var locationEntity = entity.Set<serviceData>();
                            if (newObject.serviceId == 0)
                            {
                            newObject.createDate = DateTime.Now;
                                newObject.updateDate = newObject.createDate;
                                newObject.updateUserId = newObject.createUserId;


                                locationEntity.Add(newObject);
                                entity.SaveChanges();
                                message = newObject.serviceId;
                            }
                            else
                            {
                                var tmpObject = entity.serviceData.Where(p => p.serviceId == newObject.serviceId).FirstOrDefault();

                            tmpObject.updateDate = DateTime.Now;
                           // tmpObject.serviceId = newObject.serviceId;
                            tmpObject.serviceNum = newObject.serviceNum;
                            tmpObject.type = newObject.type;
                            tmpObject.passenger = newObject.passenger;
                            tmpObject.ticketNum = newObject.ticketNum;
                            tmpObject.airline = newObject.airline;
                            tmpObject.officeId = newObject.officeId;
                            tmpObject.serviceDate = newObject.serviceDate;
                            tmpObject.pnr = newObject.pnr;
                            tmpObject.ticketvalueSP = newObject.ticketvalueSP;
                            tmpObject.ticketvalueDollar = newObject.ticketvalueDollar;
                            tmpObject.total = newObject.total;
                            tmpObject.priceBeforTax = newObject.priceBeforTax;
                            tmpObject.commitionRatio = newObject.commitionRatio;
                            tmpObject.commitionValue = newObject.commitionValue;
                            tmpObject.cost = newObject.cost;
                            tmpObject.saleValue = newObject.saleValue;
                            tmpObject.paid = newObject.paid;
                            tmpObject.profit = newObject.profit;
                            tmpObject.notes = newObject.notes;
                            tmpObject.state = newObject.state;
                       
                            tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                            tmpObject.passengerId = newObject.passengerId;
                            tmpObject.flightId = newObject.flightId;
                            tmpObject.operationId = newObject.operationId;


                            entity.SaveChanges();

                                message = tmpObject.serviceId;
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
        public async Task<ServiceData> GetByID(int itemId)
        {


            ServiceData item = new ServiceData();
            string message = "";

            ServiceData row = new ServiceData();
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    var list = entity.serviceData.ToList();
                     row = list.Where(u => u.serviceId == itemId)
                      .Select(S => new ServiceData()
                      {
                          serviceId = S.serviceId,
                          serviceNum = S.serviceNum,
                          type = S.type,
                          passenger = S.passenger,
                          ticketNum = S.ticketNum,
                          airline = S.airline,
                          officeId = S.officeId,
                          serviceDate = S.serviceDate,
                          pnr = S.pnr,
                          ticketvalueSP = S.ticketvalueSP,
                          ticketvalueDollar = S.ticketvalueDollar,
                          total = S.total,
                          priceBeforTax = S.priceBeforTax,
                          commitionRatio = S.commitionRatio,
                          commitionValue = S.commitionValue,
                          cost = S.cost,
                          saleValue = S.saleValue,
                          paid = S.paid,
                          profit = S.profit,
                          notes = S.notes,
                          state = S.state,
                          createDate = S.createDate,
                          updateDate = S.updateDate,
                          createUserId = S.createUserId,
                          updateUserId = S.updateUserId,
                          passengerId = S.passengerId,
                          flightId = S.flightId,
                          operationId = S.operationId,


                      }).FirstOrDefault();
                    return  row;
                }

            }
            catch (Exception ex)
            {
                row = new ServiceData();
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
                            serviceData objectDelete = entity.serviceData.Find(id);

                            entity.serviceData.Remove(objectDelete);
                            message = entity.SaveChanges() ;
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
            //            serviceData objectDelete = entity.serviceData.Find(userId);

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

        public async Task<string> generateCodeNumber(string type)
        {
            int sequence = await GetLastNumOfCode(type);
            sequence++;
            string strSeq = sequence.ToString();
            if (sequence <= 999999)
                strSeq = sequence.ToString().PadLeft(6, '0');
            string transNum = type.ToUpper() + "-" + strSeq;
            return transNum;
        }

        public async Task<int> GetLastNumOfCode(string type)
        {
            
                try
                {
                    List<string> numberList;
                    int lastNum = 0;
                    using (dedbEntities entity = new dedbEntities())
                    {
                        numberList = entity.serviceData.Where(b => b.serviceNum.Contains(type + "-")).Select(b => b.serviceNum).ToList();

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
                catch
                {
                    return 0;
                }
        } 
    }
}
