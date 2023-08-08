using DataEntryApp.ApiClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataEntryApp.converters
{
    class packagePeriodConverter: IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null &&
                !(values[0].ToString() == "{DependencyProperty.UnsetValue}" ||
                    values[1].ToString() == "{DependencyProperty.UnsetValue}"))
                {
                    string period ="";
                    int monthCount = int.Parse(values[0].ToString());
                    bool islimitDate = bool.Parse(values[1].ToString()) ;
                    if (!islimitDate)
                        period = MainWindow.resourcemanager.GetString("trUnLimited");
                    else
                    {
                        switch (monthCount)
                        {
                            case 1: period = MainWindow.resourcemanager.GetString("trOneMonth"); break;
                            case 3: period = MainWindow.resourcemanager.GetString("trThreeMonth"); break;
                            case 6: period = MainWindow.resourcemanager.GetString("trSixMonth"); break;
                            case 12: period = MainWindow.resourcemanager.GetString("trTwelveMonth"); break;
                        }
                    }
                    return period;
                }
            return "";
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {

            string[] values = null;
            if (value != null)
                return values = value.ToString().Split(' ');
            return values;
        }

        /*
         
         public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            CountryPackageDate s = value as CountryPackageDate;

            string period = "";

            if (!s.islimitDate)
                period = MainWindow.resourcemanager.GetString("trUnLimited"); 
            else
            {
                switch (s.monthCount)
                {
                    case 1: period  = MainWindow.resourcemanager.GetString("trOneMonth");    break;
                    case 3: period  = MainWindow.resourcemanager.GetString("trThreeMonth");  break;
                    case 6: period  = MainWindow.resourcemanager.GetString("trSixMonth");    break;
                    case 12: period = MainWindow.resourcemanager.GetString("trTwelveMonth"); break;
                }
            }
          
            return period;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        * 
         * */
    }
}
