using NFWCommonsLibrary.ViewModels;
using System.Collections.ObjectModel;

namespace DelegatesDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
	{

		public MainViewModel()
		{
			TabViewModels = new ObservableCollection<ViewModelBase>
			{
				new NumbersViewModel(),
				new HtmlViewModel()
			};
		}

		private ObservableCollection<ViewModelBase> _tabViewModels;
		public ObservableCollection<ViewModelBase> TabViewModels
		{
			get { return _tabViewModels; }
			set
			{
				_tabViewModels = value;
				OnPropertyChanged(nameof(TabViewModels));
			}
		}
	}
}
