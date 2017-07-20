using BloodDonors.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonors.Core.Repositories
{
    public interface IBloodDonationRepository
    {
        Task<BloodDonation> GetAsync(int id);
        Task<IEnumerable<BloodDonation>> GetAllAsync();
        Task AddAsync(BloodDonation bloodDonation);
        Task UpdateAsync(BloodDonation bloodDonation);
        Task DeleteAsync(int id);
    }
}
