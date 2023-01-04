using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;
    public interface ISimpleTraderViewModelFactory
    {
        public ViewModelBase CreateViewModel(ViewType viewType);
    }
}
