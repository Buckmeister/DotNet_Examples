using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace PowerSquare.Views
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        public void Align()
        {
            double newLeft = Owner.Left + Owner.Width;
            double newTop = Owner.Top;

            if (Owner.WindowState == WindowState.Maximized)
            {
                newLeft = SystemParameters.PrimaryScreenWidth - Width - 5;
                newTop = 25;
            }

            if (newLeft + Width > SystemParameters.PrimaryScreenWidth)
            {
                newLeft = SystemParameters.PrimaryScreenWidth - Width - 5;
                newTop += 25;
            }

            if (Left == newLeft && Top == newTop) return;

            if (IsVisible)
            {

                DoubleAnimation dblanimFadeOut = new DoubleAnimation(0, TimeSpan.FromSeconds(.1));
                dblanimFadeOut.Completed += (senderFO, argsFO) =>
                {
                    Hide();

                    Left = newLeft;
                    Top = newTop;

                    DoubleAnimation dblanimFadeIn = new DoubleAnimation(1, TimeSpan.FromSeconds(.1));
                    dblanimFadeIn.Completed += (senderFI, argsFI) => Show();
                    this.BeginAnimation(OpacityProperty, dblanimFadeIn);
                };
                this.BeginAnimation(OpacityProperty, dblanimFadeOut);
            }
            else
            {
                Left = newLeft;
                Top = newTop;
            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Align();
        }
    }
}
