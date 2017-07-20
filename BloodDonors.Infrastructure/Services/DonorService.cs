using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Core.Repositories;

namespace BloodDonors.Infrastructure.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository donorRepository;

        public DonorService(IDonorRepository donorRepository)
        {
            this.donorRepository = donorRepository;
        }

        public async Task<IEnumerable<DonorDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetNameAsync(string pesel)
        {
            var donor = await donorRepository.GetAsync(pesel);
            return donor.Name;
        }

        public async Task<int> HowMuchDonated(string pesel)
        {
            throw new NotImplementedException();
        }

        public async Task LoginAsync(string pesel, string password)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(string pesel, string name, BloodTypeDTO bloodTypeDTO, 
            string mail, string phone, string passowrd, string salt)
        {
            throw new NotImplementedException();
        }

        public async Task<DateTime> WhenWillBeAbleToDonateAgainAsync(string pesel)
        {
            throw new NotImplementedException();
        }
    }
}
