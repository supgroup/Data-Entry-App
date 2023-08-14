using DataEntryApp.Classes;
using DataEntryApp.View.badge;
using POS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTabTip;
using DataEntryApp.ApiClasses;
using System.Threading;
using DataEntryApp.View.sectionData;
//using DataEntryApp.View.sales;
using DataEntryApp.View.settings;
using System.Windows.Resources;
using DataEntryApp.View.windows;
using DataEntryApp.View.reports;

namespace DataEntryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ResourceManager resourcemanager;
        public static ResourceManager resourcemanagerreport;
        static public MainWindow mainWindow;
        public static string lang = "ar";
        public static string Reportlang = "ar";
        internal static Users userLogin = new Users();
        public static long userID=1;
        //public static CountryCode Region;
        internal static int? userLogInID;
        public static Boolean go_out = false;

        internal static string dateFormat;
        internal static string accuracy;
       // static public GroupObject groupObject = new GroupObject();
        //static public List<GroupObject> groupObjects = new List<GroupObject>();
        Users userModel = new Users();
        ImageBrush myBrush = new ImageBrush();
        public static string Currency;
        public static int CurrencyId;
        //public static string companyName;
        //public static string Email;
        //public static string Fax;
        //public static string Mobile;
        //public static string Address;
        //public static string Phone;
        //public static string logoImage;
        public static Country Region = new Country() ;

        static SetValues valueModel = new SetValues();

        public static string print_on_save_sale;
        public static string email_on_save_sale;
   

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                mainWindow = this;
                windowFlowDirection();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        void windowFlowDirection()
        {
            lang = "ar";
            #region
            if (lang.Equals("en"))
            {
                resourcemanager = new ResourceManager("DataEntryApp.en_file", Assembly.GetExecutingAssembly());
                grid_mainGrid.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                resourcemanager = new ResourceManager("DataEntryApp.ar_file", Assembly.GetExecutingAssembly());
                grid_mainGrid.FlowDirection = FlowDirection.RightToLeft;
            }
            #endregion
        }

        private void translate()
        {
            txt_cards.Text = resourcemanager.GetString("trCards");
            //txt_accounting.Text = resourcemanager.GetString("accounting");
            //txt_reports.Text = resourcemanager.GetString("trReports");
            txt_sectionData.Text = resourcemanager.GetString("trSectionData");
            txt_settings.Text = resourcemanager.GetString("trSettings");

            mi_changePassword.Header = resourcemanager.GetString("trChangePassword");
            BTN_logOut.Header = resourcemanager.GetString("trLogOut");
            tb_navigation.Text = resourcemanager.GetString("trCards");
            //txt_notifications.Text = resourcemanager.GetString("trNotifications");
            //txt_noNoti.Text = resourcemanager.GetString("trNoNotifications");
            //btn_showAll.Content = resourcemanager.GetString("trShowAll");
        }

        public static List<string> menuList;

        #region loading
        List<keyValueBool> loadingList;
        private async Task getImg(string imagename, Ellipse img_userLogin)
        {
            try
            {
                if (string.IsNullOrEmpty(imagename))
                {
                   clearImg();
                }
                else
                {
                    //byte[] imageBuffer = await setVLogo.downloadImage(setVLogo.value); // read this as BLOB from your DB

                    var bitmapImage = new BitmapImage();
                    //   BitmapImage img= Bitma;

                    string dir =System.IO.Directory.GetCurrentDirectory();
                    string tmpPath = System.IO.Path.Combine(dir, Global.TMPUsersFolder);
                    tmpPath = System.IO.Path.Combine(tmpPath,imagename);
                    byte[] imageBuffer = System.IO.File.ReadAllBytes(tmpPath);
                    if (imageBuffer != null)
                    {
                        using (var memoryStream = new System.IO.MemoryStream(imageBuffer))
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = memoryStream;
                            bitmapImage.EndInit();
                        }
                    }

                  
                    img_userLogin.Fill = new ImageBrush(bitmapImage);
                 
                }
            }
            catch (Exception ex) { }

        }
        async Task loading_getUserPersonalInfo()
        {
            #region user personal info
            txt_userName.Text = userLogin.name;
            string job = "";
            switch(userLogin.type)
            {
                case "us":    job = "Employee"; break;
                case "ad":    job = "Admin";    break;
              //  case "ag":    job = "Agent";    break;
            }
            txt_userJob.Text = job;
            try
            {
               await getImg(userLogin.image, img_userLogin);
                //if (!string.IsNullOrEmpty(userLogin.image))
                //{
                //    byte[] imageBuffer = await userModel.downloadImage(userLogin.image); // read this as BLOB from your DB

                //    var bitmapImage = new BitmapImage();

                //    using (var memoryStream = new System.IO.MemoryStream(imageBuffer))
                //    {
                //        bitmapImage.BeginInit();
                //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                //        bitmapImage.StreamSource = memoryStream;
                //        bitmapImage.EndInit();
                //    }

                //    img_userLogin.Fill = new ImageBrush(bitmapImage);
                //}
                //else
                //{
                 //   clearImg();
                //}
            }
            catch
            {
                clearImg();
            }
            //foreach (var item in loadingList)
            //{
            //    if (item.key.Equals("loading_getUserPersonalInfo"))
            //    {
            //        item.value = true;
            //        break;
            //    }
            //}
            #endregion
        }
       
        #endregion

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
              
                if (sender != null)
                    HelpClass.StartAwait(grid_mainGrid);
                //windowFlowDirection();
                menuList = new List<string> { "cards", "accounting", "reports",
                   "sectionData","settings"};
                //menuList = new List<string> { "applications", "sales", "reports",
                //   "sectionData","settings"};
                
                translate();

                #region loading
                //loadingList = new List<keyValueBool>();
                //bool isDone = true;
                //loadingList.Add(new keyValueBool { key = "loading_getGroupObjects", value = false });
                //loadingList.Add(new keyValueBool { key = "loading_getUserPersonalInfo", value = false });
                

              await  FillCombo.loading_getDefaultSystemInfo();
             await FillCombo.Getprintparameter();
                //loading_getGroupObjects();
              // await FillCombo.RefreshCountry();
              //  FillCombo.fillRegion();
                await loading_getUserPersonalInfo();


                #endregion

                //if (MainWindow.userLogin.type == "ag")
                //{
                //    btn_applications.Visibility = Visibility.Collapsed;
                //    btn_sectionData.Visibility = Visibility.Collapsed;
                //    btn_settings.Visibility = Visibility.Collapsed;

                //    Btn_sales_Click(btn_sales , null);
                //}
                //else
                //{
                Btn_cards_Click(btn_cards, null);
                //show_applications();

            //     Btn_book_sales_Click(uc_applications., null);
                //}
                if (sender != null)
                    HelpClass.EndAwait(grid_mainGrid);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_mainGrid);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
       

        void colorTextRefreash(string str)
        {
            foreach (TextBlock textBlock in FindControls.FindVisualChildren<TextBlock>(this))
            {
                if (textBlock.Tag != null)
                {
                    foreach (var item in menuList)
                    {
                        if (item == textBlock.Tag.ToString())
                        {
                            if (item == str)
                            textBlock.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                                else
                            textBlock.Foreground = Application.Current.Resources["LightGrey"] as SolidColorBrush;

                        }
                    }
                }
            }
        }
        void ColorIconRefreash(string str)
        {
            foreach (Path path in FindControls.FindVisualChildren<Path>(this))
            {
                if (path.Tag != null)
                {
                    foreach (var item in menuList)
                    {
                        if (item == path.Tag.ToString())
                        {
                            if (item == str)
                                path.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
                            else
                                path.Fill = Application.Current.Resources["LightGrey"] as SolidColorBrush;

                        }
                    }
                }
            }
        }
        void openVisible(string str)
        {
            foreach (Rectangle rectangle in FindControls.FindVisualChildren<Rectangle>(this))
            {
                if (rectangle.Tag != null)
                {
                    foreach (var item in menuList)
                    {
                        if (item == rectangle.Tag.ToString())
                        {
                            if (item == str)
                                rectangle.Visibility = Visibility.Visible;
                            else
                                rectangle.Visibility = Visibility.Collapsed;

                        }
                    }
                }
            }
        }
       
        private void BTN_Close_Click(object sender, RoutedEventArgs e)
        {//close
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_mainGrid);
                Application.Current.Shutdown();

                if (sender != null)
                    HelpClass.EndAwait(grid_mainGrid);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_mainGrid);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void BTN_Minimize_Click(object sender, RoutedEventArgs e)
        {//minimize
            try
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_applications_Click(object sender, RoutedEventArgs e)
        {//application
            try
            {
                Button button = sender as Button;
                colorTextRefreash(button.Tag.ToString());
                ColorIconRefreash(button.Tag.ToString());
                openVisible(button.Tag.ToString());
                grid_main.Children.Clear();
              //  grid_main.Children.Add(uc_applications.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        public void show_applications( )
        {//application
            try
            {
              //  Button button = sender as Button;
                //colorTextRefreash(button.Tag.ToString());
                //ColorIconRefreash(button.Tag.ToString());
              // openVisible(button.Tag.ToString());
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_applications.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        //private void Btn_book_sales_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Button button = sender as Button;
        //        colorTextRefreash(button.Tag.ToString());
        //        ColorIconRefreash(button.Tag.ToString());
        //       // openVisible(button.Tag.ToString());
        //        grid_main.Children.Clear();
        //     //   grid_main.Children.Add(uc_customers.Instance);
        //        grid_main.Children.Add(uc_applications.Instance);
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        //public  void Btn_sales_Click(object sender, RoutedEventArgs e)
        //{//sales
        //    try
        //    {
        //        //Button button = sender as Button;
        //        //colorTextRefreash(button.Tag.ToString());
        //        //ColorIconRefreash(button.Tag.ToString());
        //        //openVisible(button.Tag.ToString());
        //        //grid_main.Children.Clear();
        //        //grid_main.Children.Add(uc_sales.Instance);
        //        //uc_sales.Instance.Btn_sale_Click(uc_sales.Instance.btn_sale , null);
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        private void Btn_accounting_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                Button button = sender as Button;
                colorTextRefreash(button.Tag.ToString());
                ColorIconRefreash(button.Tag.ToString());
                openVisible(button.Tag.ToString());
                grid_main.Children.Clear();
                //grid_main.Children.Add(uc_sales.Instance);
                //uc_sales.Instance.Btn_sale_Click(uc_sales.Instance.btn_sale, null);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_sectionData_Click(object sender, RoutedEventArgs e)
        {//sectiondata
            try
            {
                Button button = sender as Button;
                colorTextRefreash(button.Tag.ToString());
                ColorIconRefreash(button.Tag.ToString());
                openVisible(button.Tag.ToString());
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_sectionData.Instance);
                tb_navigation.Text = resourcemanager.GetString("trSectionData");
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_reports_Click(object sender, RoutedEventArgs e)
        {//reports
            try
            {
                Button button = sender as Button;
                colorTextRefreash(button.Tag.ToString());
                ColorIconRefreash(button.Tag.ToString());
                openVisible(button.Tag.ToString());
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_reports.Instance);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorTextRefreash(button.Tag.ToString());
                ColorIconRefreash(button.Tag.ToString());
                openVisible(button.Tag.ToString());
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_settings.Instance);
                tb_navigation.Text = resourcemanager.GetString("trSettings");
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void clearImg()
        {
            Uri resourceUri = new Uri("pic/no-image-icon-90x90.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            myBrush.ImageSource = temp;
            img_userLogin.Fill = myBrush;

        }

        private async void BTN_logOut_Click(object sender, RoutedEventArgs e)
        {//log out
            try
            {
                await close();

                Application.Current.Shutdown();
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }

        }
        async Task close()
        {
            //log out
            userLogin.isOnline = 0;
            await userModel.Save(userLogin);
            //close
            wd_logIn logIn = new wd_logIn();
            logIn.Show();
            this.Close();

        }

        private async void Mi_changePassword_Click(object sender, RoutedEventArgs e)
        {//change password
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_changePassword w = new wd_changePassword();
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;

                userLogin = await userModel.GetByID(userLogin.userId);

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_cards_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorTextRefreash(button.Tag.ToString());
                ColorIconRefreash(button.Tag.ToString());
                 openVisible(button.Tag.ToString());
                grid_main.Children.Clear();
                //   grid_main.Children.Add(uc_customers.Instance);
                grid_main.Children.Add(uc_applications.Instance);
                tb_navigation.Text= resourcemanager.GetString("trCards");

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}
