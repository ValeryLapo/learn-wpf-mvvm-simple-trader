using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
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
            services.AddSingleton<IDataService<Account>, AccountDataService>();
            services.AddSingleton<SimpleTraderDbContextFactory>();
            services.AddSingleton<IBuyStockService, BuyStockService>();
            services.AddSingleton<IMajorIndexService, MajorIndexService>();

            //The reason we not making it singleton because ViewModel has state
            //It keeps track of things. 
            services.AddScoped<MainViewModel>();
            services.AddScoped<BuyViewModel>();
            services.AddScoped<INavigator, Navigator>();

            //We Want to register as much as possible with our Dependency Injection Container
            services.AddSingleton<ISimpleTraderViewModelAbstractFactory, RootSimpleTraderViewModelFactory>();
            services.AddSingleton<ISimpleTraderViewModelFactory<HomeViewModel>, HomeViewModelFactory>();
            services.AddSingleton<ISimpleTraderViewModelFactory<PortfolioViewModel>, PortfolioViewModelFactory>();
            services.AddSingleton<ISimpleTraderViewModelFactory<MajorIndexListingViewModel>, MajorIndexListingViewModelFactory>();

            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));



            return services.BuildServiceProvider();
        }
    }
}
