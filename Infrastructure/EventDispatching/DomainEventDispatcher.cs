using ExchangeRate.Application.Interfaces;
using ExchangeRate.Domain.Event;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Infrastructure.EventDispatching
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch(IDomainEvent domainEvent)
        {
            var eventHandlers = _serviceProvider.GetServices<IEventHandler<HistoryItemAddedEvent>>();

            foreach (var handler in eventHandlers)
            {
                if (handler.GetType().GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericArguments().Contains(domainEvent.GetType())))
                {
                    await ((dynamic)handler).Handle((dynamic)domainEvent);
                }
            }
        }
    }
}
