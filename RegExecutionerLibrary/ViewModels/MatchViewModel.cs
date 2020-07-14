using NFWCommonsLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RegExecutionerLibrary.ViewModels
{
    public class MatchViewModel : ViewModelBase
    {
        public MatchViewModel()
        {
        }

        private bool isValidMatchRegex = false;

		private string _inputMatch = string.Empty;
		public string InputMatch
		{
			get { return _inputMatch; }
			set
			{
				_inputMatch = value;
				OnPropertyChanged(nameof(InputMatch));
				OnPropertyChanged(nameof(OutputMatch));
			}
		}

		private string _regexMatchPattern = string.Empty;
		public string RegexMatchPattern
		{
			get { return _regexMatchPattern; }
			set
			{
				_regexMatchPattern = value;
				OnPropertyChanged(nameof(RegexMatchPattern));
				OnPropertyChanged(nameof(OutputMatch));
			}
		}

		public string OutputMatch
		{
			get { return ComputeOutputMatch(); }
		}

		private string ComputeOutputMatch()
		{
			StringBuilder outputMatch = new StringBuilder();
			try
			{
				isValidMatchRegex = true;

				MatchCollection regexMatches = Regex.Matches(InputMatch, RegexMatchPattern);

				Dictionary<string, List<string>> regexResults = new Dictionary<string, List<string>>();

				foreach (Match regexMatch in regexMatches)
				{
					int groupIndex = 0;
					foreach (Group regexGroup in regexMatch.Groups)
					{
						outputMatch.Append($"\nGroup {groupIndex}:");
						outputMatch.Append($"\n'{regexGroup.Value}' found at position {regexGroup.Index}");
						groupIndex++;
					}
				}
				outputMatch.Append("\n");
			}
			catch (Exception ex)
			{
				isValidMatchRegex = false;
				outputMatch.Clear();
				outputMatch.Append(ex.Message);
			}
			return outputMatch.ToString();
		}
	}
}