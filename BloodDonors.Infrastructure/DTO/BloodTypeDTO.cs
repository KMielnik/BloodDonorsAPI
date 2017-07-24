using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
{
    public class BloodTypeDTO
    {
        public string AboType { get; set; }
        public char RhType { get; set; }

        public BloodTypeDTO()
        {
            
        }

        public BloodTypeDTO(string aboType, char rhType)
        {
            AboType = aboType;
            RhType = rhType;
        }
    }
}
