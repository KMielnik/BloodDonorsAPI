using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Core.Repositories;
using BloodDonors.Core.Domain;

namespace BloodDonors.Infrastructure.Repositories
{
    public class InMemoryBloodTypeRepository : IBloodTypeRepository
    {
        private static readonly ISet<BloodType> bloodTypes = new HashSet<BloodType>
        {
            new BloodType("A", '+'),
            new BloodType("B", '-'),
            new BloodType("AB", '-'),
            new BloodType("O", '-'),
            new BloodType("A", '+'),
            new BloodType("B", '+'),
            new BloodType("AB", '+'),
            new BloodType("O", '+')
        };

        public async Task<IEnumerable<BloodType>> GetAllAsync()
            => await Task.FromResult(bloodTypes);
    }
}