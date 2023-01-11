using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;
        public SimpleTraderDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }


        public SimpleTraderDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();

            _configureDbContext(options);

            return new SimpleTraderDbContext(options.Options);
        }
    }
}