using Application.Features.Money.Commands.GetConvertedMoney;
using AutoMapper;
using Core.Models.Response;
using ExchangeRate.Domain.GetConvertedMondy;
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
