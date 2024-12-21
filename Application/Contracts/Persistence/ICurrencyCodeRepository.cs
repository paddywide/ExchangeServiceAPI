using ExchangeRate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.Contracts.Persistence
{

    public interface ICurrencyCodeRepository : IGenericRepository<CurrencyCode>
    {
        Task<bool> IsLeaveTypeUnique(string name);
        Task AddAllCurrencyCode(List<CurrencyCode> currencyCode);
    }
}
