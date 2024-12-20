﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Persistence.DatabaseContext
{
    public class ExchangeRateDatabaseContext : DbContext
    {
        public DbSet<CurrencyCode> CurrencyCode { get; set;  }
        public ExchangeRateDatabaseContext(DbContextOptions<ExchangeRateDatabaseContext> options) : base(options)
        {
        }
    }
}