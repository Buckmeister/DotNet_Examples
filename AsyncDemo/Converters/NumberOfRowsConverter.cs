using AsyncDemo.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AsyncDemo.Converters
{
    public class NumberOfRowsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DayModel)
            {
                DayModel d = value as DayModel;
                DateTime lastDayOfMonth = d.Date.AddMonths(1).AddDays(-1);
                int weekDayOffset = d.DayOfWeek -1;
                int numberOfDaysInGivenMonth = lastDayOfMonth.Day;
                int totalNumberOfNeededCells = numberOfDaysInGivenMonth + weekDayOffset;

                int numberOfCompletedRowsNeeded = totalNumberOfNeededCells / 7;
                int numberOfUncompletedRowsNeeded = totalNumberOfNeededCells % 7 == 0 ? 0 : 1;

                return numberOfCompletedRowsNeeded + numberOfUncompletedRowsNeeded;
            }
            else
            {
                return 6;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
