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
using System.Windows.Threading;
namespace DataEntryApp.View.archive
{
    /// <summary>
    /// Interaction logic for uc_attendance.xaml
    /// </summary>
    public partial class uc_attendance : UserControl
    {
        public uc_attendance()
        {
            try
            {
                InitializeComponent();
                //  this. += new System.Windows.Forms.KeyPressEventHandler(this.HandleKeyPress);

                // Loaded += UserControl_Loaded;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private static uc_attendance _instance;
        public static uc_attendance Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_attendance();
                return _instance;
            }
        }
        CustomersLogs customersLog = new CustomersLogs();
        IEnumerable<CustomersLogs> customersLogsQuery;
        IEnumerable<CustomersLogs> customersLogs;
        byte tgl_customersLogState;
        string searchText = "";
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);
                MainWindow.mainWindow.KeyDown += HandleKeyPress;
                //   requiredControlList = new List<string> { "name", "lastName", "AccountName",  "type", "mobile" , "country" };
                requiredControlList = new List<string> { "barcode" };
                #region translate
                //if (MainWindow.lang.Equals("en"))
                //{
                //    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.en_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.LeftToRight;
                //}
                //else
                //{
                //    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.ar_file", Assembly.GetExecutingAssembly());
                //    grid_main.FlowDirection = FlowDirection.RightToLeft;
                //}
                translate();
                #endregion

                //await FillCombo.fillCountries(cb_areaMobile);
                //await FillCombo.fillCountries(cb_areaPhone);
                //await FillCombo.fillCountries(cb_areaFax);
                //await FillCombo.fillCountriesNames(cb_country);
                //FillCombo.fillCustomersLogType(cb_type);

                //Keyboard.Focus(tb_name);

                await Search();
                Clear();

                fillCombo();

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
            //barcodeNum attendenceRecord signIn signOut attendenceFilter attendanceDays attendanceHours
            //txt_title.Text = MainWindow.resourcemanager.GetString("trCustomersLogs");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(pb_password, MainWindow.resourcemanager.GetString("trPasswordHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_passwordMirror, MainWindow.resourcemanager.GetString("trPasswordHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_mobile, MainWindow.resourcemanager.GetString("contactNumberHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("trNameHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_AccountName, MainWindow.resourcemanager.GetString("trCustomersLogNameHint"));

            dg_customersLog.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_customersLog.Columns[1].Header = MainWindow.resourcemanager.GetString("trName");
            dg_customersLog.Columns[2].Header = MainWindow.resourcemanager.GetString("trDate");
            dg_customersLog.Columns[3].Header = MainWindow.resourcemanager.GetString("loginTime");
            dg_customersLog.Columns[4].Header = MainWindow.resourcemanager.GetString("logoutTime");

