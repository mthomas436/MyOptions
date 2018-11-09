using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptionTracker.Models
{
    public class Option
    {
        [Display(Name="Id")]
        public int Optionid { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Display(Name="Strike Price")]
        public double StrikePrice { get; set; }

        [Required]
        [Display(Name="Quantity")]    
        [Range(1,int.MaxValue,ErrorMessage ="Quantity shouldbe greater than 0")]
        public int Quantity { get; set; }


        [Required]
        [Display(Name="Date Entered")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateEntered { get; set; }


        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity shouldbe greater than 0")]
        [Display(Name= "Option Price at Entry")]
        public double EntryPrice { get; set; }


        [Required]
        [Display(Name="Stock Price at Purchase")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity shouldbe greater than 0")]
        public double StockPriceatPurchace { get; set; }

        [Display(Name="Exit Price")]
        public double? ExitPrice { get; set; }


        [Display(Name="Commission")]
        public double? Commission { get; set; }

        [Display(Name="Date Closed")]
        [Column(TypeName = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateClosed { get; set; }


        [Display(Name="Notes")]
        public string Notes { get; set; }

        public Trade Trades { get; set; }
        public int Tradeid { get; set; }


        public OptionType OptionType { get; set; }

        [Display(Name = "Option Type")]
        public int OptionTypeId { get; set; }


    }
}
