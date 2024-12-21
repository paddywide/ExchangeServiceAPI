using Core.Interfaces;
using Core.Models.Response;
using Core.Models;
using MediatR;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using Application.Exception;
using Application.Contracts.CurrencyConvert;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ExchangeRate.Domain.Models;

namespace Application.Features.Money.Commands.GetConvertedMoney
{
    public class GetExchangeRateCommandHandler
        : IRequestHandler<GetExchangeRateCommand, CurrencyConvertResponse>
    {
        private readonly IExternalVendorRepository _externalVendorRepository;

        public GetExchangeRateCommandHandler(IExternalVendorRepository externalVendorRepository)
        {
            _externalVendorRepository = externalVendorRepository;
        }

        public async Task<CurrencyConvertResponse> Handle(GetExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetExchangeRateCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid ExchangeRate Request", validationResult);

            var response = await _externalVendorRepository.GetExchangeRate(request.InputCurrency);
            if (!response.IsSuccessStatusCode)
            {
                return new CurrencyConvertResponse();
            }

            var responseCurrency = await GetExchangeOneRateAsync(response, request.OutputCurrancy);
            double convertedAmount = CaculateConvertedAmount(responseCurrency, request.Amount);
            var ret = GenerateResponse(request, convertedAmount);


            return ret;
        }

        private CurrencyConvertResponse GenerateResponse(GetExchangeRateCommand request, double convertedAmount)
        {
            return new CurrencyConvertResponse()
            {
                Amount = (float)request.Amount,
                InputCurrency = request.InputCurrency,
                OutputCurrancy = request.OutputCurrancy,
                value = (double)convertedAmount
            };
        }

        private double CaculateConvertedAmount(double responseCurrency, double amount)
        {
            return responseCurrency * amount;
        }

        private async Task<double> GetExchangeOneRateAsync(HttpResponseMessage response, string OutputCurrancy)
        {
            var resp = await response.Content.ReadFromJsonAsync<ExchangeRateData>();
            if (resp is null)
                throw new NullReferenceException("GetExchangeRate Response is null");

            return resp.Conversion_rates.USD;
        }
    }

}
