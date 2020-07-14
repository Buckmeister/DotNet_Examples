using NFWCommonsLibrary.ViewModels;
using System;
using System.Text.RegularExpressions;

namespace RegExecutionerLibrary.ViewModels
{
    public class ReplaceViewModel : ViewModelBase
    {
        public ReplaceViewModel()
        {
        }

        private bool isValidReplaceRegex = false;

		private string _inputReplace = string.Empty;
		public string InputReplace
		{
			get { return _inputReplace; }
			set
			{
				_inputReplace = value;
				OnPropertyChanged(nameof(InputReplace));
				ComputeOutputReplace();
			}
		}

		private string _regexReplacePattern = string.Empty;
		public string RegexReplacePattern
		{
			get { return _regexReplacePattern; }
			set
			{
				_regexReplacePattern = value;
				OnPropertyChanged(nameof(RegexReplacePattern));
				OnPropertyChanged(nameof(OutputReplace));
			}
		}

		private string _regexReplaceWith = string.Empty;
		public string RegexReplaceWith
		{
			get { return _regexReplaceWith; }
			set
			{
				_regexReplaceWith = value;
				OnPropertyChanged(nameof(RegexReplaceWith));
				OnPropertyChanged(nameof(OutputReplace));
			}
		}

		public string OutputReplace
		{
			get { return ComputeOutputReplace(); }
		}

		private string ComputeOutputReplace()
		{
			string outputReplace;
			try
			{
				isValidReplaceRegex = true;
				outputReplace = Regex.Replace(InputReplace, RegexReplacePattern, RegexReplaceWith);
			}
			catch (Exception ex)
			{
				isValidReplaceRegex = false;
				outputReplace = ex.Message;
			}

			return outputReplace;
		}
	}
}