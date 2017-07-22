using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BloodDonors.Core.Domain;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BloodDonation, BloodDonationDTO>();
                    cfg.CreateMap<BloodType, BloodTypeDTO>();
                    cfg.CreateMap<Donor, DonorDTO>();
                    cfg.CreateMap<Personnel, PersonnelDTO>();
                })
                .CreateMapper();

    }
}
