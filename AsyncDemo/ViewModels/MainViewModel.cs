using AsyncDemo.Models;
using NFWCommonsLibrary.ErrorHandling;
using NFWCommonsLibrary.Input;
using NFWCommonsLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncDemo.ViewModels
{
	class MainViewModel : ViewModelBase
    {
		private CancellationTokenSource cts;
		private IProgress<double> progress;
		public MainViewModel()
		{
			MonthOffset = 0;
			CurrentMonth = DateTime.Today;
			progress = new Progress<double>(percent => ProgressIndicator = percent);
			Days = new ObservableCollection<DayModel>();

			GetData();
		}

		private ObservableCollection<DayModel> _days;
		public ObservableCollection<DayModel> Days
		{
			get { return _days; }
			set
			{
				_days = value;
				OnPropertyChanged(nameof(Days));
			}
		}

		private DateTime _currentMonth;
		public DateTime CurrentMonth
		{
			get { return _currentMonth; }
			set
			{
				_currentMonth = value;
				OnPropertyChanged(nameof(CurrentMonth));
			}
		}

		private int _monthOffset;
		public int MonthOffset
		{
			get { return _monthOffset; }
			set
			{
				_monthOffset = value;
				OnPropertyChanged(nameof(MonthOffset));
			}
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get { return _isBusy; }
			set
			{
				_isBusy = value;
				OnPropertyChanged(nameof(IsBusy));
				CommandManager.InvalidateRequerySuggested();
			}
		}

		private double _progressIndicator;
		public double ProgressIndicator
		{
			get { return _progressIndicator; }
			set
			{
				_progressIndicator = value;
				OnPropertyChanged(nameof(ProgressIndicator));
			}
		}

		
		private IAsyncCommand _previousCommand;
		public IAsyncCommand PreviousCommand
		{
			get
			{
				return _previousCommand ??
					(
					_previousCommand = new AsyncCommand(PreviousExecuteAsync, PreviousCanExecute, new ConsoleErrorHandler())
					);
			}
		}

		private bool PreviousCanExecute()
		{
			if (IsBusy == true) return false;
			return true;
		}

		private async Task PreviousExecuteAsync()
		{
			try
			{
				IsBusy = true;
				Days.Clear();
				var newMonth = DateTime.Today.AddMonths(--MonthOffset);
				cts = new CancellationTokenSource();
				Days = new ObservableCollection<DayModel>(await GetDataAsync(newMonth, progress, cts.Token));
				CurrentMonth = newMonth;
			}
			catch (OperationCanceledException ex)
			{
				Console.WriteLine("Canceled: " + ex.Message);
				//Do stuff to handle cancellation
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private IAsyncCommand _nextCommand;
		public IAsyncCommand NextCommand
		{
			get
			{
				return _nextCommand ??
					(
					_nextCommand = new AsyncCommand(NextExecuteAsync, NextCanExecute, new ConsoleErrorHandler())
					);
			}
		}

		private bool NextCanExecute()
		{
			if (IsBusy == true) return false;

			return true;
		}

		private async Task NextExecuteAsync()
		{
			try
			{
				IsBusy = true;
				Days.Clear();
				var newMonth = DateTime.Today.AddMonths(++MonthOffset);
				cts = new CancellationTokenSource();
				Days = new ObservableCollection<DayModel>(await GetDataAsync(newMonth, progress, cts.Token)); 
				CurrentMonth = newMonth;

			}
			catch (OperationCanceledException ex)
			{
				Console.WriteLine("Canceled: " + ex.Message);
				//Do stuff to handle cancellation
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get
			{
				return _cancelCommand ?? (_cancelCommand =
										new CommandHandler(
											CancelCommandExecute,
											CancelCommandCanExecute)
										);
			}
		}

		public bool CancelCommandCanExecute()
		{
			return true;
		}

		public void CancelCommandExecute()
		{
			cts.Cancel();
		}

		private void GetData()
		{
			IsBusy = true;
			cts = new CancellationTokenSource();
			GetDataAsync(CurrentMonth, progress, cts.Token).ContinueWith(
				task =>
				{
					try
					{
						Days = new ObservableCollection<DayModel>(task.Result);
					}
					catch (OperationCanceledException ex)
					{
						Console.WriteLine("Canceled: " + ex.Message);
						//Do stuff to handle cancellation
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error: " + ex.Message);
					}
					finally
					{
						IsBusy = false;
					}
				}, TaskScheduler.FromCurrentSynchronizationContext()
				);
		}

		private async Task<List<DayModel>> GetDataAsync(DateTime date, IProgress<double> progress, CancellationToken ct)
		{
			var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
			
			var days = new List<DayModel>();
			
			for (int i = 0; i < lastDayOfMonth.Day; i++)
			{
				ct.ThrowIfCancellationRequested();
				days.Add(new DayModel
				{
					Date = firstDayOfMonth.AddDays(i),
					Value = RandomNumberBetween(0, 1000),
				});

				progress?.Report(i * 100 / lastDayOfMonth.Day);
				await Task.Delay(50);
			}
			return days;
		}

		private static readonly Random random = new Random();
		private static double RandomNumberBetween(double minValue, double maxValue)
		{
			var next = random.NextDouble();

			return minValue + (next * (maxValue - minValue));
		}
	}
}
