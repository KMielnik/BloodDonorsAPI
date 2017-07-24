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
        private readonly IBloodDonationRepository bloodDonationRepository;
        private readonly IMapper mapper;

        public DonorService(IDonorRepository donorRepository, IBloodDonationRepository bloodDonationRepository,
            IMapper mapper)
        {
            this.donorRepository = donorRepository;
            this.bloodDonationRepository = bloodDonationRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DonorDTO>> GetAllAsync()
        {
            IEnumerable<Donor> donors = await donorRepository.GetAllAsync();
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

        /// <summary>
        /// Returns blood volume (in mililiters) donated by donor.
        /// </summary>
        public async Task<int> HowMuchDonated(string pesel)
        {
            IEnumerable<BloodDonation> bloodDonations = await bloodDonationRepository.GetAllAsync();
            var donorsDonatedBloodVolume = bloodDonations.Where(x => x.Donor.Pesel == pesel)
                .Select(x => x.Volume)
                .Sum(x => x);
            return donorsDonatedBloodVolume;
        }

        public async Task LoginAsync(string pesel, string password)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(string pesel, string name, BloodTypeDTO bloodTypeDTO, 
            string mail, string phone, string passowrd)
        {
            var donor = await donorRepository.GetAsync(pesel);
            if(donor != null)
                throw new Exception("User with that PESEL already exists");
            var salt = "saltyy";                                                                                //TO DO generate proper salt

            var bloodType = new BloodType(bloodTypeDTO.AboType, bloodTypeDTO.RhType);
            donor = new Donor(pesel, passowrd, salt, name,
                bloodType, mail, phone);
            await donorRepository.AddAsync(donor);
        }

        /// <summary>
        /// Returns Time when donor will be able to donate blood again,
        /// DateTime.MinValue when never donated before.
        /// </summary>
        public async Task<DateTime> WhenWillBeAbleToDonateAgainAsync(string pesel)
        {
            var lastDonated = (await donorRepository.GetAsync(pesel)).LastDonated;
            if(lastDonated == null)
                return DateTime.MinValue;
            if (IsMale(pesel))
                return lastDonated.Value + TimeSpan.FromDays(61);   //Male need to wait 2 months
            return lastDonated.Value + TimeSpan.FromDays(92);       //Female 3 months.
        }

        /// <summary>
        /// Assumes persons gender based by their pesel number.
        /// </summary>
        private static bool IsMale(string pesel)
        {
            var charWithPersonsGender = pesel.Substring(9, 1);  //10th char contains info about persons gender,
            return (int.Parse(charWithPersonsGender) % 2) == 1; //male if odd, female if even.
        }
    }
}
