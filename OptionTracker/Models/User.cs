using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OptionTracker.Models
{
    public class User : IdentityUser
    {
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Display(Name = "Larst Name")]
        public string Lastname { get; set; }

        public string PhotoLocation { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Trade> Trades { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }     
    }
}