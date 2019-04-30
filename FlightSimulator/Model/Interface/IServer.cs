using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FlightSimulator.Model;

namespace FlightSimulator.Model
{
    public interface IServer
    {
        void Run(int port);
        void Disconnect();
    }
}
