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
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                     list = entity.setting
                       .Select(c => new SettingCls
                       {
                           settingId=c.settingId,
                           name=c.name,
                           notes= c.notes,

                       }).ToList();

                    return list;

                }

            }
            catch
            {
                return list;
            }
        }
            public async Task<List<SettingCls>> GetByNotes(string notes)
        {
            List<SettingCls> list = new List<SettingCls>();        
            using (dedbEntities entity = new dedbEntities())
            {

                List<setting> settingList1 = entity.setting.ToList();
                 list = settingList1.Where(c => c.notes == notes).Select(c => new SettingCls
                {
                    settingId = c.settingId,
                    name = c.name,
                    notes = c.notes,
                }).ToList();

                return list;
            }
        }

        public async Task<decimal> Save(SettingCls newitem)
        {
            setting newObject = new setting();
            newObject = JsonConvert.DeserializeObject<setting>(JsonConvert.SerializeObject(newitem));
             
            decimal message = 0;
            if (newObject != null)
            {
                 
                setting tmpObject; 
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {

                        var sEntity = entity.Set<setting>();
                        if (newObject.settingId == 0)
                        {

                            sEntity.Add(newObject);
                            entity.SaveChanges();
                            message = newObject.settingId;
                        }
                        else
                        {
                            tmpObject = entity.setting.Where(p => p.settingId == newObject.settingId).FirstOrDefault();
                             
                            tmpObject.settingId = newObject.settingId;
                            tmpObject.name = newObject.name;
                            tmpObject.notes = newObject.notes;
                             
                            entity.SaveChanges();
                            message = tmpObject.settingId ;
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


        public async Task<SettingCls> GetByID(int settingId)
        {

            SettingCls item = new SettingCls();
            int Id = 0;
            
            using (dedbEntities entity = new dedbEntities())
            {

                item = entity.setting
               .Where(c => c.settingId == Id)
               .Select(c => new SettingCls
               {
                 settingId=  c.settingId,
                   name=  c.name,
                   notes=  c.notes,
               }).FirstOrDefault();
                return item;
            }
        }

        public async Task<decimal> Delete(int Id, int userId)
        {
            decimal message = 0;
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    setting sObj = entity.setting.Find(Id);

                    entity.setting.Remove(sObj);
                    message = entity.SaveChanges() ;

                    //  return Ok("medal is Deleted Successfully");
                }
                return message;
            }
            catch
            {
                return 0;
            }
        }
    }
}

