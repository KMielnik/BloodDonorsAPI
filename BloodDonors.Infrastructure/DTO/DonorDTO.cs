using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
{
    public class DonorDTO
    {
        public string Pesel { get; set; }
        public string Name { get; set; }
        public DateTime LastDonated { get; set; }
        public BloodTypeDTO BloodType { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }

        public DonorDTO()
        {
            
        }

        public DonorDTO(string pesel, string name, DateTime lastDonated, BloodTypeDTO bloodType, string mail,
            string phone)
        {
            Pesel = pesel;
            Name = name;
            LastDonated = lastDonated;
            BloodType = bloodType;
            Mail = mail;
            Phone = phone;
        }
    }
}
