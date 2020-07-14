using NFWCommonsLibrary.Deployment;
using NFWCommonsLibrary.ViewModels;
using RegExecutionerLibrary.ViewModels;
using System.Collections.ObjectModel;

namespace RegExecutionerWPF
{
	class MainViewModel : ViewModelBase
	{
		public ObservableCollection<object> Children { get; private set; }

		public MainViewModel()
		{
			Children = new ObservableCollection<object>
			{
				new MatchViewModel(),
				new ReplaceViewModel(),
				new EmailViewModel()
			};
			OnPropertyChanged(nameof(Children));
		}
		public string AppVersion 
		{
			get { return "Version: " + DeploymentAction.GetRunningVersion(); }
		}
	}
}
