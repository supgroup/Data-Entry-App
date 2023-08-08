using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataEntryApp.converters
{
    class unlimitedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _check = false;
            if (value.ToString().Equals("-1"))
                _check = true;
            else
                _check = false;

            return _check;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }
}
