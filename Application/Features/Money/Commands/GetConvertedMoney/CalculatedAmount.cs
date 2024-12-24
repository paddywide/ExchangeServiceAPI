using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.Features.Money.Commands.GetConvertedMoney
{
    public class CalculatedAmount
    {
        public float Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrancy { get; set; }
        public double value { get; set; }
        public double Rate { get; set; }
    }
}
