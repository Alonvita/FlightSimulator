using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;


namespace FlightSimulator.Model
{
    /// <summary>
    /// TcpServerUtils.
    /// 
    /// A static class providing TCPServer with outter utility specs needed.
    /// </summary>
    public static class TcpServerUtils
    {
        //get tcp state
        public static TcpState GetState(this TcpClient tcpClient)
        {
            var foo = IPGlobalProperties.GetIPGlobalProperties()
              .GetActiveTcpConnections()
              .SingleOrDefault(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint));
            return foo != null ? foo.State : TcpState.Unknown;
        }
    }
}