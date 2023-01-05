using SimpleTrader.WPF.State.Assets;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;

        private readonly ObservableCollection<AssetViewModel> _assets;
        public IEnumerable<AssetViewModel> Assets => _assets;

        public double AccountBalance => _assetStore.AccountBalance;
        public AssetSummaryViewModel(AssetStore assetStore)
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetViewModel>();

            _assetStore.StateChanged += AssetStore_StateChanged;
            ResetAssets();
        }

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.ShareAmount : -a.ShareAmount)))
                .Where(a => a.Shares >0);

            _assets.Clear();
            foreach (var assetViewModel in assetViewModels)
            {
                _assets.Add(assetViewModel);
            }
        }
    }
}
