using System.Threading.Tasks;
using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public interface IPersonnelService
    {
        Task<string> GetNameAsync(string pesel);
        Task<PersonnelDTO> GetAsync(string pesel);
        Task LoginAsync(string pesel, string password);
        Task<int> HowMuchBoodTaken(string pesel);
    }
}