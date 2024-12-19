namespace Core.Interfaces;
public interface IExternalVendorRepository
{
    Task<HttpResponseMessage> GetExchangeRate(string inputCurrency);
}