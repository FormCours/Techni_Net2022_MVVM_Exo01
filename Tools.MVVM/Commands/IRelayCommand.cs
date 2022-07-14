using System.Windows.Input;

namespace Tools.MVVM.Commands
{
    public interface IRelayCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
