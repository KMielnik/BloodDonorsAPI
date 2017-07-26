using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
{
    public class BloodTypeDTO
    {
        public string AboType { get; set; }
        public string RhType { get; set; }

        public BloodTypeDTO()
        {
            
        }

        public BloodTypeDTO(string aboType, string rhType)
        {
            AboType = aboType;
            RhType = rhType;
        }
    }
}
