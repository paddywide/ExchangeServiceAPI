using ExchangeRate.Application.Interfaces;
using ExchangeRate.Domain.Aggregate.History;
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

        public HistoryService(IDomainEventDispatcher domainEventDispatcher)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task CreateHistory()
        {
            var order = new History("John Doe");
            order.AddItem("Laptop", "Laptop", (double)1, DateTime.Now);

            foreach (var domainEvent in order.DomainEvents)
            {
                await _domainEventDispatcher.Dispatch(domainEvent);
            }

            order.ClearDomainEvents();
        }
    }
}
