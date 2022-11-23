using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
        private readonly IMajorIndexService _majorIndexService;

        private MajorIndex _apple;
        public MajorIndex Apple
        {
            get => _apple;
            set
            {
                _apple = value;
                OnPropertyChanged(nameof(Apple));
            }
        }


        private MajorIndex _amazon;
        public MajorIndex Amazon
        {
            get => _amazon;
            set
            {
                _amazon = value;
                OnPropertyChanged(nameof(Amazon));
            }
        }

        private MajorIndex _google;
        public MajorIndex Google
        {
            get => _google;
            set
            {
                _google = value;
                OnPropertyChanged(nameof(Google));
            }
        }

        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            _majorIndexService = majorIndexService;
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndexes();
            return majorIndexViewModel;
        }
        private void LoadMajorIndexes()
        {
            _majorIndexService.GetMajorIndex(MajorIndexType.Apple).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Apple = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.Amazon).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Amazon = task.Result;
                }
            });
            _majorIndexService.GetMajorIndex(MajorIndexType.Google).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Google = task.Result;
                }
            });
        }
    }
}
