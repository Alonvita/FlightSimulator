using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface IClient
    {
        void Stop();
        /// <summary>
        /// Start().
        /// </summary>
        void Start();
        void SendCommand(String command);
    }
}
