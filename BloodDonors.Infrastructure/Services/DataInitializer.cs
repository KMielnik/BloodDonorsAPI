using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodDonors.Core.Domain;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IDonorService donorService;
        private readonly IBloodDonationService bloodDonationService;
        private readonly IPersonnelService personnelService;
        private readonly IBloodTypeService bloodTypeService;

        public DataInitializer(IDonorService donorService, IBloodDonationService bloodDonationService,
            IPersonnelService personnelService, IBloodTypeService bloodTypeService)
        {
            this.donorService = donorService;
            this.bloodDonationService = bloodDonationService;
            this.personnelService = personnelService;
            this.bloodTypeService = bloodTypeService;
        }

        public async Task SeedAsync()
        {
            IEnumerable<DonorDTO> donors = await donorService.GetAllAsync();
            if (donors.Any())
                return;

            var random = new Random();

            var bloodTypes = new List<BloodTypeDTO>
            {
                new BloodTypeDTO("A", "-"),
                new BloodTypeDTO("B", "-"),
                new BloodTypeDTO("AB", "-"),
                new BloodTypeDTO("O", "-"),
                new BloodTypeDTO("A", "+"),
                new BloodTypeDTO("B", "+"),
                new BloodTypeDTO("AB", "+"),
                new BloodTypeDTO("O", "+")
            };

            foreach (var bloodType in bloodTypes)
            {
                await bloodTypeService.AddAsync(bloodType);
            }

            for (var i = 0; i < 10; i++)
            {
                var pesel = $"{i}1234567890";
                var password = pesel;
                var name = $"{GetOrdinalString(i)} Donor";
                var bloodTypeDto = bloodTypes[random.Next(bloodTypes.Count)];
                var mail = $"donor{i}@wp.pl";

                await donorService.RegisterAsync(pesel, name, bloodTypeDto, mail, pesel, password);


                pesel = $"{i}0987654321";
                password = pesel;
                name = $"{GetOrdinalString(i)} Personnel";

                await personnelService.RegisterAsync(pesel, password, name);
            }

            List<string> donorPesels = (await donorService.GetAllAsync()).Select(x => x.Pesel).ToList();
            List<string> personnelPesels = (await personnelService.GetAllAsync()).Select(x => x.Pesel).ToList();

            for (var i = 0; i < 250; i++)
            {
                var volume = random.Next(500, 1250); //VERY generous :P
                var donorPesel = donorPesels[random.Next(donorPesels.Count)];
                var personnelPesel = personnelPesels[random.Next(personnelPesels.Count)];

                await bloodDonationService.AddBloodDonationAsync(DateTime.Now, volume, donorPesel, personnelPesel);
            }
        }

        private string GetOrdinalString(int i)
        {
            switch (i)
            {
                case 0:
                    return "Zeroth";
                case 1:
                    return "First";
                case 2:
                    return "Second";
                case 3:
                    return "Third";
                case 4:
                    return "Fourth";
                case 5:
                    return "Fifth";
                case 6:
                    return "Sixth";
                case 7:
                    return "Seventh";
                case 8:
                    return "Eight";
                case 9:
                    return "Ninth";
                default:
                    return $"{i}th";
            }
        }
    }
}