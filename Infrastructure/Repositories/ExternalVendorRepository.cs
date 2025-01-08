using Core.Interfaces;
using ExchangeRate.Domain.Errors;
using ExchangeRate.Domain.Models;
using Infrastructure.Services;
using System.Net.Http.Json;
using ExchangeRate.Domain.Primitive;
using ExchangeRate.Domain.Primitive.Result;

namespace Infrastructure.Repositories
{
    public class ExternalVendorRepository(
        IExchangeServiceHttpClientService exchangeServiceHttpClientService)
        : IExternalVendorRepository
    {
        public async Task<ResultT<ExchangeRateData>> GetExchangeRate(string inputCurrency)
        {
            using var response = await exchangeServiceHttpClientService.GetData(inputCurrency);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<ExchangeRateData>();
                return data;
            }
            else
            {
                return ResultT<ExchangeRateData>.Failure(Error.UnableGetPublicApiResponse("code 1","unable to get public API"));
            }
        }
    }
}
