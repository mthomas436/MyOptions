using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models
{
    public class OptionType
    {
        [Display(Name = "Type")]
        public int OptionTypeId { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        public ICollection<Option> Options { get; set; }
    }
}
