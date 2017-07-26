using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BloodDonors.Infrastructure.EntityFramework
{
    public class BloodDonorsContext : DbContext
    {
        private readonly IConfiguration configuration;

        public BloodDonorsContext(DbContextOptions<BloodDonorsContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (configuration.GetSection("sql:inMemory").Value == "true")
            {
                optionsBuilder.UseInMemoryDatabase();
                return;
            }
            optionsBuilder.UseSqlServer(configuration.GetSection("sql:connectionString").Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}