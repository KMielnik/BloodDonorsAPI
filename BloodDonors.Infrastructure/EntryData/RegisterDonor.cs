using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.EntryData
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
