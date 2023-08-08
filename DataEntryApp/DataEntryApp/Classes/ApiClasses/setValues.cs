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
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

using System.Security.Claims;
using DataEntryApp.ApiClasses;
namespace DataEntryApp.Classes
{
    public class SetValues
    {
        public int valId { get; set; }
        public string value { get; set; }
        public Nullable<int> isDefault { get; set; }
        public Nullable<int> isSystem { get; set; }
        public string notes { get; set; }
        public Nullable<int> settingId { get; set; }
        //setting
        public string name { get; set; }
        public async Task<List<SetValues>> GetAll()
        {

            List<SetValues> list = new List<SetValues>();
            try
            {


                using (dedbEntities entity = new dedbEntities())
                {


                   list = entity.setValues

               .Select(c => new SetValues
               {
                valId =  c.valId,
                 value = c.value,
                  isDefault= c.isDefault,
                 isSystem = c.isSystem,
                notes  = c.notes,
                settingId =  c.settingId,

               })
                           .ToList();

                    return list;

                }

            }
            catch
            {
                return list;
            }

        }


        //print

        public async Task<List<SetValues>> GetBySetvalNote(string setvalnote)
        {
            List<SetValues> list = new List<SetValues>();
         

             

            // DateTime cmpdate = DateTime.Now.AddDays(newdays);
            try
            {


                using (dedbEntities entity = new dedbEntities())
                {
                    // setting sett = entity.setting.Where(s => s.name == name).FirstOrDefault();
                     list = entity.setValues.ToList().Where(x => x.notes == setvalnote)
                         .Select(X => new SetValues
                         {
                            valId= X.valId,
                           value=  X.value,
                          isDefault  = X.isDefault,
                            isSystem =X.isSystem,
                          settingId =  X.settingId,
                          notes =  X.notes,
                             name = entity.setting.ToList().Where(s => s.settingId == X.settingId).FirstOrDefault().name,

                         })
                         .ToList();

                    return list;

                }

            }
            catch
            {
                return list;
            }

        }
        // email
        public async Task<List<SetValues>> GetBySetName(string name)
        {

            List<SetValues> list = new List<SetValues>();
            try
            {


                using (dedbEntities entity = new dedbEntities())
                {
                    setting sett = entity.setting.Where(s => s.name == name).FirstOrDefault();
                     list = entity.setValues.Where(x => sett.settingId == x.settingId)
                         .Select(X => new SetValues
                         {
                          
                                valId= X.valId,
                           value=  X.value,
                          isDefault  = X.isDefault,
                            isSystem =X.isSystem,
                          settingId =  X.settingId,
                          notes =  X.notes,

                         })
                         .ToList();
                    return list;

                }

            }
            catch
            {
                return list;
            }

        }
        // email
        public async Task<decimal> SaveValueByNotes(SetValues obj)
        {

            decimal message = 0;
            setValues newObject = new setValues();
            newObject = JsonConvert.DeserializeObject<setValues>(JsonConvert.SerializeObject(obj));

            if (newObject != null)
            {


                setValues tmpObject = null;


                try
                {
                    if (newObject.settingId == 0 || newObject.settingId == null)
                    {
                        Nullable<int> id = null;
                        newObject.settingId = id;
                    }
                    using (dedbEntities entity = new dedbEntities())
                    {
                        setValues defItem = new setValues();
                        var sEntity = entity.Set<setValues>();

                        defItem = entity.setValues.Where(p => p.settingId == newObject.settingId).FirstOrDefault();



                        if (newObject.valId == 0)
                        {
                            if (newObject.isDefault == 1)
                            {
                                // get the row with same settingId of newObject
                                if (defItem != null)
                                {
                                    defItem.isDefault = 0;
                                    entity.SaveChanges();
                                }
                            }
                            else //newObject.isDefault ==0 
                            {
                                if (defItem == null)//other values isDefault not 1 
                                {
                                    newObject.isDefault = 1;
                                }

                            }
                            sEntity.Add(newObject);
                            message = newObject.valId ;
                            entity.SaveChanges();
                        }
                        else
                        {
                            if (newObject.isDefault == 1)
                            {
                                defItem.isDefault = 0;//reset the other default to 0 if exist
                            }
                            var tmps1 = sEntity.ToList();
                            tmpObject = tmps1.Where(p => p.notes == newObject.notes && p.settingId == newObject.settingId && p.valId == newObject.valId).FirstOrDefault();
                            //   tmpObject.valId = newObject.valId;
                            // tmpObject.notes = newObject.notes;
                            tmpObject.value = newObject.value;
                            tmpObject.isDefault = newObject.isDefault;
                            tmpObject.isSystem = newObject.isSystem;

                            tmpObject.settingId = newObject.settingId;
                            entity.SaveChanges();
                            message = tmpObject.valId ;
                        }


                    }


                    return message;

                }
                catch
                {
             
                    return 0;
                }


            }

            return message;

        }

