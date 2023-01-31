using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        public BuyViewModel(IStockPriceService stockPriveService, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriveService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService, accountStore);
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

        }

        private string _symbol;
        public string Symbol
        {
            get => _symbol;
            set
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));
            }
        }

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get => _searchResultSymbol;
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));
            }
        }


        private double _stockPrice;
        public double StockPrice
        {
            get => _stockPrice;
            set
            {
                _stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int _sharesToBuy;
        public int SharesToBuy
        {
            get => _sharesToBuy;
            set
            {
                _sharesToBuy = value;
                OnPropertyChanged(nameof(SharesToBuy));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public double TotalPrice
        {
            get => _sharesToBuy * _stockPrice;
        }
        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyStockCommand { get; set; }

        public MessageViewModel ErrorMessageViewModel { get; }
        public MessageViewModel StatusMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }

    }
}
