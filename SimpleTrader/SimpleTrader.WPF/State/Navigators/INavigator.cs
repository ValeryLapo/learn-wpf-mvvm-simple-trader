﻿using SimpleTrader.WPF.ViewModels;
using System;

namespace SimpleTrader.WPF.State.Navigators
{
    public enum ViewType
    {
        Login,
        Home,
        Portfolio,
        Buy,
        Sell
    }
    public interface INavigator
    {
        event Action StateChanged;
        ViewModelBase CurrentViewModel { get; set; }
    }
}
