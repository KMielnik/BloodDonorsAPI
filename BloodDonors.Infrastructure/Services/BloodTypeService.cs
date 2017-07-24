using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BloodDonors.Core.Domain;
using BloodDonors.Core.Repositories;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public class BloodTypeService : IBloodTypeService
    {
        private readonly IBloodTypeRepository bloodTypeRepository;
        private readonly IMapper mapper;

        public BloodTypeService(IBloodTypeRepository bloodTypeRepository, IMapper mapper)
        {
            this.bloodTypeRepository = bloodTypeRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BloodTypeDTO>> GetAllAsync()
        {
            IEnumerable<BloodType> bloodTypes = await bloodTypeRepository.GetAllAsync();
            return bloodTypes.Select(x => mapper.Map<BloodType, BloodTypeDTO>(x));
        }
    }
}