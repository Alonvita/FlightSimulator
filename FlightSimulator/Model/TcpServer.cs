using FlightSimulator.Model;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FlightSimulator.Model
{
    class TcpServer : IServer
    {
        public TcpClient _tcpClient = null;
        public NetworkStream _netStream = null;
        public CommandHandler _commandHandler = null;

        public TcpServer(CommandHandler ch) { this._commandHandler = ch; }
        public void Run(int port)
        {
            var listener = new TcpListener(IPAddress.Any, port);
            Console.WriteLine("Waiting for connection.....");

            try
            {
                listener.Start();
                Thread thread = new Thread(() => {
                    while (true)
                    {
                        _tcpClient = listener.AcceptTcpClient();
                        if (this._commandHandler != null)
                            this._commandHandler.Execute(this);
                        else
                            throw this.NotImplementedException();
                    }
                });
                thread.Start();
            }
            catch (Exception e)
            {
                Disconnect();
                Console.WriteLine(e.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            _netStream.Close();
            Client.Close();
        }
    }
}
