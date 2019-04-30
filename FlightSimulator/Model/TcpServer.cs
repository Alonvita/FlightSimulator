using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FlightSimulator.Model
{
    public class TcpServer : IServer
    {
        private static int BUFFER_SIZE = 1024;
        private int m_port;
        private Thread m_thread = null;
        private string m_data;
        //Notify about data from the simulator
        public string Data
        {
            get { return m_data; }
            set { m_data = value; NotifyPropertyChanged("Data"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RunServer(int port)
        {
            this.m_port = port;
            try
            {
                // open the thread that the commands will be on a different thread
                m_thread = new Thread(Predicat);
                m_thread.IsBackground = true;
                m_thread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void Predicat(object obj)
        {
            TcpClient tcpClient = null;
            NetworkStream netStream = null;
            var listener = new TcpListener(IPAddress.Any, m_port);

            listener.Start();
            tcpClient = listener.AcceptTcpClient();
            netStream = tcpClient.GetStream();
            Console.WriteLine("Flight simulator is connected!");

            while (true)
            {
                try
                {
                    if (TcpServerUtils.GetState(tcpClient) == System.Net.NetworkInformation.TcpState.Closed)
                    {
                        Console.WriteLine("Client disconnected gracefully");
                        break;
                    }


                    Data = ReadStream(netStream);
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("netstream has died");
                }
            }

            tcpClient.Close();
            netStream.Close();
            listener.Stop();
            tcpClient.Dispose();
            netStream.Dispose();
        }

        //check if the tcp connection is closed
        private bool IsConnected(TcpClient tcp)
        {
            if (tcp.Client.Poll(0, SelectMode.SelectRead))
            {
                byte[] buff = new byte[1];
                if (tcp.Client.Receive(buff, SocketFlags.Peek) == 0)
                    return false;
            }

            return true;
        }

        private void NotifyPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        //Read data stream and decode is
        private string ReadStream(NetworkStream netstream)
        {
            byte[] buff = new byte[BUFFER_SIZE];

            int dataread = netstream.Read(buff, 0, buff.Length);
            string stringread = Encoding.UTF8.GetString(buff, 0, dataread);

            return stringread;
        }
    }
}