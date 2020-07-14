using FormValidationLibrary.Validators;
using NFWCommonsLibrary.Input;
using NFWCommonsLibrary.ViewModels;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace FormValidationLibrary.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private double numberOfPropertiesWithValidation;
		private double progressPercentagePerProperty;
		private readonly ConcurrentDictionary<string, bool> isPropertyValid = new ConcurrentDictionary<string, bool>();
		
		public MainViewModel()
        {
			ErrorsChanged += UpdateProgress;
			ResetApplication();

			numberOfPropertiesWithValidation = isPropertyValid.Keys.Count;
			progressPercentagePerProperty = 100 / numberOfPropertiesWithValidation;
		}

		private string _firstName;
		[Display(Name = "Vorname")]
		[Required]
		[MinLength(2, ErrorMessage = "Vornamen bestehen gewöhnlich aus mehr als einen Buchstaben.")]
		[MaxLength(20, ErrorMessage = "Ihr Vorname ist wirklich ungewöhnlich lang.")]
		[RegularExpression(@"^[A-Z][a-zäüöß]+([\- ][A-Z][a-zäüöß]+)*$", ErrorMessage = "Vornamen fangen gewöhnlich mit einem Großbuchstaben an und enthalten keine Sonderzeichen.")]
		public string FirstName
		{
			get { return _firstName; }
			set
			{
				_firstName = value;
				ValidateProperty(value);
				OnPropertyChanged(nameof(FirstName));
			}
		}

		private string _lastName;
		[Display(Name = "Nachname")]
		[Required]
		[MinLength(2, ErrorMessage = "Nachnamen bestehen gewöhnlich aus mehr als einen Buchstaben.")]
		[MaxLength(20, ErrorMessage = "Ihr Nachname ist wirklich ungewöhnlich lang.")]
		[RegularExpression(@"^[A-Z][a-zäüöß]+([\- ][A-Z][a-zäüöß]+)*$", ErrorMessage = "Nachnamen fangen gewöhnlich mit einem Großbuchstaben an und enthalten keine Sonderzeichen.")]
		public string LastName
		{
			get { return _lastName; }
			set
			{
				_lastName = value;
				ValidateProperty(value);
				OnPropertyChanged(nameof(LastName));
			}
		}

		private string _emailAddress;
		[Display(Name = "E-Mail-Adresse")]
		[Required]
		//[EmailAddress(ErrorMessage = "Keine gültige E-Mail-Adresse")]
		[RegularExpression("^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", 
							ErrorMessage ="Keine gültige E-Mail-Adresse")]
		public string EmailAddress
		{
			get { return _emailAddress; }
			set
			{
				_emailAddress = value;
				ValidateProperty(value);
				OnPropertyChanged(nameof(EmailAddress));
			}
		}

		private string _phoneNumber;
		[Display(Name = "Telefonnummer")]
		[Required]
		[MinLength(8, ErrorMessage = "Die angegebene Nummer ist zu kurz")]
		[Phone(ErrorMessage = "Keine gültige Telefonnummer")]
		public string PhoneNumber
		{
			get { return _phoneNumber; }
			set
			{
				_phoneNumber = value;
				ValidateProperty(value);
				OnPropertyChanged(nameof(PhoneNumber));
			}
		}

		private string _iban;
		[Display(Name = "IBAN")]
		[Required]
		[Iban(ErrorMessage = "Kein gültiger IBAN")]
		public string Iban
		{
			get { return _iban; }
			set
			{
				
				_iban = value.Replace(" ", "");

				ValidateProperty(_iban);
				OnPropertyChanged(nameof(Iban));
			}
		}

		private double _progress;
		public double Progress
		{
			get { return _progress; }
			set
			{
				_progress = value;
				OnPropertyChanged(nameof(Progress));
			}
		}

		public Func<double, string> ProgressFormatter { get; private set; } = value => string.Format("{0:0.#}", value);

		private bool _resetCommandEanbled;
		public bool ResetCommandEnabled
		{
			get { return _resetCommandEanbled; }
			set
			{
				_resetCommandEanbled = value;
				OnPropertyChanged(nameof(ResetCommandEnabled));
			}
		}

		private ICommand _resetCommand;
		public ICommand ResetCommand
		{
			get
			{
				return _resetCommand ?? (_resetCommand =
										new CommandHandler(
											ResetCommandExecute,
											ResetCommandCanExecute)
										);
			}
		}

		public bool ResetCommandCanExecute()
		{
			if (HasErrors)
			{
				ResetCommandEnabled = false;
				return false;
			}

			ResetCommandEnabled = true;
			return true;
		}

		public void ResetCommandExecute()
		{
			ResetApplication();
		}

		private void ResetApplication()
		{
			FirstName = string.Empty;
			LastName = string.Empty;
			EmailAddress = string.Empty;
			PhoneNumber = string.Empty;
			Iban = string.Empty;
			Progress = 0;
		}

		private void UpdateProgress(object sender, DataErrorsChangedEventArgs e)
		{
			bool wasValidBefore = false;
			bool isValidNow = false;

			isPropertyValid.TryGetValue(e.PropertyName, out wasValidBefore);
			if (GetErrors(e.PropertyName) is null) isValidNow = true;

			// Validation went from "good" to "bad"
			if (wasValidBefore && !isValidNow)
			{
				isPropertyValid.AddOrUpdate(e.PropertyName, false, (key, oldValue) => false);
				Progress -= progressPercentagePerProperty;
			}

			// Validation went from "bad" to "good"
			if (!wasValidBefore && isValidNow)
			{
				isPropertyValid.AddOrUpdate(e.PropertyName, true, (key, oldValue) => true);
				Progress += progressPercentagePerProperty;
			}

			if (Progress < 0) Progress = 0;
			if (Progress > 100) Progress = 100;

		}
	}
}
