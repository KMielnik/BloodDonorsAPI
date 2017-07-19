using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Core.Domain
{
    class Donor
    {
        public string Pesel { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public string Name { get; protected set; }
        public DateTime LastDonated { get; protected set; }
        public BloodType BloodType { get; protected set; }
        public string Mail { get; protected set; }
        public string Phone { get; protected set; }
    }
}
