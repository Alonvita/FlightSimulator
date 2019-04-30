using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Input;
using FlightSimulator.Model;


namespace FlightSimulator.ViewModels
{
    public class AutoPilotVM : BaseNotif
    {
        private ICommand m_okCmd;
        private ICommand m_clearCmd;

        // check if the user write or not in the textbox
        private bool m_isWrite = false;
        private String m_color;
        private String m_data = "";
        private String m_blank = "";

        /// <summary>
        /// ChangeColor.
        /// </summary>
        public String ChangeColor
        {
            get
            {
                if (m_blank != "")
                {
                    m_color = "Pink";
                }
                else
                {
                    m_color = "White";
                }
                return m_color;
            }
        }

        /// <summary>
        /// CommandsFromUser.
        /// </summary>
        public String CommandsFromUser
        {
            set
            {
                m_data = value;
                m_blank = value;
                m_isWrite = true;
                NotifyPropertyChanged("CommandsFromUser");
                NotifyPropertyChanged("ChangeColor");
            }
            get
            {
                if (!m_isWrite)
                {
                    NotifyPropertyChanged("ChangeColor");
                }
                return m_data;
            }

        }

        /// <summary>
        /// OnClickClear().
        /// </summary>
        public void OnClickClear()
        {
            m_blank = "";
        }

        /// <summary>
        /// ClearCmd property.
        /// </summary>
        public ICommand ClearCmd
        {
            get
            {
                return m_clearCmd ?? (m_clearCmd = new CommandHandler(() => ClearTextbox()));
            }
        }

        /// <summary>
        /// ClearTextbox().
        /// </summary>
        public void ClearTextbox()
        {
            CommandsFromUser = "";
            m_isWrite = false;
        }

        /// <summary>
        /// OkCommand property.
        /// </summary>
        public ICommand OkCommand
        {
            get
            {
                return m_okCmd ?? (m_okCmd = new CommandHandler(() => ParseCommands()));
            }
        }

        /// <summary>
        /// ParseCommands().
        /// Parses the commands that are passed in the data.
        /// </summary>
        private void ParseCommands()
        {
            String[] allCommands = Parse(m_data);
                

            Queue<String> tokens = new Queue<string>();
            foreach (String token in allCommands)
                tokens.Enqueue(token);
            SendData(tokens);
        }

        /// <summary>
        /// Parse(string data).
        /// Utility parse function
        ///
        /// </summary>
        /// <param name="data">data as string</param>
        /// <returns>an array of strings with the data, split by \r\n</returns>
        private String[] Parse(string data)
        {
            return data.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// SendData(Queue<String> tokens)
        /// </summary>
        /// <param name="tokens">a queue of strings</param>
        private void SendData(Queue<String> tokens)
        {
            var timer = new Timer
            {
                Interval = 2000, // send every 2 seconds

                AutoReset = true
            };


            timer.Elapsed += (sender, e) => OnTimedEvent(sender, e, tokens);
            if (tokens.Count != 0)
                timer.Enabled = true;
        }

        /// <summary>
        /// OnTimedEvent(Object source, ElapsedEventArgs e, Queue<string> commands).
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="e">args</param>
        /// <param name="commands">queue of strings</param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e, Queue<string> commands)
        {
            switch (commands.Count)
            {
                case 0:
                    ((Timer)source).Stop(); // Stop the timer that fired the event
                    break;
                default:
                    CommandSingleton.Instance.SendCommand(commands.Dequeue());
                    break;
            }
        }
    }
}