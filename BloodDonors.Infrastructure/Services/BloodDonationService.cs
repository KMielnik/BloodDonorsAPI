using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public class BloodDonationService : IBloodDonationService
    {
        public async Task<IEnumerable<BloodDonationDTO>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> HowMuchBloodHasBeenDonatedEver()
        {
            throw new System.NotImplementedException();
        }
    }
}