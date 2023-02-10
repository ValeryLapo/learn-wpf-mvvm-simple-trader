using System.Threading.Tasks;
using System.Windows.Input;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.Commands;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel : ViewModelBase
    {
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

        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand LoadMajorIndexesCommand { get; }
        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            LoadMajorIndexesCommand = new LoadMajorIndexesCommand(this, majorIndexService);
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);

            majorIndexViewModel.LoadMajorIndexesCommand.Execute(null);

            return majorIndexViewModel;
        }


    }
}
