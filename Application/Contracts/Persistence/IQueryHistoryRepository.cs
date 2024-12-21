using ExchangeRate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.Contracts.Persistence
{

    public interface IQueryHistoryRepository
    {
        Task AddHistory(QueryHistory queryHistory);
    }
}
