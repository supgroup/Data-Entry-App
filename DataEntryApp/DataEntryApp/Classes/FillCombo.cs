using DataEntryApp.ApiClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing.Printing;
using System.Threading;
using System.Windows;
using System.Threading.Tasks;
using System;
 
namespace DataEntryApp.Classes
{
    public class FillCombo
    {
        /// <summary>
        /// Nationality
        /// </summary>
        /// 
        static Nationalities nationalModel = new Nationalities();
        static IEnumerable<Nationalities> nationalityList;
        static public async Task fillNationality(ComboBox combo)
        {            
            nationalityList = await nationalModel.GetAll();
            combo.ItemsSource = nationalityList.Where(x => x.isActive ==true);
            combo.SelectedValuePath = "nationalityId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;
        }
        //
        static Departments departmentModel = new Departments();
        static IEnumerable<Departments> departmentList;
        static public async Task fillDepartment(ComboBox combo)
        {
            departmentList = await departmentModel.GetAll();
            combo.ItemsSource = departmentList;
            combo.SelectedValuePath = "departmentId";
            combo.DisplayMemberPath = "name";
            combo.SelectedIndex = -1;
        }
        //static Packages package = new Packages();
        //static IEnumerable<Packages> packages;

        //static PackageUser packageUser = new PackageUser();
        //static IEnumerable<PackageUser> packageUsers;

        //static IEnumerable<Packages> agentPackages;

        //static CountryPackageDate cpd = new CountryPackageDate();
        //static IEnumerable<CountryPackageDate> countryPackageDates;
        //static public async Task fillPackageUser(ComboBox combo)
        //{
        //    packageUsers = await packageUser.GetAll();
        //    combo.ItemsSource = packageUsers.Where(x => x.isActive == 1);
        //    combo.SelectedValuePath = "packageUserId";
        //    combo.DisplayMemberPath = "packageSaleCode";
        //}
        /*
        static public async Task fillBookNum(ComboBox combo , int customerId)
        {
            packageUsers = await packageUser.GetByCustomerId(customerId);
            foreach (var i in packageUsers)
            {
                i.packageNumber = i.packageNumber + "     " + i.packageName;
            }
            combo.ItemsSource = packageUsers.Where(x => x.isActive == 1 );
            combo.SelectedValuePath = "packageUserId";
            combo.DisplayMemberPath = "packageNumber";
            //combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillBookNumAgent(ComboBox combo, int customerId , int agentId)
        {
            packageUsers = await packageUser.GetByCustomerId(customerId);
            packageUsers = packageUsers.Where(p => p.userId == agentId);
            foreach (var i in packageUsers)
            {
                i.packageNumber = i.packageNumber + "     " + i.packageName;
            }
            combo.ItemsSource = packageUsers.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "packageUserId";
            combo.DisplayMemberPath = "packageNumber";
            //combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillPackage(ComboBox combo)
        {
            packages = await package.GetAll();
            combo.ItemsSource = packages.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "packageId";
            combo.DisplayMemberPath = "packageName";
            combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillPackageByCustomer(ComboBox combo , int customerId)
        {
            packages = await package.GetByCustomerCountry(customerId);
            combo.ItemsSource = packages.Where(x => x.isActive == 1 );
            combo.SelectedValuePath = "packageId";
            combo.DisplayMemberPath = "packageName";
            combo.SelectedIndex = -1;
        }
        */
        /*
        static public void fillRegion( )
        {
            MainWindow.Region = countrynum.Where(C=> C.isDefault==1).FirstOrDefault();
           
        }
        */
        /*
        static public async Task fillPeriod(ComboBox combo, int customerId, int packageId)
        {
            countryPackageDates = await cpd.GetByCustomerPackId(customerId , packageId);

            foreach (CountryPackageDate cpd in countryPackageDates)
            {
                string period = "";

                period = HelpClass.getPeriod(cpd);

                cpd.notes = period + "       " + cpd.price + " " + cpd.currency;
            }
            combo.ItemsSource = countryPackageDates.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "Id";
            combo.DisplayMemberPath = "notes";
            combo.SelectedIndex = -1;
        }
        */
        /*
        static public async Task fillAgentPackage(ComboBox combo , int agentId)
        {
            agentPackages = await package.GetPackagesByAgent(agentId);
            combo.ItemsSource = agentPackages.Where(p => p.isActive == 1);
            combo.SelectedValuePath = "packageId";
            combo.DisplayMemberPath = "packageName";
        }
        */
        /// <summary>
        /// Programs
        /// </summary>
        /// 
        /*
        static Programs program = new Programs();
        static IEnumerable<Programs> programs;
        static public async Task fillProgram(ComboBox combo)
        {
            programs = await program.GetAll();
            combo.ItemsSource = programs.Where(x =>  x.isActive == 1);
            combo.SelectedValuePath = "programId";
            combo.DisplayMemberPath = "name";
        }
        */
        /*
        /// <summary>
        /// Version
        /// </summary>
        static Versions version = new Versions();
        static IEnumerable<Versions> versions;
        static public async Task fillVersion(ComboBox combo)
        {
            versions = await version.GetAll();
            combo.ItemsSource = versions.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "verId";
            combo.DisplayMemberPath = "name";
        }

        static public async Task fillVersionByProgram(ComboBox combo, int programId)
        {
            versions = await version.GetAll();
            combo.ItemsSource = versions.Where(x => x.programId == programId &&  x.isActive == 1);
            combo.SelectedValuePath = "verId";
            combo.DisplayMemberPath = "name";
        }
        */
        /// <summary>
        /// User & Agent 
        /// </summary>
        static Users user = new Users();
        static IEnumerable<Users> users;
      
