using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public class BloodDonationService : IBloodDonationService
    {
        private readonly IBloodDonationRepository bloodDonationRepository;
        private readonly IDonorRepository donorRepository;
        private readonly IBloodTypeRepository bloodTypeRepository;
        private readonly IPersonnelRepository personnelRepository;
        private readonly IMapper mapper;

        public BloodDonationService(IBloodDonationRepository bloodDonationRepository, IDonorRepository donorRepository,
            IBloodTypeRepository bloodTypeRepository, IPersonnelRepository personnelRepository, IMapper mapper)
        {
            this.bloodDonationRepository = bloodDonationRepository;
            this.donorRepository = donorRepository;
            this.bloodTypeRepository = bloodTypeRepository;
            this.personnelRepository = personnelRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BloodDonationDTO>> GetAllAsync()
        {
            IEnumerable<BloodDonation> bloodDonations = await bloodDonationRepository.GetAllAsync();
            return bloodDonations.Select(x => mapper.Map<BloodDonation, BloodDonationDTO>(x));
        }

        public async Task<int> HowMuchBloodHasBeenDonatedEver()
            => (await bloodDonationRepository.GetAllAsync()).Sum(x => x.Volume);

        public async Task AddBloodDonationAsync(DateTime dateOfDonation, int volume, string donorPesel,
            string personnelPesel)
        {
            var guid = Guid.NewGuid();
            var donor = await donorRepository.GetAsync(donorPesel);
            if (donor == null)
                throw new Exception($"{nameof(donor)} not found.");

            var personnel = await personnelRepository.GetAsync(personnelPesel);
            if (personnel == null)
                throw new Exception($"{nameof(personnel)} not found.");

            var bloodType = donor.BloodType;
            donor.UpdateTimeOfLastDonation(dateOfDonation);
            await donorRepository.UpdateAsync(donor);

            var bloodDonation = new BloodDonation(guid, dateOfDonation, volume, bloodType, donor, personnel);
            await bloodDonationRepository.AddAsync(bloodDonation);
        }

        public async Task<int> HowMuchBloodTakenByPersonnel(string pesel)
        {
            IEnumerable<BloodDonation> allBloodDonations = await bloodDonationRepository.GetAllAsync();
            return allBloodDonations.Where(x => x.BloodTaker.Pesel == pesel)
                .Sum(x => x.Volume);
        }

        /// <summary>
        /// Returns a list of all Honored Blood Donors, which donated over 20 liters of blood.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DonorScoreDTO>> GetHonoraryDonorsAsync()
        {
            IEnumerable<BloodDonation> allBloodDonations = await bloodDonationRepository.GetAllAsync();
            IOrderedEnumerable<DonorScoreDTO> allPeopleWhoDonatedOver20Liters = allBloodDonations
                .Select(x => new {x.Donor.Name, x.Volume})
                .GroupBy(x => x.Name)
                .Select(g => new DonorScoreDTO(g.Key, g.Sum(x => x.Volume)))
                .Where(x => x.Volume > 20000)
                .OrderByDescending(x => x.Volume)
                .ThenBy(x => x.Name);

            return allPeopleWhoDonatedOver20Liters;
        }
    }
}