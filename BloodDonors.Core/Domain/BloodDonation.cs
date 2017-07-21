using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonors.Core.Domain
{
    public class BloodDonation
    {
        public int Id { get; protected set; }
        public DateTime DateOfDonation { get; protected set; }
        public int Volume { get; protected set; }
        public BloodType BloodType { get; protected set; }
        public Donor Donor { get; protected set; }
        public Personnel BloodTaker { get; protected set; }

        public BloodDonation(int id, DateTime dateOfDonation, int volume, 
            BloodType bloodType, Donor donor, Personnel bloodTaker)
        {
            Id = id;
            DateOfDonation = dateOfDonation;
            Volume = volume;
            BloodType = bloodType ?? throw new Exception($"{nameof(bloodType)} can't be null'");
            Donor = donor ?? throw new Exception($"{nameof(donor)} can't be null'");
            BloodTaker = bloodTaker ?? throw new Exception($"{nameof(bloodTaker)} can't be null'");
        }
    }
}
