using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleTrader.WPF.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public virtual void Dispose() { }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
