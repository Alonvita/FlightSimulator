using System.Windows;
using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for MyFlightBoard.xaml
    /// </summary>
    public partial class MyFlightBoard : UserControl
    {
        public MyFlightBoard()
        {
            InitializeComponent();
            FlightBoardVMSingelton.Instance.PropertyChanged += FlightBoardResource.Vm_PropertyChanged;
            DataContext = FlightBoardVMSingelton.Instance;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            Connect.IsEnabled = false;
        }

        private void FlightBoardResource_Loaded(object sender, RoutedEventArgs e)
        {
            return;
        }
    }
}