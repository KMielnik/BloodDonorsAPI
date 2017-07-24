using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
{
    public class BloodDonationDTO
    {
        public DateTime DateOfDonation { get;  set; }
        public int Volume { get;  set; }
        public BloodTypeDTO BloodType { get;  set; }
        public DonorDTO Donor { get;  set; }
        public PersonnelDTO BloodTaker { get;  set; }

        public BloodDonationDTO()
        {
            
        }

        public BloodDonationDTO(DateTime dateOfDonation, int volume, BloodTypeDTO bloodType, DonorDTO donor, PersonnelDTO bloodTaker)
        {
            DateOfDonation = dateOfDonation;
            Volume = volume;
            BloodType = bloodType;
            Donor = donor;
            BloodTaker = bloodTaker;
        }
    }
}
