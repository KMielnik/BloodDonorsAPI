using System;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.EntryData
{
    public class AddDonation
    {
        public DateTime DateOfDonation { get; set; }
        public int Volume { get; set; }
        public BloodTypeDTO BloodType { get; set; }
        public string DonorPesel { get; set; }
        public string BloodTakerPesel { get; set; }
    }
}
