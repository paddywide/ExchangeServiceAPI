using ExchangeRate.Application.Interfaces;
using ExchangeRate.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.EventHandlers.Domain
{

    public class HistoryItemAddedEventHandler : IEventHandler<HistoryItemAddedEvent>
    {
        public Task Handle(HistoryItemAddedEvent domainEvent)
        {
            Console.WriteLine($"InputCurrency {domainEvent.InputCurrency}: OutputCurrancy '{domainEvent.OutputCurrancy}' Rate {domainEvent.Rate}.");
            return Task.CompletedTask;
        }
    }
}
