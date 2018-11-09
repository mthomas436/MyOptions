using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptionTracker.Models;

namespace OptionTracker.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Trade> Trades { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<TradeType> TradeTypes { get; set; }
        public DbSet<OptionType> OptionTypes { get; set; }
        public DbSet<User> User { get; set; }

    }
}
