using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Domain.Aggregate.History
{
    public class HistoryItem
    {
        public string InputCurrency { get; set; }
        public string OutputCurrancy { get; set; }
        public double Rate { get; set; }
        public DateTime? DateQueried { get; set; }

        public HistoryItem(string inputCurrency, string outputCurrancy, double rate, DateTime dateQueried)
        {
            InputCurrency = inputCurrency;
            OutputCurrancy = outputCurrancy;
            Rate = rate;
            DateQueried = dateQueried;
        }
    }
}
