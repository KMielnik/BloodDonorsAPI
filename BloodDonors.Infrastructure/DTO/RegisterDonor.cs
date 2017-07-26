using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
{
    public class RegisterDonor
    {
        public string Pesel { get; set; }
        public string Name { get; set; }
        public BloodTypeDTO BloodType { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
}
