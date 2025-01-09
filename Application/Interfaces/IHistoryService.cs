using ExchangeRate.Domain.GetConvertedMondy;
using ExchangeRate.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Application.Interfaces
{
    public interface IHistoryService
    {
        Task CreateHistory(QueryHistory queryHistory);
    }
}
