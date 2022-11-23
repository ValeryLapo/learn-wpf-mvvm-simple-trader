using System.Windows;
using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.ViewModels;

namespace SimpleTrader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow window = new MainWindow();
            window.DataContext = new MainViewModel();
            window.Show();

            new StockPriceService().GetPrice("AAPPL");



            base.OnStartup(e);
        }
    }
}
