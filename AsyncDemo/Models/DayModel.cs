using NFWCommonsLibrary.Models;
using System;

namespace AsyncDemo.Models
{
    class DayModel : ModelBase
    {
		private DateTime _date;
		public DateTime Date
		{
			get { return _date; }
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
				OnPropertyChanged(nameof(DayOfMonth));
				OnPropertyChanged(nameof(DayOfWeek));
			}
		}

		public int DayOfMonth
		{
			get => Date.Day;
		}

		public int DayOfWeek
		{
			// Monday is 1 - Sunday is 7
			get => Date.DayOfWeek == 0 ? (int)Date.DayOfWeek + 7 : (int)Date.DayOfWeek;
		}

		private double _value;
		public double Value
		{
			get { return _value; }
			set
			{
				_value = value;
				OnPropertyChanged(nameof(Value));
			}
		}
	}
}
