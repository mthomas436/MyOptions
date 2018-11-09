using Microsoft.CodeAnalysis.Options;
using OptionTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Data
{
    public interface IOptionsRepository
    {
        Task<Option> AddOption(Option option);
        Task<Option> GetOption(int optionid);
        Task<string> DeleteOption(Option option);
        Task<Option> UpdateOption(Option stock);
        Task<Trade> GetTrade(int tradeid);
        Task<IEnumerable<OptionType>> getOptionTypes();
        
    }
}
