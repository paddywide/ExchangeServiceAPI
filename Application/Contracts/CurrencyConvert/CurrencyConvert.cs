using MediatR;
using Core.Models.Response;

namespace Application.Contracts.CurrencyConvert
{
    public class CurrencyConvert : ICurrencyConvert
    {
        public bool GetRate(HttpResponseMessage email)
        {
            return true;
        }
    }
}
