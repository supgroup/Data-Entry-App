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





                Btn_general_Click(btn_general, null);

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
            btn_general.Content = MainWindow.resourcemanager.GetString("trGeneral");
            btn_reportsSettings.Content = MainWindow.resourcemanager.GetString("trPrint");
            btn_emails.Content = MainWindow.resourcemanager.GetString("trEmail");
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
    }
}