        static public async Task fillAgent(ComboBox combo)
        {
            users = await user.GetAll();
            combo.ItemsSource = users.Where(x => x.isActive == 1 && x.type == "ag");
            combo.SelectedValuePath = "userId";
            combo.DisplayMemberPath = "name";
        }

        static public async Task fillAgentByCountry(ComboBox combo , int countryID)
        {
            users = await user.GetAll();
            foreach(var a in users)
            {
                if (a.userId == 3)
                    a.name = "Supclouds";
                else
                a.name = a.name + " " + a.lastName;
            }
            combo.ItemsSource = users.Where(x => x.isActive == 1 && x.type == "ag" && x.countryId == countryID);
            combo.SelectedValuePath = "userId";
            combo.DisplayMemberPath = "name";
        }
        /*
        /// <summary>
        /// Customer
        /// </summary>
        static Customers customer = new Customers();
        static IEnumerable<Customers> customers;
        static public async Task fillCustomer(ComboBox combo)
        {
            customers = await customer.GetAll();
            //foreach (var c in customers)
            //{
            //    c.custname = c.custCode + " " + c.custname + " " + c.lastName;
            //}
            combo.ItemsSource = customers.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "custId";
            //combo.DisplayMemberPath = "custname";
            combo.DisplayMemberPath = "company";
        }
        static public async Task fillCustomerByAgent(ComboBox combo , int agentID)
        {
            customers = await customer.GetCustomersByAgent(agentID);
            //foreach(var c in customers)
            //{
            //    c.custname = c.custCode + " " + c.custname + " " + c.lastName;
            //}
            combo.ItemsSource = customers.Where(x => x.isActive == 1);
            combo.SelectedValuePath = "custId";
            //combo.DisplayMemberPath = "custname";
            combo.DisplayMemberPath = "company";
        }
        */
        #region Countries
        /// <summary>
        /// area code methods
        /// </summary>
        /// <returns></returns>
        /// 
        //phone 
        public static IEnumerable<Country> countrynum;
        public static IEnumerable<City> citynum;
        public static IEnumerable<City> citynumofcountry;
        public static Country countrycodes = new Country();
        public static City cityCodes = new City();

