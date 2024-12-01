using MediatR;
using Core.Models.Response;

namespace Application.Business
{
    public record Convert(CurrencyConvertResponse exchangeRate) : IRequest<CurrencyConvertResponse>;
    public class ConvertHandler()
        : IRequestHandler<Convert, CurrencyConvertResponse>
    {
        public async Task<CurrencyConvertResponse> Handle(Convert request, CancellationToken cancellationToken)
        {
            return new CurrencyConvertResponse()
            {
                Amount = (float)0,
                InputCurrency = "",
                OutputCurrancy = "",
                value = (float)0
            };
        }
    }
}
