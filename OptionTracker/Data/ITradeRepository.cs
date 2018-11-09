using OptionTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Data
{
    public interface ITradeRepository
    {
        Task<IEnumerable<Trade>> GetTrades(string userid);
        Task<Trade> GetTrade(int tradeid);
        Task<Trade> AddTrade(Trade trade);
        Task<string> DeleteTrade(int id);
        Task<Trade> UpdateTrade(Trade trade);

        Task<IEnumerable<TradeType>> GetTradeTypes();

        Task<IEnumerable<Option>> GetOptionDetail(int tradeid);

    }
}
