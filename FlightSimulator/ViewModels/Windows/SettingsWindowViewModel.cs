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

        public void setWindow(Window w)
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

        public int FlightCommandPort
        {
            get { return m_model.FlightCommandPort; }
            set
            {
                m_model.FlightCommandPort = value;
                NotifyPropertyChanged("FlightCommandPort");
            }
        }

        public int FlightInfoPort
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
        #region ClickCommand
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick()
        {
            m_model.SaveSettings();
            m_window.Close();
        }
        #endregion

        #region CancelCommand
        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new CommandHandler(() => OnCancel()));
            }
        }
        private void OnCancel()
        {
            m_model.ReloadSettings();
            m_window.Close();
        }
        #endregion
        #endregion
    }
}
