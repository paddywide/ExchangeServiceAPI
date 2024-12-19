using Core.Interfaces;
using Infrastructure.Services;

namespace Infrastructure.Repositories
{
    public class ExternalVendorRepository(
        IExchangeServiceHttpClientService exchangeServiceHttpClientService)
        : IExternalVendorRepository
    {
        public async Task<HttpResponseMessage> GetExchangeRate(string inputCurrency)
        {
            return await exchangeServiceHttpClientService.GetData(inputCurrency);
        }
    }
}
