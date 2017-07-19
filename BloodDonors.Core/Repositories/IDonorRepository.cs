using BloodDonors.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonors.Core.Repositories
{
    public interface IDonorRepository
    {
        Task<Donor> GetAsync(string pesel);
        Task<IEnumerable<Donor>> GetAllAsync();
        Task AddAsync(Donor donor);
        Task UpdateAsync(Donor donor);
        Task DeleteAsync(string pesel);
    }
}
