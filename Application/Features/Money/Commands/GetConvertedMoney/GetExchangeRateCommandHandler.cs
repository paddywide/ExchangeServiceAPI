using Core.Interfaces;
using Core.Models.Response;
using Core.Models;
using MediatR;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;

namespace Application.Features.Money.Commands.GetConvertedMoney
{
    public class GetExchangeRateQueryHandler
        : IRequestHandler<GetExchangeRateCommand, CurrencyConvertResponse>
    {
        private readonly IExternalVendorRepository _externalVendorRepository;

        public GetExchangeRateQueryHandler(IExternalVendorRepository externalVendorRepository)
        {
            _externalVendorRepository = externalVendorRepository;
        }

        public async Task<CurrencyConvertResponse> Handle(GetExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetExchangeRateCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new Exception( validationResult.Errors.ToArray().ToString());

            var response = await _externalVendorRepository.GetExchangeRate();
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadFromJsonAsync<ExchangeRate>();
                float convertedAmount = resp.Conversion_rates.USD * request.Amount;
                return new CurrencyConvertResponse()
                {
                    Amount = (float)request.Amount,
                    InputCurrency = request.InputCurrency,
                    OutputCurrancy = request.OutputCurrancy,
                    value = (float)convertedAmount
                };
            }
            return new CurrencyConvertResponse();
        }
    }

}
