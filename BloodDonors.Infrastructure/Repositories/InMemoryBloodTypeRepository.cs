using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Core.Repositories;
using BloodDonors.Core.Domain;

namespace BloodDonors.Infrastructure.Repositories
{
    public class InMemoryBloodTypeRepository : IBloodTypeRepository
    {
        private static readonly ISet<BloodType> bloodTypes = new HashSet<BloodType>();

        public async Task<IEnumerable<BloodType>> GetAllAsync()
            => await Task.FromResult(bloodTypes);

        public async Task AddAsync(BloodType bloodType)
            => await Task.FromResult(bloodTypes.Add(bloodType));
    }
}