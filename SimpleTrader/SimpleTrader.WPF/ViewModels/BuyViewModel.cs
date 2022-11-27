using System.Windows.Input;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;

namespace SimpleTrader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase
    {
        public BuyViewModel(IStockPriceService stockPriveService, IBuyStockService buyStockService)
        {
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriveService);
            BuyStockCommand = new BuyStockCommand(this, buyStockService); 
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

        private double _stockPrice;
        public double StockPrice
        {
            get => _stockPrice;
            set
            {
                _stockPrice = value;
                OnPropertyChanged(nameof(StockPrice));
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
    }
}
