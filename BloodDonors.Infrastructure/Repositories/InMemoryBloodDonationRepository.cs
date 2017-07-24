using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using System.Linq;

namespace BloodDonors.Infrastructure.Repositories
{
    public class InMemoryBloodDonationRepository : IBloodDonationRepository
    {
        private static readonly ISet<BloodDonation> bloodDonations = new HashSet<BloodDonation>();

        public async Task AddAsync(BloodDonation bloodDonation)
            => await Task.FromResult(bloodDonations.Add(bloodDonation));

        public async Task<IEnumerable<BloodDonation>> GetAllAsync()
            => await Task.FromResult(bloodDonations);

        public async Task<BloodDonation> GetAsync(Guid guid)
            => await Task.FromResult(bloodDonations.SingleOrDefault(x => x.Guid == guid));

        public async Task DeleteAsync(Guid guid)
        {
            var bloodDonation = await GetAsync(guid);
            bloodDonations.Remove(bloodDonation);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(BloodDonation bloodDonation)
        {
            await Task.CompletedTask;
        }
    }
}
