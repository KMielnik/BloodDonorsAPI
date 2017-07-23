using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository personnelRepository;
        private readonly IBloodDonationRepository bloodDonationRepository;
        private readonly IMapper mapper;

        public PersonnelService(IPersonnelRepository personnelRepository,
            IBloodDonationRepository bloodDonationRepository, IMapper mapper)
        {
            this.personnelRepository = personnelRepository;
            this.bloodDonationRepository = bloodDonationRepository;
            this.mapper = mapper;
        }

        public async Task<string> GetNameAsync(string pesel)
        {
            var personnel = await personnelRepository.GetAsync(pesel);
            return personnel?.Name ?? "";
        }

        public async Task<PersonnelDTO> GetAsync(string pesel)
        {
            var personnel = await personnelRepository.GetAsync(pesel);
            return mapper.Map<Personnel, PersonnelDTO>(personnel);
        }

        public async Task LoginAsync(string pesel, string password)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns blood volume (in mililiters) donated by donor.
        /// </summary>
        public async Task<int> HowMuchBoodTaken(string pesel)
        {
            IEnumerable<BloodDonation> allBloodDonations = await bloodDonationRepository.GetAllAsync();
            var bloodVolume = allBloodDonations
                .Where(x => x.BloodTaker.Pesel == pesel)
                .Select(x => x.Volume)
                .Sum();
            return bloodVolume;
        }
    }
}