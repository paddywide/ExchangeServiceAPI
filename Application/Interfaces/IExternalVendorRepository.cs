using ExchangeRate.Domain.Models;
using ExchangeRate.Domain.Primitive.Result;

namespace Core.Interfaces;
public interface IExternalVendorRepository
{
    Task<ResultT<ExchangeRateData>> GetExchangeRate(string inputCurrency);
}