using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExchangeServiceHttpClientService(HttpClient httpClient, IHttpClientFactory httpClientFactory, IConfiguration configuration) : IExchangeServiceHttpClientService
    {
        public async Task<HttpResponseMessage> GetData(string inputCurrency)
        {
            var client = httpClientFactory.CreateClient();
            string apiKey = GetConfiguration("ExchangeUri:ApiKey");
            string baseUri = GetConfiguration("ExchangeUri:BaseUri");
            string fullUri = baseUri + "/" + apiKey + "/latest/" + inputCurrency;

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, fullUri);
            return await client.SendAsync(httpRequestMessage);
        }

        private string GetConfiguration(string key)
        {
            return configuration[key];
        }
    }
}
