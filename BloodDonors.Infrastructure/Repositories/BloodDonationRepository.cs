using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using BloodDonors.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BloodDonors.Infrastructure.Repositories
{
    public class BloodDonationRepository : IBloodDonationRepository
    {
        private readonly BloodDonorsContext context;

        public BloodDonationRepository(BloodDonorsContext context)
        {
            this.context = context;
        }

        public async Task<BloodDonation> GetAsync(Guid id)
            => await context.BloodDonations
                .Include(x => x.BloodType)
                .Include(x => x.BloodTaker)
                .Include(x => x.Donor)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<BloodDonation>> GetAllAsync()
            => await context.BloodDonations
            .Include(x=>x.BloodType)
            .Include(x=>x.BloodTaker)
            .Include(x=>x.Donor)
            .ToListAsync();

        public async Task AddAsync(BloodDonation bloodDonation)
        {
            await context.BloodDonations.AddAsync(bloodDonation);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BloodDonation bloodDonation)
        {
            context.BloodDonations.Update(bloodDonation);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var bloodDonation = await context.BloodDonations.FindAsync(id);
            context.BloodDonations.Remove(bloodDonation);
            await context.SaveChangesAsync();
        }
    }
}