using ExchangeRate.Domain.Guards;
using ExchangeRate.Domain.Primitive.Result;
using ExchangeRate.Domain.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeRate.Domain.Event;
using ExchangeRate.Domain.GetConvertedMondy;
using ExchangeRate.Domain.Models;
using ExchangeRate.Donmain.Contract;

namespace ExchangeRate.Domain.RequestedPublicRate
{

    public sealed class RequestedPublicRate : AggregateRoot
    {
        private readonly IQueryHistoryRepository _queryHistoryRepository;

        public RequestedPublicRate(IQueryHistoryRepository queryHistoryRepository)
        {
            _queryHistoryRepository = queryHistoryRepository;
        }

        public async void SaveToQueryHistory(QueryHistory queryHistory)
        {
            queryHistory.DateQueried = DateTime.UtcNow;
            await _queryHistoryRepository.AddHistory(queryHistory);


            AddDomainEvent(new QueryRateCompletedEvent());
        }

    }

}
