using ExchangeRate.Domain.Event;
using ExchangeRate.Domain.Primitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Domain.Aggregate.History
{
    public class History : AggregateRoot
    {
        //public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public List<HistoryItem> Items { get; private set; } = new();

        public History(string customerName)
        {
            //Id = Guid.NewGuid();
            CustomerName = customerName;
        }

        public void AddItem(string inputCurrency, string outputCurrancy, double rate, DateTime dateQueried)
        {
            var item = new HistoryItem(inputCurrency, outputCurrancy, rate, dateQueried);
            Items.Add(item);

            // Add domain event
            AddDomainEvent(new HistoryItemAddedEvent(inputCurrency, outputCurrancy, rate));
        }
    }
}
