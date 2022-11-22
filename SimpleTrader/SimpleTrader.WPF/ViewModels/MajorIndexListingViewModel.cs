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


        private MajorIndex _facebook;
        public MajorIndex Facebook
        {
            get => _facebook;
            set
            {
                _facebook = value;
                OnPropertyChanged(nameof(Facebook));
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
            _majorIndexService.GetMajorIndex(MajorIndexType.Facebook).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Facebook = task.Result;
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
