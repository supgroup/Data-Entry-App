using DataEntryApp;
using DataEntryApp.ApiClasses;
using MaterialDesignThemes.Wpf;
using netoaster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using Tulpep.NotificationWindow;
using System.Configuration;
namespace DataEntryApp.Classes
{
    class HelpClass
    {
       static public BrushConverter brushConverter = new BrushConverter();
        public static ImageBrush imageBrush = new ImageBrush();

        static Users userModel = new Users();
        //static Customers customerModel = new Customers();
        //static Packages packageModel = new Packages();

        public static async Task<string> generateRandomString(int length, string type, string _class, int id)
        {
            Random random = new Random(); string returnStr = "";
            const string chars = "abcdefghijklmnopqrstuvwxyz";
           
            string str  = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            if (!await isCodeExist(str, type, _class, id))
                returnStr = str;
            else
                await generateRandomString(length, type, _class, id);

            return returnStr;
        }

        public static async Task<bool> isCodeExist(string randomNum, string type, string _class, int id)
        {
            bool iscodeExist = false;
            try
            {
                if (_class.Equals("Users"))
                {
                    List<Users> users = await userModel.GetAll();
                    
                    if (users.Any(u => u.code == randomNum && u.userId != id && u.type == type))
                        iscodeExist = true;
                    else
                        iscodeExist = false;
                }
                /*
                else if (_class.Equals("Customers"))
                {
                    List<Customers> customers = await customerModel.GetAll();

                    if (customers.Any(b => b.custCode == randomNum && b.custId != id))
                        iscodeExist = true;
                    else
                        iscodeExist = false;
                }
                else if (_class.Equals("Packages"))
                {
                    List<Packages> packages = await packageModel.GetAll();

                    if (packages.Any(b => b.packageCode == randomNum && b.packageId != id))
                        iscodeExist = true;
                    else
                        iscodeExist = false;
                }
                */

            }
            catch { }
            return iscodeExist;
        }

        public static void SetError(Control c, Path p_error, ToolTip tt_error, string tr)
        {
            p_error.Visibility = Visibility.Visible;
            tt_error.Content = MainWindow.resourcemanager.GetString(tr);
            c.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
        }

