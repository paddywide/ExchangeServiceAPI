﻿using Core.Interfaces;
using Core.Models.Response;
using MediatR;
using System.Net.Http.Json;
using ExchangeRate.Domain.Models;
using ExchangeRate.Donmain.Contract;
using AutoMapper;
using ExchangeRate.Domain.Primitive.Result;
using FluentValidation.Results;
using ExchangeRate.Domain.Errors;
using ExchangeRate.Domain.GetConvertedMondy;
using ExchangeRate.Application.Interfaces;
using ExchangeRate.Application.Services;

namespace Application.Features.Money.Commands.GetConvertedMoney
{
    public class GetExchangeRateCommandHandler
        : IRequestHandler<GetExchangeRateCommand, ResultT<CurrencyConvertResponse>>
    {
        private readonly IExternalVendorRepository _externalVendorRepository; 
        private readonly IQueryHistoryRepository _queryHistoryRepository;
        private readonly ICurrencyCodeRepository _currencyCodeRepository;
        private readonly IHistoryService _historyService;
        private readonly IMapper _mapper;

        public GetExchangeRateCommandHandler(IExternalVendorRepository externalVendorRepository, IQueryHistoryRepository queryHistoryRepository
                                           , IMapper mapper , ICurrencyCodeRepository currencyCodeRepository, IHistoryService historyService)
        {
            _externalVendorRepository = externalVendorRepository;
            _queryHistoryRepository = queryHistoryRepository;
            _currencyCodeRepository = currencyCodeRepository;
            _historyService = historyService;
            _mapper = mapper;
        }

        public async Task<ResultT<CurrencyConvertResponse>> Handle(GetExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await ValidateRequest(request);
            if (validationResult.Errors.Any())
            {
                return ConfigurationErrors.RequestValidationError(validationResult.Errors);
            }

            var response = await _externalVendorRepository.GetExchangeRate(request.InputCurrency);
            if (!response.IsSuccess)
            {
                return ConfigurationErrors.UnableGetPublicApiResponse;
            }
            
            var calculateRate = await CalculateRate(response, request);
            var queryHistoryToCreate = _mapper.Map<QueryHistory>(calculateRate);
            await _historyService.CreateHistory(queryHistoryToCreate);
            var ret = _mapper.Map<CurrencyConvertResponse>(calculateRate);

            return ret;
        }

        private async Task<CalculatedAmount> CalculateRate(ResultT<ExchangeRateData> response, GetExchangeRateCommand request)
        {
            var rate = await GetExchangeOneRateAsync(response, request.OutputCurrancy);
            double convertedAmount = CaculateConvertedAmount(rate, request.Amount);
            var ret = GenerateResponse(request, convertedAmount, rate);
            return ret;
        }

        private async Task<ValidationResult> ValidateRequest(GetExchangeRateCommand request)
        {
            var validator = new GetExchangeRateCommandValidator(_currencyCodeRepository);
            var result = await validator.ValidateAsync(request);
            return result;
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

        private async Task<double> GetExchangeOneRateAsync(ResultT<ExchangeRateData> response, string OutputCurrancy)
        {
            if (response.Value is null)
                throw new NullReferenceException("GetExchangeRate Response is null");

            return response.Value.Conversion_rates.USD;
        }
        private async void InsertIntoQueryHistory(CalculatedAmount calculateRate)
        {
            var queryHistoryToCreate = _mapper.Map<QueryHistory>(calculateRate);
            queryHistoryToCreate.DateQueried = DateTime.UtcNow;
            await _queryHistoryRepository.AddHistory(queryHistoryToCreate);
        }
    }
}
