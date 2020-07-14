using NFWCommonsLibrary.Models;

namespace DelegatesDemo.Models
{
    class FormatEntry : ModelBase
    {
		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		private string _displayName;
		public string DisplayName
		{
			get { return _displayName; }
			set
			{
				_displayName = value;
				OnPropertyChanged(nameof(DisplayName));
			}
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
