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

 
        public double? TotalTradeCost
        {
            get
            {
                double totalTradeCost = 0;
                foreach (var option in Options)
                {
                    totalTradeCost += option.OptionCost.Value;
                }
                return totalTradeCost;
            }
        }


        public double? TotalExitPrice
        {
            get
            {
                double totalExitPrice = 0;
                foreach (var option in Options)
                {
                    if (option.DateClosed != null)
                        totalExitPrice += option.ClosingTotalPrice.Value;
                }
                return totalExitPrice;
            }
        }


        public double? TotalProfitLoss
        {
            get
            {
                double totalPL = 0;
                foreach (var option in Options)
                {
                    if(option.DateClosed != null)
                        totalPL += option.ProfitLoss.Value;
                }
                   
                return totalPL;
            }
        }

    }


}
