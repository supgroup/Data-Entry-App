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
using System.Text.RegularExpressions;
namespace DataEntryApp.View.windows 
{
    /// <summary>
    /// Interaction logic for wd_returnInvoice.xaml
    /// </summary>
    public partial class wd_CopyCountSetting : Window
    {
      
      
        public wd_CopyCountSetting()
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

               //   Properties.Settings.Default.reportPrinter=cb_repname.Text;
                Properties.Settings.Default.Save();
                Properties.Settings.Default.Reload();
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("reportPrinter");
           //     config.AppSettings.Settings.Add("reportPrinter", cb_repname.Text);

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
           // cb_repname.ItemsSource = printersList;
            //cb_repname.DisplayMemberPath = "name";
            //cb_repname.SelectedValuePath = "name";
            string n= HelpClass.getfromConfig("reportPrinter");
          //  cb_repname.SelectedValue = n;
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
                tb_repPrintCount.Focus();
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
             
            txt_title.Text = MainWindow.resourcemanager.GetString("printCopyCount");
            btn_save.Content = MainWindow.resourcemanager.GetString("trSave");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_repPrintCount, MainWindow.resourcemanager.GetString("printCopyCount"));
 
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
        private void Tb_count_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                if (textBox == null)
                {
                    return;
                }



                if (textBox.Name == "tb_purCopyCount" || textBox.Name == "tb_saleCopyCount" || textBox.Name == "tb_repPrintCount" || textBox.Name == "tb_directEntry")
                    HelpClass.InputJustNumber(ref textBox);

                if (textBox.Name == "tb_purCopyCount")
                {
                    if (int.TryParse(textBox.Text, out _numPurCopyCount))
                        numPurCopyCount = int.Parse(textBox.Text);
                }
                else if (textBox.Name == "tb_saleCopyCount")
                {
                    if (int.TryParse(textBox.Text, out _numSaleCopyCount))
                        numSaleCopyCount = int.Parse(textBox.Text);
                }
                else if (textBox.Name == "tb_repPrintCount")
                {
                    if (int.TryParse(textBox.Text, out _numRepPrintCount))
                        numRepPrintCount = int.Parse(textBox.Text);
                }
                else if (textBox.Name == "tb_directEntry")
                {
                    if (int.TryParse(textBox.Text, out _numDirectEntry))
                        numDirectEntry = int.Parse(textBox.Text);
                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
                //SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void Tb_count_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Regex regex = new Regex("[^0-9]+");
                e.Handled = regex.IsMatch(e.Text);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
                //SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void Tb_PreventSpaces(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = e.Key == Key.Space;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
               // SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        #region NumericCount

        private int _numPurCopyCount = 1;
        public int numPurCopyCount
        {
            get { return _numPurCopyCount; }
            set
            {
                _numPurCopyCount = value;
             //   tb_purCopyCount.Text = value.ToString();
            }
        }


        private int _numSaleCopyCount = 1;
        public int numSaleCopyCount
        {
            get { return _numSaleCopyCount; }
            set
            {
                _numSaleCopyCount = value;
              //  tb_saleCopyCount.Text = value.ToString();
            }
        }


        private int _numRepPrintCount = 1;
        public int numRepPrintCount
        {
            get { return _numRepPrintCount; }
            set
            {
                _numRepPrintCount = value;
                tb_repPrintCount.Text = value.ToString();
            }
        }

        private int _numDirectEntry = 1;
        public int numDirectEntry
        {
            get { return _numDirectEntry; }
            set
            {
                _numDirectEntry = value;
               // tb_directEntry.Text = value.ToString();
            }
        }

        private void Btn_countUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button.Tag.ToString() == "purCopyCount")
                    numPurCopyCount++;
                else if (button.Tag.ToString() == "saleCopyCount")
                    numSaleCopyCount++;
                else if (button.Tag.ToString() == "repPrintCount")
                    numRepPrintCount++;
                else if (button.Tag.ToString() == "directEntry")
                    numDirectEntry++;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
              //  SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void Btn_countDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;

                if (button.Tag.ToString() == "purCopyCount" && numPurCopyCount > 1)
                    numPurCopyCount--;
                else if (button.Tag.ToString() == "saleCopyCount" && numSaleCopyCount > 1)
                    numSaleCopyCount--;
                else if (button.Tag.ToString() == "repPrintCount" && numRepPrintCount > 1)
                    numRepPrintCount--;
                else if (button.Tag.ToString() == "directEntry" && numDirectEntry > 1)
                    numDirectEntry--;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            //    SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            //    SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

           
        }
        private void Tb_validateEmptyLostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = sender.GetType().Name;
                validateEmpty(name, sender);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
                //SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void validateEmpty(string name, object sender)
        {
            try
            {
                if (name == "TextBox")
                {
                      if ((sender as TextBox).Name == "tb_repPrintCount")
                        HelpClass.validateEmptyTextBox((TextBox)sender, p_errorRepPrintCount, tt_errorRepPrintCount, "trEmptyError");
                    }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
              //  SectionData.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion
    }
}
