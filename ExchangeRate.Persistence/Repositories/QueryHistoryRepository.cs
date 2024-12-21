using ExchangeRate.Application.Contracts.Persistence;
using ExchangeRate.Domain.Models;
using ExchangeRate.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Persistence.Repositories
{
    public class QueryHistoryRepository : IQueryHistoryRepository
    {
        protected readonly ExchangeDatabaseContext _context;

        public QueryHistoryRepository(ExchangeDatabaseContext context) 
        {
            this._context = context;
        }


        public async Task AddHistory(QueryHistory queryHistory)
        {
            await _context.AddAsync(queryHistory);
            await _context.SaveChangesAsync();
        }

    }
}
