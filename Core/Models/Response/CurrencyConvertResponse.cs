﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Response
{
    public class CurrencyConvertResponse()
    {
        public float Amount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrancy { get; set; }
        public float value { get; set; }
    }
}
