namespace SimpleTrader.WPF.ViewModels
{
    public interface ISearchSymbolViewModel
    {
        string Symbol { get; }
        string SearchResultSymbol { set; }
        double StockPrice { set; }
        string ErrorMessage { set; }
    }
}