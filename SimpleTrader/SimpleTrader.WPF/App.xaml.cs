using System;
using System.Windows;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            //GetService - returns null if service not found.
            //GetRequiredService - throws and Exception if service not found

            //IBuyStockService buyStockService = serviceProvider.GetRequiredService<IBuyStockService>();
            //our injection container have injected all services inside required service.

            //we are not going to pass service provider whenever we need to.
            //That is knows as a Service Located Pattern
            //!!! Dont't pass around your IServiceProvider !!!

            MainWindow window = serviceProvider.GetRequiredService<MainWindow>();

            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();
            //three ways of adding service
            //1. Singleton - one service per application
            //2. Transient - different instance everytime
            //3. Scoped - one instance per "scope"

            services.AddSingleton<IStockPriceService, StockPriceService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<IAccountService, AccountDataService>();
            services.AddSingleton<SimpleTraderDbContextFactory>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();

            services.AddSingleton<IPasswordHasher<string>, PasswordHasher<string>>();

            //The reason we not making it singleton because ViewModel has state
            //It keeps track of things. 
            services.AddScoped<MainViewModel>();
            services.AddScoped<BuyViewModel>();
            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();
            services.AddSingleton<IAccountStore, AccountStore>();

            //We Want to register as much as possible with our Dependency Injection Container
            services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();
            services.AddSingleton<BuyViewModel>(); 
            services.AddSingleton<PortfolioViewModel>();
            services.AddSingleton<HomeViewModel>(serviceProvider => new HomeViewModel(
                MajorIndexListingViewModel.LoadMajorIndexViewModel(
                    serviceProvider.GetRequiredService<IMajorIndexService>())));

            services.AddSingleton<CreateViewModel<HomeViewModel>>((serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<HomeViewModel>();
            });

            services.AddSingleton<CreateViewModel<PortfolioViewModel>>((serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<PortfolioViewModel>();
            });

            services.AddSingleton<CreateViewModel<BuyViewModel>>(serviceProvider =>
            {
                return () => serviceProvider.GetRequiredService<BuyViewModel>();
            });

            services.AddSingleton<ViewModelRenavigator<HomeViewModel>>();
            services.AddSingleton<CreateViewModel<LoginViewModel>>(serviceProvider =>
            {
                return () => new LoginViewModel(
                    serviceProvider.GetRequiredService<IAuthenticator>(),
                    serviceProvider.GetRequiredService<ViewModelRenavigator<HomeViewModel>>());
            });
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
