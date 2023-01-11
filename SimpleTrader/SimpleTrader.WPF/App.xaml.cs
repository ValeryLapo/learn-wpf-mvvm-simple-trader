﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.AuthenticationServices;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.State.Assets;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Windows;
using System.Windows.Media;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c =>
                {
                    c.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((context, services) =>
                {
                    string connectionString = context.Configuration.GetConnectionString("sqlite");
                    Action<DbContextOptionsBuilder> configureDbcontext =  o => o.UseSqlite(connectionString);
                    services.AddDbContext<SimpleTraderDbContext>(configureDbcontext);
                    services.AddSingleton(new SimpleTraderDbContextFactory(configureDbcontext));

                    services.AddSingleton<IStockPriceService, StockPriceService>();
                    services.AddSingleton<IAuthenticationService, AuthenticationService>();
                    services.AddSingleton<IDataService<Account>, AccountDataService>();
                    services.AddSingleton<IAccountService, AccountDataService>();
                    services.AddSingleton<IBuyStockService, BuyStockService>();
                    services.AddSingleton<ISellStockService, SellStockService>();
                    services.AddSingleton<IMajorIndexService, MajorIndexService>();

                    services.AddSingleton<IPasswordHasher<string>, PasswordHasher<string>>();

                    //The reason we not making it singleton because ViewModel has state
                    //It keeps track of things. 
                    services.AddScoped<MainViewModel>();
                    services.AddSingleton<INavigator, Navigator>();
                    services.AddSingleton<IAuthenticator, Authenticator>();
                    services.AddSingleton<IAccountStore, AccountStore>();
                    services.AddSingleton<AssetStore>();

                    //We Want to register as much as possible with our Dependency Injection Container
                    services.AddSingleton<ISimpleTraderViewModelFactory, SimpleTraderViewModelFactory>();
                    services.AddSingleton<BuyViewModel>();
                    services.AddSingleton<PortfolioViewModel>();
                    services.AddSingleton<AssetSummaryViewModel>();
                    services.AddSingleton<HomeViewModel>(serviceProvider => new HomeViewModel(
                        MajorIndexListingViewModel.LoadMajorIndexViewModel(serviceProvider.GetRequiredService<IMajorIndexService>()),
                        serviceProvider.GetRequiredService<AssetSummaryViewModel>()));

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

                    services.AddSingleton<ViewModelRenavigator<LoginViewModel>>();
                    services.AddSingleton<CreateViewModel<RegisterViewModel>>(serviceProvider =>
                    {
                        return () => new RegisterViewModel(serviceProvider.GetRequiredService<IAuthenticator>(),
                            serviceProvider.GetRequiredService<ViewModelRenavigator<LoginViewModel>>(),
                            serviceProvider.GetRequiredService<ViewModelRenavigator<LoginViewModel>>());
                    });

                    services.AddSingleton<ViewModelRenavigator<HomeViewModel>>();
                    services.AddSingleton<ViewModelRenavigator<RegisterViewModel>>();
                    services.AddSingleton<CreateViewModel<LoginViewModel>>(serviceProvider =>
                    {
                        return () => new LoginViewModel(
                            serviceProvider.GetRequiredService<IAuthenticator>(),
                            serviceProvider.GetRequiredService<ViewModelRenavigator<HomeViewModel>>(),
                            serviceProvider.GetRequiredService<ViewModelRenavigator<RegisterViewModel>>());
                    });
                    services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                });
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            //IServiceProvider serviceProvider = CreateServiceProvider();

            //GetService - returns null if service not found.
            //GetRequiredService - throws and Exception if service not found

            //IBuyStockService buyStockService = serviceProvider.GetRequiredService<IBuyStockService>();
            //our injection container have injected all services inside required service.

            //we are not going to pass service provider whenever we need to.
            //That is knows as a Service Located Pattern
            //!!! Dont't pass around your IServiceProvider !!!

            //MainWindow window = serviceProvider.GetRequiredService<MainWindow>();
            _host.Start();

            SimpleTraderDbContextFactory contextFactory =
                _host.Services.GetRequiredService<SimpleTraderDbContextFactory>();
            using (SimpleTraderDbContext context = contextFactory.CreateDbContext())
            {
                context.Database.Migrate();
            }
            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }


    }
}
