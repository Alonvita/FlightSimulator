using System.Windows.Controls;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AutoPilot : UserControl
    {
        public AutoPilot()
        {
            InitializeComponent();
            DataContext = AutoPilotVMSingelton.Instance;
        }
    }
}