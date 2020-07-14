using NFWCommonsLibrary.ViewModels;
using System.Text.RegularExpressions;

namespace RegExecutionerLibrary.ViewModels
{
    public class EmailViewModel : ViewModelBase
    {
        public EmailViewModel()
        {
        }

        private string _inputEmail = string.Empty;
        public string InputEmail 
        { 
            get => _inputEmail;
            set 
            { 
                _inputEmail = value;
                OnPropertyChanged(nameof(InputEmail));
                OnPropertyChanged(nameof(OutputEmail));
            } 
        }

        public string PatternEmail { get; } = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

        // Alternative Pattern
        //"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])";

        public string OutputEmail { get { return ComputeOutput(); } }

        private bool _isValidEmail;
        public bool IsValidEmail
        {
            get => _isValidEmail;
            set
            {
                if (_isValidEmail != value)
                {
                    _isValidEmail = value;
                    OnPropertyChanged(nameof(IsValidEmail));
                }
            }
        }

        private string ComputeOutput()
        {
            Match m = Regex.Match(InputEmail, PatternEmail);
            string result;

            if (m.Success)
            {
                IsValidEmail = true;
                result = "'" + m.Value + "' is a valid Email Address";
            }
            else
            {
                IsValidEmail = false;
                result = "'" + InputEmail + "' is an invalid Address";
            }
            return result;
        }
    }
}
