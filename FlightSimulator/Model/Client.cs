using System;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Net.NetworkInformation;

using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{
    public class Client : IClient
    {

        public Dictionary<string, double> pathRead = new Dictionary<string, double>();
        public Dictionary<string, string> SimulatorPathsDict = new Dictionary<string, string>();

        private Timer timer;
        private NetworkStream m_networkStream;
        private TcpClient m_client;
        private IPEndPoint ep;

        public Client()
        {
            ep = new IPEndPoint(IPAddress.Parse(Properties.Settings.Default.FlightServerIP),
                                                               Properties.Settings.Default.FlightCommandPort);
            m_client = new TcpClient();

            timer = new Timer(2000);
            timer.AutoReset = true;
            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, ep);

            SimulatorPathsDict.Add("aileron", "/controls/flight/aileron");
            SimulatorPathsDict.Add("elevator", "/controls/flight/elevator");
            SimulatorPathsDict.Add("rudder", "/controls/flight/rudder");
            SimulatorPathsDict.Add("throttle", "/controls/engines/current-engine/throttle");
        }

        /// <summary>
        /// OnTimedEvent(object sender, ElapsedEventArgs e, IPEndPoint ep).
        /// 
        /// </summary>
        /// <param name="sender"> sender as object</param>
        /// <param name="e"> ElapsedEventArgs </param>
        /// <param name="ep"> IPEndPoint </param>
        private void OnTimedEvent(object sender, ElapsedEventArgs e, IPEndPoint ep)
        {
            if ((TcpServerUtils.GetState(this.m_client) != TcpState.Closed) &&
                (TcpServerUtils.GetState(this.m_client) != TcpState.Unknown))
            {
                Console.WriteLine("Connected usccessful!");
                this.m_networkStream = m_client.GetStream();
                Timer timer = (Timer)sender; // Get the timer that fired the event
                timer.Stop(); // Stop the timer that fired the event
            }
            else
            {
                Console.WriteLine("Connecting...");
                try
                {
                    m_client.Connect(ep);
                #pragma warning disable CS0168 // The variable 'ex' is declared but never used
                }
                catch (System.Net.Sockets.SocketException ex) { };
                #pragma warning restore CS0168 // The variable 'ex' is declared but never used
            }
        }
        
        public void Start() { timer.Start(); }

        //method for sending data from joystick
        public void SetInfo(List<string> tokens)
        {
            if ((TcpServerUtils.GetState(this.m_client) != TcpState.Closed) &&
                (TcpServerUtils.GetState(this.m_client) != TcpState.Unknown))
            {
                string command = "set ";
                command += SimulatorPathsDict[tokens[0]] + " " + tokens[1] + "\r\n";
                byte[] byteTime = Encoding.ASCII.GetBytes(command.ToString());
                m_networkStream.Write(byteTime, 0, byteTime.Length);
            }
        }

        public void SendCommand(String command)
        {
            if ((TcpServerUtils.GetState(m_client) != TcpState.Closed) &&
                (TcpServerUtils.GetState(m_client) != TcpState.Unknown))
            {
                if (command.Length != 0)
                {
                    using (NetworkStream stream = new NetworkStream(m_client.Client, false))
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        byte[] data = Encoding.ASCII.GetBytes((command + "\r\n"));
                        Console.WriteLine(command);
                        writer.Write(data);
                        writer.Flush();
                    }
                }
            }
        }
        
        public void Stop()
        {
            if (TcpServerUtils.GetState(this.m_client) == TcpState.Established)
            {
                this.m_client.Close();
                this.m_client.Dispose();
            }
        }
    }
}