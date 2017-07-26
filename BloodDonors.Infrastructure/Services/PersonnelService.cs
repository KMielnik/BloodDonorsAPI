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
    public class PersonnelService : IPersonnelService
    {
        private readonly IPersonnelRepository personnelRepository;
        private readonly IBloodDonationRepository bloodDonationRepository;
        private readonly IMapper mapper;
        private readonly IEncrypter encrypter;

        public PersonnelService(IPersonnelRepository personnelRepository,
            IBloodDonationRepository bloodDonationRepository, IMapper mapper, IEncrypter encrypter)
        {
            this.personnelRepository = personnelRepository;
            this.bloodDonationRepository = bloodDonationRepository;
            this.mapper = mapper;
            this.encrypter = encrypter;
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

        public async Task<IEnumerable<PersonnelDTO>> GetAllAsync()
        {
            IEnumerable<Personnel> personnels = await personnelRepository.GetAllAsync();
            return personnels.Select(x => mapper.Map<Personnel, PersonnelDTO>(x));
        }

        public async Task LoginAsync(string pesel, string password)
        {
            var personnel = await personnelRepository.GetAsync(pesel);
            if (personnel == null)
                throw new Exception("Personnel not found");

            var hash = encrypter.GetHash(password, personnel.Salt);
            if(personnel.Password == hash)
                return;
            throw new Exception("Incorrect password");
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

        public async Task RegisterAsync(string pesel, string password, string name)
        {
            var personnel = await personnelRepository.GetAsync(pesel);
            if (personnel != null)
                throw new Exception("User with that pesel already exists");

            var salt = encrypter.GetSalt(password);
            var hash = encrypter.GetHash(password, salt);

            personnel = new Personnel(pesel, hash, salt, name);
            await personnelRepository.RegisterAsync(personnel);
        }
    }
}