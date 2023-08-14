using netoaster;
using DataEntryApp.Classes;
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
using System.Windows.Shapes;
using System.Configuration;

namespace DataEntryApp.View.windows 
{
    /// <summary>
    /// Interaction logic for wd_returnInvoice.xaml
    /// </summary>
    public partial class wd_printers : Window
    {
      
      
        public wd_printers()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
             //   HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        List<string> printersList = new List<string>();
        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
               // HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender != null)
                    HelpClass.StartAwait(grid_main);

             //   string barcode = cb_repname.Text;

                  Properties.Settings.Default.reportPrinter=cb_repname.Text;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("reportPrinter");
                config.AppSettings.Settings.Add("reportPrinter", cb_repname.Text);

                config.Save(ConfigurationSaveMode.Modified);

                // Force a reload of a changed section.
                ConfigurationManager.RefreshSection("appSettings");
                
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopSave"), animation: ToasterAnimation.FadeIn);
                await Task.Delay(1000);
                this.Close();
                if (sender != null)
                    HelpClass.EndAwait(grid_main);

            }
            catch (Exception ex)
            {
                if (sender != null)
                    HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
               // HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        public void fillcb_repname()
        {
            printersList = HelpClass.getsystemPrinters();
            cb_repname.ItemsSource = printersList;
            //cb_repname.DisplayMemberPath = "name";
            //cb_repname.SelectedValuePath = "name";
            string n= HelpClass.getfromConfig("reportPrinter");
            cb_repname.SelectedValue = n;
        }

        void windowFlowDirection()
        {
           string lang = "ar";
            #region
            if (lang.Equals("en"))
            {
           //    MainWindow. resourcemanager = new ResourceManager("DataEntryApp.en_file", Assembly.GetExecutingAssembly());
                grid_mainGrid.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
             //   MainWindow.resourcemanager = new ResourceManager("DataEntryApp.ar_file", Assembly.GetExecutingAssembly());
                grid_mainGrid.FlowDirection = FlowDirection.RightToLeft;
            }
            #endregion
        }
        private  void Window_Loaded(object sender, RoutedEventArgs e)
        {
       
            try
            {
                cb_repname.Focus();
                if (sender != null)
                    HelpClass.StartAwait(grid_main);
                #region translate
                //if (AppSettings.lang.Equals("en"))
                //{
                //    MainWindow.resourcemanager = new ResourceManager("POS.en_file", Assembly.GetExecutingAssembly());
                //}
                //else
                //{
                //    MainWindow.resourcemanager = new ResourceManager("POS.ar_file", Assembly.GetExecutingAssembly());
                //}
                windowFlowDirection();
                fillcb_repname();
                translate();
                #endregion

               
              
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
     
        private void translate()
        {
             
            txt_title.Text = MainWindow.resourcemanager.GetString("printername");
            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_repname, MainWindow.resourcemanager.GetString("printername"));
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
