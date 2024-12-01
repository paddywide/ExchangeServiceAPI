using Core.Interfaces;
using Core.Models.Request;
using Core.Models.Response;
using Core.Models;
using MediatR;
using System.Net.Http.Json;

namespace Application.Query
{
    public record GetExchangeRateQuery(CurrencyConvertRequest convertRequest) : IRequest<CurrencyConvertResponse>;
    public class GetExchangeRateQueryHandler(IExternalVendorRepository externalVendorRepository)
        : IRequestHandler<GetExchangeRateQuery, CurrencyConvertResponse>
    {
        public async Task<CurrencyConvertResponse> Handle(GetExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var response = await externalVendorRepository.GetExchangeRate();
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadFromJsonAsync<ExchangeRate>();
                float convertedAmount = (resp.Conversion_rates.USD) * (request.convertRequest.Amount);
                return new CurrencyConvertResponse()
                {
                    Amount = (float)request.convertRequest.Amount,
                    InputCurrency = request.convertRequest.InputCurrency,
                    OutputCurrancy = request.convertRequest.OutputCurrancy,
                    value = (float)convertedAmount
                };
            }
            return new CurrencyConvertResponse();
        }
    }

}
