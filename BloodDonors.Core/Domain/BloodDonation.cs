using System;

namespace BloodDonors.Core.Domain
{
    public class BloodDonation
    {
        public Guid Id { get; protected set; }
        public DateTime DateOfDonation { get; protected set; }
        public int Volume { get; protected set; }
        public BloodType BloodType { get; protected set; }
        public Donor Donor { get; protected set; }
        public Personnel BloodTaker { get; protected set; }

        public BloodDonation(Guid id, DateTime dateOfDonation, int volume, 
            BloodType bloodType, Donor donor, Personnel bloodTaker)
        {
            Id = id;
            DateOfDonation = dateOfDonation;
            Volume = volume;
            BloodType = bloodType ?? throw new ArgumentNullException($"{nameof(bloodType)} can't be null'");
            Donor = donor ?? throw new ArgumentNullException($"{nameof(donor)} can't be null'");
            BloodTaker = bloodTaker ?? throw new ArgumentNullException($"{nameof(bloodTaker)} can't be null'");
        }

        public BloodDonation()
        {
            
        }
    }
}
