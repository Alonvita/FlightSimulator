using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model
{
    class FlightSimulatorModel
    {
        private TcpServer _server;
        public FlightBoardModel(CommandHandler ch)
        {
            this._server = new TcpServer(ch);
            //this._infoChannel.MyEvent += dh;
        }

        public void start(int port)
        {
            _server.Run(port);
        }

        public void stop()
        {
            _server.Disconnect();
        }
    }
}
