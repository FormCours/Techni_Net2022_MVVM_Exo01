using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tools.MVVM.ViewModels
{
    // ViewModelBase => Met en place la méthode pour notifier la vue d'un changement
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            RaiseAllCanExecuteChanged();
        }

        protected abstract void RaiseAllCanExecuteChanged();
    }
}
