using Core.Interfaces;
using Core.Models.Response;
using Core.Models;
using MediatR;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using Application.Exception;
using Application.Contracts.CurrencyConvert;

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
                throw new BadRequestException("Invalid ExchangeRate Request", validationResult);

            var response = await _externalVendorRepository.GetExchangeRate();
            if (!response.IsSuccessStatusCode)
            {
                return new CurrencyConvertResponse();
            }

            var responseCurrency = await GetExchangeOneRateAsync(response, request.OutputCurrancy);
            float convertedAmount = CaculateConvertedAmount(responseCurrency, request.Amount);
            var ret = GenerateResponse(request, convertedAmount);
            return ret;
        }

        private CurrencyConvertResponse GenerateResponse(GetExchangeRateCommand request, float convertedAmount)
        {
            return new CurrencyConvertResponse()
            {
                Amount = (float)request.Amount,
                InputCurrency = request.InputCurrency,
                OutputCurrancy = request.OutputCurrancy,
                value = (float)convertedAmount
            };
        }

        private float CaculateConvertedAmount(float responseCurrency, float amount)
        {
            return responseCurrency * amount;
        }

        private async Task<float> GetExchangeOneRateAsync(HttpResponseMessage response, string OutputCurrancy)
        {
            var resp = await response.Content.ReadFromJsonAsync<ExchangeRate>();

            return resp.Conversion_rates.USD;
        }
    }

}
