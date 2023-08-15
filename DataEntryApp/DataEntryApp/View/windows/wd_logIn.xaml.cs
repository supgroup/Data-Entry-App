using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection; 
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataEntryApp.ApiClasses;
using DataEntryApp.Classes;
 
namespace DataEntryApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_logIn.xaml
    /// </summary>
    public partial class wd_logIn : Window
    {
        BrushConverter brushConverter = new BrushConverter();
        //public static string lang = "en";
        bool logInProcessing = false;
        Users userModel = new Users();
        Users user = new Users();
        public wd_logIn()
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                bdrLogIn.RenderTransform = Animations.borderAnimation(-100, bdrLogIn, true);

                #region properties
                if (Properties.Settings.Default.userName != string.Empty)
                {
                    txtUserName.Text = Properties.Settings.Default.userName;
                    txtPassword.Password = Properties.Settings.Default.password;
                   // MainWindow.lang = "en";
                    MainWindow.lang = Properties.Settings.Default.Lang;
                    //menuIsOpen = Properties.Settings.Default.menuIsOpen;
                    cbxRemmemberMe.IsChecked = true;

                }
                else
                {
                    txtUserName.Clear();
                    txtPassword.Clear();
                    MainWindow.lang = "ar";
                    //menuIsOpen = "close";
                    cbxRemmemberMe.IsChecked = false;
                }
                #endregion

                #region translate
                MainWindow.lang = "ar";
                if (MainWindow.lang.Equals("en"))
                {
                    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.en_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                    //bdr_imageAr.Visibility = Visibility.Hidden;
                    //bdr_image.Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.ar_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                    //bdr_imageAr.Visibility = Visibility.Visible;
                    //bdr_image.Visibility = Visibility.Hidden;
                }
                translate();
                #endregion

                #region Arabic Number
                CultureInfo ci = CultureInfo.CreateSpecificCulture(Thread.CurrentThread.CurrentCulture.Name);
                ci.NumberFormat.DigitSubstitution = DigitShapes.Context;
                Thread.CurrentThread.CurrentCulture = ci;
                #endregion

                HelpClass.EndAwait(grid_main);

                if (txtUserName.Text.Equals(""))
                    Keyboard.Focus(txtUserName);
                else if (txtPassword.Password.Equals(""))
                    Keyboard.Focus(txtPassword);
              
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    btnLogIn_Click(btnLogIn, null);
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (Exception)
            { }
        }

     

        private async void btnLogIn_Click(object sender, RoutedEventArgs e)
        {//log in
            try
            {
              
             // AddNewConnectionString();
               
                //using (dedbEntities entity = new dedbEntities())
                //{
                //    if (entity.Database.Exists())
                //    {
                //        MessageBox.Show("ok");
                //        //string db = entity.Database.Connection.Database;
                //        //string ds = entity.Database.Connection.DataSource;
                //    }
                //    else
                //    {
                //        AddNewConnectionString(".\\SQLEXPRESS", "bookdb");
                  
                //        MessageBox.Show("not exist");
                //    }
                //}
                if (!logInProcessing)
                {
                    logInProcessing = true;
                    
                    HelpClass.StartAwait(grid_main);

                    HelpClass.clearValidate(p_errorUserName);
                    HelpClass.clearValidate(p_errorPassword);

                    string password = Md5Encription.MD5Hash("Inc-m" + txtPassword.Password);
                    string userName = txtUserName.Text;
                    //user = await userModel.GetByID(2);

                    user = await userModel.Getloginuser(userName, password);

                    if (user.AccountName == null)
                    {
                        //user does not exist
                        HelpClass.SetValidate(p_errorUserName, "trUserNotFound");
                    }
                    else
                    {
                        if (user.userId == 0)
                        {
                            //wrong password
                        HelpClass.SetValidate(p_errorPassword, "trWrongPassword");
                        }
                        else
                        {
                            //correct
                            //send user info to main window
                            MainWindow.userID = user.userId;
                            MainWindow.userLogin = user;

                             

                            //make user online
                            user.isOnline = 1;

                            decimal s = await userModel.Save(user);
                         
                            #region remember me
                            if (cbxRemmemberMe.IsChecked.Value)
                            {
                                Properties.Settings.Default.userName = txtUserName.Text;
                               // Properties.Settings.Default.password = txtPassword.Password;
                                Properties.Settings.Default.Lang = MainWindow.lang;
                                //Properties.Settings.Default.menuIsOpen = menuIsOpen;
                                 Properties.Settings.Default.password ="";
                            }
                            else
                            {
                                Properties.Settings.Default.userName = "";
                                Properties.Settings.Default.password = "";
                                Properties.Settings.Default.Lang = "";
                                //Properties.Settings.Default.menuIsOpen = "";
                            }
                            Properties.Settings.Default.Save();
                            #endregion
                            MainWindow.Region.countryId = 1;

                            MainWindow main = new MainWindow();

                            main.Show();
                            this.Close();
                        }
                    }

                    HelpClass.EndAwait(grid_main);
                    logInProcessing = false;
                }
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                logInProcessing = false;
                HelpClass.ExceptionMessage(ex, this);
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {//close
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

      
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Collect all generations of memory.
            GC.Collect();

        }

        #region validate
        private void validateEmpty(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender.GetType().Name == "TextBox")
                {
                    if (txtUserName.Text.Equals(""))
                        HelpClass.SetValidate(p_errorUserName, "trEmptyUserNameToolTip");
                }
                else if (sender.GetType().Name == "PasswordBox")
                {
                    if (txtPassword.Password.Equals(""))
                        HelpClass.SetValidate(p_errorPassword, "trEmptyPasswordToolTip");
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void validateTextChanged(object sender, TextChangedEventArgs e)
        {
            clearValidate(p_errorUserName);
         }
        private void validatePasswordChanged(object sender, RoutedEventArgs e)
        {
            clearValidate(p_errorPassword);
         }


        private void P_showPassword_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                txtShowPassword.Text = txtPassword.Password;
                txtShowPassword.Visibility = Visibility.Visible;
                txtPassword.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void P_showPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                txtShowPassword.Visibility = Visibility.Collapsed;
                txtPassword.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        #endregion

        #region methods
        private void translate()
        {
            cbxRemmemberMe.Content = MainWindow.resourcemanager.GetString("trRememberMe");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(txtUserName, MainWindow.resourcemanager.GetString("trUserName"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(txtPassword, MainWindow.resourcemanager.GetString("trPassword"));
            txt_logIn.Text = MainWindow.resourcemanager.GetString("trLogIn");
            txt_close.Text = MainWindow.resourcemanager.GetString("trClose");
        }
        private void clearValidate(Path p)
        {
            try
            {
                HelpClass.clearValidate(p);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        #region get language from database
       
        #endregion

        #endregion



    }
}
