using BloodDonors.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonors.Infrastructure.Services
{
    public interface IDonorService
    {
        Task<string> GetNameAsync(string pesel);
        Task<DateTime> WhenWillBeAbleToDonateAgainAsync(string pesel);
        Task RegisterAsync(string pesel, string name,
            BloodTypeDTO bloodTypeDTO, string mail,
            string phone, string passowrd, string salt);
        Task LoginAsync(string pesel, string password);
        Task<IEnumerable<DonorDTO>> GetAllAsync();
        Task<DonorDTO> GetAsync(string pesel);
        Task<int> HowMuchDonated(string pesel);
    }
}
