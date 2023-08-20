using DataEntryApp.Classes;
 
using System;
using System.Collections.Generic;
using System.Linq;
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
using DataEntryApp.View.windows;
using netoaster;
using System.Configuration;


namespace DataEntryApp.View.settings
{
    /// <summary>
    /// Interaction logic for uc_settings.xaml
    /// </summary>
    public partial class uc_settings : UserControl
    { 
        private static uc_settings _instance;
        public static uc_settings Instance
        {
            get
            {
                //if (_instance == null)
                    _instance = new uc_settings();
                return _instance;
            }
        }
        public uc_settings()
        {
            InitializeComponent();
        }
        public static List<string> menuList;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_mainGrid);
                menuList = new List<string> { "emails" ,"general","reportsSettings"};
                if (HelpClass.isSupportPermision)
                {
                    brd_Uri.Visibility = Visibility.Visible;
                    string uri = Properties.Settings.Default.APIUri;
                    brd_backup.Visibility = Visibility.Visible;
                    if (uri.Length > 5)
                    {
                        tb_Uri.Text = uri.Remove(uri.Length - 5);
                    }
                    else
                    {
                        tb_Uri.Text = "";
                    }
                }
                else
                {
                    brd_Uri.Visibility = Visibility.Collapsed;
                    brd_backup.Visibility = Visibility.Collapsed;
                }
                brd_backup.Visibility = Visibility.Collapsed;

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    grid_mainGrid.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    grid_mainGrid.FlowDirection = FlowDirection.RightToLeft;
                }
                translate();
                #endregion





               // Btn_general_Click(btn_general, null);

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
        public void translate()
        {
            //btn_general.Content = MainWindow.resourcemanager.GetString("trGeneral");
            //btn_reportsSettings.Content = MainWindow.resourcemanager.GetString("trPrint");
            //btn_emails.Content = MainWindow.resourcemanager.GetString("trEmail");
            txt_printSetting.Text   = MainWindow.resourcemanager.GetString("thePrint");
            txt_printcount.Text = MainWindow.resourcemanager.GetString("printCopyCount");
            txt_printSettingHint.Text = MainWindow.resourcemanager.GetString("selectprint");
            txt_printcountHint.Text = MainWindow.resourcemanager.GetString("enterCopyCount");
            //enterCopyCount selectprint
        }
        void colorButtonRefreash(string str)
        {
            foreach (Button button in FindControls.FindVisualChildren<Button>(this))
            {
                if (button.Tag != null)
                {
                    foreach (var item in menuList)
                    {
                        if (item == button.Tag.ToString())
                        {
                            if (item == str)
                                button.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                            else
                                button.Background = Application.Current.Resources["SecondColor"] as SolidColorBrush;

                        }
                    }
                }
            }
        }
        private void Btn_general_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                grid_main.Children.Clear();
                uc_general uc = new uc_general();
                grid_main.Children.Add(uc);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_emails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Button button = sender as Button;
                //colorButtonRefreash(button.Tag.ToString());
                //grid_main.Children.Clear();
                
                //uc_emailGeneral uc = new uc_emailGeneral();
                //grid_main.Children.Add(uc);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_reportsSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                colorButtonRefreash(button.Tag.ToString());
                grid_main.Children.Clear();
                //grid_main.Children.Add(uc_emailGeneral.Instance);
                uc_reportsSettings uc = new uc_reportsSettings();
                grid_main.Children.Add(uc);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

      

        private void Btn_printSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                //if (MainWindow.groupObject.HasPermissionAction(companySettingsPermission, MainWindow.groupObjects, "one") || SectionData.isAdminPermision())
                //{
                Window.GetWindow(this).Opacity = 0.2;
                wd_printers w = new wd_printers();
                //w.windowType = "r";
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                //}
                //else
                //    Toaster.ShowInfo(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_printcount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                //if (MainWindow.groupObject.HasPermissionAction(companySettingsPermission, MainWindow.groupObjects, "one") || SectionData.isAdminPermision())
                //{
                Window.GetWindow(this).Opacity = 0.2;
                wd_CopyCountSetting w = new wd_CopyCountSetting();
                //w.windowType = "r";
                w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
                //}
                //else
                //    Toaster.ShowInfo(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trdontHavePermission"), animation: ToasterAnimation.FadeIn);
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void Btn_uri_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_backup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_saveActivationSite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tb_PreventSpaces(object sender, KeyEventArgs e)
        {

        }

        private void Tb_validateEmptyTextChange(object sender, TextChangedEventArgs e)
        {

        }

        private void Tb_validateEmptyLostFocus(object sender, RoutedEventArgs e)
        {

        }

        private async void Btn_saveUri_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                if(tb_Uri.Text != "")
                {
                    string apiUri = tb_Uri.Text + @"/api/";
                    Properties.Settings.Default.APIUri = apiUri;
                    Properties.Settings.Default.Save();
                    Global.APIUri = apiUri;

                    Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopSave"), animation: ToasterAnimation.FadeIn);
                    await Task.Delay(1000);
                }
                //
               
                //
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
    }
}
