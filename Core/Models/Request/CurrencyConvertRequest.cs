using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Request
{
    //public record CurrencyConvertRequest(float Amount, string InputCurrency, string OutputCurrancy);
    public class CurrencyConvertRequest
    {
        public float Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrancy { get; set; }
    }
}
