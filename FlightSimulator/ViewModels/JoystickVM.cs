using System;
using System.Collections.Generic;

namespace FlightSimulator.ViewModels
{
    public class JoystickVM
    {
        private double m_aileron = 0;
        private double m_rudder = 0;
        private double m_throttle = 0;
        private double m_elevator = 0;
        
        /// <summary>
        /// Parameters getters and setters.
        /// </summary>
        public double Aileron
        {
            set
            {
                List<string> arg = new List<string>();
                m_aileron = value;
                arg.Add("aileron");
                arg.Add(m_aileron.ToString());
                CommandSingleton.Instance.SetInfo(arg);
            }
            get { return m_aileron; }
        }
        public double Rudder
        {
            set
            {
                List<string> arg = new List<string>();
                m_rudder = value;
                arg.Add("rudder");
                arg.Add(m_rudder.ToString());
                CommandSingleton.Instance.SetInfo(arg);
            }
            get { return m_rudder; }
        }
        public double Throttle
        {
            set
            {
                List<string> arg = new List<string>();
                m_throttle = value;
                arg.Add("throttle");
                arg.Add(m_throttle.ToString());
                CommandSingleton.Instance.SetInfo(arg);
            }
            get { return m_throttle; }
        }
        public double Elevator
        {
            set
            {
                List<string> arg = new List<string>();
                m_elevator = value;
                arg.Add("elevator");
                arg.Add(m_elevator.ToString());
                CommandSingleton.Instance.SetInfo(arg);
            }
            get { return m_elevator; }
        }
    }
}