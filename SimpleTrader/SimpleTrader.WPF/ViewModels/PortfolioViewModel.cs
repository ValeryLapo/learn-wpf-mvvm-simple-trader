using SimpleTrader.WPF.State.Assets;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleTrader.WPF.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {
        
        
        public AssetListingViewModel AssetListingViewModel { get; }

        public PortfolioViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
        }
        }
}
