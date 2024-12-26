using ExchangeRate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Donmain.Contract
{

    public interface ICurrencyCodeRepository : IGenericRepository<CurrencyCode>
    {
        Task<bool> IsLeaveTypeUnique(string name); 
        Task<bool> IsCurrencyCodeExisted(string name); 
        Task AddAllCurrencyCode(List<CurrencyCode> currencyCode);
    }
}
