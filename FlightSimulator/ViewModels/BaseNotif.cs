using System;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    public abstract class BaseNotif : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}