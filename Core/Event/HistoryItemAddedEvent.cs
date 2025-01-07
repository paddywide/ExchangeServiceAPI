using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExchangeRate.Domain.Event
{
    public class HistoryItemAddedEvent : IDomainEvent
    {

        public string InputCurrency { get; }
        public string OutputCurrancy { get; }
        public double Rate { get; }

        public HistoryItemAddedEvent(string inputCurrency, string outputCurrancy, double rate)
        {
            InputCurrency = inputCurrency;
            OutputCurrancy = outputCurrancy;
            Rate = rate;
        }

    }
}
