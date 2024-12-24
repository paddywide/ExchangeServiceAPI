using Application.Features.Money.Commands.GetConvertedMoney;
using AutoMapper;
using Core.Models.Response;
using ExchangeRate.Application.Features.Money.Commands.GetConvertedMoney;
using ExchangeRate.Domain.Models;

namespace ExchangeRate.Application.MappingProfiles
{
    public class QueryHistoryProfile : Profile
    {
        public QueryHistoryProfile()
        {
            CreateMap<GetExchangeRateCommand, CalculatedAmount>();
            CreateMap<CalculatedAmount, QueryHistory>();
            CreateMap<CalculatedAmount, CurrencyConvertResponse>();
        }
    }
}
