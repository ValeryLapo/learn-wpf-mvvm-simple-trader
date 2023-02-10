using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.Commands
{
    public class LoadMajorIndexesCommand : AsyncCommandBase
    {
        private readonly MajorIndexListingViewModel _majorIndexListingViewModel;
        private readonly IMajorIndexService _majorIndexService;
        public LoadMajorIndexesCommand(MajorIndexListingViewModel majorIndexListingViewModel, IMajorIndexService majorIndexService)
        {
            _majorIndexListingViewModel = majorIndexListingViewModel;
            _majorIndexService = majorIndexService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _majorIndexListingViewModel.IsLoading = true;

            await Task.WhenAll(LoadAmazon(), LoadApple(), LoadGoogle());

            _majorIndexListingViewModel.IsLoading = false;
        }

        private async Task LoadApple()
        {
            _majorIndexListingViewModel.Apple = await _majorIndexService.GetMajorIndex(MajorIndexType.Apple);
        }

        private async Task LoadAmazon()
        {
            _majorIndexListingViewModel.Amazon = await _majorIndexService.GetMajorIndex(MajorIndexType.Amazon);
        }

        private async Task LoadGoogle()
        {
            _majorIndexListingViewModel.Google = await _majorIndexService.GetMajorIndex(MajorIndexType.Google);
        }
    }
}
