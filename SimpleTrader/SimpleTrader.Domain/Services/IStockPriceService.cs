using System.Threading.Tasks;
using SimpleTrader.Domain.Exceptions;

namespace SimpleTrader.Domain.Services
{
    public interface IStockPriceService
    {
        /// <summary>
        /// Get the share price for a symbol.
        /// </summary>
        /// <param name="symbol">The symbol to het the price of.</param>
        /// <returns>The prive of symbol.</returns>
        /// <exception cref="InvalidSymbolException">Thrown if symbol does not exist</exception>
        /// <exception cref="Exception">Thrown if getting the Symbol fails.</exception>
        Task<double> GetPrice(string symbol);
    }
}
