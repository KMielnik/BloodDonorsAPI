using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using BloodDonors.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BloodDonors.Infrastructure.Repositories
{
    public class BloodTypeRepository : IBloodTypeRepository
    {
        private readonly BloodDonorsContext context;

        public BloodTypeRepository(BloodDonorsContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<BloodType>> GetAllAsync()
            => await context.BloodTypes.ToListAsync();

        public async Task<BloodType> GetAsync(string aboType, string rhType)
        {
            var bloodType = context.BloodTypes
                .Where(x => x.AboType == aboType)
                .SingleOrDefault(x => x.RhType == rhType);

            return await Task.FromResult(bloodType);
        }

        public async Task AddAsync(BloodType bloodType)
        {
            await context.BloodTypes.AddAsync(bloodType);
            await context.SaveChangesAsync();
        }
    }
}