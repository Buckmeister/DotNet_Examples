using System;
using System.Globalization;
using System.Windows.Data;

namespace AsyncDemo.Converters
{
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double d = (double)value;

                if (d < 300) return "OrangeRed";
                if (d < 600) return "Orange";
                return "ForestGreen";
            }
            else
            {
                return "DarkGray";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
