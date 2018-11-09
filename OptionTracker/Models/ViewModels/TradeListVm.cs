using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models.ViewModels
{
    public class TradeListVm
    {
        public Trade Trade { get; set; }
        public IEnumerable<Trade> Trades { get; set; }
        public IEnumerable<TradeType> TradeTypes { get; set; }
    }
}
