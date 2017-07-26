using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using BloodDonors.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace BloodDonors.Infrastructure.Repositories
{
    public class PersonnelRepository : IPersonnelRepository
    {
        private readonly BloodDonorsContext context;

        public PersonnelRepository(BloodDonorsContext context)
        {
            this.context = context;
        }

        public async Task<Personnel> GetAsync(string pesel)
            => await context.Personnels.FindAsync(pesel);

        public async Task<IEnumerable<Personnel>> GetAllAsync()
            => await context.Personnels.ToListAsync();

        public async Task RegisterAsync(Personnel personnel)
        {
            await context.Personnels.AddAsync(personnel);
            await context.SaveChangesAsync();
        }
    }
}