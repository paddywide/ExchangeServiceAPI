using ExchangeRate.Application.Contracts.Persistence;
using ExchangeRate.Domain.Models;
using ExchangeRate.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Persistence.Repositories
{
    public class CurrencyCodeRepository : GenericRepository<CurrencyCode>, ICurrencyCodeRepository
    {
        public CurrencyCodeRepository(ExchangeDatabaseContext context) : base(context)
        {
        }
        
        public async Task<bool> IsCurrencyCodeExisted(string code)
        {
            return await _context.CurrencyCode.AnyAsync(q => q.Code == code);
        }
        public async Task<bool> IsLeaveTypeUnique(string code)
        {
            return await _context.CurrencyCode.AnyAsync(q => q.Code == code) == false;
        }
        public async Task<List<CurrencyCode>> GetAllCurrencyCode()
        {
            var leaveRequest = await _context.CurrencyCode
                .ToListAsync(); ;

            return leaveRequest;
        }
        public async Task AddAllCurrencyCode(List<CurrencyCode> currencyCode)
        {
            List<CurrencyCode> notExistedInDB = currencyCode.Where(x => !_context.CurrencyCode.Any(y => y == x)).ToList();

            await _context.AddRangeAsync(notExistedInDB);
            await _context.SaveChangesAsync();
        }
    }
}