        static public async Task<IEnumerable<Country>> RefreshCountry()
        {
           countrynum = await countrycodes.GetAll();
            countrynum = countrynum.Where(x => x.countryId == 9).ToList();
            return countrynum;
        }
        static public async Task<IEnumerable<City>> RefreshCity()
        {
            citynum = await cityCodes.Get();
            return citynum;
        }
        static public async Task fillCountries(ComboBox combo)
        {
            if (countrynum is null)
                await RefreshCountry();

            combo.ItemsSource = countrynum.ToList();
            combo.SelectedValuePath = "countryId";
            combo.DisplayMemberPath = "code";
        }
        static public async Task fillCountriesNames(ComboBox combo)
        {
            if (countrynum is null)
                await RefreshCountry();

            combo.ItemsSource = countrynum.ToList();
            combo.SelectedValuePath = "countryId";
            combo.DisplayMemberPath = "name";
        }
        static public async Task fillCountriesLocal(ComboBox combo , int countryid,Border border)
        {
            if (citynum is null)
                await RefreshCity();
            FillCombo.citynumofcountry = FillCombo.citynum.Where(b => b.countryId == countryid).OrderBy(b => b.cityCode).ToList();
            combo.ItemsSource = FillCombo.citynumofcountry;
            combo.SelectedValuePath = "cityId";
            combo.DisplayMemberPath = "cityCode";
            if (FillCombo.citynumofcountry.Count() > 0)
                border.Visibility = Visibility.Visible;
            else
                border.Visibility = Visibility.Collapsed;
        }
        static public async Task fillCountriesLocal(ComboBox combo, int countryid )
        {
            if (citynum is null)
                await RefreshCity();
            FillCombo.citynumofcountry = FillCombo.citynum.Where(b => b.countryId == countryid).OrderBy(b => b.cityCode).ToList();
            combo.ItemsSource = FillCombo.citynumofcountry;
            combo.SelectedValuePath = "cityId";
            combo.DisplayMemberPath = "cityCode";
        
        }
        #endregion

