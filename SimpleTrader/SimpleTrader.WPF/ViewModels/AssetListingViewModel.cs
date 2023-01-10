
using SimpleTrader.WPF.State.Assets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleTrader.WPF.ViewModels
{
    public class AssetListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<AssetViewModel> _assets;
        private readonly AssetStore _assetStore;
        private readonly Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> _filterAssets;
        public IEnumerable<AssetViewModel> Assets => _assets;

        public AssetListingViewModel(AssetStore assetStore) : this(assetStore, assets => assets)
        {
            _assetStore = assetStore;
        }

        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<AssetViewModel>, IEnumerable<AssetViewModel>> filterAssets)
        {
            _assetStore = assetStore;
            _assets = new ObservableCollection<AssetViewModel>();
            _filterAssets = filterAssets;

            _assetStore.StateChanged += AssetStore_StateChanged;
            ResetAssets();
        }

        private void AssetStore_StateChanged()
        {
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<AssetViewModel> assetViewModels = _assetStore.AssetTransactions
                .GroupBy(t => t.Asset.Symbol)
                .Select(g => new AssetViewModel(g.Key, g.Sum(a => a.IsPurchase ? a.ShareAmount : -a.ShareAmount)))
                .Where(a => a.Shares > 0)
                .OrderByDescending(a => a.Shares);

            assetViewModels = _filterAssets(assetViewModels);
            _assets.Clear();
            foreach (var assetViewModel in assetViewModels)
            {
                _assets.Add(assetViewModel);
            }
        }
    }
}
