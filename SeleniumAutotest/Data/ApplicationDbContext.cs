using Microsoft.EntityFrameworkCore;

using SeleniumAutotest.Core.Scenarios;
using SeleniumAutotest.Data.Models;

namespace SeleniumAutotest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<ScenarioAction> ScenarioActions { get; set; }
        public DbSet<ScenarioJournal> ScenarioJournals { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
