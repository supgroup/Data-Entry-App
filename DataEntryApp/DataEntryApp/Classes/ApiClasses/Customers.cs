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



        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Customers>> GetAll()
        {

            List<Customers> List = new List<Customers>();
            bool canDelete = false;
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    List = (from S in entity.customers
                            join N in entity.nationalities on S.nationalityId equals N.nationalityId into JN
                            join D in entity.departments on S.departmentId equals D.departmentId into JD
                            from NAT in JN.DefaultIfEmpty()
                            from DEP in JD.DefaultIfEmpty()
                            select new Customers()
                            {
                                custId = S.custId,
                                custname = S.custname,
                                lastName = S.lastName,
                                mobile = S.mobile,
                                department =DEP.name,
                                barcode = S.barcode,
                                printDate = S.printDate,
                                image = S.image,
                                notes = S.notes,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                isActive = S.isActive,
                                nationalityId = S.nationalityId,
                                Nationality = NAT.name,
                                departmentId = S.departmentId,
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

        public async Task<decimal> Save(Customers newitem)
        {
            customers newObject = new customers();
            newObject = JsonConvert.DeserializeObject<customers>(JsonConvert.SerializeObject(newitem));

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
                departments depmodel = new departments();
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        var locationEntity = entity.Set<customers>();
                        if (newObject.custId == 0)
                        {
                            newObject.createDate = DateTime.Now;
                            newObject.updateDate = newObject.createDate;
                            newObject.updateUserId = newObject.createUserId;
                            //nat
                            newObject.nationalityId= await savenationality(newObject, newitem);
                            newObject.departmentId = await savedepartment(newObject, newitem);
                            //

                            locationEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.custId;
                        }
                        else
                        {
                            var tmpObject = entity.customers.Where(p => p.custId == newObject.custId).FirstOrDefault();
                            tmpObject.updateDate = DateTime.Now;
                          //  tmpObject.custId = newObject.custId;
                            tmpObject.custname = newObject.custname;
                            tmpObject.lastName = newObject.lastName;
                            tmpObject.mobile = newObject.mobile;
                            tmpObject.department = newObject.department;
                            tmpObject.barcode = newObject.barcode;
                            tmpObject.printDate = newObject.printDate;
                            tmpObject.image = newObject.image;
                            tmpObject.notes = newObject.notes;
                          //  tmpObject.createUserId = newObject.createUserId;
                            tmpObject.updateUserId = newObject.updateUserId;
                          //  tmpObject.createDate = newObject.createDate;
                         //   tmpObject.updateDate = newObject.updateDate;
                            tmpObject.isActive = newObject.isActive;
                            //nat
                            //nat
                            tmpObject.nationalityId = await savenationality(newObject, newitem);
                            tmpObject.departmentId = await savedepartment(newObject, newitem);
                            //
                            //


                            entity.SaveChanges();

                            message = tmpObject.custId;
                        }
                    }
                    return message;
                }
                catch(Exception ex)
                {
                    
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        public async Task<long?> savenationality(customers newObject,Customers newitem)
        {
            
                Nationalities natmodel = new Nationalities();
                natmodel.createDate = newObject.createDate;
                natmodel.createUserId = newObject.createUserId;
                natmodel.updateUserId = newObject.updateUserId;
                natmodel.name = newitem.Nationality;
                natmodel.nationalityId = newitem.nationalityId==null?0:(long) newitem.nationalityId;
                long nid = await natmodel.FindorSave(natmodel);
                if (nid > 0)
                {
                    newObject.nationalityId = nid;
                }
                else
                {
                    newObject.nationalityId = null;
                }
            
            return newObject.nationalityId;
        }
        public async Task<long?> savedepartment(customers newObject, Customers newitem)
        {

            Departments departmentmodel = new Departments();

            departmentmodel.name = newitem.department;
            departmentmodel.departmentId = newitem.departmentId == null ? 0 : (long)newitem.departmentId;
            long depid = await departmentmodel.FindorSave(departmentmodel);
            if (depid > 0)
            {
                newObject.departmentId = depid;
            }
            else
            {
                newObject.departmentId = null;
            }

            return newObject.departmentId;
        }
        public async Task<Customers> GetByID(int itemId)
        {


            Customers item = new Customers();
           

            Customers row = new Customers();
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    var list = entity.customers.ToList();
                    row = list.Where(u => u.custId == itemId)
                     .Select(S => new Customers()
                     {
                         custId = S.custId,
                         custname = S.custname,
                         lastName = S.lastName,
                         mobile = S.mobile,
                         department = S.department,
                         barcode = S.barcode,
                         printDate = S.printDate,
                         image = S.image,
                         notes = S.notes,
                         createUserId = S.createUserId,
                         updateUserId = S.updateUserId,
                         createDate = S.createDate,
                         updateDate = S.updateDate,
                         isActive = S.isActive,
                         nationalityId = S.nationalityId,


                     }).FirstOrDefault();
                    return row;
                }

            }
            catch (Exception ex)
            {
                row = new Customers();
                //userrow.name = ex.ToString();
                return row;
            }
        }
        public async Task<decimal> Delete(long id, long signuserId, bool final)
        {

            decimal message = 0;
            if (final)
            {
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        customers objectDelete = entity.customers.Find(id);

                        entity.customers.Remove(objectDelete);
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
            //            customers objectDelete = entity.customers.Find(userId);

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
