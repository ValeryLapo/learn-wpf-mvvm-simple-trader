﻿using SimpleTrader.Domain.Models;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services
{
    public interface IAccountService : IDataService<Account>
    {
        Task<Account> GetByUsername(string username);
        Task<Account> GetByEmail(string email);
    }
}
