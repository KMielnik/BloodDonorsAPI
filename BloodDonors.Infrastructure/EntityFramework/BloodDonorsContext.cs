﻿using BloodDonors.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BloodDonors.Infrastructure.EntityFramework
{
    public class BloodDonorsContext : DbContext
    {
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<BloodDonation> BloodDonations { get; set; }

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
            modelBuilder.Entity<BloodType>()
                .HasKey(x => new {x.AboType, x.RhType});

            modelBuilder.Entity<Donor>(
                entity =>
                {
                    entity.HasKey(x => x.Pesel);
                });

            modelBuilder.Entity<Personnel>()
                .HasKey(x => x.Pesel);

            modelBuilder.Entity<BloodDonation>()
                .HasKey(x => x.Id);
        }
    }
}