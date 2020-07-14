using PowerSquare.ViewModels;
using System;
using System.Windows;
using System.Windows.Threading;

namespace PowerSquare.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer tmAlignHistoryWindow;
        private readonly HistoryWindow historyWindow;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();

            tmAlignHistoryWindow = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 0, 0, 500),
                IsEnabled = false,
            };

            tmAlignHistoryWindow.Tick += tmAlignHistoryWindow_Tick;

            historyWindow = new HistoryWindow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            historyWindow.Owner = this;
            historyWindow.DataContext = DataContext;
            historyWindow.Align();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            if (historyWindow.IsVisible)
            {
                historyWindow.Hide();
            }
            else
            {
                historyWindow.Show();
            }
            
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow
            {
                Owner = this,
                DataContext = new SettingsViewModel()
            };

            settingsWindow.Show();
        }

        private void InitiateHistoryWindowAlignment()
        {
            tmAlignHistoryWindow.IsEnabled = true;
            tmAlignHistoryWindow.Stop();
            tmAlignHistoryWindow.Start();
        }

        void tmAlignHistoryWindow_Tick(object sender, EventArgs e)
        {
            tmAlignHistoryWindow.IsEnabled = false;
            historyWindow.Align();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            InitiateHistoryWindowAlignment();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InitiateHistoryWindowAlignment();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
