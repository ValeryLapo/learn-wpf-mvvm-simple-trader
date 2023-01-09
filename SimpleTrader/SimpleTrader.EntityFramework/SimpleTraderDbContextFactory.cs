using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContextFactory
    {
        private readonly string _connectionString;
        public SimpleTraderDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SimpleTraderDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();

            //this method only available with sqlserver nuget package
            options.UseSqlServer(_connectionString);

            return new SimpleTraderDbContext(options.Options);
        }
    }
}