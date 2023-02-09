using SimpleTrader.WPF.State.Assets;

namespace SimpleTrader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public MajorIndexListingViewModel MajorIndexListingViewModel { get; }
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public HomeViewModel(MajorIndexListingViewModel majorIndexListingViewModel, AssetSummaryViewModel assetSummaryViewModel)
        {
            MajorIndexListingViewModel = majorIndexListingViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }

        public override void Dispose()
        {
            AssetSummaryViewModel.Dispose();
            MajorIndexListingViewModel.Dispose();
            base.Dispose();
        }


    }
}
