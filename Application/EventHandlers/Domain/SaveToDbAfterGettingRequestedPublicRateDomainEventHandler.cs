using AutoMapper;
using ExchangeRate.Application.Contracts;
using ExchangeRate.Domain.Event;
using ExchangeRate.Domain.Models;
using ExchangeRate.Donmain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.EventHandlers.Domain
{

    internal sealed class SaveToDbAfterGettingRequestedPublicRateDomainEventHandler
        : IDomainEventHandler<QueryRateCompletedEvent>
    {
        private readonly IQueryHistoryRepository _queryHistoryRepository;
        private readonly IMapper _mapper;

        public SaveToDbAfterGettingRequestedPublicRateDomainEventHandler(
            IQueryHistoryRepository queryHistoryRepository, IMapper mapper)
        {
            _queryHistoryRepository = queryHistoryRepository;
            _mapper = mapper;
        }

        public async Task Handle(QueryRateCompletedEvent notification, CancellationToken cancellationToken)
        {
            int i = 0;
        }
    }
}
