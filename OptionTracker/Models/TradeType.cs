using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models
{
    public class TradeType
    {
        public int TradeTypeId { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        public ICollection<Trade> Trades { get; set; }
    }
}