        #region fill user type
        static public void fillUserType(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trAdmin")       , Value = "ad" },
                new { Text = MainWindow.resourcemanager.GetString("trEmployee")    , Value = "us" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill package month
        static public void fillPackageMonth(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trOneMonth")    , Value = "1" },
                new { Text = MainWindow.resourcemanager.GetString("trThreeMonth")  , Value = "3" },
                new { Text = MainWindow.resourcemanager.GetString("trSixMonth")    , Value = "6" },
                new { Text = MainWindow.resourcemanager.GetString("trTwelveMonth") , Value = "12" },
                new { Text = MainWindow.resourcemanager.GetString("trUnLimited")   , Value = "0" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill Agent Level
        static public void fillAgentLevel(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trVip")       , Value = "Vip" },
                new { Text = MainWindow.resourcemanager.GetString("trNormal")   , Value = "Normal" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }
        #endregion

        #region fill booked
        static public void fillBooked(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trAll")      , Value = "all" },
                new { Text = MainWindow.resourcemanager.GetString("trBooked")   , Value = "true" },
                new { Text = MainWindow.resourcemanager.GetString("trUnBooked") , Value = "false" },
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill server state
        static public void fillServerState(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trOnline")    , Value = "True" },
                new { Text = MainWindow.resourcemanager.GetString("trOffline")   , Value = "False" }
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region fill offline activation 
        static public void fillOfflineActivation(ComboBox combo)
        {
            var typelist = new[] {
                new { Text = MainWindow.resourcemanager.GetString("trExtend")    , Value = "rn" },
                new { Text = MainWindow.resourcemanager.GetString("trUpgrade")   , Value = "up" }
                 };
            combo.DisplayMemberPath = "Text";
            combo.SelectedValuePath = "Value";
            combo.ItemsSource = typelist;

        }

        #endregion

        #region Report
        public static string sale_copy_count="1";
 
        public static string print_on_save_sale ;
      
        public static string email_on_save_sale ;
     
        public static string rep_printer_name;
    
 
        public static string rep_print_count="1";
        public static string docPapersize;
    
        static public string getdefaultPrinters()
        {

            PrinterSettings settings = new PrinterSettings();
            string defaultPrinterName = settings.PrinterName;


            return defaultPrinterName;
        }
        static public string getsettingPrinter()
        {
            string printer = "";
            if (Properties.Settings.Default.reportPrinter==null || Properties.Settings.Default.reportPrinter=="")
            {
                printer = getdefaultPrinters();
            }
            else
            {
                printer = Properties.Settings.Default.reportPrinter;
            }
            return printer;
        }

        public static async Task Getprintparameter()
        {

            List<SetValues> printList = new List<SetValues>();
            printList = await valueModel.GetBySetvalNote("print");
            /*
             sale_copy_count = printList.Where(X => X.name == "sale_copy_count").FirstOrDefault().value;

             pur_copy_count = printList.Where(X => X.name == "pur_copy_count").FirstOrDefault().value;
             print_on_save_pur = printList.Where(X => X.name == "print_on_save_pur").FirstOrDefault().value;
            email_on_save_pur = printList.Where(X => X.name == "email_on_save_pur").FirstOrDefault().value;

            sale_copy_count = printList.Where(X => X.name == "sale_copy_count").FirstOrDefault().value;

            pur_copy_count = printList.Where(X => X.name == "pur_copy_count").FirstOrDefault().value;

            rep_print_count = printList.Where(X => X.name == "rep_copy_count").FirstOrDefault().value;

            Allow_print_inv_count = printList.Where(X => X.name == "Allow_print_inv_count").FirstOrDefault().value;
             */
            print_on_save_sale = printList.Where(X => X.name == "print_on_save_sale").FirstOrDefault().value;
            email_on_save_sale = printList.Where(X => X.name == "email_on_save_sale").FirstOrDefault().value;
            if (print_on_save_sale==null)
            {
                print_on_save_sale = "0";
            }
            if (email_on_save_sale == null)
            {
                email_on_save_sale = "0";
            }
        }

        static public void FillSideCombo(ComboBox COMBO)
        {
            #region fill deposit to combo
            var list = new[] {
  new { Text = MainWindow.resourcemanager.GetString("trAccounting")  , Value = "accounting" },
            new { Text = MainWindow.resourcemanager.GetString("trSales")  , Value = "sales" },
          

             };
            COMBO.DisplayMemberPath = "Text";
            COMBO.SelectedValuePath = "Value";
            COMBO.ItemsSource = list;
            #endregion

        }


        static SettingCls setModel = new SettingCls();
        static SetValues valueModel = new SetValues();
        static int nameId, addressId, emailId, mobileId, phoneId, faxId, logoId, taxId;
        public static string logoImage= "champer_industry_logo.png";
        public static string companyName;
        public static string Email;
        public static string Fax;
        public static string Mobile;
        public static string Address;
        public static string Phone;

        public static List<SettingCls> settingList = new List<SettingCls>();
        public static List<SetValues> setValueList = new List<SetValues>();

        static public async Task getsettingList()
        {

            SettingCls settingModel = new SettingCls();
            settingList = await settingModel.GetAll();

            //return country;
        }
        static public async Task getsetValueList()
        {

            SetValues setValuesModel = new SetValues();
            setValueList = await setValuesModel.GetAll();


        }
        public static string rep_copy_count = "1";
        public static async Task< int> loading_getDefaultSystemInfo()
        {
            try
            {
                await getsettingList();
                await getsetValueList ();
                List<SettingCls> settingsCls = settingList;
                List<SetValues> settingsValues = setValueList;
                SettingCls set = new SettingCls();
                SetValues setV = new SetValues();
                List<char> charsToRemove = new List<char>() { '@', '_', ',', '.', '-' };
                #region get company name
                
                        //get company name
                        set = settingsCls.Where(s => s.name == "com_name").FirstOrDefault<SettingCls>();
                        nameId = set.settingId;
                        setV = settingsValues.Where(i => i.settingId == nameId).FirstOrDefault();
                        if (setV != null)
                            companyName = setV.value;

                  
                #endregion

                #region  get company address
               
                        //get company address
                        set = settingsCls.Where(s => s.name == "com_address").FirstOrDefault<SettingCls>();
                        addressId = set.settingId;
                        setV = settingsValues.Where(i => i.settingId == addressId).FirstOrDefault();
                        if (setV != null)
                            Address = setV.value;
                
                #endregion

                #region  get company email
              
                        //get company email
                        set = settingsCls.Where(s => s.name == "com_email").FirstOrDefault<SettingCls>();
                        emailId = set.settingId;
                        setV = settingsValues.Where(i => i.settingId == emailId).FirstOrDefault();
                        if (setV != null)
                            Email = setV.value;
                 
                #endregion

                #region  get company mobile
             
                        //get company mobile
                        set = settingsCls.Where(s => s.name == "com_mobile").FirstOrDefault<SettingCls>();
                        mobileId = set.settingId;
                        setV = settingsValues.Where(i => i.settingId == mobileId).FirstOrDefault();
                        if (setV != null)
                        {
                            charsToRemove.ForEach(x => setV.value = setV.value.Replace(x.ToString(), String.Empty));
                            Mobile = setV.value;
                        }
                
                #endregion

                #region  get company phone
               
                        //get company phone
                        set = settingsCls.Where(s => s.name == "com_phone").FirstOrDefault<SettingCls>();
                        phoneId = set.settingId;
                        setV = settingsValues.Where(i => i.settingId == phoneId).FirstOrDefault();
                        if (setV != null)
                        {
                            charsToRemove.ForEach(x => setV.value = setV.value.Replace(x.ToString(), String.Empty));
                            Phone = setV.value;
                        }
              
                #endregion

                #region  get company fax
                
                        //get company fax
                        set = settingsCls.Where(s => s.name == "com_fax").FirstOrDefault<SettingCls>();
                        faxId = set.settingId;
                        setV = settingsValues.Where(i => i.settingId == faxId).FirstOrDefault();
                        if (setV != null)
                        {
                            charsToRemove.ForEach(x => setV.value = setV.value.Replace(x.ToString(), String.Empty));
                            Fax = setV.value;
                        }
                
                #endregion

                #region   get company logo
                //get company logo
                set = settingsCls.Where(s => s.name == "com_logo").FirstOrDefault<SettingCls>();
                logoId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == logoId).FirstOrDefault();
                if (setV != null)
                {
                   // logoImage = setV.value;
                    //await setV.getImg(logoImage);
                }


                #endregion
                #region get repcopy count

                //get company name
                set = settingsCls.Where(s => s.name == "rep_copy_count").FirstOrDefault<SettingCls>();
                nameId = set.settingId;
                setV = settingsValues.Where(i => i.settingId == nameId).FirstOrDefault();
                if (setV != null)
                {
                    if (setV.value!=null && setV.value !="")
                    {
                        rep_copy_count = setV.value;
                    }
                    else
                    {
                        rep_copy_count ="1";
                    }
                   
                }
                else
                {
                    rep_copy_count ="1";
                }
                #endregion
                return 1;
           
            }
            catch (Exception)
            { return 0; }
            //foreach (var item in loadingList)
            //{
            //    if (item.key.Equals("loading_getDefaultSystemInfo"))
            //    {
            //        item.value = true;
            //        break;
            //    }
            //}

        }

        public static string AgentNameConv(Users userModel)
        {
            if (userModel.userId == 3)
                return "Supclouds";
            else
                return userModel.name + " " + userModel.lastName;
        }
        public static string DateConvert(DateTime? date)
        {
            DateTime datetemp;

            if (date is DateTime)
                date = (DateTime)date;
            else return date.ToString();

            datetemp = DateTime.Parse(date.ToString());
            switch (MainWindow.dateFormat)
            {
                case "ShortDatePattern":
                    return datetemp.ToString(@"dd/MM/yyyy");
                case "LongDatePattern":
                    return datetemp.ToString(@"dddd, MMMM d, yyyy");
                case "MonthDayPattern":
                    return datetemp.ToString(@"MMMM dd");
                case "YearMonthPattern":
                    return datetemp.ToString(@"MMMM yyyy");
                default:
                    return datetemp.ToString(@"dd/MM/yyyy");
            }

        }


        public static string serverActiveConv(int? isActive)
        {
            string value = "";
            if (isActive == 1)
                value = MainWindow.resourcemanagerreport.GetString("trValid");
            else
                value = MainWindow.resourcemanagerreport.GetString("trInValid");

            return value;
        }
        /*
        public static string PeriodConv(CountryPackageDate CountryPackageDateModel)
        {
            string period = "";
            int monthCount = int.Parse(CountryPackageDateModel.monthCount.ToString());
            bool islimitDate = bool.Parse(CountryPackageDateModel.islimitDate.ToString());
            if (!islimitDate)
                period = MainWindow.resourcemanager.GetString("trUnLimited");
            else
            {
                switch (monthCount)
                {
                    case 1: period = MainWindow.resourcemanagerreport.GetString("trOneMonth"); break;
                    case 3: period = MainWindow.resourcemanagerreport.GetString("trThreeMonth"); break;
                    case 6: period = MainWindow.resourcemanagerreport.GetString("trSixMonth"); break;
                    case 12: period = MainWindow.resourcemanagerreport.GetString("trTwelveMonth"); break;
                }
            }
            return period;
        }



        public static string serverActiveationTypeConv(bool? isOnlineServer)
        {
            string value = "";
            if (isOnlineServer == true)
                value = MainWindow.resourcemanagerreport.GetString("trOnline");
            else
                value = MainWindow.resourcemanagerreport.GetString("trOffline");

            return value;
        }
        */
        public static string UnlimitedConvert(int count)
        {
            string value = "";
            if (count == -1)
                value = MainWindow.resourcemanagerreport.GetString("trUnLimited");
            else
                value = count.ToString();
            return value;
        }
        #endregion
        #region email

        #endregion
    }
}
