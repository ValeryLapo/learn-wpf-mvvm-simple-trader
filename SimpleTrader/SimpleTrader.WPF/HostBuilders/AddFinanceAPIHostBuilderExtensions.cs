using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleTrader.FinancialModelingPrepAPI;

namespace SimpleTrader.WPF.HostBuilders
{
    public static class AddFinanceAPIHostBuilderExtensions
    {
        public static IHostBuilder AddFinanceAPI(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddHttpClient<FinancialModelingHttpClient>(c =>
                    c.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/"));
            });

            return host;
        }
    }
}
