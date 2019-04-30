using System.Windows;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            this.DataContext = MySettingVMSingelton.Instance;
            MySettingVMSingelton.Instance.SetWindow(this);
        }

    }
}