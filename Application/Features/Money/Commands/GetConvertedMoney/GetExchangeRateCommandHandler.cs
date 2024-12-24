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
using ExchangeRate.Application.Contracts.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using ExchangeRate.Application.Features.Money.Commands.GetConvertedMoney;

namespace Application.Features.Money.Commands.GetConvertedMoney
{
    public class GetExchangeRateCommandHandler
        : IRequestHandler<GetExchangeRateCommand, CurrencyConvertResponse>
    {
        private readonly IExternalVendorRepository _externalVendorRepository; 
        private readonly IQueryHistoryRepository _queryHistoryRepository;
        private readonly ICurrencyCodeRepository _currencyCodeRepository;
        private readonly IMapper _mapper;

        public GetExchangeRateCommandHandler(IExternalVendorRepository externalVendorRepository, IQueryHistoryRepository queryHistoryRepository
                                            , ICurrencyCodeRepository currencyCodeRepository, IMapper mapper)
        {
            _externalVendorRepository = externalVendorRepository;
            _queryHistoryRepository = queryHistoryRepository;
            _currencyCodeRepository = currencyCodeRepository;
            _mapper = mapper;
        }

        public async Task<CurrencyConvertResponse> Handle(GetExchangeRateCommand request, CancellationToken cancellationToken)
        {
            ValidateRequest(request);
            var response = await GetPublicExchangeRate(request.InputCurrency);
            var calculateRate = await CalculateRate(response, request);
            InsertIntoQueryHistory(calculateRate);
            var ret = _mapper.Map<CurrencyConvertResponse>(calculateRate);

            return ret;
        }

        private async void InsertIntoQueryHistory(CalculatedAmount calculateRate)
        {
            var queryHistoryToCreate = _mapper.Map<QueryHistory>(calculateRate);
            queryHistoryToCreate.DateQueried = DateTime.UtcNow;
            await _queryHistoryRepository.AddHistory(queryHistoryToCreate);
        }

        private async Task<CalculatedAmount> CalculateRate(HttpResponseMessage response, GetExchangeRateCommand request)
        {
            var rate = await GetExchangeOneRateAsync(response, request.OutputCurrancy);
            double convertedAmount = CaculateConvertedAmount(rate, request.Amount);
            var ret = GenerateResponse(request, convertedAmount, rate);
            return ret;
        }

        private async Task<HttpResponseMessage> GetPublicExchangeRate(string inputCurrency)
        {
            var response = await _externalVendorRepository.GetExchangeRate(inputCurrency);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpResponseNullException("Cannot get public exchange rate");
            }
            return response;
        }

        private async void ValidateRequest(GetExchangeRateCommand request)
        {
            var validator = new GetExchangeRateCommandValidator(_currencyCodeRepository, _queryHistoryRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
                throw new BadRequestException("Invalid ExchangeRate Request", validationResult);
        }

        private CalculatedAmount GenerateResponse(GetExchangeRateCommand request, double convertedAmount, double rate)
        {
            var ret = _mapper.Map<CalculatedAmount>(request);
            ret.value = convertedAmount;
            ret.Rate = rate;

            return ret;
        }

        private double CaculateConvertedAmount(double rate, double amount)
        {
            return rate * amount;
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
