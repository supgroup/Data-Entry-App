
using netoaster;
using DataEntryApp.View.windows;
using DataEntryApp.Classes;
using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using DataEntryApp.View.windows;
using System.IO;

namespace DataEntryApp.View.applications
{
    /// <summary>
    /// Interaction logic for us_programs.xaml
    /// </summary>
    public partial class uc_programs : UserControl
    {
        public uc_programs()
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
        private static uc_programs _instance;
        public static uc_programs Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_programs();
                return _instance;
            }
        }
        /*
        Programs program = new Programs();


        IEnumerable<Programs> programsQuery;
        IEnumerable<Programs> programs;
        */
        byte tgl_programState;
        string searchText = "";
        public static List<string> requiredControlList;
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main, "main_loaded");
                requiredControlList = new List<string> { "name" };
                if (MainWindow.lang.Equals("en"))
                {
                    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.en_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    MainWindow.resourcemanager = new ResourceManager("DataEntryApp.ar_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }
                translate();
                Keyboard.Focus(tb_name);
                /*
                await RefreshProgramsList();
                await Search();
                Clear();
                */
                HelpClass.EndAwait(grid_main, "main_loaded");
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main, "main_loaded");
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void translate()
        {
            txt_title.Text = MainWindow.resourcemanager.GetString("cardData");
            //txt_active.Text = MainWindow.resourcemanager.GetString("trActive");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, MainWindow.resourcemanager.GetString("trSearchHint"));
            //txt_programHeader.Text = MainWindow.resourcemanager.GetString("trPrograms");
            //txt_baseInformation.Text = MainWindow.resourcemanager.GetString("trBaseInformation");
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_code, MainWindow.resourcemanager.GetString("trCodeHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_name, MainWindow.resourcemanager.GetString("trNameHint"));
            //txt_isActive.Text = MainWindow.resourcemanager.GetString("trActive");
            //txt_details.Text = MainWindow.resourcemanager.GetString("trDetails");

            //MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_discountType, MainWindow.resourcemanager.GetString("trTypeDiscountHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_discountValue, MainWindow.resourcemanager.GetString("trDiscountValueHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_startDate, MainWindow.resourcemanager.GetString("trStartDateHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(dp_endDate, MainWindow.resourcemanager.GetString("trEndDateHint"));
            //TextBox tbStart = (TextBox)tp_startTime.Template.FindName("PART_TextBox", tp_startTime);
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tbStart, MainWindow.resourcemanager.GetString("trStartTimeHint"));
            //TextBox tbEnd = (TextBox)tp_endTime.Template.FindName("PART_TextBox", tp_endTime);
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tbEnd, MainWindow.resourcemanager.GetString("trEndTimeHint"));
            //MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_note, MainWindow.resourcemanager.GetString("trNoteHint"));

            //txt_addButton.Text = MainWindow.resourcemanager.GetString("trAdd");
            //txt_updateButton.Text = MainWindow.resourcemanager.GetString("trUpdate");
            //txt_deleteButton.Text = MainWindow.resourcemanager.GetString("trDelete");
            //tt_add_Button.Content = MainWindow.resourcemanager.GetString("trAdd");
            //tt_update_Button.Content = MainWindow.resourcemanager.GetString("trUpdate");
            //tt_delete_Button.Content = MainWindow.resourcemanager.GetString("trDelete");

            //dg_program.Columns[0].Header = MainWindow.resourcemanager.GetString("trCode");
            //dg_program.Columns[1].Header = MainWindow.resourcemanager.GetString("trName");
            //dg_program.Columns[2].Header = MainWindow.resourcemanager.GetString("trValue");
            //dg_program.Columns[3].Header = MainWindow.resourcemanager.GetString("trStartDate");
            //dg_program.Columns[4].Header = MainWindow.resourcemanager.GetString("trEndDate");

            //tt_startTime.Content = MainWindow.resourcemanager.GetString("trStartTime");
            //tt_endTime.Content = MainWindow.resourcemanager.GetString("trEndTime");

            //tt_clear.Content = MainWindow.resourcemanager.GetString("trClear");
            //tt_refresh.Content = MainWindow.resourcemanager.GetString("trRefresh");
            //tt_report.Content = MainWindow.resourcemanager.GetString("trPdf");
            //tt_print.Content = MainWindow.resourcemanager.GetString("trPrint");
            //tt_excel.Content = MainWindow.resourcemanager.GetString("trExcel");
            //tt_pieChart.Content = MainWindow.resourcemanager.GetString("trPieChart");
            //tt_count.Content = MainWindow.resourcemanager.GetString("trCount");
            //btn_items.Content = MainWindow.resourcemanager.GetString("trItems");

        }
        private async void Btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {//refresh

                HelpClass.StartAwait(grid_main);
                /*
                await RefreshProgramsList();
                await Search();
                */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
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
                //if (programs is null)
                //    await RefreshProgramsList();
                //tgl_programState = 1;
                //await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Tgl_isActive_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //if (programs is null)
                //    await RefreshProgramsList();
                //tgl_programState = 0;
                //await Search();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Btn_clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                /*
                Clear();
                */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private void Dg_program_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //selection
                /*
                if (dg_program.SelectedIndex != -1)
                {
                    program = dg_program.SelectedItem as Programs;
                    this.DataContext = program;

                    if (program != null)
                    {
                        #region delete
                        if (program.canDelete)
                            btn_delete.Content = MainWindow.resourcemanager.GetString("trDelete");
                        else
                        {
                            if (program.isActive == 0)
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trActive");
                            else
                                btn_delete.Content = MainWindow.resourcemanager.GetString("trInActive");
                        }
                        #endregion
                    }
                }

                clearValidate();
                */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_add_Click(object sender, RoutedEventArgs e)
        {//add
            try
            {
                HelpClass.StartAwait(grid_main);
                /*
                program = new Programs();
                if (validate())
                {
                    string isExist = await program.isExistCode(tb_code.Text.Trim());
                    if (isExist == "0") { 
                    program.programCode = tb_code.Text.Trim();
                    program.name = tb_name.Text;
                    program.details = tb_details.Text;
                    program.notes = tb_notes.Text;
                    program.isActive = 1;
                    program.createUserId = MainWindow.userLogin.userId;
                    program.updateUserId = MainWindow.userLogin.userId;

                        decimal s = await program.Save(program);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopAdd"), animation: ToasterAnimation.FadeIn);
                        Clear();
                        await RefreshProgramsList();
                        await Search();
                    }
                    }
                    else
                    {
                        MessageBox.Show("exist ");
                    }
                }
                */
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
                /*
                if (program.programId > 0)
                {
                    if (validate())
                {
                    program.name = tb_name.Text;
                    program.details = tb_details.Text;
                    program.notes = tb_notes.Text;
                    program.updateUserId = MainWindow.userLogin.userId;

                        decimal s = await program.Save(program);
                    if (s <= 0)
                        Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                    else
                    {
                        Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopUpdate"), animation: ToasterAnimation.FadeIn);
                        await RefreshProgramsList();
                        await Search();
                    }
                }
                }
                else
                    Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trSelectItemFirst"), animation: ToasterAnimation.FadeIn);
                */
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        private async void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {//delete

                HelpClass.StartAwait(grid_main);
                /*
                if (program.programId != 0)
                {
                    if ((!program.canDelete) && (program.isActive == 0))
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
                        if (program.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDelete");
                        if (!program.canDelete)
                            w.contentText = MainWindow.resourcemanager.GetString("trMessageBoxDeactivate");
                        w.ShowDialog();
                        Window.GetWindow(this).Opacity = 1;
                        #endregion
                        if (w.isOk)
                        {
                            string popupContent = "";
                            if (program.canDelete) popupContent = MainWindow.resourcemanager.GetString("trPopDelete");
                            if ((!program.canDelete) && (program.isActive == 1)) popupContent = MainWindow.resourcemanager.GetString("trPopInActive");

                            decimal s = await program.Delete(program.programId, MainWindow.userLogin.userId, program.canDelete);
                            if (s < 0)
                                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
                            else
                            {
                                program.programId = 0;
                                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopDelete"), animation: ToasterAnimation.FadeIn);

                                await RefreshProgramsList();
                                await Search();
                                Clear();

                            }
                        }
                    }

                }
                */
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
            /*
            program.isActive = 1;
            decimal s = await program.Save(program);
            if (s <= 0)
                Toaster.ShowWarning(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopError"), animation: ToasterAnimation.FadeIn);
            else
            {
                Toaster.ShowSuccess(Window.GetWindow(this), message: MainWindow.resourcemanager.GetString("trPopActive"), animation: ToasterAnimation.FadeIn);
                await RefreshProgramsList();
                await Search();
            }
            */
        }
        #region Refresh & Search
        async Task Search()
        {
            //search
            //if (programs is null)
            //    await RefreshProgramsList();
            //searchText = tb_search.Text.ToLower();
            //programsQuery = programs.Where(s => (s.programCode.ToLower().Contains(searchText) ||
            //s.name.ToLower().Contains(searchText)
            //) && s.isActive == tgl_programState);
            //RefreshProgramsView();
        }
        //async Task<IEnumerable<Programs>> RefreshProgramsList()
        //{
        //  //  programs = await program.GetAll();
        //    return programs;
        //}
        //void RefreshProgramsView()
        //{
        //    dg_program.ItemsSource = programsQuery;
        //    txt_count.Text = programsQuery.Count().ToString();
        //}
        #endregion
        #region validate - clearValidate - textChange - lostFocus - . . . . 
        //void Clear()
        //{
        //    this.DataContext = new Programs();
        //    clearValidate();
        //}
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
                validate();
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
                validate();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this);
            }
        }
        bool validate()
        {
            bool isValid = true;
            try
            {
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(this).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    System.Windows.Shapes.Path path = FindControls.FindVisualChildren<System.Windows.Shapes.Path>(this).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    Border border = FindControls.FindVisualChildren<Border>(this).Where(x => x.Name == "brd_" + control)
                         .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(this).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    System.Windows.Shapes.Path path = FindControls.FindVisualChildren<System.Windows.Shapes.Path>(this).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    Border border = FindControls.FindVisualChildren<Border>(this).Where(x => x.Name == "brd_" + control)
                         .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmpty(comboBox.Text, path))
                            isValid = false;
                }
            }
            catch { }
            return isValid;
        }
        void clearValidate()
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    System.Windows.Shapes.Path path = FindControls.FindVisualChildren<System.Windows.Shapes.Path>(this).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }
        #endregion


        #region reports

        ReportCls reportclass = new ReportCls();
        LocalReport rep = new LocalReport();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public void BuildReport()
        {

             

            List<ReportParameter> paramarr = new List<ReportParameter>();

            string addpath;
            bool isArabic = ReportCls.checkLang();
            if (isArabic)
            {
                addpath = @"\Reports\Applications\En\EnPrograms.rdlc";

            }
            else
            {
                addpath = @"\Reports\Applications\En\EnPrograms.rdlc";
            }
          
            string reppath = reportclass.PathUp(Directory.GetCurrentDirectory(), 2, addpath);
   
           

            //clsReports.programsReport(programsQuery, rep, reppath, paramarr);
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

                if (saveFileDialog.ShowDialog() == true)
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
                    saveFileDialog.Filter = "EXCEL|*.xls;";
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

        private void Btn_print_card_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_previewcard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_pdfcard_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
