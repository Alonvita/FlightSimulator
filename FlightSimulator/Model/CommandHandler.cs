using System;
using System.Net.Sockets;
using System.Windows.Input;

namespace FlightSimulator.Model
{
    public class CommandHandler : ICommand
    {
        private Action _action;

        public CommandHandler(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            if (_action != null)
                return true;
            return false;
        }

#pragma warning disable CS0067 // The event 'CommandHandler.CanExecuteChanged' is never used
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067 // The event 'CommandHandler.CanExecuteChanged' is never used

        public void Execute(object parameter)
        {
            _action();
        }
    }
}