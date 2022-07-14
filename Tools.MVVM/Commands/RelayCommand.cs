using System;

namespace Tools.MVVM.Commands
{
    public class RelayCommand : IRelayCommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action _Execute;
        private readonly Func<bool>? _CanExecute;

        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            if(execute is null) throw new ArgumentNullException(nameof(execute));

            _Execute = execute;
            _CanExecute = canExecute;
        }


        public void Execute(object? parameter)
        {
            _Execute();
        }

        public bool CanExecute(object? parameter)
        {
            return _CanExecute is null ? true : _CanExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
