using AsyncDemo.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AsyncDemo.Converters
{
    public class FirstColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DayModel)
            {
                DayModel d = value as DayModel;
                // Day of the week starts with 1 - the first column index starts with 7
                return d.DayOfWeek-1;
            }
            else
            {
                return 0;
            }   
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                return (int)value + 1;
            else
                return value;
        }
    }
}