        public async Task<decimal> Save(SetValues obj)
        {
            decimal message = 0;
            setValues newObject = new setValues();
            newObject = JsonConvert.DeserializeObject<setValues>(JsonConvert.SerializeObject(obj));

            if (newObject != null)
            {
                setValues tmpObject = null;
                try
                {
                    if (newObject.settingId == 0 || newObject.settingId == null)
                    {
                        Nullable<int> id = null;
                        newObject.settingId = id;
                    }
                    using (dedbEntities entity = new dedbEntities())
                    {
                        var sEntity = entity.Set<setValues>();
                        setValues defItem = entity.setValues.Where(p => p.settingId == newObject.settingId && p.isDefault == 1).FirstOrDefault();

                        if (newObject.valId == 0)
                        {
                            if (newObject.isDefault == 1)
                            { // get the row with same settingId of newObject
                                if (defItem != null)
                                {
                                    defItem.isDefault = 0;
                                    entity.SaveChanges();
                                }
                            }
                            else //Object.isDefault ==0 
                            {
                                if (defItem == null)//other values isDefault not 1 
                                {
                                    newObject.isDefault = 1;
                                }

                            }
                            sEntity.Add(newObject);
                            message = newObject.valId;
                            entity.SaveChanges();
                        }
                        else
                        {
                            if (newObject.isDefault == 1)
                            {
                                defItem.isDefault = 0;//reset the other default to 0 if exist
                            }
                            tmpObject = entity.setValues.Where(p => p.valId == newObject.valId).FirstOrDefault();
                            tmpObject.valId = newObject.valId;
                            tmpObject.notes = newObject.notes;
                            tmpObject.value = newObject.value;
                            tmpObject.isDefault = newObject.isDefault;
                            tmpObject.isSystem = newObject.isSystem;

                            tmpObject.settingId = newObject.settingId;
                            entity.SaveChanges();
                            message = tmpObject.valId;
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
            public async Task<SetValues> GetByID(int valId)
        {
            SetValues item = new SetValues();
                int Id = 0;
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {

                       item = entity.setValues 
                   .Where(c => c.valId == Id)
                   .Select(c => new SetValues
                   {
                      
                            valId =  c.valId,
                 value = c.value,
                  isDefault= c.isDefault,
                 isSystem = c.isSystem,
                notes  = c.notes,
                settingId =  c.settingId,

                   }).FirstOrDefault();
                        return item;

                    }

                }
                catch
                {
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
                        setValues sObj = entity.setValues.Find(Id);

                        entity.setValues.Remove(sObj);
                        message = entity.SaveChanges() ;

                    }
                    return message;
                }
                catch
                {
                    return 0;
                }
            }


            // get is exist

            //image part
            #region image


        //    public async Task<string> uploadImage(string imagePath, string imageName, int valId)
         
        //{
        //    if (imagePath != "")
        //    {
        //        //string imageName = userId.ToString();
        //        MultipartFormDataContent form = new MultipartFormDataContent();
        //        // get file extension
        //        var ext = imagePath.Substring(imagePath.LastIndexOf('.'));
        //        var extension = ext.ToLower();
        //        try
        //        {
        //            // configure trmporery path
        //            //string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        //            string dir = Directory.GetCurrentDirectory();
        //            string tmpPath = Path.Combine(dir, Global.TMPSettingFolder);
        //            tmpPath = Path.Combine(tmpPath, imageName + extension);
        //            if (System.IO.File.Exists(tmpPath))
        //            {
        //                System.IO.File.Delete(tmpPath);
        //            }
        //            // resize image
        //            ImageProcess imageP = new ImageProcess(150, imagePath);
        //            imageP.ScaleImage(tmpPath);
        //            Image newimg=Image.FromFile(tmpPath);
                  
        //            newimg.Save(tmpPath);
        //            File.Copy(tmpPath, fileSavePath, true);
        //            // read image file
        //            var stream = new FileStream(tmpPath, FileMode.Open, FileAccess.Read);

        //            // create http client request
        //            using (var client = new HttpClient())
        //            {
        //                client.BaseAddress = new Uri(Global.APIUri);
        //                client.Timeout = System.TimeSpan.FromSeconds(3600);
        //                string boundary = string.Format("----WebKitFormBoundary{0}", DateTime.Now.Ticks.ToString("x"));
        //                HttpContent content = new StreamContent(stream);
        //                content.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
        //                content.Headers.Add("client", "true");

        //                string fileName = imageName + extension;
        //                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //                {
        //                    Name = imageName,
        //                    FileName = fileName
        //                };
        //                form.Add(content, "fileToUpload");

        //                var response = await client.PostAsync(@"setValues/PostImage", form);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    // save image name in DB
        //                    SetValues setValues = new SetValues();
        //                    setValues.valId = valId;
        //                    setValues.value = fileName;
        //                    await updateImage(setValues);
        //                    return fileName;
        //                }
        //            }
        //            stream.Dispose();
        //            //delete tmp image
        //            if (System.IO.File.Exists(tmpPath))
        //            {
        //                System.IO.File.Delete(tmpPath);
        //            }
        //        }
        //        catch
        //        { return ""; }
        //    }
        //    return "";
        //}
        public async Task<string> saveImage(string imageSource, string imageName )

        {
            if ( imageSource != "")
            {
              //  Image newimg = Image.FromFile("");
                // get file extension
                var ext = imageSource.Substring(imageSource.LastIndexOf('.'));
                var extension = ext.ToLower();
                try
                {
                    // configure trmporery path
                    //string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    string dir = Directory.GetCurrentDirectory();
                    string tmpPath = Path.Combine(dir, Global.TMPSettingFolder);
                    string imageext = imageName + extension;
                    tmpPath = Path.Combine(tmpPath, imageext);
                    if (System.IO.File.Exists(tmpPath))
                    {
                        System.IO.File.Delete(tmpPath);
                    }
                    File.Copy(imageSource, tmpPath, true);
                    // resize image
                    ImageProcess imageP = new ImageProcess(150, imageSource);
                    imageP.ScaleImage(tmpPath);
                    //vallogo.value = imageext;
                    //vallogo.Save(vallogo);
                    return imageext;
                }
                catch
                { return ""; }
            }
            return "";
        }
        //public async Task<decimal> updateImage(SetValues setValues)
        //{
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    string method = "setValues/UpdateImage";

        //    var myContent = JsonConvert.SerializeObject(setValues);
        //    parameters.Add("Object", myContent);
        //    return await APIResult.post(method, parameters);

        //    //string message = "";

        //    //// ... Use HttpClient.
        //    //ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

        //    //string myContent = JsonConvert.SerializeObject(setValues);

        //    //using (var client = new HttpClient())
        //    //{
        //    //    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //    //    client.BaseAddress = new Uri(Global.APIUri);
        //    //    client.DefaultRequestHeaders.Clear();
        //    //    client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
        //    //    client.DefaultRequestHeaders.Add("Keep-Alive", "3600");

        //    //    HttpRequestMessage request = new HttpRequestMessage();

        //    //    // encoding parameter to get special characters
        //    //    myContent = HttpUtility.UrlEncode(myContent);

        //    //    request.RequestUri = new Uri(Global.APIUri + "setValues/UpdateImage?SetValuesObject=" + myContent);
        //    //    request.Headers.Add("APIKey", Global.APIKey);
        //    //    request.Method = HttpMethod.Post;
        //    //    //set content type
        //    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //    var response = await client.SendAsync(request);

        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //        message = await response.Content.ReadAsStringAsync();
        //    //        message = JsonConvert.DeserializeObject<string>(message);
        //    //    }
        //    //    return message;
        //    //}
        //}

        public async Task<byte[]> downloadImage(string imageName)

        {
            Stream jsonString = null;
            byte[] byteImg = null;
            Image img = null;
            // ... Use HttpClient.
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (var client = new HttpClient())
            {
                ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                client.BaseAddress = new Uri(Global.APIUri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                client.DefaultRequestHeaders.Add("Keep-Alive", "3600");
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri(Global.APIUri + "setValues/GetImage?imageName=" + imageName);
                //request.Headers.Add("APIKey", Global.APIKey);
                request.Method = HttpMethod.Get;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    jsonString = await response.Content.ReadAsStreamAsync();
                    img = Bitmap.FromStream(jsonString);
                    byteImg = await response.Content.ReadAsByteArrayAsync();

                    // configure trmporery path
                    //string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    string dir = Directory.GetCurrentDirectory();
                    string tmpPath = Path.Combine(dir, Global.TMPSettingFolder);
                    if (!Directory.Exists(tmpPath))
                        Directory.CreateDirectory(tmpPath);
                    tmpPath = Path.Combine(tmpPath, imageName);
                    if (System.IO.File.Exists(tmpPath))
                    {
                        System.IO.File.Delete(tmpPath);
                    }
                    using (FileStream fs = new FileStream(tmpPath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        fs.Write(byteImg, 0, byteImg.Length);
                    }
                }
                return byteImg;
            }
        }

        public async Task getImg(string imageName)
        {
           
            try
            {
                if (string.IsNullOrEmpty(imageName))
                {
                   // SectionData.clearImg(img_customer);
                }
                else
                {
                  
                       
                           
                       
                       
                   
                
                }
            }
            catch { }
        }
        //public async Task getImg(string imageName)
        //{

        //    try
        //    {
        //        if (string.IsNullOrEmpty(imageName))
        //        {
        //            // SectionData.clearImg(img_customer);
        //        }
        //        else
        //        {
        //            byte[] imageBuffer = await this.downloadImage(imageName); // read this as BLOB from your DB

        //            var bitmapImage = new BitmapImage();

        //            if (imageBuffer != null)
        //            {
        //                using (var memoryStream = new MemoryStream(imageBuffer))
        //                {
        //                    bitmapImage.BeginInit();
        //                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //                    bitmapImage.StreamSource = memoryStream;
        //                    bitmapImage.EndInit();
        //                    Bitmap serial_bitmap = new Bitmap(memoryStream);
        //                    //string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        //                    string dir = Directory.GetCurrentDirectory();
        //                    string tmpPath = System.IO.Path.Combine(dir, Global.TMPSettingFolder);
        //                    tmpPath = System.IO.Path.Combine(tmpPath, imageName);
        //                    serial_bitmap.Save(tmpPath);
        //                }


        //            }

        //        }
        //    }
        //    catch { }
        //}
        #endregion

        //


    }
}

