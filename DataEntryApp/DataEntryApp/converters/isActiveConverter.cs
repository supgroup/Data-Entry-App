using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataEntryApp.converters
{
    class isActiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Equals("1"))
                value = MainWindow.resourcemanager.GetString("trValid");
            else if (value.ToString().Equals("0"))
                value = MainWindow.resourcemanager.GetString("trInValid");
            else
                value = "";
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
