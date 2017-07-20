using System;
using System.Collections.Generic;
using System.Text;
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
            new Donor("97010311457","pass","salt","Kamil",new BloodType(),"ibiki@wp.pl","510692262"),
            new Donor("00321812831","pass","salt","Kasia",new BloodType(),"kaska@wp.pl","502672381")
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
