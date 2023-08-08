using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DataEntryApp.converters
{
    class agentNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values , Type targetType, object parameter, CultureInfo culture)
        {
            int id = (int)values[0];
            string name = (string)values[1];
            string last = (string)values[2];

            if (id == 3)
                return "Supclouds";
            else
                return name + " " + last;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
