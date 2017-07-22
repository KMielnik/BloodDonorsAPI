using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using System.Linq;

namespace BloodDonors.Infrastructure.Repositories
{
    public class InMemoryDonorRepository : IDonorRepository
    {
        private static readonly ISet<Donor> donors = new HashSet<Donor>
        {
            new Donor("95011312121","pass","salt","Kamil",new BloodType("A",'+'),"i15fki@wp.pl","513696267"),
            new Donor("00321312831","pass","salt","Kasia",new BloodType("O",'-'),"kaska@wp.pl","512672581")
        };

        public async Task AddAsync(Donor donor)
            => await Task.FromResult(donors.Add(donor));

        public async Task<IEnumerable<Donor>> GetAllAsync()
            => await Task.FromResult(donors);

        public async Task<Donor> GetAsync(string pesel)
            => await Task.FromResult(donors.SingleOrDefault(x => x.Pesel == pesel));

        public async Task DeleteAsync(string pesel)
        {
            var donor = await GetAsync(pesel);
            donors.Remove(donor);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Donor donor)
        {
            await Task.CompletedTask;
        }
    }
}
