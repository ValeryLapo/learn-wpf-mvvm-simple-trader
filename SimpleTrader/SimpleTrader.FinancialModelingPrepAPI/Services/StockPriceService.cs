﻿using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Services;
using SimpleTrader.FinancialModelingPrepAPI.Results;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private readonly FinancialModelingHttpClient _httpClient;
        public StockPriceService(FinancialModelingHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<double> GetPrice(string symbol)
        {
            string uri = "quote-short/" + symbol + "?apikey=a2a9ea418386d3583ff2f6db975fa03d";

            StockPriceResult stockPriceResult = await _httpClient.GetAsync<StockPriceResult>(uri);

            if (stockPriceResult.Price == 0)
            {
                throw new InvalidResponseException(symbol);
            }
            return stockPriceResult.Price;
        }
    }
}
