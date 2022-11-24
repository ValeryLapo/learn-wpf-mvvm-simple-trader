using System.Windows;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            IStockPriceService stockPriceService= new StockPriceService();
            IDataService<Account> accountService = new AccountDataService(new SimpleTraderDbContextFactory());
            IBuyStockService buyStockService = new BuyStockService(stockPriceService, accountService);

            Account buyer = await accountService.Get(3);

            await buyStockService.BuyStock(buyer, "T", 50);
            MainWindow window = new MainWindow();
            window.DataContext = new MainViewModel();
            window.Show();

            new StockPriceService().GetPrice("AAPPL");



            base.OnStartup(e);
        }
    }
}
