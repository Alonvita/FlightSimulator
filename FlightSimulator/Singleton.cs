using FlightSimulator.ViewModels;
using FlightSimulator.Model;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator
{
    public class InfoSingleton
    {
        private static TcpServer _instance = null;

        public static TcpServer Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new TcpServer();
                return _instance;
            }
        }
    }

    public class CommandSingleton
    {
        private static Client _instance = null;

        public static Client Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new Client();
                return _instance;
            }
        }
    }

    public class FlightBoardModelSingelton
    {
        private static FlightBoardModel _instance = null;

        public static FlightBoardModel Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new FlightBoardModel();
                return _instance;
            }
        }
    }

    public class AutoPilotVMSingelton
    {
        private static AutoPilotVM _instance = null;

        public static AutoPilotVM Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new AutoPilotVM();
                return _instance;
            }
        }
    }

    public class FlightBoardVMSingelton
    {
        private static FlightBoardVM _instance = null;

        public static FlightBoardVM Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new FlightBoardVM();
                return _instance;
            }
        }
    }

    public class JoystickVMSingelton
    {
        private static JoystickVM _instance = null;

        public static JoystickVM Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new JoystickVM();
                return _instance;
            }
        }
    }

    public class MainWindowVMSingelton
    {
        private static MainWindowViewModel _instance = null;

        public static MainWindowViewModel Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new MainWindowViewModel();
                return _instance;
            }
        }
    }

    public class MySettingVMSingelton
    {
        private static SettingsWindowViewModel _instance = null;

        public static SettingsWindowViewModel Instance
        {
            private set { }
            get
            {
                if (_instance == null)
                    _instance = new SettingsWindowViewModel(ApplicationSettingsModel.Instance);
                return _instance;
            }
        }
    }
}