using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Domain.Models
{
    public class ExchangeRate
    {
        public string Result { get; set; }
        public string Base_code { get; set; }
        public string Time_last_update_utc { get; set; }
        public Conversion_Rates Conversion_rates { get; set; }
    }
}
