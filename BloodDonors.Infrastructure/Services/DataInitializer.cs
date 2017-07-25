using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var random = new Random();
            List<BloodTypeDTO> bloodTypes = (await bloodTypeService.GetAllAsync()).ToList();

            if (donors.Any())
                return;
            for (var i = 0; i < 10; i++)
            {
                var pesel = $"{i}1234567890";
                var name = $"{i} donor";
                var bloodTypeDTO = bloodTypes[random.Next(bloodTypes.Count)];
                var mail = $"donor{i}@wp.pl";

                await donorService.RegisterAsync(pesel, name, bloodTypeDTO, mail, pesel, "password");
            }
        }
    }
}