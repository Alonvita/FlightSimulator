using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using System.Windows;
using System.Windows.Input;

namespace FlightSimulator.ViewModels.Windows
{

    public class SettingsWindowViewModel : BaseNotif
    {
        private ISettingsModel m_model;
        private Window m_window;

        public SettingsWindowViewModel(ISettingsModel model)
        {
            m_model = model;
        }

        public void SetWindow(Window w)
        {
            m_window = w;
        }

        public string FlightServerIP
        {
            get { return m_model.FlightServerIP; }
            set
            {
                m_model.FlightServerIP = value;
                NotifyPropertyChanged("FlightServerIP");
            }
        }

        public int FlightCommandPortProperty
        {
            get { return m_model.FlightCommandPort; }
            set
            {
                m_model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        public int FlightInfoPortProperty
        {
            get { return m_model.FlightInfoPort; }
            set
            {
                m_model.FlightInfoPort = value;
                NotifyPropertyChanged("FlightInfoPort");
            }
        }



        public void SaveSettings()
        {
            m_model.SaveSettings();
        }

        public void ReloadSettings()
        {
            m_model.ReloadSettings();
        }

        #region Commands
        #region OnClickCommand

        private ICommand m_OnClickCommand;
        public ICommand OnClickCommand
        {
            get
            {
                return m_OnClickCommand ?? (m_OnClickCommand = new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick()
        {
            m_model.SaveSettings();
            m_window.Close();
        }
        #endregion

        #region OnCancelCommand
        private ICommand m_OnCancelCommand;
        public ICommand OnCancelCommand
        {
            get
            {
                return m_OnCancelCommand ?? (m_OnCancelCommand = new CommandHandler(() => Cancel()));
            }
        }
        private void Cancel()
        {
            m_model.ReloadSettings();
            m_window.Close();
        }
        #endregion
        #endregion
    }
}
