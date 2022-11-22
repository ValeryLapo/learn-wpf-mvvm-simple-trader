using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using static System.Net.WebRequestMethods;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        //We will call to the API, deserialize the response to our MajorIndex
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using HttpClient client = new HttpClient();

            string uri = "https://financialmodelingprep.com/api/v3/quote/";
            string uriSuffix = GetUriSuffix(indexType);
            string apikey = "?apikey=a2a9ea418386d3583ff2f6db975fa03d";

            HttpResponseMessage response =
                await client.GetAsync(string.Concat(uri,uriSuffix,apikey));


            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<MajorIndex> majorIndex = JsonConvert.DeserializeObject<List<MajorIndex>>(jsonResponse);
            majorIndex[0].Type = indexType;
            return majorIndex[0];
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            return indexType switch
            {
                MajorIndexType.Apple => "AAPL",
                MajorIndexType.Facebook => "FB",
                MajorIndexType.Google => "GOOG",
                _ => throw new ArgumentOutOfRangeException(nameof(indexType), indexType, null)
            };
        }
    }
}
