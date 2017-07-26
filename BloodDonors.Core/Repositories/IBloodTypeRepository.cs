using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;

namespace BloodDonors.Core.Repositories
{
    public interface IBloodTypeRepository
    {
        Task<IEnumerable<BloodType>> GetAllAsync();
        Task AddAsync(BloodType bloodType);
    }
}