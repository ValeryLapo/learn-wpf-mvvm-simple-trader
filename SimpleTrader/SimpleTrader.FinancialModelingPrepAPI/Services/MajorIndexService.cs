using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private readonly FinancialModelingHttpClient _httpClient;
        public MajorIndexService(FinancialModelingHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //We will call to the API, deserialize the response to our MajorIndex
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {

            string uri = "quote/" + GetUriSuffix(indexType) + "?apikey=a2a9ea418386d3583ff2f6db975fa03d";

            MajorIndex majorIndex = await _httpClient.GetAsync<MajorIndex>(uri);
            majorIndex.Type = indexType;
            return majorIndex;
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            return indexType switch
            {
                MajorIndexType.Apple => "AAPL",
                MajorIndexType.Amazon => "AMZN",
                MajorIndexType.Google => "GOOG",
                _ => throw new Exception("MajorIndexType does not have suffix defined")
            };
        }
    }
}
