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
                SettingCls set = new SettingCls();
                SetValues setV = new SetValues();
                set = FillCombo.settingList.Where(s => s.name == "rep_copy_count").FirstOrDefault<SettingCls>();
               int nameId = set.settingId;
                setV = FillCombo.setValueList.Where(i => i.settingId == nameId).FirstOrDefault();
                setV.value = tb_repPrintCount.Text;
                decimal res = await setV.Save(setV);
                if (res>0)
                {
                    Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopSave"), animation: ToasterAnimation.FadeIn);
                    await Task.Delay(1000);
                  await  FillCombo.loading_getDefaultSystemInfo();
                }
                else
                {
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                }
                
             
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

        public void fill_repPrintCount()
        {
            tb_repPrintCount.Text = FillCombo.rep_copy_count;
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
        private async void Window_Loaded(object sender, RoutedEventArgs e)
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
                translate();
                await FillCombo.loading_getDefaultSystemInfo();
                fill_repPrintCount();
               
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



                if (  textBox.Name == "tb_repPrintCount"  )
                    HelpClass.InputJustNumber(ref textBox);

                 if (textBox.Name == "tb_repPrintCount")
                {
                    if (int.TryParse(textBox.Text, out _numRepPrintCount))
                        numRepPrintCount = int.Parse(textBox.Text);
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
                 
                  if (button.Tag.ToString() == "repPrintCount")
                    numRepPrintCount++;
 
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

              
               if (button.Tag.ToString() == "repPrintCount" && numRepPrintCount > 1)
                    numRepPrintCount--;
                 
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
