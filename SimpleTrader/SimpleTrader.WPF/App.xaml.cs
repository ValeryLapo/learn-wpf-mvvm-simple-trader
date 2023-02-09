using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;
using System.Windows;
using SimpleTrader.WPF.HostBuilders;

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
                .AddConfiguration()
                .AddDbContext()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews();
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
