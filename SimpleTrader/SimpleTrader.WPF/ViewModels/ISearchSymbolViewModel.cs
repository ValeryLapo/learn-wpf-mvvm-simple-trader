using System.ComponentModel;

namespace SimpleTrader.WPF.ViewModels
{
    public interface ISearchSymbolViewModel : INotifyPropertyChanged
    {
        string Symbol { get; }
        string SearchResultSymbol { set; }
        double StockPrice { set; }
        string ErrorMessage { set; }
        bool CanSearchSymbol { get; }
    }
}