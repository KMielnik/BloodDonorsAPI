using System.Threading.Tasks;

namespace BloodDonors.Infrastructure.DTO
{
    public class DonorScoreDTO
    {
        public string Name;
        public int Volume;

        public DonorScoreDTO(string name, int volume)
        {
            Name = name;
            Volume = volume;
        }

        public DonorScoreDTO()
        {
            
        }
    }
}