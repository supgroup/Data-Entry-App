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

namespace DataEntryApp.View.sectionData
{
    /// <summary>
    /// Interaction logic for uc_Nationalities.xaml
    /// </summary>
    public partial class uc_nationality : UserControl
    {
        public uc_nationality()
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
        private static uc_nationality _instance;
        public static uc_nationality Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_nationality();
                return _instance;
            }
        }
        Nationalities nationality = new Nationalities();
        IEnumerable<Nationalities> NationalitiesQuery;
        IEnumerable<Nationalities> Nationalities;
        bool tgl_Nationalitiestate;
        string searchText = "";
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);
                //   requiredControlList = new List<string> { "name", "lastName", "AccountName",  "type", "mobile" , "country" };
                requiredControlList = new List<string> { "name"  };
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
                //FillCombo.fillUserType(cb_type);

                Keyboard.Focus(tb_name);

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

            txt_title.Text = MainWindow.resourcemanager.GetString("nationality");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
    
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("nationalityHint"));
 

            dg_nationality.Columns[0].Header = MainWindow.resourcemanager.GetString("trNo.");
            dg_nationality.Columns[1].Header = MainWindow.resourcemanager.GetString("nationality");
            //dg_nationality.Columns[2].Header = MainWindow.resourcemanager.GetString("trUserName");
            //dg_nationality.Columns[2].Header = MainWindow.resourcemanager.GetString("contactNumber");
            //contactNumberHint
            tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            //tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            //tt_preview.Content = MainWindow.resourcemanager.GetString("trPreview");
            //tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            tt_count.Content = MainWindow.resourcemanager.GetString("trCount");

            btn_add.Content = MainWindow.resourcemanager.GetString("trAdd");
            btn_update.Content = MainWindow.resourcemanager.GetString("trUpdate");
            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
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
        #region Add - Update - Delete - Search - Tgl - Clear - DG_SelectionChanged - refresh
        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        { //add
            try
            {
              
                HelpClass.StartAwait(grid_main);

                //chk duplicate userName
                bool duplicateUserName = false;
                duplicateUserName = await chkIfNameIsExists(tb_name.Text, 0);
                //chk password length
                //bool passLength = false;
              //  passLength = chkPasswordLength(pb_password.Password);

                nationality = new Nationalities();
                if (HelpClass.validate(requiredControlList, this) && duplicateUserName    )
                {
                    //if (cb_type.SelectedValue != null)
                    //{
                        //tb_code.Text = await nationality.generateCodeNumber(cb_type.SelectedValue.ToString());
                        //nationality.code = await HelpClass.generateRandomString(3, "us", "Nationalities", 0);
                      
                    //}
                    nationality.name = tb_name.Text;
                    //nationality.lastName = tb_lastName.Text;
                    //nationality.countryId = Convert.ToInt32(cb_country.SelectedValue);
                    //nationality.AccountName = tb_AccountName.Text;
                    //nationality.password = Md5Encription.MD5Hash("Inc-m" + pb_password.Password); ;
                    ////nationality.email = tb_email.Text;
                    //nationality.mobile =  tb_mobile.Text; ;
                    //if (!tb_phone.Text.Equals(""))
                    //    nationality.phone = cb_areaPhone.Text + "-" + cb_areaPhoneLocal.Text + "-" + tb_phone.Text;
                    //if (!tb_fax.Text.Equals(""))
                    //    nationality.fax = cb_areaFax.Text + "-" + cb_areaFaxLocal.Text + "-" + tb_fax.Text;
                    //if (cb_type.SelectedValue != null)
                    //    nationality.type = cb_type.SelectedValue.ToString();
                    //nationality.address = tb_address.Text;
                    //nationality.notes = tb_notes.Text;
                    nationality.isActive = true;
                    nationality.createUserId = MainWindow.userLogin.userId;
                    nationality.updateUserId = MainWindow.userLogin.userId;

                    decimal s = await nationality.Save(nationality);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);

                        //if (isImgPressed)
                        //{
                        //    int userId = (int)s;
                        //    string b = await nationality.uploadImage(imgFileName,
                        //        Md5Encription.MD5Hash("Inc-m" + userId.ToString()), userId);
                        //    nationality.image = b;
                        //    isImgPressed = false;
                        //}

                        Clear();
                        await RefreshNationalitiesList();
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
                if (nationality.nationalityId > 0)
                {
                    //chk duplicate userName
                    bool duplicateUserName = false;
                    //duplicateUserName = await chkIfUserNameIsExists(tb_name.Text, nationality.userId);
                    duplicateUserName = await chkIfNameIsExists(tb_name.Text, nationality.nationalityId);
                    if (HelpClass.validate(requiredControlList, this) && duplicateUserName  )
                    {
                        //nationality.code = nationality.code;
                        nationality.name = tb_name.Text;
                        //nationality.lastName = tb_lastName.Text;
                        //nationality.countryId = Convert.ToInt32(cb_country.SelectedValue);
                        //nationality.AccountName = tb_AccountName.Text;
                        ////nationality.password = Md5Encription.MD5Hash("Inc-m" + pb_password.Password); ;
                        ////nationality.email = tb_email.Text;
                        //nationality.mobile =  tb_mobile.Text; ;
                        //if (!tb_phone.Text.Equals(""))
                        //    nationality.phone = cb_areaPhone.Text + "-" + cb_areaPhoneLocal.Text + "-" + tb_phone.Text;
                        //if (!tb_fax.Text.Equals(""))
                        //    nationality.fax = cb_areaFax.Text + "-" + cb_areaFaxLocal.Text + "-" + tb_fax.Text;
                        //if (cb_type.SelectedValue != null)
                        //    nationality.type = cb_type.SelectedValue.ToString();
                        //nationality.address = tb_address.Text;
                        //nationality.notes = tb_notes.Text;
                        //nationality.isActive = 1;
                        nationality.createUserId = MainWindow.userLogin.userId;
                        nationality.updateUserId = MainWindow.userLogin.userId;

                        decimal s = await nationality.Save(nationality);
                        if (s <= 0)
                            Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                        else
                        {
                            Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);

                            //if (isImgPressed)
                            //{
                            //    int userId = (int)s;
                            //    string b = await nationality.uploadImage(imgFileName, Md5Encription.MD5Hash("Inc-m" + userId.ToString()), userId);
                            //    nationality.image = b;
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

                            await RefreshNationalitiesList();
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
                if (nationality.nationalityId != 0)
                {
                    if ((!nationality.canDelete) && (nationality.isActive == true))
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
                        if (nationality.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                        if (!nationality.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion

                        if (w.isOk)
                        {
                            string popupContent = "";
                            if (nationality.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                            if ((!nationality.canDelete) && (nationality.isActive ==true)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                            decimal s = await nationality.Delete(nationality.nationalityId, MainWindow.userLogin.userId, nationality.canDelete);
                            if (s <= 0)
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("cannotdelete"), animation: ToasterAnimation.FadeIn);
                            else
                            {
                                nationality.nationalityId = 0;
                                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                                await RefreshNationalitiesList();
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
            nationality.isActive = true;
            decimal s = await nationality.Save(nationality);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshNationalitiesList();
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
                if (Nationalities is null)
                    await RefreshNationalitiesList();
                tgl_Nationalitiestate = true;
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
                if (Nationalities is null)
                    await RefreshNationalitiesList();
                tgl_Nationalitiestate = false;
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
        private void Dg_nationality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                if (dg_nationality.SelectedIndex != -1)
                {
                    nationality = dg_nationality.SelectedItem as Nationalities;
                    this.DataContext = nationality;
                    if (nationality != null)
                    {
                        //tb_code.Text = nationality.code;
                        //cb_country.SelectedValue = nationality.countryId;
                        this.DataContext = nationality;
                        //await getImg();
                        #region delete
                        if (nationality.canDelete)
                            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        else
                        {
                            if (nationality.isActive == false)
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trActive");
                            else
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trInActive");
                        }
                        #endregion
                        //HelpClass.getMobile(nationality.mobile, tb_mobile);
                        //HelpClass.getPhone(nationality.phone, cb_areaPhone, cb_areaPhoneLocal, tb_phone);
                        //HelpClass.getPhone(nationality.fax, cb_areaFax, cb_areaFaxLocal, tb_fax);
                    }
                }

                HelpClass.clearValidate(requiredControlList, this);
                //p_error_email.Visibility = Visibility.Collapsed;

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {//refresh
            try
            {
                HelpClass.StartAwait(grid_main);
                await RefreshNationalitiesList();
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
            if (Nationalities is null)
                await RefreshNationalitiesList();
            searchText = tb_search.Text.ToLower();
            NationalitiesQuery = Nationalities.Where(s => ( 
            s.name.ToLower().Contains(searchText)  
            
            ) && s.isActive == tgl_Nationalitiestate);

            RefreshNationalitiesView();
        }
        async Task<IEnumerable<Nationalities>> RefreshNationalitiesList()
        {
            Nationalities = await nationality.GetAll();
          //  Nationalities = Nationalities ;
            return Nationalities;
        }
        void RefreshNationalitiesView()
        {
            dg_nationality.ItemsSource = NationalitiesQuery;
            txt_count.Text = NationalitiesQuery.Count().ToString();
        }
        #endregion

        #region validate - clearValidate - textChange - lostFocus - . . . . 
        private void Cb_country_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//select country
            try
            {
                //    cb_areaMobile.SelectedIndex = cb_country.SelectedIndex;
                //    cb_areaFax.SelectedIndex = cb_country.SelectedIndex;
                //    cb_areaPhone.SelectedIndex = cb_country.SelectedIndex;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }

        }
        void Clear()
        {
            this.DataContext = new Nationalities();

            #region password-code
            //pb_password.Clear();
            //tb_passwordMirror.Clear();
            //tb_code.Text = "";
            #endregion
            #region mobile-Phone-fax-country
            //cb_areaMobile.SelectedValue = MainWindow.Region.countryId;
            //cb_areaPhone.SelectedValue = MainWindow.Region.countryId;
            //cb_areaFax.SelectedValue = MainWindow.Region.countryId;
            //cb_country.SelectedIndex = -1;
            //cb_areaMobile.SelectedIndex = -1;
            //cb_areaPhone.SelectedIndex = -1;
            //cb_areaFax.SelectedIndex = -1;
            //cb_areaPhoneLocal.SelectedIndex = -1;
            //cb_areaFaxLocal.SelectedIndex = -1;
            //tb_mobile.Clear();
            //tb_phone.Clear();
            //tb_fax.Clear();
            //tb_email.Clear();
            #endregion
            #region image
            //HelpClass.clearImg(btn_image);
            #endregion

            // last 
            HelpClass.clearValidate(requiredControlList, this);
            //p_error_email.Visibility = Visibility.Collapsed;
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
                //p_error_password.Visibility = Visibility.Collapsed;
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
        private async Task<bool> chkIfNameIsExists(string  name, long uId)
        {
            bool isValid = true;
            if (Nationalities == null)
                await RefreshNationalitiesList();
            if (Nationalities.Any(i => i.name == name && i.nationalityId != uId))
                isValid = false;
            if (!isValid)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trErrorDuplicateUserNameToolTip"), animation: ToasterAnimation.FadeIn);
            return isValid;
        }

        #region Password
        //private void ValidateEmpty_PasswordChanged(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        HelpClass.validate(requiredControlList, this);
        //        p_error_password.Visibility = Visibility.Collapsed;
        //    }
        //    catch (Exception ex)
        //    {
        //        HelpClass.ExceptionMessage(ex, this);
        //    }
        //}
        //private void P_showPassword_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        tb_passwordMirror.Text = pb_password.Password;
        //        tb_passwordMirror.Visibility = Visibility.Visible;
        //        pb_password.Visibility = Visibility.Collapsed;
        //    }
        //    catch (Exception ex)
        //    { HelpClass.ExceptionMessage(ex, this); }
        //}
        //private void P_showPassword_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    try
        //    {
        //        tb_passwordMirror.Visibility = Visibility.Collapsed;
        //        pb_password.Visibility = Visibility.Visible;
        //    }
        //    catch (Exception ex)
        //    { HelpClass.ExceptionMessage(ex, this); }
        //}
        //private bool chkPasswordLength(string password)
        //{
        //    bool isValid = true;

        //    if (password.Length < 6)
        //        isValid = false;

        //    if (!isValid)
        //    {
        //        p_error_password.Visibility = Visibility.Visible;
        //        #region Tooltip
        //        ToolTip toolTip = new ToolTip();
        //        toolTip.Content = MainWindow.resourcemanager.GetString("trErrorPasswordLengthToolTip");
        //        toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
        //        p_error_password.ToolTip = toolTip;
        //        #endregion
        //    }

        //    return isValid;
        //}
        #endregion

        #region Phone
        int? countryid;
        private async void Cb_areaPhone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    HelpClass.StartAwait(grid_main);
            //    if (cb_areaPhone.SelectedValue != null)
            //    {
            //        if (cb_areaPhone.SelectedIndex >= 0)
            //        {
            //            countryid = int.Parse(cb_areaPhone.SelectedValue.ToString());
            //            await FillCombo.fillCountriesLocal(cb_areaPhoneLocal, (int)countryid, brd_areaPhoneLocal);
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
        private async void Cb_areaFax_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    try
            //    {
            //        HelpClass.StartAwait(grid_main);
            //        if (cb_areaFax.SelectedValue != null)
            //        {
            //            if (cb_areaFax.SelectedIndex >= 0)
            //            {
            //                countryid = int.Parse(cb_areaFax.SelectedValue.ToString());
            //                await FillCombo.fillCountriesLocal(cb_areaFaxLocal, (int)countryid, brd_areaFaxLocal);
            //            }
            //        }
            //        HelpClass.EndAwait(grid_main);
            //    }
            //    catch (Exception ex)
            //    {
            //        HelpClass.EndAwait(grid_main);
            //        HelpClass.ExceptionMessage(ex, this);
            //    }
        }

        #endregion

        #region Image
        string imgFileName = "pic/no-image-icon-125x125.png";
        bool isImgPressed = false;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        private void Btn_image_Click(object sender, RoutedEventArgs e)
        {
            //select image
            try
            {
                HelpClass.StartAwait(grid_main);
                isImgPressed = true;
                openFileDialog.Filter = "Images|*.png;*.jpg;*.bmp;*.jpeg;*.jfif";
                if (openFileDialog.ShowDialog() == true)
                {
                    HelpClass.imageBrush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Relative));
                    //btn_image.Background = HelpClass.imageBrush;
                    imgFileName = openFileDialog.FileName;
                }
                else
                { }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        //private async Task getImg()
        //{
        //    if (string.IsNullOrEmpty(nationality.image))
        //    {
        //        HelpClass.clearImg(btn_image);
        //    }
        //    else
        //    {
        //        byte[] imageBuffer = await nationality.downloadImage(nationality.image); // read this as BLOB from your DB

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
        //            string tmpPath = System.IO.Path.Combine(dir, Global.TMPNationalitiesFolder);
        //            tmpPath = System.IO.Path.Combine(tmpPath, nationality.image);
        //            openFileDialog.FileName = tmpPath;
        //        }
        //        else
        //            HelpClass.clearImg(btn_image);
        //    }
        //}

        private async Task getImg()
        {

            //try
            //{
            //    if (string.IsNullOrEmpty(nationality.image))
            //    {
            //        HelpClass.clearImg(btn_image);
            //    }
            //    else
            //    {
                     
            //        var bitmapImage = new BitmapImage();
                    

            //        string dir = Directory.GetCurrentDirectory();
            //        string tmpPath = System.IO.Path.Combine(dir, Global.TMPNationalitiesFolder);
            //        tmpPath = System.IO.Path.Combine(tmpPath, nationality.image);
            //        byte[] imageBuffer = System.IO.File.ReadAllBytes(tmpPath);
            //        if (imageBuffer != null)
            //        {
            //            using (var memoryStream = new MemoryStream(imageBuffer))
            //            {
            //                bitmapImage.BeginInit();
            //                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            //                bitmapImage.StreamSource = memoryStream;
            //                bitmapImage.EndInit();
            //            }
            //        }

                     
            //        btn_image.Background = new ImageBrush(bitmapImage);
            //        //   openFileDialog.FileName = tmpPath;


            //        //}
            //        //    else
            //        //        HelpClass.clearImg(img_customer);
            //    }
            //}
            //catch (Exception ex) { }

        }
        #endregion

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
            //if (isArabic)
            //{
                addpath = @"\Reports\SectionData\Ar\ArNationalities.rdlc";

            //}
            //else
            //{
            //    addpath = @"\Reports\SectionData\En\EnAgents.rdlc";
            //}
            //D:\myproj\posproject3\DataEntryApp\DataEntryApp\Reports\statisticReports\En\EnBook.rdlc
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
            //     subTitle = clsReports.ReportTabTitle(firstTitle, secondTitle);
            //  Title = MainWindow.resourcemanagerreport.GetString("trAccountantReport");

            //clsReports.NationalitiesReport(NationalitiesQuery, rep, reppath, paramarr);
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

        private void Btn_pieChart_Click(object sender, RoutedEventArgs e)
        {

        }

 
    }
}
