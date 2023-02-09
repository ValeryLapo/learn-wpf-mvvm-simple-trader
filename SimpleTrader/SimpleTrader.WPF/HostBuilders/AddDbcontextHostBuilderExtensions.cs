using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.EntityFramework;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddDbcontextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("sqlite");
                Action<DbContextOptionsBuilder> configureDbcontext = o => o.UseSqlite(connectionString);
                services.AddDbContext<SimpleTraderDbContext>(configureDbcontext);
                services.AddSingleton(new SimpleTraderDbContextFactory(configureDbcontext));
            });

            return host;
        }
    }
}
