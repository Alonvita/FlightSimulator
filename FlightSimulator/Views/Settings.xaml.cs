using System.Windows;
using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        readonly UserControl mainWindow;

        public Settings(UserControl _mainWindow)
        {
            InitializeComponent();

            this.mainWindow = _mainWindow;

            this.Height = mainWindow.Height / 2;
            this.Width = mainWindow.Width / 2;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
        }
    }
}
