using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Infrastructure.DTO
{
    public class PersonnelDTO
    {
        public string Pesel { get; set; }
        public string Name { get; set; }

        public PersonnelDTO()
        {
            
        }

        public PersonnelDTO(string pesel, string name)
        {
            Pesel = pesel;
            Name = name;
        }
    }
}
