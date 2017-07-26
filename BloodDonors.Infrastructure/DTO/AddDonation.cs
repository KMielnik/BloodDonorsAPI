using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
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
