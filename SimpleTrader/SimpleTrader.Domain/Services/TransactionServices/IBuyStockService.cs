﻿using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Exceptions;
using System.Threading.Tasks;
using System;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public interface IBuyStockService
    {
        /// <summary>
        /// Purchase a stock for an account.
        /// </summary>
        /// <param name="buyer">The account of the buyer.</param>
        /// <param name="stock">The symbol purchased.</param>
        /// <param name="shares">The amount of shares.</param>
        /// <returns>The updated account</returns>
        /// <exception cref="InsufficientFundsException">Thrown if the account has an insufficient balance</exception>
        /// <exception cref="InvalidSymbolException">Thrown if the purchased wymbol is invalid.</exception>
        /// <exception cref="Exception">Thrown if the putchsed symbol is invalid.</exception>
        Task<Account> BuyStock(Account buyer, string stock, int shares);
    }
}
