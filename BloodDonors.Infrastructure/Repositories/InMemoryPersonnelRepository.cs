using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using System.Linq;

namespace BloodDonors.Infrastructure.Repositories
{
    public class InMemoryPersonnelRepository : IPersonnelRepository
    {
        private static readonly ISet<Personnel> personnels = new HashSet<Personnel>();

        public async Task<IEnumerable<Personnel>> GetAllAsync()
            => await Task.FromResult(personnels);

        public async Task<Personnel> GetAsync(string pesel)
            => await Task.FromResult(personnels.SingleOrDefault(x => x.Pesel == pesel));
    }
}
