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
using System.Windows.Shapes;
using netoaster;

namespace DataEntryApp.View.windows
{
    /// <summary>
    /// Interaction logic for wd_saleNote.xaml
    /// </summary>
    public partial class wd_saleNote : Window
    {
        public wd_saleNote()
        {
            InitializeComponent();
        }
        public static List<string> requiredControlList;

        SetValues setVNote = new SetValues();
        SettingCls set = new SettingCls();
        SettingCls setModel = new SettingCls();
        SetValues valueModel = new SetValues();
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {//mouse down
            try
            {
                DragMove();
            }
            catch (Exception ex)
            {
            }
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    //Btn_save_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load

            try
            {
                HelpClass.StartAwait(grid_main);
              //  sale_note
                requiredControlList = new List<string> { "" };

                #region translate
                if (MainWindow.lang.Equals("en"))
                {
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }
                translat();
                #endregion

               // List<SettingCls> settingsCls = await setModel.GetAll();
                List<SetValues> settingsValues = await valueModel.GetBySetName("sale_note");
                setVNote = settingsValues.FirstOrDefault();
                tb_notes.Text = setVNote.value;
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }

        private void translat()
        {

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
                this.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {//unload
            GC.Collect();
        }

        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {//close
            this.Close();
        }

        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                setVNote.value = tb_notes.Text.Trim();
                decimal res = await valueModel.Save(setVNote);
                if (res>0)
                {
                    Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopSave"), animation: ToasterAnimation.FadeIn);
                    await Task.Delay(1500);
                }
                else
                {
                    Toaster.ShowWarning(Window.GetWindow(this), message: "Not saved", animation: ToasterAnimation.FadeIn);
                    await Task.Delay(1500);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
       

        }
    }
}
