﻿using ExchangeRate.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Domain.Models
{
    public class CurrencyCode : BaseEntity
    {
        public string Code { get; set; }
        //public bool IsActive { get; set; }
    }
}
