using DelegatesDemo.Models;
using NFWCommonsLibrary.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DelegatesDemo.ViewModels
{
	class NumbersViewModel : ViewModelBase
    {
		//private delegate string NumberFormatter(int number);

		public NumbersViewModel()
		{
			FormatEntries = new ObservableCollection<FormatEntry>
			{
				new FormatEntry { DisplayName = "Zahlen als Ziffern", Name = "Numbers" },
				new FormatEntry { DisplayName = "Zahlen als Worte", Name = "Words" },
				new FormatEntry { DisplayName = "Zahlen als Römische Zahlen", Name = "Roman" }
			};

			CurrentFormat = FormatEntries.FirstOrDefault().ToString();
		}

		private string _inputString = string.Empty;
		public string InputString
		{
			get { return _inputString; }
			set
			{
				_inputString = value;
				OnPropertyChanged(nameof(InputString));
				ComposeContent();
			}
		}



		private ObservableCollection<FormatEntry> _formatEntries;
		public ObservableCollection<FormatEntry> FormatEntries
		{
			get { return _formatEntries; }
			set
			{
				_formatEntries = value;
				OnPropertyChanged(nameof(FormatEntries));
			}
		}

		private string _currentFormat;
		public string CurrentFormat
		{
			get { return _currentFormat; }
			set
			{
				_currentFormat = value;
				OnPropertyChanged(nameof(CurrentFormat));
				ComposeContent();
			}
		}

		private string _textOutput;
		public string TextOutput
		{
			get { return _textOutput; }
			set
			{
				_textOutput = value;
				OnPropertyChanged(nameof(TextOutput));
			}
		}

		private Func<int, string> SetNumberFormatter()
		{
			if (CurrentFormat=="Words")
			{
				return NumberFormatterWords;
			}
			else if (CurrentFormat == "Roman")
			{
				return NumberFormatterRoman;
			}
			else
			{
				return  i => i.ToString();
			}
		}

		private void ComposeContent()
		{
			StringBuilder sb = new StringBuilder();
			Func<int, string> formatter = SetNumberFormatter();

			foreach (string character in Regex.Split(InputString, string.Empty))
			{
				if(int.TryParse(character, out int number))
				{
					string formattedNumber = formatter?.Invoke(number);

					sb.Append($"{formattedNumber} ");
				}
			}
			TextOutput = sb.ToString();
		}

		
		private string NumberFormatterWords(int value)
		{
			switch(value)
			{
				case 1:
					return "Eins";
				case 2:
					return "Zwei";
				case 3:
					return "Drei";
				case 4:
					return "Vier";
				case 5:
					return "Fünf";
				case 6:
					return "Sechs";
				case 7:
					return "Sieben";
				case 8:
					return "Acht";
				case 9:
					return "Neun";
				case 0:
					return "Null";
				default:
					throw new NotSupportedException();

			}
		}

		private string NumberFormatterRoman(int value)
		{
			switch (value)
			{
				case 1:
					return "I";
				case 2:
					return "II";
				case 3:
					return "III";
				case 4:
					return "IV";
				case 5:
					return "V";
				case 6:
					return "VI";
				case 7:
					return "VII";
				case 8:
					return "VIII";
				case 9:
					return "IX";
				case 0:
					return "N";
				default:
					throw new NotSupportedException();

			}
		}
	}
}
