using Newtonsoft.Json;
using SimpleTrader.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingHttpClient : HttpClient
    {
        public FinancialModelingHttpClient()
        {
            this.BaseAddress = new Uri("https://financialmodelingprep.com/api/v3/");
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response =
                await GetAsync(string.Concat(uri));


            string jsonResponse = await response.Content.ReadAsStringAsync();
            var deserializedResponse = JsonConvert.DeserializeObject<List<T>>(jsonResponse);

            if (deserializedResponse.Count == 0)
            {
                throw new InvalidResponseException(uri);
            }
            return deserializedResponse[0];
        }
    }
}
