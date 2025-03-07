﻿using SimpleTrader.Domain.Models;
using System;
using SimpleTrader.Domain.Exceptions;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public interface ISellStockService
    {
        /// <summary>
        /// Sell a stock for an account
        /// </summary>
        /// <param name="seller">The account of the seller.</param>
        /// <param name="symbol">The symbol sold.</param>
        /// <param name="shares">The amount of shares to sell</param>
        /// <returns>The updated account.</returns>

        /// <exception cref="InsufficientSharesException">Thrown if the seller has insufficient shares.</exception>
        /// <exception cref="InvalidSymbolException">Thrown if the purchased wymbol is invalid.</exception>
        /// <exception cref="Exception">Thrown if the putchsed symbol is invalid.</exception>
        Task<Account> SellStock(Account seller, string symbol, int shares);
    }
}
