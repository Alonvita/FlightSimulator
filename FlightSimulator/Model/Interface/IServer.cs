using System.ComponentModel;

namespace FlightSimulator.Model
{
    interface IServer : INotifyPropertyChanged
    {

        void RunServer(int port);
    }
}
