using System;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.State.Navigators
{
    public enum ViewType
    {
        Login,
        Home,
        Portfolio,
        Buy
    }
    public interface INavigator
    {
        event Action StateChanged;
        ViewModelBase CurrentViewModel { get; set; }
    }
}
