using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
 

namespace OptionTracker.Models.ViewModels
{
    public class CreateTradeVM
    {
        public Trade Trade { get; set; }
        public Option Option { get; set; }
        public List<SelectListItem> TradeTypes { get; set; }
        public List<SelectListItem> OptionTypes { get; set; }
        public IEnumerable<Option> Options { get; set; }

        public bool CloseTrade { get; set; }

        public double TotalTradeCost { get; set; }
        public double TotalExitPrice { get; set; }

        public CreateTradeVM()
        {
            this.TotalTradeCost = 0;
            this.TotalExitPrice = 0;
        }

    }


}
