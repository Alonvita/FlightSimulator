using FlightSimulator.Model;
using System;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardVM : BaseNotif
    {
        /// <summary>
        /// m_settingCommand.
        /// </summary>
        private ICommand m_settingCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return m_settingCommand ?? (m_settingCommand = new CommandHandler(() => SettingsOnClick()));
            }
        }

        /// <summary>
        /// m_connectCommand.
        /// </summary>
        private ICommand m_connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return m_connectCommand ?? (m_connectCommand = new CommandHandler(() => ConnectOnClick()));
            }
        }

        /// <summary>
        /// ConnectOnClick().
        /// </summary>
        private void ConnectOnClick()
        {
            InfoSingleton.Instance.RunServer(ApplicationSettingsModel.Instance.FlightInfoPort);
            CommandSingleton.Instance.Start();
        }

        /// <summary>
        /// SettingsOnClick().
        /// </summary>
        private void SettingsOnClick()
        {
            Views.Settings set = new Views.Settings();
            set.ShowDialog();
        }

        /// <summary>
        /// m_long.
        /// 
        /// longitude
        /// </summary>
        private Double m_long;

        public Double Lon
        {
            get { return m_long; }
            set { m_long = value; NotifyPropertyChanged("Lon"); }
        }

        /// <summary>
        /// m_lat.
        /// 
        /// latitude
        /// </summary>
        private Double m_lat;
        public Double Lat
        {
            get { return m_lat; }
            set { m_lat = value; NotifyPropertyChanged("Lat"); }
        }

        /// <summary>
        /// FlightBoardVM()
        /// </summary>
        public FlightBoardVM()
        {
            FlightBoardModelSingelton.Instance.PropertyChanged += FBModel_PropertyChanged;
        }

        public void Stop()
        {
            FlightBoardModelSingelton.Instance.Stop();
            CommandSingleton.Instance.Stop();
        }

        protected void FBModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string rawData = FlightBoardModelSingelton.Instance.Data;
            string[] tokens = rawData.Split(',');
            try
            {
                // parse the lon and the lat
                Lon = Double.Parse(tokens[0]);
                Lat = Double.Parse(tokens[1]);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}