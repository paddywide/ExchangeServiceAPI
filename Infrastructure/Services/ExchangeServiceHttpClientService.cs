using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExchangeServiceHttpClientService(HttpClient httpClient, IHttpClientFactory httpClientFactory) : IExchangeServiceHttpClientService
    {
        public async Task<HttpResponseMessage> GetData()
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://v6.exchangerate-api.com/v6/53cf2953cdaa1c3f08d63816/latest/AUD");
            return await client.SendAsync(httpRequestMessage);
        }
    }
}
