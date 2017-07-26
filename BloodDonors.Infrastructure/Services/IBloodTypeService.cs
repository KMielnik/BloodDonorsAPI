using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public interface IBloodTypeService
    {
        Task<IEnumerable<BloodTypeDTO>> GetAllAsync();
        Task AddAsync(BloodTypeDTO bloodTypeDto);
    }
}