using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using BloodDonors.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BloodDonors.Infrastructure.Repositories
{
    public class DonorRepository : IDonorRepository
    {
        private readonly BloodDonorsContext context;

        public DonorRepository(BloodDonorsContext context)
        {
            this.context = context;
        }

        public async Task<Donor> GetAsync(string pesel)
            => await context.Donors.FindAsync(pesel);

        public async Task<IEnumerable<Donor>> GetAllAsync()
            => await context.Donors.ToListAsync();

        public async Task AddAsync(Donor donor)
        {
            await context.Donors.AddAsync(donor);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Donor donor)
        {
            context.Donors.Update(donor);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string pesel)
        {
            var donor = await context.Donors.FindAsync(pesel);
            context.Donors.Remove(donor);
            await context.SaveChangesAsync();
        }
    }
}