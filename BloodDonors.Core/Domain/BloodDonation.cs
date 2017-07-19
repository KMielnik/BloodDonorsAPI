using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Core.Domain
{
    class BloodDonation
    {
        public int Id { get; protected set; }
        public DateTime DateOfDonation { get; protected set; }
        public int Volume { get; protected set; }
        public BloodType BloodType { get; protected set; }
        public Donor Donor { get; protected set; }
        public Personnel BloodTaker { get; protected set; }
    }
}
