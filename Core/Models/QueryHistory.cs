using ExchangeRate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Domain.Models
{
    public class QueryHistory
    {
        public int Id { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrancy { get; set; }
        public double Rate { get; set; }
        public DateTime? DateQueried { get; set; }
    }
}
