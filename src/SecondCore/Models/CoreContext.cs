using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SecondCore.Models;
using Microsoft.Extensions.Configuration;

namespace SecondCore
{
    public class CoreContext : DbContext
    {
        private IConfigurationRoot _config;

        public CoreContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:CoreContextConnection"]);
        }
    }
}
