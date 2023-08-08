using DataEntryApp.ApiClasses;
using DataEntryApp.Classes;
using DataEntryApp.View.windows;
using Microsoft.Win32;
using netoaster;
using DataEntryApp.View.windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
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

using Microsoft.Reporting.WinForms;

namespace DataEntryApp.View.badge
{
    /// <summary>
    /// Interaction logic for uc_customers.xaml
    /// </summary>
    public partial class uc_customers : UserControl
    {
        public uc_customers()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private static uc_customers _instance;
        public static uc_customers Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_customers();
                return _instance;
            }
        }
        
        Customers customer = new Customers();
        IEnumerable<Customers> customersQuery;
        IEnumerable<Customers> customers;
        bool tgl_customerState;
        string searchText = "";
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);
                requiredControlList = new List<string> { "custname", "lastName",  "mobile" , "company" , "country" };

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.en_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.ar_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }
                translate();
                #endregion

                //await FillCombo.fillCountries(cb_areaMobile);
                //await FillCombo.fillCountries(cb_areaPhone);
                //await FillCombo.fillCountries(cb_areaFax);
                //await FillCombo.fillCountriesNames(cb_country);
                FillCombo.fillAgentLevel(cb_custlevel);

                Keyboard.Focus(tb_name);

                await RefreshCustomersList();
                await Search();
                Clear();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void translate()
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_custCode, MainWindow.resourcemanager.GetString("trCodeHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("trNameHint"));

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_mobile, MainWindow.resourcemanager.GetString("trMobileHint"));
    
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_company, MainWindow.resourcemanager.GetString("trCompanyHint"));
            txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_custlevel, MainWindow.resourcemanager.GetString("trLevelHint"));
            txt_contactInformation.Text = MainWindow.resourcemanager.GetString("trContactInformation");


            dg_customer.Columns[0].Header = MainWindow.resourcemanager.GetString("trCode");
            dg_customer.Columns[1].Header = MainWindow.resourcemanager.GetString("trName");
            dg_customer.Columns[2].Header = MainWindow.resourcemanager.GetString("trName");
            dg_customer.Columns[3].Header = MainWindow.resourcemanager.GetString("trMobile");

            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");

        }
        #region Add - Update - Delete - Search - Tgl - Clear - DG_SelectionChanged - refresh
        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        {//add
            try
            {
                //Country coumod = new Country();
                //TimeSpan ts = new TimeSpan();
                //ts = await coumod.GetOffsetTime(int.Parse(tb_custname.Text));
                //tb_lastName.Text = ts.ToString();

                HelpClass.StartAwait(grid_main);
                customer = new Customers();
                if (HelpClass.validate(requiredControlList, this) && HelpClass.IsValidEmail(this))
                {
                    ////tb_custCode.Text = await customer.generateCodeNumber("cu");
                    //tb_custCode.Text = await HelpClass.generateRandomString(5, "", "Customers", 0);
                    //customer.custCode = tb_custCode.Text;
                    //customer.custname = tb_custname.Text;
                    //customer.lastName = tb_lastName.Text;
                    //customer.countryId = Convert.ToInt32(cb_country.SelectedValue);
                    //customer.email = tb_email.Text;
                    //customer.mobile = cb_areaMobile.Text + "-" + tb_mobile.Text; 
                    //if (!tb_phone.Text.Equals(""))
                    //    customer.phone = cb_areaPhone.Text + "-" + cb_areaPhoneLocal.Text + "-" + tb_phone.Text;
                    //if (!tb_fax.Text.Equals(""))
                    //    customer.fax = cb_areaFax.Text + "-" + cb_areaFaxLocal.Text + "-" + tb_fax.Text;
                    //if (cb_custlevel.SelectedValue != null)
                    //    customer.custlevel = cb_custlevel.SelectedValue.ToString();
                    //customer.company = tb_company.Text.Trim();
                    //customer.address = tb_address.Text;
                    //customer.notes = tb_notes.Text;
                    //customer.isActive = 1;
                    //customer.createUserId = MainWindow.userLogin.userId;
                    //customer.updateUserId = MainWindow.userLogin.userId;
                    //customer.balance = 0;

                    decimal s =await customer.Save(customer);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);

                  

                        Clear();
                        await RefreshCustomersList();
                        await Search();
                    }
                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_update_Click(object sender, RoutedEventArgs e)
        {//update
            try
            {
                HelpClass.StartAwait(grid_main);
                if (customer.custId > 0)
                {
                    if (HelpClass.validate(requiredControlList, this) && HelpClass.IsValidEmail(this))
                {
                    //customer.custname = tb_custname.Text;
                    //customer.lastName = tb_lastName.Text;
                    //customer.countryId = Convert.ToInt32(cb_country.SelectedValue);
                    //customer.email = tb_email.Text;
                    //customer.mobile = cb_areaMobile.Text + "-" + tb_mobile.Text; ;
                    //if (!tb_phone.Text.Equals(""))
                    //    customer.phone = cb_areaPhone.Text + "-" + cb_areaPhoneLocal.Text + "-" + tb_phone.Text;
                    //if (!tb_fax.Text.Equals(""))
                    //    customer.fax = cb_areaFax.Text + "-" + cb_areaFaxLocal.Text + "-" + tb_fax.Text;
                    //if (cb_custlevel.SelectedValue != null)
                    //    customer.custlevel = cb_custlevel.SelectedValue.ToString();
                    //customer.company = tb_company.Text.Trim();
                    //customer.address = tb_address.Text;
                    //customer.notes = tb_notes.Text;
                    //customer.createUserId = MainWindow.userLogin.userId;
                    //customer.updateUserId = MainWindow.userLogin.userId;
                    //customer.balance = 0;

                        decimal s = await customer.Save(customer);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);
                        
                    

                            await RefreshCustomersList();
                            await Search();
                        }
                }
                }
                else
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trSelectItemFirst"), animation: ToasterAnimation.FadeIn);

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_delete_Click(object sender, RoutedEventArgs e)
        {//delete
            try
            {
                HelpClass.StartAwait(grid_main);

                if (customer.custId != 0)
                {
                    if ((!customer.canDelete) && (!(bool)customer.isActive ))
                    {
                        #region
                        Window.GetWindow(this).Opacity = 0.2;
                        wd_acceptCancelPopup w = new wd_acceptCancelPopup();
                        w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxActivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion
                        if (w.isOk)
                            await activate();
                    }
                    else
                    {
                        #region
                        Window.GetWindow(this).Opacity = 0.2;
                        wd_acceptCancelPopup w = new wd_acceptCancelPopup();
                        if (customer.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                        if (!customer.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion
                        if (w.isOk)
                        {
                            string popupContent = "";
                            if (customer.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                            if ((!customer.canDelete) && (!(bool)customer.isActive)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                            decimal s = await customer.Delete(customer.custId, MainWindow.userLogin.userId, customer.canDelete);
                            if (s < 0)
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                            else
                            {
                                customer.custId = 0;
                                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                                await RefreshCustomersList();
                                await Search();
                                Clear();
                            }
                        }
                    }
                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async Task activate()
        {//activate
            customer.isActive =true;
            decimal s = await customer.Save(customer);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshCustomersList();
                await Search();
            }
        }
        #endregion
        #region events
        private async void Tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tgl_isActive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (customers is null)
                    await RefreshCustomersList();
                tgl_customerState = true;
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tgl_isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (customers is null)
                    await RefreshCustomersList();
                tgl_customerState =false;
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                Clear();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Dg_customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //selection
                if (dg_customer.SelectedIndex != -1)
                {
                    customer = dg_customer.SelectedItem as Customers;
                    this.DataContext = customer;
                    if (customer != null)
                    {
                        //tb_custCode.Text = customer.custCode;
                        //cb_country.SelectedValue = customer.countryId;
                        //this.DataContext = customer;
                        //await getImg();
                        //#region delete
                        //if (customer.canDelete)
                        //    btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        //else
                        //{
                        //    if (customer.isActive == 0)
                        //        btn_delete.Content = MainWindow.resourcemanager.GetString("trActive");
                        //    else
                        //        btn_delete.Content = MainWindow.resourcemanager.GetString("trInActive");
                        //}
                        //#endregion
                        //HelpClass.getMobile(customer.mobile, cb_areaMobile, tb_mobile);
                        //HelpClass.getPhone(customer.phone, cb_areaPhone, cb_areaPhoneLocal, tb_phone);
                        //HelpClass.getPhone(customer.fax, cb_areaFax, cb_areaFaxLocal, tb_fax);
                    }
                }
                HelpClass.clearValidate(requiredControlList, this);
              //  p_error_email.Visibility = Visibility.Collapsed;

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {//refresh

                HelpClass.StartAwait(grid_main);
                await RefreshCustomersList();
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion

        #region Refresh & Search
        async Task Search()
        {
            //search
            if (customers is null)
                await RefreshCustomersList();

            searchText = tb_search.Text.ToLower();
            customersQuery = customers.Where(s => (
            s.custname.ToLower().Contains(searchText) ||
            s.mobile.ToLower().Contains(searchText)
            ) && (bool)s.isActive == tgl_customerState);
            RefreshCustomersView();
        }
        async Task<IEnumerable<Customers>> RefreshCustomersList()
        {
            customers = await customer.GetAll();
            return customers;
        }
        void RefreshCustomersView()
        {
            dg_customer.ItemsSource = customersQuery;
            txt_count.Text = customersQuery.Count().ToString();
        }
        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 
        //private void Cb_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{//select country
        //    try
        //    {
        //        cb_areaMobile.SelectedIndex = cb_country.SelectedIndex;
        //        cb_areaFax.SelectedIndex = cb_country.SelectedIndex;
        //        cb_areaPhone.SelectedIndex = cb_country.SelectedIndex;
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.ExceptionMessage(ex, this);
        //    }

        //}

        void Clear()
        {
            this.DataContext = new Customers();

            //#region code
            //tb_custCode.Text = "";
            //#endregion

            #region mobile-Phone-fax-email-country
            //cb_areaMobile.SelectedValue = MainWindow.Region.countryId;
            //cb_areaPhone.SelectedValue = MainWindow.Region.countryId;
            //cb_areaFax.SelectedValue = MainWindow.Region.countryId;
            //cb_country.SelectedIndex = -1;
            //cb_areaMobile.SelectedIndex = -1;
            //cb_areaPhone.SelectedIndex = -1;
            //cb_areaFax.SelectedIndex = -1;
            //cb_areaPhoneLocal.SelectedIndex = -1;
            //cb_areaFaxLocal.SelectedIndex = -1;
           tb_mobile.Clear();
            //tb_phone.Clear();
            //tb_fax.Clear();
            //tb_email.Clear();
            #endregion

            //#region image
            //HelpClass.clearImg(btn_image);
            //#endregion

            // last 
            HelpClass.clearValidate(requiredControlList, this);
           // p_error_email.Visibility = Visibility.Collapsed;
        }
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                //only  digits
                TextBox textBox = sender as TextBox;
                HelpClass.InputJustNumber(ref textBox);
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Code_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                //only english and digits
                Regex regex = new Regex("^[a-zA-Z0-9. -_?]*$");
                if (!regex.IsMatch(e.Text))
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }

        }
        private void Spaces_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = e.Key == Key.Space;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void ValidateEmpty_TextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void validateEmpty_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #endregion
        
    //    #region Phone
    //    int? countryid;
    //    private async void Cb_areaPhone_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        try
    //        {
    //            HelpClass.StartAwait(grid_main);
    //            if (cb_areaPhone.SelectedValue != null)
    //            {
    //                if (cb_areaPhone.SelectedIndex >= 0)
    //                {
    //                    countryid = int.Parse(cb_areaPhone.SelectedValue.ToString());
    //                    await FillCombo.fillCountriesLocal(cb_areaPhoneLocal, (int)countryid, brd_areaPhoneLocal);
    //                }
    //            }
    //            HelpClass.EndAwait(grid_main);
    //        }
    //        catch (Exception ex)
    //        {
    //            HelpClass.EndAwait(grid_main);
    //            HelpClass.ExceptionMessage(ex, this);
    //        }
    //    }
    //    private async void Cb_areaFax_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //        try
    //        {
    //            HelpClass.StartAwait(grid_main);
    //            if (cb_areaFax.SelectedValue != null)
    //            {
    //                if (cb_areaFax.SelectedIndex >= 0)
    //                {
    //                    countryid = int.Parse(cb_areaFax.SelectedValue.ToString());
    //                    await FillCombo.fillCountriesLocal(cb_areaFaxLocal, (int)countryid, brd_areaFaxLocal);
    //                }
    //            }
    //        HelpClass.EndAwait(grid_main);
    //    }
    //        catch (Exception ex)
    //        {
    //            HelpClass.EndAwait(grid_main);
    //            HelpClass.ExceptionMessage(ex, this);
    //    }
    //}

    //    #endregion

        //#region Image
        //string imgFileName = "pic/no-image-icon-125x125.png";
        //bool isImgPressed = false;
        //OpenFileDialog openFileDialog = new OpenFileDialog();
        //SaveFileDialog saveFileDialog = new SaveFileDialog();
        //private void Btn_image_Click(object sender, RoutedEventArgs e)
        //{
        //    //select image
        //    try
        //    {
        //        HelpClass.StartAwait(grid_main);
        //        isImgPressed = true;
        //        openFileDialog.Filter = "Images|*.png;*.jpg;*.bmp;*.jpeg;*.jfif";
        //        if (openFileDialog.ShowDialog() == true)
        //        {
        //            HelpClass.imageBrush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Relative));
        //            btn_image.Background = HelpClass.imageBrush;
        //            imgFileName = openFileDialog.FileName;
        //        }
        //        else
        //        { }
        //        HelpClass.EndAwait(grid_main);
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.EndAwait(grid_main);
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        //private async Task getImg()
        //{
        //    if (string.IsNullOrEmpty(customer.image))
        //    {
        //        HelpClass.clearImg(btn_image);
        //    }
        //    else
        //    {
        //        byte[] imageBuffer = await customer.downloadImage(customer.image); // read this as BLOB from your DB

        //        var bitmapImage = new BitmapImage();
        //        if (imageBuffer != null)
        //        {
        //            using (var memoryStream = new MemoryStream(imageBuffer))
        //            {
        //                bitmapImage.BeginInit();
        //                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //                bitmapImage.StreamSource = memoryStream;
        //                bitmapImage.EndInit();
        //            }

        //            btn_image.Background = new ImageBrush(bitmapImage);
        //            // configure trmporary path
        //            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        //            string tmpPath = System.IO.Path.Combine(dir, Global.TMPCustomersFolder);
        //            tmpPath = System.IO.Path.Combine(tmpPath, customer.image);
        //            openFileDialog.FileName = tmpPath;
        //        }
        //        else
        //            HelpClass.clearImg(btn_image);
        //    }
        //}
        //#endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Collect all generations of memory.
            GC.Collect();
        }

        #region reports

        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog2 = new SaveFileDialog();

        public void BuildReport()
        {

            //string firstTitle = "paymentsReport";
            ////string secondTitle = "";
            ////string subTitle = "";
            //string Title = "";

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            if (isArabic)
            {
                addpath = @"\Reports\SectionData\En\EnCustomers.rdlc";

            }
            else
            {
                addpath = @"\Reports\SectionData\En\EnCustomers.rdlc";
            }
            //D:\myproj\posproject3\DataEntryApp\DataEntryApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

          //  clsReports.CustomersReport(customersQuery, rep, reppath, paramarr);
            clsReports.setReportLanguage(paramarr);
            clsReports.Header(paramarr);

            rep.SetParameters(paramarr);

            rep.Refresh();

        }

        private void Btn_pdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                BuildReport();

                saveFileDialog.Filter = "PDF|*.pdf;";

                if (saveFileDialog2.ShowDialog() == true)
                {
                    string filepath = saveFileDialog.FileName;
                    LocalReportExtensions.ExportToPDF(rep, filepath);
                }
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void Btn_preview_Click(object sender, RoutedEventArgs e)
        {
            //preview
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                Window.GetWindow(this).Opacity = 0.2;

                string pdfpath = "";
                //
                pdfpath = @"\Thumb\report\temp.pdf";
                pdfpath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, pdfpath);

                BuildReport();

                LocalReportExtensions.ExportToPDF(rep, pdfpath);
                wd_previewPdf w = new wd_previewPdf();
                w.pdfPath = pdfpath;
                if (!string.IsNullOrEmpty(w.pdfPath))
                {
                    w.ShowDialog();
                    w.wb_pdfWebViewer.Dispose();


                }
                Window.GetWindow(this).Opacity = 1;
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_print_Click(object sender, RoutedEventArgs e)
        {
            //print
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                BuildReport();
                LocalReportExtensions.PrintToPrinterbyNameAndCopy(rep, FillCombo.getdefaultPrinters(), FillCombo.rep_print_count == null ? short.Parse("1") : short.Parse(FillCombo.rep_print_count));
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_exportToExcel_Click(object sender, RoutedEventArgs e)
        {
            //excel
            try
            {
                HelpClass.StartAwait(grid_main);

                #region
                //Thread t1 = new Thread(() =>
                //{
                BuildReport();
                this.Dispatcher.Invoke(() =>
                {
                    saveFileDialog2.Filter = "EXCEL|*.xls;";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filepath = saveFileDialog.FileName;
                        LocalReportExtensions.ExportToExcel(rep, filepath);
                    }
                });


                //});
                //t1.Start();
                #endregion

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }



        #endregion

    }
}
