using MediatR;
using Core.Models.Response;
using System.Net.Mail;

namespace Application.Contracts.CurrencyConvert
{
    //public record CurrencyConvert(CurrencyConvertResponse exchangeRate) : IRequest<CurrencyConvertResponse>;
    //public class ConvertHandler()
    //    : IRequestHandler<CurrencyConvert, CurrencyConvertResponse>
    //{
    //    public async Task<CurrencyConvertResponse> Handle(CurrencyConvert request, CancellationToken cancellationToken)
    //    {
    //        return new CurrencyConvertResponse()
    //        {
    //            Amount = 0,
    //            InputCurrency = "",
    //            OutputCurrancy = "",
    //            value = 0
    //        };
    //    }
    //}
    public interface ICurrencyConvert
    {
        bool GetRate(HttpResponseMessage email);
    }

}