            txt_recordAttendance.Text = MainWindow.resourcemanager.GetString("attendenceRecord");
            btn_login.Content = MainWindow.resourcemanager.GetString("signIn");
            btn_logout.Content = MainWindow.resourcemanager.GetString("signOut");
            btn_send.Content = MainWindow.resourcemanager.GetString("send");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_barcode, MainWindow.resourcemanager.GetString("barcodeNum"));
            txt_filter.Text = MainWindow.resourcemanager.GetString("attendenceFilter");

            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_custname, MainWindow.resourcemanager.GetString("trNameHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_Nationality, MainWindow.resourcemanager.GetString("nationalityHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_department, MainWindow.resourcemanager.GetString("specializationHint"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_fromDate, MainWindow.resourcemanager.GetString("from"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_toDate, MainWindow.resourcemanager.GetString("to"));
            //
            txt_custname.Text = MainWindow.resourcemanager.GetString("trName");
            txt_nationality.Text = MainWindow.resourcemanager.GetString("nationality");
            txt_department.Text = MainWindow.resourcemanager.GetString("specialization");
            txt_attendanceDays.Text = MainWindow.resourcemanager.GetString("attendanceDays");
            txt_attendanceHour.Text = MainWindow.resourcemanager.GetString("attendanceHours");

            ////contactNumberHint
            //tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            //tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            //tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            //tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            //btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            //btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            //btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
            //  txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");


            //  MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_code, MainWindow.resourcemanager.GetString("trCodeHint"));

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_lastName, MainWindow.resourcemanager.GetString("trLastNameHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_country, MainWindow.resourcemanager.GetString("trCountryHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_email, MainWindow.resourcemanager.GetString("trEmailHint"));

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_phone, MainWindow.resourcemanager.GetString("trPhoneHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_fax, MainWindow.resourcemanager.GetString("trFaxHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_address, MainWindow.resourcemanager.GetString("trAdressHint"));
            //txt_workInformation.Text = MainWindow.resourcemanager.GetString("trWorkInformation");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_type, MainWindow.resourcemanager.GetString("trJobHint"));
            //txt_loginInformation.Text = MainWindow.resourcemanager.GetString("trLoginInformation");

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_notes, MainWindow.resourcemanager.GetString("trNoteHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_address, MainWindow.resourcemanager.GetString("trAdressHint"));


        }
        private static DispatcherTimer timer;
        #region//to handle barcode characters



        // for barcode
        public List<Control> controls;
        DateTime _lastKeystroke = new DateTime(0);
        static private string _BarcodeStr = "";
        static private object _Sender;
        bool _IsFocused = false;
        #endregion
        //private async void HandleKeyPress(object sender, KeyEventArgs e)
        //{
        //    try



        //        {
        //        //if (!_IsFocused)
        //        //{
        //        //    //Control c = CheckActiveControl();
        //        //    //if (c == null)
        //        //        tb_barcode.Focus();
        //        //    _IsFocused = true;
        //        //}
        //        if (sender != null)
        //            HelpClass.StartAwait(grid_main);
        //        TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
        //        if (elapsed.TotalMilliseconds > 80)
        //        {
        //            _BarcodeStr = "";

        //        }

        //        string digit = "";
        //        // record keystroke & timestamp 
        //        if (e.Key >= Key.D0 && e.Key <= Key.D9)
        //        {
        //            //digit pressed!         
        //            digit = e.Key.ToString().Substring(1);
        //            // = "1" when D1 is pressed
        //        }
        //        else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
        //        {
        //            digit = e.Key.ToString().Substring(6); // = "1" when NumPad1 is pressed
        //        }
        //        else if (e.Key >= Key.A && e.Key <= Key.Z)
        //            digit = e.Key.ToString();
        //        else if (e.Key == Key.OemMinus)
        //        {
        //            digit = "-";
        //        }

        //        _BarcodeStr += digit.ToString();
        //        _lastKeystroke = DateTime.Now;
        //        // process barcode




        //          if (e.Key.ToString() == "Return"  )
        //        {
        //            //   await dealWithBarcode(_BarcodeStr);
        //            await dealWithBarcode(_BarcodeStr);

        //            tb_barcode.Text = "";
        //            //if (_Sender != null) //clear barcode from inputs
        //            //{
        //            //    DatePicker dt = _Sender as DatePicker;
        //            //     TextBox tb = _Sender as TextBox;
        //            //    tb_barcode.Text = string.Empty;
        //            //    //if (dt != null)
        //            //    //{
        //            //    //    if (dt.Name == "dp_desrvedDate")
        //            //    //        _BarcodeStr = _BarcodeStr.Substring(1);
        //            //    //}
        //            //    //else if (tb != null)
        //            //    //{
        //            //    //    if (tb.Name == "tb_barcode" )// remove barcode from text box
        //            //    //    {
        //            //    //        string tbString = tb.Text;
        //            //    //        string newStr = "";
        //            //    //        int startIndex = tbString.IndexOf(_BarcodeStr);
        //            //    //        if (startIndex != -1)
        //            //    //            newStr = tbString.Remove(startIndex, _BarcodeStr.Length);

        //            //    //        tb.Text = newStr;
        //            //    //    }
        //            //    //}
        //            //}
        //          //  _BarcodeStr = "";
        //            e.Handled = true;
        //            _IsFocused = false;
        //        }
        //        _Sender = null;

        //        //if (e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl))
        //        //{
        //        //    switch (e.Key)
        //        //    {
        //        //        case Key.P:
        //        //            //handle P key
        //        //            Btn_printInvoice_Click(null, null);
        //        //            break;
        //        //        case Key.S:
        //        //            //handle S key
        //        //            Btn_save_Click(btn_save, null);
        //        //            break;
        //        //        case Key.I:
        //        //            //handle S key
        //        //            Btn_items_Click(null, null);
        //        //            break;
        //        //    }
        //        //}
        //        if (sender != null)
        //            HelpClass.EndAwait(grid_main);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (sender != null)
        //            HelpClass.EndAwait(grid_main);
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        // DateTime _lastKeystroke = new DateTime(0);

        private async void HandleKeyPress(object sender, KeyEventArgs e)
        {
            tb_barcode.Focus();

            // check timing (keystrokes within 80 ms)
            TimeSpan elapsed = (DateTime.Now - _lastKeystroke);
            if (elapsed.TotalMilliseconds > 80)
                _BarcodeStr = "";

            // record keystroke & timestamp
            // _BarcodeStr += e.Key.ToString();
            _lastKeystroke = DateTime.Now;

            // process barcode
            string digit = "";
            // record keystroke & timestamp 
            if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                //digit pressed!         
                digit = e.Key.ToString().Substring(1);
                // = "1" when D1 is pressed
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                digit = e.Key.ToString().Substring(6); // = "1" when NumPad1 is pressed
            }
            else if (e.Key >= Key.A && e.Key <= Key.Z)
                digit = e.Key.ToString();
            else if (e.Key == Key.OemMinus)
            {
                digit = "-";
            }
            _BarcodeStr += digit;

            // if (e.Key == Key.Return)
            if (e.Key.ToString() == "Return")
            {
                await dealWithBarcode(_BarcodeStr);
                tb_barcode.Text = "";

            }

            //   _BarcodeStr = "";
        }

        private async Task dealWithBarcode(string barcode)
        {
            await sendbarcode(barcode);
            tb_barcode.Text = "";
        }


        #region Add - Update - Delete - Search - Tgl - Clear - DG_SelectionChanged - refresh

        private async void Btn_update_Click(object sender, RoutedEventArgs e)
        {//update
            /*
            try
            {
                HelpClass.StartAwait(grid_main);
                if (customersLog.customersLogId > 0)
                {
                    //chk duplicate customersLogName
                    bool duplicateCustomersLogName = false;
                    duplicateCustomersLogName = await chkIfCustomersLogNameIsExists(tb_name.Text, customersLog.customersLogId);
                    if (HelpClass.validate(requiredControlList, this) && duplicateCustomersLogName && HelpClass.IsValidEmail(this))
                    {
                        //customersLog.code = customersLog.code;
                        customersLog.name = tb_name.Text;
                        //customersLog.lastName = tb_lastName.Text;
                        //customersLog.countryId = Convert.ToInt32(cb_country.SelectedValue);
                        customersLog.AccountName = tb_AccountName.Text;
                        //customersLog.password = Md5Encription.MD5Hash("Inc-m" + pb_password.Password); ;
                        //customersLog.email = tb_email.Text;
                        customersLog.mobile = tb_mobile.Text; ;
                        //if (!tb_phone.Text.Equals(""))
                        //    customersLog.phone = cb_areaPhone.Text + "-" + cb_areaPhoneLocal.Text + "-" + tb_phone.Text;
                        //if (!tb_fax.Text.Equals(""))
                        //    customersLog.fax = cb_areaFax.Text + "-" + cb_areaFaxLocal.Text + "-" + tb_fax.Text;
                        //if (cb_type.SelectedValue != null)
                        //    customersLog.type = cb_type.SelectedValue.ToString();
                        //customersLog.address = tb_address.Text;
                        //customersLog.notes = tb_notes.Text;
                        //customersLog.isActive = 1;
                        customersLog.createCustomersLogId = MainWindow.customersLogLogin.customersLogId;
                        customersLog.updateCustomersLogId = MainWindow.customersLogLogin.customersLogId;

                        decimal s = await customersLog.Save(customersLog);
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);

                            //if (isImgPressed)
                            //{
                            //    int customersLogId = (int)s;
                            //    string b = await customersLog.uploadImage(imgFileName, Md5Encription.MD5Hash("Inc-m" + customersLogId.ToString()), customersLogId);
                            //    customersLog.image = b;
                            //    isImgPressed = false;
                            //    if (!b.Equals(""))
                            //    {
                            //        await getImg();
                            //    }
                            //    else
                            //    {
                            //        HelpClass.clearImg(btn_image);
                            //    }
                            //}

                            await RefreshCustomersLogsList();
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
            */
        }
        private async void Btn_delete_Click(object sender, RoutedEventArgs e)
        {//delete
            /*
            try
            {
                HelpClass.StartAwait(grid_main);
                if (customersLog.customersLogId != 0)
                {
                    if ((!customersLog.canDelete) && (customersLog.isActive == 0))
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
                        if (customersLog.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                        if (!customersLog.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion

                        if (w.isOk)
                        {
                            string popupContent = "";
                            if (customersLog.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                            if ((!customersLog.canDelete) && (customersLog.isActive == 1)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                            decimal s = await customersLog.Delete(customersLog.customersLogId, MainWindow.customersLogLogin.customersLogId, customersLog.canDelete);
                            if (s < 0)
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                            else
                            {
                                customersLog.customersLogId = 0;
                                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                                await RefreshCustomersLogsList();
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
            */
        }
        private async Task activate()
        {//activate
            /*
            customersLog.isActive = 1;
            decimal s = await customersLog.Save(customersLog);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshCustomersLogsList();
                await Search();
            }
            */
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
                if (customersLogs is null)
                    await RefreshCustomersLogsList();
                tgl_customersLogState = 1;
                await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                //HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tgl_isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                if (customersLogs is null)
                    await RefreshCustomersLogsList();
                tgl_customersLogState = 0;
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
        {//clear
            try
            {
                HelpClass.StartAwait(grid_main);

                Clear();
                //p_error_password.Visibility = Visibility.Collapsed;
                //p_error_email.Visibility = Visibility.Collapsed;

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_clearFilter_Click(object sender, RoutedEventArgs e)
        {//clear
            try
            {
                HelpClass.StartAwait(grid_main);

                ClearFilter();
                //p_error_password.Visibility = Visibility.Collapsed;
                //p_error_email.Visibility = Visibility.Collapsed;

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        
        private async void Dg_customersLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//selection
            try
            {
                HelpClass.StartAwait(grid_main);
                if (dg_customersLog.SelectedIndex != -1)
                {
                    customersLog = dg_customersLog.SelectedItem as CustomersLogs;
                    if (customersLog != null)
                    {
                        //tb_code.Text = customersLog.code;
                        //cb_country.SelectedValue = customersLog.countryId;
                        //this.DataContext = customersLog;
                        tb_custname.Text = customersLog.custname;
                        tb_nationality.Text = customersLog.Nationality;
                        tb_department.Text = customersLog.department;
                        tb_attendanceDays.Text = calcDays().ToString();
                        tb_attendanceHour.Text = calcHours().ToString(@"hh\:mm\:ss");

                        //HelpClass.getPhone(customersLog.phone, cb_areaPhone, cb_areaPhoneLocal, tb_phone);
                        //HelpClass.getPhone(customersLog.fax, cb_areaFax, cb_areaFaxLocal, tb_fax);
                    }
                }
                //HelpClass.clearValidate(requiredControlList, this);
                //p_error_email.Visibility = Visibility.Collapsed;

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private long calcDays()
        {
            long days = 0;
            //  customersLog
            var list = customersLogs.Where(s => s.custId == customersLog.custId && s.sInDate != null && s.sOutDate != null).
                GroupBy(g => ((DateTime)g.sInDate).Date).Select(g => new Customers { custId = g.FirstOrDefault().custId.Value }).ToList();
            days = list.Count();
            return days;
        }
        private TimeSpan calcHours()
        {

            //  customersLog
            List<CustomersLogs> list = customersLogs.Where(s => s.custId == customersLog.custId && s.sInDate != null && s.sOutDate != null).ToList();
            TimeSpan total = new TimeSpan();
            foreach (CustomersLogs row in list)
            {
                total += (DateTime)row.sOutDate - (DateTime)row.sInDate;

            }
            return total;
        }
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
            try
            {
                HelpClass.StartAwait(grid_main);
                Clear();
                await RefreshCustomersLogsList();
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

            if (customersLogs is null)
                await RefreshCustomersLogsList();
            searchText = tb_search.Text.ToLower();

            customersLogsQuery = customersLogs.Where(s => (s.custname.ToLower().Contains(searchText) || s.custId.ToString().Contains(searchText))
           &&
           //customer
           (cb_custname.SelectedIndex != -1 ? s.custId == (long)cb_custname.SelectedValue : true)
            &&
            // Nationality
           (cb_Nationality.SelectedIndex != -1 ? s.nationalityId == (long)cb_Nationality.SelectedValue : true)
            &&
            // department
           (cb_department.SelectedIndex != -1 ? s.departmentId == (long)cb_department.SelectedValue : true)
           &&
            //start date
            (dp_fromDate.SelectedDate != null ? s.sInDate.Value.Date >= dp_fromDate.SelectedDate.Value.Date : true)
            &&
            //end date
            (dp_toDate.SelectedDate != null ? s.sInDate.Value.Date <= dp_toDate.SelectedDate.Value.Date : true)
            );
            RefreshCustomersLogsView();
        }
        async Task<IEnumerable<CustomersLogs>> RefreshCustomersLogsList()
        {

            customersLogs = await customersLog.GetAll();
            // customersLogs = customersLogs.Where(x => x.type != "ag");

            return customersLogs;
        }
        void RefreshCustomersLogsView()
        {
            dg_customersLog.ItemsSource = customersLogsQuery;
        }
        #endregion
        private void fillCombo()
        {
            fillcustomers();
            fillnationalities();
            filldepartments();

        }
        #region filter
        List<Customers> customers = new List<Customers>();
        private void fillcustomers()
        {
            customers = customersLogs.GroupBy(g => g.custId).Select(g => new Customers { custId = g.FirstOrDefault().custId.Value, custname = g.FirstOrDefault().custname }).ToList();
            //if (vendors.Where(x => x.name == "unknown").Count() > 0)
            //    vendors.Where(x => x.name == "unknown").FirstOrDefault().name = AppSettings.resourcemanager.GetString("trUnKnown");
            cb_custname.SelectedValuePath = "custId";
            cb_custname.DisplayMemberPath = "custname";
            cb_custname.ItemsSource = customers;
        }

        List<Nationalities> nationalities = new List<Nationalities>();
        private void fillnationalities()
        {
            nationalities = customersLogs.GroupBy(g => g.nationalityId).Select(g => new Nationalities { nationalityId = g.FirstOrDefault().nationalityId.Value, name = g.FirstOrDefault().Nationality }).ToList();
            //if (vendors.Where(x => x.name == "unknown").Count() > 0)
            //    vendors.Where(x => x.name == "unknown").FirstOrDefault().name = AppSettings.resourcemanager.GetString("trUnKnown");
            cb_Nationality.SelectedValuePath = "nationalityId";
            cb_Nationality.DisplayMemberPath = "name";
            cb_Nationality.ItemsSource = nationalities;
        }

        List<Departments> departments = new List<Departments>();
        private void filldepartments()
        {
            departments = customersLogs.GroupBy(g => g.departmentId).Select(g => new Departments { departmentId = g.FirstOrDefault().departmentId.Value, name = g.FirstOrDefault().department }).ToList();
            //if (vendors.Where(x => x.name == "unknown").Count() > 0)
            //    vendors.Where(x => x.name == "unknown").FirstOrDefault().name = AppSettings.resourcemanager.GetString("trUnKnown");
            cb_department.SelectedValuePath = "departmentId";
            cb_department.DisplayMemberPath = "name";
            cb_department.ItemsSource = departments;
        }

        private async void Cb_custname_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}

                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_Nationality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}

                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Cb_department_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}

                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Dp_fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}

                await Search();

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Dp_toDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (cb_custname.SelectedItem != null)
                //{

                //}

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
        #region validate - clearValidate - textChange - lostFocus - . . . . 
        void Clear()
        {
            tb_barcode.Text = "";

            tb_search.Text = "";
            cb_custname.SelectedItem = null;
            cb_custname.Text = "";
            cb_Nationality.SelectedItem = null;
            cb_Nationality.Text = "";
            cb_department.SelectedItem = null;
            cb_department.Text = "";
            dp_fromDate.SelectedDate = null;
            dp_fromDate.Text = "";
            dp_toDate.SelectedDate = null;
            dp_toDate.Text = "";
            //
            tb_custname.Text = "-";
            tb_nationality.Text = "-";
            tb_department.Text = "-";
            tb_attendanceDays.Text = "-";
            tb_attendanceHour.Text = "-";
            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
        void ClearFilter()
        {
            //tb_barcode.Text = "";

          //  tb_search.Text = "";
            cb_custname.SelectedItem = null;
            cb_custname.Text = "";
            cb_Nationality.SelectedItem = null;
            cb_Nationality.Text = "";
            cb_department.SelectedItem = null;
            cb_department.Text = "";
            dp_fromDate.SelectedDate = null;
            dp_fromDate.Text = "";
            dp_toDate.SelectedDate = null;
            dp_toDate.Text = "";
            //
           
            // last 
           // HelpClass.clearValidate(requiredControlList, this);
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
        private async Task<bool> chkIfCustomersLogNameIsExists(string customersLogname, long uId)
        {
            bool isValid = true;
            /*
             if (customersLogs == null)
                 await RefreshCustomersLogsList();
             if (customersLogs.Any(i => i.AccountName == customersLogname && i.customersLogId != uId))
                 isValid = false;
             if (!isValid)
                 Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trErrorDuplicateCustomersLogNameToolTip"), animation: ToasterAnimation.FadeIn);
          */
            return isValid;
        }





        #region Image
        string imgFileName = "pic/no-image-icon-125x125.png";
        bool isImgPressed = false;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        #endregion

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                MainWindow.mainWindow.KeyDown -= HandleKeyPress;
                // await newDraft();
                timer.Stop();

                GC.Collect();
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }

            // Collect all generations of memory.

        }
        #region reports

        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog2 = new SaveFileDialog();

        public void BuildReport()
        {

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            //if (isArabic)
            //{
            addpath = @"\Reports\Attendence\Attendence.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\Applications\En\EnPrograms.rdlc";
            //}

            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);

            

            clsReports.AttendenceReport(customersLogsQuery, rep, reppath, paramarr);
            // clsReports.setReportLanguage(paramarr);
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
        string type = "in";
        private void Btn_pieChart_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_login.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                btn_login.Foreground = Application.Current.Resources["White"] as SolidColorBrush;

                btn_logout.Background = Application.Current.Resources["White"] as SolidColorBrush;
                btn_logout.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;

                //   string barcode = "";

                type = "in";
                //sign in




            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Btn_logout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_logout.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                btn_logout.Foreground = Application.Current.Resources["White"] as SolidColorBrush;

                btn_login.Background = Application.Current.Resources["White"] as SolidColorBrush;
                btn_login.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;

                //logout

                type = "out";
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async Task sendbarcode(string barcode)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                // string barcode = "";
                if (barcode!="")
                {
                    if (type == "in")
                    {
                        //in


                        //   barcode = tb_barcode.Text;

                        decimal s = await customersLog.savelog(barcode, type);

                        if (s <= 0)
                        {
                            if (s == -4)
                            {
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("brcodenotexist"), animation: ToasterAnimation.FadeIn);
                            }
                            else
                            {
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                            }
                        }
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msginsuccess"), animation: ToasterAnimation.FadeIn);



                            Clear();
                            await RefreshCustomersLogsList();
                            await Search();
                            fillCombo();
                        }


                    }
                    else
                    {
                        //out


                        //  barcode = tb_barcode.Text;
                        decimal s = await customersLog.savelog(barcode, type);
                        Clear();
                        if (s <= 0)
                        {
                            if (s == -4)
                            {
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("brcodenotexist"), animation: ToasterAnimation.FadeIn);
                            }
                            else
                            {
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgnoout"), animation: ToasterAnimation.FadeIn);
                            }
                        }

                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgoutsuccess"), animation: ToasterAnimation.FadeIn);

                            Clear();
                            await RefreshCustomersLogsList();
                            await Search();
                            fillCombo();
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
        private async void Btn_send_Click(object sender, RoutedEventArgs e)
        {
            if (HelpClass.validate(requiredControlList, this))
                {
                await sendbarcode(tb_barcode.Text);
            }
            
            //try
            //{
            //    HelpClass.StartAwait(grid_main);
            //    string barcode = "";
            //    if (HelpClass.validate(requiredControlList, this))
            //    {
            //        if (type == "in")
            //        {
            //            //in


            //            barcode = tb_barcode.Text;

            //            decimal s = await customersLog.savelog(barcode, type);

            //            if (s <= 0)
            //            {
            //                if (s == -4)
            //                {
            //                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("brcodenotexist"), animation: ToasterAnimation.FadeIn);
            //                }
            //                else
            //                {
            //                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            //                }
            //            }
            //            else
            //            {
            //                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msginsuccess"), animation: ToasterAnimation.FadeIn);



            //                Clear();
            //                await RefreshCustomersLogsList();
            //                await Search();
            //                fillCombo();
            //            }


            //        }
            //        else
            //        {
            //            //out


            //            barcode = tb_barcode.Text;




            //            decimal s = await customersLog.savelog(barcode, type);
            //            Clear();
            //            if (s <= 0)
            //            {
            //                if (s == -4)
            //                {
            //                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("brcodenotexist"), animation: ToasterAnimation.FadeIn);
            //                }
            //                else
            //                {
            //                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgnoout"), animation: ToasterAnimation.FadeIn);
            //                }
            //            }

            //            else
            //            {
            //                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("msgoutsuccess"), animation: ToasterAnimation.FadeIn);

            //                Clear();
            //                await RefreshCustomersLogsList();
            //                await Search();
            //                fillCombo();
            //            }


            //        }
            //    }






            //    HelpClass.EndAwait(grid_main);


            //}
            //catch (Exception ex)
            //{
            //    HelpClass.EndAwait(grid_main);
            //    HelpClass.ExceptionMessage(ex, this);
            //}
        }
    }
}
