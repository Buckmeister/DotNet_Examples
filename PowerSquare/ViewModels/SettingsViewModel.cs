using NFWCommonsLibrary.Internationalization;
using NFWCommonsLibrary.ViewModels;
using PowerSquare.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace PowerSquare.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            
            languageEntries = new ObservableCollection<LanguageEntryModel>
            {
                new LanguageEntryModel(Translate.ResString("Language_1"), Translate.ResString("LanguageDisplay_1")),
                new LanguageEntryModel(Translate.ResString("Language_2"), Translate.ResString("LanguageDisplay_2"))
            };

            if (string.IsNullOrEmpty(Language))
            {
                Language = LanguageEntries.FirstOrDefault().ToString();
            }
        }

        private ObservableCollection<LanguageEntryModel> languageEntries;
        public ObservableCollection<LanguageEntryModel> LanguageEntries 
        { 
            get { return languageEntries; }

            set
            {
                languageEntries = value;
                OnPropertyChanged(nameof(LanguageEntries));
            }
        }

        public string Language
        {
            get { return Properties.Settings.Default.Language; }

            set
            {
                Properties.Settings.Default.Language = value;
                OnPropertyChanged(nameof(Language));
            }
        }
    }
}
