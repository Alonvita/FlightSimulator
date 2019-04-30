using FlightSimulator.Model;
using FlightSimulator.ViewModels;
using FlightSimulator.ViewModels.Windows;

namespace FlightSimulator
{
    public class InfoSingleton
    {
        private static TcpServer m_instance = null;

        public static TcpServer Instance
        {
            private set { }
            get
            {
                if (m_instance == null)
                    m_instance = new TcpServer();
                return m_instance;
            }
        }
    }

    public class CommandSingleton
    {
        private static Client m_instance = null;

        public static Client Instance
        {
            private set { }
            get
            {
                if (m_instance == null)
                    m_instance = new Client();
                return m_instance;
            }
        }
    }

    public class FlightBoardModelSingelton
    {
        private static FlightBoardModel m_instance = null;

        public static FlightBoardModel Instance
        {
            private set { }
            get
            {
                if (m_instance == null)
                    m_instance = new FlightBoardModel();
                return m_instance;
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
        private static FlightBoardVM m_instance = null;

        public static FlightBoardVM Instance
        {
            private set { }
            get
            {
                if (m_instance == null)
                    m_instance = new FlightBoardVM();
                return m_instance;
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
        private static MainWindowViewModel m_instance = null;

        public static MainWindowViewModel Instance
        {
            private set { }
            get
            {
                if (m_instance == null)
                    m_instance = new MainWindowViewModel();
                return m_instance;
            }
        }
    }

    public class MySettingVMSingelton
    {
        private static SettingsWindowViewModel m_instance = null;

        public static SettingsWindowViewModel Instance
        {
            private set { }
            get
            {
                if (m_instance == null)
                    m_instance = new SettingsWindowViewModel(ApplicationSettingsModel.Instance);
                return m_instance;
            }
        }
    }
}