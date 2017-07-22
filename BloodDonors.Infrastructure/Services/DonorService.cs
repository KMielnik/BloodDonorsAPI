using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodDonors.Core.Domain;
using BloodDonors.Infrastructure.DTO;
using BloodDonors.Core.Repositories;

namespace BloodDonors.Infrastructure.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepository donorRepository;
        private readonly IMapper mapper;

        public DonorService(IDonorRepository donorRepository,IMapper mapper)
        {
            this.donorRepository = donorRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DonorDTO>> GetAllAsync()
        {
            var donors = await donorRepository.GetAllAsync();
            return donors.Select(donor => mapper.Map<Donor, DonorDTO>(donor));
        }

        public async Task<DonorDTO> GetAsync(string pesel)
        {
            var donor = await donorRepository.GetAsync(pesel);
            return mapper.Map<Donor,DonorDTO>(donor);
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
