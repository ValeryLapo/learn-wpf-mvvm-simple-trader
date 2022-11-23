using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        public async Task<double> GetPrice(string symbol)
        {
            using FinancialModelingHttpClient client = new FinancialModelingHttpClient();

            string uri = "quote-short/" +symbol+ "?apikey=a2a9ea418386d3583ff2f6db975fa03d";

            StockPriceResult stockPriceResult = await client.GetAsync<StockPriceResult>(uri);

            if (stockPriceResult.Price == 0)
            {
                throw new InvalidResponseException(symbol);
            }
            return stockPriceResult.Price;
        }
    }
}
