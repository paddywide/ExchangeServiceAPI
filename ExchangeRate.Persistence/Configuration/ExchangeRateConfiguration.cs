using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeRate.Domain.Models;

namespace ExchangeRate.Persistence.Configurations
{
    internal class ExchangeRateConfiguration : IEntityTypeConfiguration<CurrencyCode>
    {
        public void Configure(EntityTypeBuilder<CurrencyCode> builder)
        {
            builder.HasData(
                new CurrencyCode
                {
                    Id = 1,
                    Code = "AUD",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                },
                 new CurrencyCode
                 {
                     Id = 2,
                     Code = "USD",
                     DateCreated = DateTime.Now,
                     DateModified = DateTime.Now
                 }
            );

            builder.Property(q => q.Code)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(3);
        }
    }
}
