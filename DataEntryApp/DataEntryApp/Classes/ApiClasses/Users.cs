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
    public class Users
    {
        public long userId { get; set; }
        public string name { get; set; }
        public string AccountName { get; set; }
        public string lastName { get; set; }
        public string company { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string address { get; set; }
        public string agentLevel { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public string password { get; set; }
        public string type { get; set; }
        public string image { get; set; }
        public string notes { get; set; }
        public decimal balance { get; set; }
        public Nullable<long> createUserId { get; set; }
        public Nullable<long> updateUserId { get; set; }
        public Nullable<int> isActive { get; set; }
        public string code { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public Nullable<int> groupId { get; set; }
        public Nullable<byte> balanceType { get; set; }
        public string job { get; set; }
        public Nullable<byte> isOnline { get; set; }
        public Nullable<int> countryId { get; set; }
       
        public string fullName { get; set; }       
        public bool canDelete { get; set; }
      


        private string urimainpath = "users/";

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        /// 
        public async Task<List<Users>> GetAll()
        {

            List<Users> List = new List<Users>();
            bool canDelete = false;
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                    List = (from S in entity.users
                            select new Users()
                            {
                                userId = S.userId,
                                name = S.name,
                                AccountName = S.AccountName,
                                lastName = S.lastName,
                                company = S.company,
                                email = S.email,
                                phone = S.phone,
                                mobile = S.mobile,
                                fax = S.fax,
                                address = S.address,
                                agentLevel = S.agentLevel,
                                createDate = S.createDate,
                                updateDate = S.updateDate,
                                code = S.code,
                                password = S.password,
                                type = S.type,
                                image = S.image,
                                notes = S.notes,
                                balance = S.balance,
                                createUserId = S.createUserId,
                                updateUserId = S.updateUserId,
                                isActive = S.isActive,
                                isAdmin = S.isAdmin,
                                groupId = S.groupId,
                                balanceType = S.balanceType,
                                job = S.job,
                                canDelete = true,
                                        countryId = S.countryId,
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
        public async Task<List<Users>> GetUsersActive()
        {
            List<Users> List = new List<Users>();
   
          
          
                using (dedbEntities entity = new dedbEntities())
                {
                List = entity.users.Where(S => S.isActive == 1 && S.userId != 1)
                    .Select(S => new Users
                    {
                        userId = S.userId,
                        name = S.name,
                        AccountName = S.AccountName,
                        lastName = S.lastName,
                        company = S.company,
                        email = S.email,
                        phone = S.phone,
                        mobile = S.mobile,
                        fax = S.fax,
                        address = S.address,
                        agentLevel = S.agentLevel,
                        createDate = S.createDate,
                        updateDate = S.updateDate,
                        code = S.code,
                        password = S.password,
                        type = S.type,
                        image = S.image,
                        notes = S.notes,
                        balance = S.balance,
                        createUserId = S.createUserId,
                        updateUserId = S.updateUserId,
                        isActive = S.isActive,
                        isAdmin = S.isAdmin,
                        groupId = S.groupId,
                        balanceType = S.balanceType,
                        job = S.job,
                        isOnline = S.isOnline,
                        countryId = S.countryId,


                    })
                    .ToList();

                    return List;
                }
            
           
        }
        public async Task<Users> Getloginuser(string userName, string password)
        {
            Users user = new Users();

           
            List<Users> usersList = new List<Users>();




            Users emptyuser = new Users();

            emptyuser.createDate = DateTime.Now;
            emptyuser.updateDate = DateTime.Now;
                //emptyuser.username = userName;
                emptyuser.createUserId = 0;
                emptyuser.updateUserId = 0;
                emptyuser.userId = 0;
                emptyuser.isActive = 0;
                emptyuser.isOnline = 0;
                emptyuser.canDelete = false;
                emptyuser.balance = 0;
                emptyuser.balanceType = 0;
                try
                {

                    using (dedbEntities entity = new dedbEntities())
                    {
                        usersList = entity.users.Where(S => S.isActive == 1 && S.AccountName == userName)
                        .Select(S => new Users
                        {
                            userId = S.userId,
                            name = S.name,
                            AccountName = S.AccountName,
                            lastName = S.lastName,
                            company = S.company,
                            email = S.email,
                            phone = S.phone,
                            mobile = S.mobile,
                            fax = S.fax,
                            address = S.address,
                            agentLevel = S.agentLevel,
                            createDate = S.createDate,
                            updateDate = S.updateDate,
                            code = S.code,
                            password = S.password,
                            type = S.type,
                            image = S.image,
                            notes = S.notes,
                            balance = S.balance,
                            createUserId = S.createUserId,
                            updateUserId = S.updateUserId,
                            isActive = S.isActive,
                            isAdmin = S.isAdmin,
                            groupId = S.groupId,
                            balanceType = S.balanceType,
                            job = S.job,
                            isOnline = S.isOnline,
                            countryId = S.countryId,
                        })
                        .ToList();

                        if (usersList == null || usersList.Count <= 0)
                        {
                            user = emptyuser;
                            // rong user
                            return user;
                        }
                        else
                        {
                            user = usersList.Where(i => i.AccountName == userName).FirstOrDefault();
                            if (user.password.Equals(password))
                            {
                                // correct username and pasword
                                return user;
                            }
                            else
                            {
                                // rong pass return just username
                                user = emptyuser;
                                user.AccountName = userName;
                                return user;

                            }
                        }
                    }

                }
                catch
                {
                return emptyuser;
                }
           

             
        }

        public async Task<decimal> Save(Users newitem)
        {
            users newObject = new users();
            newObject = JsonConvert.DeserializeObject<users>(JsonConvert.SerializeObject(newitem));

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
                            var locationEntity = entity.Set<users>();
                            if (newObject.userId == 0)
                            {
                            newObject.createDate = DateTime.Now;
                                newObject.updateDate = newObject.createDate;
                                newObject.updateUserId = newObject.createUserId;


                                locationEntity.Add(newObject);
                                entity.SaveChanges();
                                message = newObject.userId ;
                            }
                            else
                            {
                                var tmpObject = entity.users.Where(p => p.userId == newObject.userId).FirstOrDefault();

                            tmpObject.updateDate = DateTime.Now;
                                tmpObject.userId = newObject.userId;
                                tmpObject.name = newObject.name;
                                tmpObject.AccountName = newObject.AccountName;
                                tmpObject.lastName = newObject.lastName;
                                tmpObject.company = newObject.company;
                                tmpObject.email = newObject.email;
                                tmpObject.phone = newObject.phone;
                                tmpObject.mobile = newObject.mobile;
                                tmpObject.fax = newObject.fax;
                                tmpObject.address = newObject.address;
                                tmpObject.agentLevel = newObject.agentLevel;
                                //  tmpObject.createDate = newObject.createDate;

                                tmpObject.code = newObject.code;
                                tmpObject.password = newObject.password;
                                tmpObject.type = newObject.type;
                                tmpObject.image = newObject.image;
                                tmpObject.notes = newObject.notes;
                                tmpObject.balance = newObject.balance;
                                //   tmpObject.createUserId = newObject.createUserId;
                                tmpObject.updateUserId = newObject.updateUserId;
                                tmpObject.isActive = newObject.isActive;
                                tmpObject.isOnline = newObject.isOnline;
                                tmpObject.countryId = newObject.countryId;


                                entity.SaveChanges();

                                message = tmpObject.userId ;
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
        public async Task<decimal> SaveImageName(int  userId,string imageName)
        {
        
            decimal message = 0;
            if (userId != 0)
            {
                try
                {
                    using (dedbEntities entity = new dedbEntities())
                    {
                        var locationEntity = entity.Set<users>();
                       
                        {
                            var tmpObject = entity.users.Where(p => p.userId == userId).FirstOrDefault();                           
                            tmpObject.image = imageName;
                            entity.SaveChanges();

                            message = tmpObject.userId;
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
        public async Task<Users> GetByID(long userId)
        {


            Users item = new Users();
            string message = "";

            Users userrow = new Users();
            try
            {
                using (dedbEntities entity = new dedbEntities())
                {
                    var userl = entity.users.ToList();
                    userrow = userl.Where(u => u.userId == userId)
                      .Select(S => new Users()
                      {
                          userId = S.userId,
                          name = S.name,
                          AccountName = S.AccountName,
                          lastName = S.lastName,
                          company = S.company,
                          email = S.email,
                          phone = S.phone,
                          mobile = S.mobile,
                          fax = S.fax,
                          address = S.address,
                          agentLevel = S.agentLevel,
                          createDate = S.createDate,
                          updateDate = S.updateDate,
                          code = S.code,
                          password = S.password,
                          type = S.type,
                          image = S.image,
                          notes = S.notes,
                          balance = S.balance,
                          createUserId = S.createUserId,
                          updateUserId = S.updateUserId,
                          isActive = S.isActive,
                          isAdmin = S.isAdmin,
                          groupId = S.groupId,
                          balanceType = S.balanceType,
                          job = S.job,
                          isOnline = S.isOnline,
                          countryId = S.countryId,

                      }).FirstOrDefault();
                    return userrow;
                }

            }
            catch (Exception ex)
            {
                userrow = new Users();
                //userrow.name = ex.ToString();
                return userrow;
            }






        }
        public async Task<decimal> Delete(long userId, long signuserId, bool final)
        {

            decimal message = 0;
                if (final)
                {
                    try
                    {
                        using (dedbEntities entity = new dedbEntities())
                        {
                            users objectDelete = entity.users.Find(userId);

                            entity.users.Remove(objectDelete);
                            message = entity.SaveChanges() ;
                            return message;

                        }
                    }
                    catch
                    {
                        return 0;

                    }
                }
                else
                {
                    try
                    {
                        using (dedbEntities entity = new dedbEntities())
                        {
                            users objectDelete = entity.users.Find(userId);

                            objectDelete.isActive = 0;
                            objectDelete.updateUserId = signuserId;
                        objectDelete.updateDate = DateTime.Now;
                            message = entity.SaveChanges() ;

                            return message;
                        }
                    }
                    catch
                    {
                        return 0;
                    }
                }
 
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
                        numberList = entity.users.Where(b => b.code.Contains(type + "-")).Select(b => b.code).ToList();

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

        //public async Task<string> uploadImage(string imagePath, string imageName, int userId)
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
        //            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        //            string tmpPath = Path.Combine(dir, Global.TMPFolder);
        //            tmpPath = Path.Combine(tmpPath, imageName + extension);
        //            if (System.IO.File.Exists(tmpPath))
        //            {
        //                System.IO.File.Delete(tmpPath);
        //            }
        //            // resize image
        //            ImageProcess imageP = new ImageProcess(150, imagePath);
        //            imageP.ScaleImage(tmpPath);

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

        //                var response = await client.PostAsync(@"users/PostUserImage", form);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    // save image name in DB
        //                    Users user = new Users();
        //                    user.balance = 0;
        //                    user.userId = userId;
        //                    user.image = fileName;
        //                    await updateImage(user);
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

        public async Task<string> uploadImage(string imageSource, string imageName,int userId)

        {
            if (imageSource != "")
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
                    string tmpPath = Path.Combine(dir, Global.TMPUsersFolder);
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
                    await SaveImageName(userId,imageext);
                    return imageext;
                }
                catch(Exception ex)
                { return ""; }
            }
            return "";
        }
        public async Task<decimal> updateImage(Users user)

        {


            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //string method = urimainpath + "UpdateImage";

            //var myContent = JsonConvert.SerializeObject(user);
            //parameters.Add("Object", myContent);
            //return await APIResult.post(method, parameters);
            return 1;

        }
        //public async Task<byte[]> downloadImage(string imageName)

        //{
        //    Stream jsonString = null;
        //    byte[] byteImg = null;
        //    Image img = null;
        //    // ... Use HttpClient.
        //    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        //    using (var client = new HttpClient())
        //    {
        //        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        //        client.BaseAddress = new Uri(Global.APIUri);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
        //        client.DefaultRequestHeaders.Add("Keep-Alive", "3600");
        //        HttpRequestMessage request = new HttpRequestMessage();
        //        request.RequestUri = new Uri(Global.APIUri + "Users/GetImage?imageName=" + imageName);
        //        request.Headers.Add("APIKey", Global.APIKey);
        //        request.Method = HttpMethod.Get;
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage response = await client.SendAsync(request);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            jsonString = await response.Content.ReadAsStreamAsync();
        //            img =Bitmap.FromStream(jsonString);
        //            byteImg = await response.Content.ReadAsByteArrayAsync();

        //            // configure trmporery path
        //            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        //            string tmpPath = Path.Combine(dir, Global.TMPUsersFolder);
        //            if (!Directory.Exists(tmpPath))
        //                Directory.CreateDirectory(tmpPath);
        //            tmpPath = Path.Combine(tmpPath, imageName);
        //            if (System.IO.File.Exists(tmpPath))
        //            {
        //                System.IO.File.Delete(tmpPath);
        //            }
        //            using (FileStream fs = new FileStream(tmpPath, FileMode.Create, FileAccess.ReadWrite))
        //            {
        //                fs.Write(byteImg, 0, byteImg.Length);
        //            }
        //        }
        //        return byteImg;
        //    }
        //}


     
    }
}
