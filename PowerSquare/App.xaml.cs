using System.Globalization;
using System.Threading;
using System.Windows;
using PowerSquare.Properties;

namespace PowerSquare
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            if (!string.IsNullOrEmpty(Settings.Default.Language))
            {
                CultureInfo ci = new CultureInfo(Settings.Default.Language);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
        }

        
    }
}
