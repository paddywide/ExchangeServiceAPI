using Core.Models.Response;
using ExchangeRate.Domain.Primitive.Result;
using MediatR;

namespace Application.Features.Money.Commands.GetConvertedMoney
{
    public record GetExchangeRateCommand : IRequest<ResultT<CurrencyConvertResponse>>
    {
        public float Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrancy { get; set; }
    }
}
