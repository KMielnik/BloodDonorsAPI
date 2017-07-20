using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
{
    public class BloodDonationDTO
    {
        public int Id { get;  set; }
        public DateTime DateOfDonation { get;  set; }
        public int Volume { get;  set; }
        public BloodTypeDTO BloodType { get;  set; }
        public DonorDTO Donor { get;  set; }
        public PersonnelDTO BloodTaker { get;  set; }
    }
}
