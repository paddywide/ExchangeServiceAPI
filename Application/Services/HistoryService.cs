using ExchangeRate.Application.Interfaces;
using ExchangeRate.Domain.Aggregate.History;
using ExchangeRate.Domain.GetConvertedMondy;
using ExchangeRate.Domain.Models;
using ExchangeRate.Donmain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.Services
{

    public class HistoryService : IHistoryService
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IQueryHistoryRepository _queryHistoryRepository;

        public HistoryService(IDomainEventDispatcher domainEventDispatcher, IQueryHistoryRepository queryHistoryRepository)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _queryHistoryRepository = queryHistoryRepository;
        }

        public async Task CreateHistory(QueryHistory queryHistory)
        {
            var order = new History("John Doe");
            order.AddItem("Laptop", "Laptop", (double)1, DateTime.Now);

            queryHistory.DateQueried = DateTime.Now;
            await _queryHistoryRepository.AddHistory(queryHistory);

            foreach (var domainEvent in order.DomainEvents)
            {
                await _domainEventDispatcher.Dispatch(domainEvent);
            }

            order.ClearDomainEvents();
        }
    }
}
