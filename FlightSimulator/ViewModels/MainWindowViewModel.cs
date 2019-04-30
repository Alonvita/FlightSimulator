using FlightSimulator.ViewModels;

namespace FlightSimulator.Model
{
    public class MainWindowViewModel : BaseNotif
    {
        private CommandHandler m_exitHandler;
        public CommandHandler ExitCommand
        {
            private set
            {

            }
            get => m_exitHandler ?? (m_exitHandler = new CommandHandler(() =>
            {
                System.Windows.Application.Current.Exit += Current_Exit;
                System.Windows.Application.Current.Shutdown();
            }));
        }

        private void Current_Exit(object sender, System.Windows.ExitEventArgs e)
        {
            FlightBoardVMSingelton.Instance.Stop();
        }
    }
}