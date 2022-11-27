using System;
using System.Collections.Generic;
using System.Text;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class RootSimpleTraderViewModelFactory : ISimpleTraderViewModelAbstractFactory
    {
        private readonly ISimpleTraderViewModelFactory<HomeViewModel> _homeViewModelFactory;
        private readonly ISimpleTraderViewModelFactory<PortfolioViewModel> _portfolioViewModelFactory;
        private readonly BuyViewModel _buyViewModel;

        public RootSimpleTraderViewModelFactory(ISimpleTraderViewModelFactory<HomeViewModel> homeViewModelFactory,
            ISimpleTraderViewModelFactory<PortfolioViewModel> portfolioViewModelFactory,
            BuyViewModel buyViewModel)
        {
            _homeViewModelFactory = homeViewModelFactory;
            _portfolioViewModelFactory = portfolioViewModelFactory;
            _buyViewModel = buyViewModel;
        }
        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _homeViewModelFactory.CreateViewModel(),
                ViewType.Portfolio => _portfolioViewModelFactory.CreateViewModel(),
                ViewType.Buy => _buyViewModel,
                _ => throw new ArgumentException("ViewType does not have a ViewModel", nameof(viewType))
            };
        }
    }
}
