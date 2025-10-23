

using System;
using System.Windows.Input;

namespace CheckFileNameWithICommand
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _command;
        private readonly Func<T, bool> _canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<T> command, Func<T,bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException();
            _canExecute = canExecute;
            _command = command;
        }

        public void Execute(object parameter)
        {
            _command((T) parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T) parameter);
        }
    }
}