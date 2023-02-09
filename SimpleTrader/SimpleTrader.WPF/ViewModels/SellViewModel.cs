using System.Windows.Input;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;

namespace SimpleTrader.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        public AssetListingViewModel AssetListingViewModel { get; }

        private AssetViewModel _selectedAsset;

        public AssetViewModel SelectedAsset
        {
            get
            {
                return _selectedAsset;
            }
            set
            {
                _selectedAsset = value;
                OnPropertyChanged(nameof(SelectedAsset));
            }
        }

        public string Symbol => SelectedAsset.Symbol;


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

        private int _sharesToSell;
        public int SharesToSell
        {
            get => _sharesToSell;
            set
            {
                _sharesToSell = value;
                OnPropertyChanged(nameof(SharesToSell));
                OnPropertyChanged(nameof(TotalPrice));
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
        public double TotalPrice
        {
            get => _sharesToSell * _stockPrice;
        }

        public ICommand SearchSymbolCommand { get; }
        public ICommand SellStockCommand { get; }
        public SellViewModel(AssetStore assetStore, IStockPriceService stockPriceService, IAccountStore accountStore, ISellStockService sellStockService)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
            SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
            SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();
        }

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

        public override void Dispose()
        {
            AssetListingViewModel.Dispose();
            StatusMessageViewModel.Dispose();
            ErrorMessageViewModel.Dispose();
            base.Dispose();
        }
    }
}
