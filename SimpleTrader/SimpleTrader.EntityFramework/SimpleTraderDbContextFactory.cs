using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace SimpleTrader.EntityFramework
{
    public class SimpleTraderDbContextFactory: IDesignTimeDbContextFactory<SimpleTraderDbContext>
    {
        public SimpleTraderDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<SimpleTraderDbContext>();

            //this method only available with sqlserver nuget package
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SimpleTraderDB;Trusted_Connection=True");

            return new SimpleTraderDbContext(options.Options);
        }
    }
}
