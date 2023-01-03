
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public interface IRootSimpleTraderViewModelFactory
    {
        public ViewModelBase CreateViewModel(ViewType viewType);
    }
}
