using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {

        private INavigator _navigator;
        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = viewType switch
                {
                    ViewType.Home => new HomeViewModel(MajorIndexViewModel.LoadMajorIndexViewModel(new MajorIndexService())),
                    ViewType.Portfolio => new PortfolioViewModel(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
