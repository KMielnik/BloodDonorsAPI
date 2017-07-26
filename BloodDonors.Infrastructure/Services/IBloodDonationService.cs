using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public interface IBloodDonationService
    {
        Task<IEnumerable<BloodDonationDTO>> GetAllAsync();
        Task<int> HowMuchBloodHasBeenDonatedEver();
        Task AddBloodDonationAsync(DateTime dateOfDonation, int volume, string donorPesel,
            string personnelPesel);

        Task<int> HowMuchBloodTakenByPersonnel(string pesel);
        Task<IEnumerable<DonorScoreDTO>> GetHonoraryDonorsAsync();
    }
}