        public static bool validateEmpty(string str, Path p_error)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(str))
            {
                p_error.Visibility = Visibility.Visible;
                #region Tooltip
                ToolTip toolTip = new ToolTip();
                toolTip.Content = MainWindow.resourcemanager.GetString("trIsRequired");
                toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                p_error.ToolTip = toolTip;
                #endregion
                isValid = false;
            }
            else
            {
                p_error.Visibility = Visibility.Collapsed;
            }
            return isValid;
        }

        public static void SetValidate(Path p_error, string tr)
        {
            #region Tooltip error
            p_error.Visibility = Visibility.Visible;
            ToolTip toolTip_userName = new ToolTip();
            toolTip_userName.Content = MainWindow.resourcemanager.GetString(tr);
            toolTip_userName.Style = Application.Current.Resources["ToolTipError"] as Style;
            p_error.ToolTip = toolTip_userName;
            #endregion
        }
        public static void clearValidate( Path p_error)
        {
            p_error.Visibility = Visibility.Collapsed;
        }
        #region validateEmpty 
        /*
        public static bool validateEmptyTextBox(TextBox tb, Path p_error)
        {
            bool isValid = true;
            if (tb.Text.Equals(""))
            {
                p_error.Visibility = Visibility.Visible;
                #region Tooltip
                ToolTip toolTip = new ToolTip();
                toolTip.Content = MainWindow.resourcemanager.GetString("trIsRequired");
                toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                p_error.ToolTip = toolTip;
                #endregion
                isValid = false;
            }
            else
            {
                p_error.Visibility = Visibility.Collapsed;
            }
            return isValid;
        }
        public static bool validateEmptyComboBox(ComboBox cb, Path p_error, ToolTip tt_error, string tr)
        {
            bool isValid = true;

            if (cb.SelectedIndex == -1)
            {
                p_error.Visibility = Visibility.Visible;
                tt_error.Content = MainWindow.resourcemanager.GetString(tr);
                cb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
                isValid = false;
            }
            else
            {
                cb.Background = (Brush)brushConverter.ConvertFrom("#f8f8f8");
                p_error.Visibility = Visibility.Collapsed;

            }
            return isValid;
        }
        public static bool validateEmptyPassword(PasswordBox pb, Path p_error, ToolTip tt_error, string tr)
        {
            bool isValid = true;

            if (pb.Password.Equals(""))
            {
                HelpClass.showPasswordValidate(pb, p_error, tt_error, "trEmptyPasswordToolTip");
                p_error.Visibility = Visibility.Visible;
                isValid = false;
            }
            else
            {
                HelpClass.clearPasswordValidate(pb, p_error);
                p_error.Visibility = Visibility.Collapsed;
            }
            return isValid;
        }
        public static bool validateEmail(TextBox tb, Path p_error, ToolTip tt_error)
        {
            bool isValid = true;
            if (!tb.Text.Equals(""))
            {
                if (!ValidatorExtensions.IsValid(tb.Text))
                {
                    p_error.Visibility = Visibility.Visible;
                    tt_error.Content = MainWindow.resourcemanager.GetString("trErrorEmailToolTip");
                    tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
                isValid = false;
                }
                else
                {
                    p_error.Visibility = Visibility.Collapsed;
                    tb.Background = (Brush)brushConverter.ConvertFrom("#f8f8f8");
                isValid = true;
                }
            }
            return isValid;
        }
        public static void validateEmptyDatePicker(DatePicker dp, Path p_error, ToolTip tt_error, string tr)
        {
            TextBox tb = (TextBox)dp.Template.FindName("PART_TextBox", dp);
            if (tb.Text.Trim().Equals(""))
            {
                p_error.Visibility = Visibility.Visible;
                tt_error.Content = MainWindow.resourcemanager.GetString(tr);
                tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
            }
            else
            {
                tb.Background = (Brush)brushConverter.ConvertFrom("#f8f8f8");
                p_error.Visibility = Visibility.Collapsed;
            }
        }
        public static void validateSmalThanDateNowDatePicker(DatePicker dp, Path p_error, ToolTip tt_error, string tr)
        {
            TextBox tb = (TextBox)dp.Template.FindName("PART_TextBox", dp);
            if (dp.SelectedDate < DateTime.Now)
            {
                p_error.Visibility = Visibility.Visible;
                tt_error.Content = MainWindow.resourcemanager.GetString(tr);
                tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
            }
            else
            {
                tb.Background = (Brush)brushConverter.ConvertFrom("#f8f8f8");
                p_error.Visibility = Visibility.Collapsed;
            }
        }
        */
        #endregion
        #region clearValidate
        
        public static void clearTextBoxValidate(TextBox tb , Path p_error)
        {
            tb.Background = (Brush)brushConverter.ConvertFrom("#f8f8f8");
            p_error.Visibility = Visibility.Collapsed;
        }
        
        public static void clearPasswordValidate(PasswordBox pb, Path p_error)
        {
            pb.Background = (Brush)brushConverter.ConvertFrom("#f8f8f8");
            p_error.Visibility = Visibility.Collapsed;
        }
        /*
        public static void clearComboBoxValidate(ComboBox cb, Path p_error)
        {
            cb.Background = (Brush)brushConverter.ConvertFrom("#f8f8f8");
            p_error.Visibility = Visibility.Collapsed;
        }
         */
        #endregion
        #region validate
        public static bool validate(List<string> requiredControlList ,UserControl userControl)
        {
            bool isValid = true;
            try
            {
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(userControl).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmpty(comboBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    PasswordBox passwordBox = FindControls.FindVisualChildren<PasswordBox>(userControl).Where(x => x.Name == "pb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (passwordBox != null && path != null)
                        if (!HelpClass.validateEmpty(passwordBox.Password, path))
                            isValid = false;
                }
                #region Email
                IsValidEmail(userControl);
                #endregion


            }
            catch { }
            return isValid;

        }
        public static bool validate(List<string> requiredControlList, Window userControl)
        {
            bool isValid = true;
            try
            {
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(userControl).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmpty(comboBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    PasswordBox passwordBox = FindControls.FindVisualChildren<PasswordBox>(userControl).Where(x => x.Name == "pb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (passwordBox != null && path != null)
                        if (!HelpClass.validateEmpty(passwordBox.Password, path))
                            isValid = false;
                }
                #region Email
                IsValidEmail(userControl);
                #endregion


            }
            catch { }
            return isValid;

        }
        public static bool validateWindow(List<string> requiredControlList, Window window)
        {
            bool isValid = true;
            try
            {
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(window).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(window).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(window).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(window).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmpty(comboBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    PasswordBox passwordBox = FindControls.FindVisualChildren<PasswordBox>(window).Where(x => x.Name == "pb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(window).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (passwordBox != null && path != null)
                        if (!HelpClass.validateEmpty(passwordBox.Password, path))
                            isValid = false;
                }
               
            }
            catch { }
            return isValid;

        }
        public static bool IsValidEmail(UserControl userControl)
        {//for email
            bool isValidEmail = true;
            TextBox textBoxEmail = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_email")
                    .FirstOrDefault();
            Path pathEmail = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_email")
                    .FirstOrDefault();
            if (textBoxEmail != null && pathEmail != null)
            {
                if (textBoxEmail.Text.Equals(""))
                    return isValidEmail;
                else
                {
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                          RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    isValidEmail = regex.IsMatch(textBoxEmail.Text);

                    if (!isValidEmail)
                    {
                        pathEmail.Visibility = Visibility.Visible;
                        #region Tooltip
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = MainWindow.resourcemanager.GetString("trErrorEmailToolTip");
                        toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                        pathEmail.ToolTip = toolTip;
                        #endregion
                        isValidEmail = false;
                    }
                    else
                    {
                        pathEmail.Visibility = Visibility.Collapsed;
                    }
                }
            }
            return isValidEmail;

        }
        public static bool IsValidEmail(Window userControl)
        {//for email
            bool isValidEmail = true;
            TextBox textBoxEmail = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_email")
                    .FirstOrDefault();
            Path pathEmail = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_email")
                    .FirstOrDefault();
            if (textBoxEmail != null && pathEmail != null)
            {
                if (textBoxEmail.Text.Equals(""))
                    return isValidEmail;
                else
                {
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                          RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    isValidEmail = regex.IsMatch(textBoxEmail.Text);

                    if (!isValidEmail)
                    {
                        pathEmail.Visibility = Visibility.Visible;
                        #region Tooltip
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = MainWindow.resourcemanager.GetString("trErrorEmailToolTip");
                        toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                        pathEmail.ToolTip = toolTip;
                        #endregion
                        isValidEmail = false;
                    }
                    else
                    {
                        pathEmail.Visibility = Visibility.Collapsed;
                    }
                }
            }
            return isValidEmail;

        }
        public static bool IsValidEmailWindow(Window window)
        {//for email
            bool isValidEmail = true;
            TextBox textBoxEmail = FindControls.FindVisualChildren<TextBox>(window).Where(x => x.Name == "tb_email")
                    .FirstOrDefault();
            Path pathEmail = FindControls.FindVisualChildren<Path>(window).Where(x => x.Name == "p_error_email")
                    .FirstOrDefault();
            if (textBoxEmail != null && pathEmail != null)
            {
                if (textBoxEmail.Text.Equals(""))
                    return isValidEmail;
                else
                {
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                          RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    isValidEmail = regex.IsMatch(textBoxEmail.Text);

                    if (!isValidEmail)
                    {
                        pathEmail.Visibility = Visibility.Visible;
                        #region Tooltip
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = MainWindow.resourcemanager.GetString("trErrorEmailToolTip");
                        toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                        pathEmail.ToolTip = toolTip;
                        #endregion
                        isValidEmail = false;
                    }
                    else
                    {
                        pathEmail.Visibility = Visibility.Collapsed;
                    }
                }
            }
            return isValidEmail;

        }
        public static void clearValidate(List<string> requiredControlList, UserControl userControl)
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }
        public static void clearValidate(List<string> requiredControlList, Window userControl)
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }
        public static void clearValidateWindow(List<string> requiredControlList, Window window)
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    Path path = FindControls.FindVisualChildren<Path>(window).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }
        #endregion
        public static void showTextBoxValidate(TextBox tb, Path p_error, ToolTip tt_error, string tr)
        {
            p_error.Visibility = Visibility.Visible;
            tt_error.Content = MainWindow.resourcemanager.GetString(tr);
            tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
        }
        public static void showPasswordValidate(PasswordBox tb, Path p_error, ToolTip tt_error, string tr)
        {
            p_error.Visibility = Visibility.Visible;
            tt_error.Content = MainWindow.resourcemanager.GetString(tr);
            tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
        }
        public static void showComboBoxValidate(ComboBox cb, Path p_error, ToolTip tt_error, string tr)
        {
            p_error.Visibility = Visibility.Visible;
            tt_error.Content = MainWindow.resourcemanager.GetString(tr);
            cb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
        }

        public static void showDatePickerValidate(DatePicker dp, Path p_error, ToolTip tt_error, string tr)
        {
            TextBox tb = (TextBox)dp.Template.FindName("PART_TextBox", dp);

            p_error.Visibility = Visibility.Visible;
            tt_error.Content = MainWindow.resourcemanager.GetString(tr);
            tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
        }

        public static void showTimePickerValidate(TimePicker tp, Path p_error, ToolTip tt_error, string tr)
        {
            TextBox tb = (TextBox)tp.Template.FindName("PART_TextBox", tp);

            p_error.Visibility = Visibility.Visible;
            tt_error.Content = MainWindow.resourcemanager.GetString(tr);
            tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
        }

       

     

        public static void validateDuplicateCode(TextBox tb, Path p_error, ToolTip tt_error ,string tr)
        {
            p_error.Visibility = Visibility.Visible;
            tt_error.Content = MainWindow.resourcemanager.GetString(tr);
            tb.Background = (Brush)brushConverter.ConvertFrom("#15FF0000");
        }

        public static void getMobile(string _mobile , ComboBox _area , TextBox _tb)
        {//mobile
            if ((_mobile != null))
            {
                string area = _mobile;
                string[] pharr = area.Split('-');
                int j = 0;
                string phone = "";

                foreach (string strpart in pharr)
                {
                    if (j == 0)
                    {
                        area = strpart;
                    }
                    else
                    {
                        phone = phone + strpart;
                    }
                    j++;
                }

                _area.Text = area;

                _tb.Text = phone.ToString();
            }
            else
            {
                _area.SelectedIndex = -1;
                _tb.Clear();
            }
        }

        public static void getPhone(string _phone , ComboBox _area , ComboBox _areaLocal , TextBox _tb)
        {//phone
            if ((_phone != null))
            {
                string area = _phone;
                string[] pharr = area.Split('-');
                int j = 0;
                string phone = "";
                string areaLocal = "";
                foreach (string strpart in pharr)
                {
                    if (j == 0)
                        area = strpart;
                    else if (j == 1)
                        areaLocal = strpart;
                    else
                        phone = phone + strpart;
                    j++;
                }

                _area.Text = area;
                _areaLocal.Text = areaLocal;
                _tb.Text = phone.ToString();
            }
            else
            {
                _area.SelectedIndex = -1;
                _areaLocal.SelectedIndex = -1;
                _tb.Clear();
            }
        }
         

        public static void clearImg(Button img)
        {
            Uri resourceUri = new Uri("pic/no-image-icon-125x125.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            imageBrush.ImageSource = temp;
            img.Background = imageBrush;
        }
        public static decimal calcPercentage(decimal value, decimal percentage)
        {
            decimal percentageVal = (value * percentage) / 100;

            return percentageVal;
        }
        public static void defaultDatePickerStyle(DatePicker dp)
        {
            dp.Loaded += delegate
            {
                var textBox1 = (TextBox)dp.Template.FindName("PART_TextBox", dp);
                if (textBox1 != null)
                {
                    textBox1.Background = dp.Background;
                    textBox1.BorderThickness = dp.BorderThickness;
                }
            };
        }
         
        static public void searchInComboBox(ComboBox cbm)
        {
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cbm.Items);
            itemsViewOriginal.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(cbm.Text)) return true;
                else
                {
                    if (((string)o).Contains(cbm.Text)) return true;
                    else return false;
                }
            });
            itemsViewOriginal.Refresh();
        }

        static public bool isAdminPermision()
        {
            //if (MainWindow.userLogin.userId == 1 || MainWindow.userLogin.userId == 2)
            if (MainWindow.userLogin.isAdmin == true)
                return true;
            return false;
        }
        /// <summary>
        /// لمنع  الصفر بالبداية
        /// </summary>
        /// <param name="txb"></param>
        static public void InputJustNumber(ref TextBox txb)
        {
            if (txb.Text.Count() == 2 && txb.Text == "00")
            {
                string myString = txb.Text;
                myString = Regex.Replace(myString, "00", "0");
                txb.Text = myString;
                txb.Select(txb.Text.Length, 0);
                txb.Focus();
            }
        }
        
        static async public void ExceptionMessage(Exception ex ,object window)
        {
            try
            {
            //Message
            if (ex.HResult == -2146233088)
                Toaster.ShowError(window as Window, message: MainWindow.resourcemanager.GetString("trNoInternetConnection"), animation: ToasterAnimation.FadeIn);
            else
                Toaster.ShowError(window as Window, message: ex.HResult + " || " + ex.Message, animation: ToasterAnimation.FadeIn);
                ErrorClass errorClass = new ErrorClass();
                errorClass.num = ex.HResult.ToString();
                errorClass.msg = ex.Message;
                errorClass.stackTrace = ex.StackTrace;
                errorClass.targetSite = ex.TargetSite.ToString();
                errorClass.createUserId = MainWindow.userLogin.userId;
                await errorClass.Save(errorClass);
            }
            catch
            {

            }
        }

        static public void StartAwait(Grid grid, string progressRingName = "")
        {
            grid.IsEnabled = false;
            grid.Opacity = 0.6;
            MahApps.Metro.Controls.ProgressRing progressRing = new MahApps.Metro.Controls.ProgressRing();
            progressRing.Name = "prg_awaitRing" + progressRingName;
            progressRing.Foreground = App.Current.Resources["MainColor"] as Brush;
            progressRing.IsActive = true;
            Grid.SetRowSpan(progressRing, 10);
            Grid.SetColumnSpan(progressRing, 10);
            grid.Children.Add(progressRing);
        }
        static public void EndAwait(Grid grid, string progressRingName = "")
        {
            MahApps.Metro.Controls.ProgressRing progressRing = FindControls.FindVisualChildren<MahApps.Metro.Controls.ProgressRing>(grid)
                .Where(x => x.Name == "prg_awaitRing" + progressRingName).FirstOrDefault();
            grid.Children.Remove(progressRing);

            var progressRingList = FindControls.FindVisualChildren<MahApps.Metro.Controls.ProgressRing>(grid)
                 .Where(x => x.Name == "prg_awaitRing" + progressRingName);
            if (progressRingList.Count() == 0)
            {
                grid.IsEnabled = true;
                grid.Opacity = 1;
            }

        }
        /*
        public static string getPeriod(CountryPackageDate cpd)
        {
            string period = "";

            if (!cpd.islimitDate)
                period = MainWindow.resourcemanager.GetString("trUnLimited");
            else
            {
                switch (cpd.monthCount)
                {
                    case 1 : period = MainWindow.resourcemanager.GetString("trOneMonth"); break;
                    case 3 : period = MainWindow.resourcemanager.GetString("trThreeMonth"); break;
                    case 6 : period = MainWindow.resourcemanager.GetString("trSixMonth"); break;
                    case 12: period = MainWindow.resourcemanager.GetString("trTwelveMonth"); break;
                }
            }
            return period;
        }
        */
        public static string translate(string str)
        {
            string _str = "";


            #region  mainWindow
            if (str == "home")
                _str = MainWindow.resourcemanager.GetString("trHome");
            else if (str == "catalog")
                _str = MainWindow.resourcemanager.GetString("trCatalog");
                else if (str == "storage")
                _str = MainWindow.resourcemanager.GetString("trStore");
                else if (str == "purchase")
                _str = MainWindow.resourcemanager.GetString("trPurchases");
                else if (str == "sales")
                _str = MainWindow.resourcemanager.GetString("trSales");
                else if (str == "accounts")
                _str = MainWindow.resourcemanager.GetString("trAccounting");
                else if (str == "reports")
                _str = MainWindow.resourcemanager.GetString("trReports");
                else if (str == "HelpClass")
                _str = MainWindow.resourcemanager.GetString("trHelpClass");
                else if (str == "settings")
                _str = MainWindow.resourcemanager.GetString("trSettings");
                #endregion
           
            #region  storage
            if (str == "locations")
            _str = MainWindow.resourcemanager.GetString("trLocation");
            else if (str == "section")
            _str = MainWindow.resourcemanager.GetString("trSection");
            else if (str == "reciptOfInvoice")
            _str = MainWindow.resourcemanager.GetString("trInvoice");
            else if (str == "itemsStorage")
            _str = MainWindow.resourcemanager.GetString("trStorage");
            else if (str == "importExport")
            _str = MainWindow.resourcemanager.GetString("trMovements");
            else if (str == "itemsDestroy")
            _str = MainWindow.resourcemanager.GetString("trDestructive");
            else if (str == "shorstre")
            _str = MainWindow.resourcemanager.GetString("trShorstre");
            else if (str == "inventory")
            _str = MainWindow.resourcemanager.GetString("trStocktaking");
            else if (str == "storageStatistic")
            _str = MainWindow.resourcemanager.GetString("trStatistic");
            #endregion

            #region  Account
            else if (str == "posAccounting")
            _str = MainWindow.resourcemanager.GetString("trPOS");
            else if (str == "payments")
            _str = MainWindow.resourcemanager.GetString("trPayments");
            else if (str == "received")
            _str = MainWindow.resourcemanager.GetString("trReceived");
            else if (str == "bonds")
            _str = MainWindow.resourcemanager.GetString("trBonds");
            else if (str == "banksAccounting")
            _str = MainWindow.resourcemanager.GetString("trBanks");
            else if (str == "ordersAccounting")
            _str = MainWindow.resourcemanager.GetString("trOrders");
            else if (str == "subscriptions")
            _str = MainWindow.resourcemanager.GetString("trSubscriptions");
            else if (str == "accountsStatistic")
            _str = MainWindow.resourcemanager.GetString("trStatistic");
            #endregion

            #region  catalog
            else if (str == "categories")
            _str = MainWindow.resourcemanager.GetString("trCategories");
            else if (str == "item")
            _str = MainWindow.resourcemanager.GetString("trItems");
            else if (str == "properties")
            _str = MainWindow.resourcemanager.GetString("trProperties");
            else if (str == "units")
            _str = MainWindow.resourcemanager.GetString("trUnits");
            else if (str == "storageCost")
            _str = MainWindow.resourcemanager.GetString("trStorageCost");
            #endregion

            #region  purchase
            if (str == "payInvoice")
            _str = MainWindow.resourcemanager.GetString("trInvoice");
            else if (str == "purchaseOrder")
            _str = MainWindow.resourcemanager.GetString("trOrders");
            else if (str == "purchaseStatistic")
            _str = MainWindow.resourcemanager.GetString("trStatistic");
            #endregion

            #region  sales
            if (str == "reciptInvoice")
            _str = MainWindow.resourcemanager.GetString("trInvoice");
            else if (str == "coupon")
            _str = MainWindow.resourcemanager.GetString("trCoupon");
            else if (str == "offer")
            _str = MainWindow.resourcemanager.GetString("trOffer");
            else if (str == "package")
            _str = MainWindow.resourcemanager.GetString("trPackage");
            else if (str == "quotation")
            _str = MainWindow.resourcemanager.GetString("trQuotations");
            else if (str == "salesOrders")
            _str = MainWindow.resourcemanager.GetString("trOrders");
            else if (str == "medals")
            _str = MainWindow.resourcemanager.GetString("trMedals");
            else if (str == "membership")
            _str = MainWindow.resourcemanager.GetString("trMembership");
            else if (str == "salesStatistic")
            _str = MainWindow.resourcemanager.GetString("trStatistic");
            #endregion

            #region  HelpClass
            if (str == "suppliers")
            _str = MainWindow.resourcemanager.GetString("trSuppliers");
            else if (str == "customers")
            _str = MainWindow.resourcemanager.GetString("trCustomers");
            else if (str == "users")
            _str = MainWindow.resourcemanager.GetString("trUsers");
            else if (str == "branches")
            _str = MainWindow.resourcemanager.GetString("trBranches");
            else if (str == "stores")
            _str = MainWindow.resourcemanager.GetString("trStores");
            else if (str == "pos")
            _str = MainWindow.resourcemanager.GetString("trPOS");
            else if (str == "banks")
            _str = MainWindow.resourcemanager.GetString("trBanks");
            else if (str == "cards")
            _str = MainWindow.resourcemanager.GetString("trCard");
            else if (str == "shippingCompany")
            _str = MainWindow.resourcemanager.GetString("trShippingCompanies");
            #endregion

            #region  settings
            if (str == "general")
            _str = MainWindow.resourcemanager.GetString("trGeneral");
            else if (str == "reportsSettings")
            _str = MainWindow.resourcemanager.GetString("trReports");
            else if (str == "permissions")
            _str = MainWindow.resourcemanager.GetString("trPermission");
            else if (str == "emailsSetting")
            _str = MainWindow.resourcemanager.GetString("trEmail");
            else if (str == "emailTemplates")
            _str = MainWindow.resourcemanager.GetString("trEmailTemplates");
            #endregion
           

            return _str;
            
        }

        static public async Task<string> getCountry(int ID)
        {
            string country = "";
            Country countryModel = new Country();
            List<Country> countries =  await countryModel.GetAll();
            country = countries.Where(c => c.countryId == ID).Select(c => new { country = c.name}).FirstOrDefault().country;
            return country;
        }

        static public string setDateFormat(DateTime value)
        {
            DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date;
            if (value is DateTime)
                date = (DateTime)value;
            else return value.ToString();

            switch (MainWindow.dateFormat)
            {
                case "ShortDatePattern":
                    return date.ToString(@"dd/MM/yyyy");
                case "LongDatePattern":
                    return date.ToString(@"dddd, MMMM d, yyyy");
                case "MonthDayPattern":
                    return date.ToString(@"MMMM dd");
                case "YearMonthPattern":
                    return date.ToString(@"MMMM yyyy");
                default:
                    return date.ToString(@"dd/MM/yyyy");
            }
        }

        public static string DecTostring(decimal? dec)
        {
            string sdc = "0";
            if (dec == null)
            {

            }
            else
            {
                decimal dc = decimal.Parse(dec.ToString());

                switch (MainWindow.accuracy)
                {
                    case "0":
                        sdc = string.Format("{0:F0}", dc);
                        break;
                    case "1":
                        sdc = string.Format("{0:F1}", dc);
                        break;
                    case "2":
                        sdc = string.Format("{0:F2}", dc);
                        break;
                    case "3":
                        sdc = string.Format("{0:F3}", dc);
                        break;
                    default:
                        sdc = string.Format("{0:F1}", dc);
                        break;
                }

            }


            return sdc;
        }


        public static string AddNewConnectionString(string servername, string dbName = "bookdb")
        {
            string messg = "ok";

            /* This code provides access to configuration files using OpenMappedExeConfiguration,method. You can use the OpenExeConfiguration method instead. For further informatons,consult the MSDN, it gives you more inforamtions about config files access methods*/

            ExeConfigurationFileMap oConfigFile = new ExeConfigurationFileMap();

            oConfigFile.ExeConfigFilename = System.IO.Directory.GetCurrentDirectory() + "\\DataEntryApp.exe.config";

            Configuration oConfiguration = ConfigurationManager.OpenMappedExeConfiguration(oConfigFile, ConfigurationUserLevel.None);

            //Define a connection string settings incuding the name and the connection string
            //string server = ".\\SQLEXPRESS";
            ////string dbName = "bookdb";
            string connstr = "metadata = res://*/bookModel.csdl|res://*/bookModel.ssdl|res://*/bookModel.msl;provider=System.Data.SqlClient;provider connection string='data source=[[servername]];initial catalog=[[dbname]];integrated security=True;MultipleActiveResultSets=True;App=EntityFramework'";
            connstr = connstr.Replace("[[servername]]", servername);
            connstr = connstr.Replace("[[dbname]]", dbName);
            ConnectionStringSettings oConnectionSettings = new ConnectionStringSettings("dedbEntities", connstr, "System.Data.EntityClient");

            //Adding the connection string to the oConfiguration object

            oConfiguration.ConnectionStrings.ConnectionStrings.Remove(oConnectionSettings);
            oConfiguration.ConnectionStrings.ConnectionStrings.Add(oConnectionSettings);

            //Save the new connection string settings

            oConfiguration.Save(ConfigurationSaveMode.Full);

            MessageBox.Show(string.Format("Connection {0} is added", oConnectionSettings.Name));

            //Restart the application to be sure that connection is restored in the config file

            // Application.st();
            return messg;
        }
        //public static bool checkConnectionString()
        //{
        //    using (dedbEntities entity = new dedbEntities())
        //    {
        //        if (entity.Database.Exists())
        //        {
        //            //MessageBox.Show("ok");
        //            //string db = entity.Database.Connection.Database;
        //            //string ds = entity.Database.Connection.DataSource;
        //            return true;
        //        }
        //        else
        //        {
        //            // AddNewConnectionString(".\\SQLEXPRESS", "bookdb");
        //        //    MessageBox.Show("not exist");
        //            return false;
                   
        //        }
        //    }
        //}
    }

}
