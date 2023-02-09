using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Services;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels.Factories;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(CreateHomeViewModel);
                services.AddSingleton<PortfolioViewModel>();
                services.AddSingleton<BuyViewModel>();
                services.AddSingleton<SellViewModel>();
                services.AddSingleton<AssetSummaryViewModel>();
                services.AddSingleton<MainViewModel>();

                services.AddSingleton<CreateViewModel<HomeViewModel>>((serviceProvider) => () => serviceProvider.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<PortfolioViewModel>>((serviceProvider) => () => serviceProvider.GetRequiredService<PortfolioViewModel>());
                services.AddSingleton<CreateViewModel<BuyViewModel>>(serviceProvider =>() => serviceProvider.GetRequiredService<BuyViewModel>());
                services.AddSingleton<CreateViewModel<SellViewModel>>(serviceProvider => () => serviceProvider.GetRequiredService<SellViewModel>());
                services.AddSingleton<CreateViewModel<LoginViewModel>>(s => () => CreateLoginViewModel(s));
                services.AddSingleton<CreateViewModel<RegisterViewModel>>(s => () => CreateRegisterViewModel(s));

                services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();

                services.AddSingleton<ViewModelRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelRenavigator<HomeViewModel>>();
                services.AddSingleton<ViewModelRenavigator<RegisterViewModel>>();

            });

            return hostBuilder;
        }

        private static HomeViewModel CreateHomeViewModel(IServiceProvider services)
        {
            return new HomeViewModel(MajorIndexListingViewModel.LoadMajorIndexViewModel(services.GetRequiredService<IMajorIndexService>()),
                services.GetRequiredService<AssetSummaryViewModel>());
        }

        private static LoginViewModel CreateLoginViewModel(IServiceProvider services)
        {
            return new LoginViewModel(
                services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelRenavigator<HomeViewModel>>(),
                services.GetRequiredService<ViewModelRenavigator<RegisterViewModel>>());
        }

        private static RegisterViewModel CreateRegisterViewModel(IServiceProvider services)
        {
            return new RegisterViewModel(services.GetRequiredService<IAuthenticator>(),
                services.GetRequiredService<ViewModelRenavigator<LoginViewModel>>(),
                services.GetRequiredService<ViewModelRenavigator<LoginViewModel>>());
        }
    }
}
