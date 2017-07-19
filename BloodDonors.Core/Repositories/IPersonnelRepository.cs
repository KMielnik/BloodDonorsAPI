using BloodDonors.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonors.Core.Repositories
{
    public interface IPersonnelRepository
    {
        Task<Personnel> GetAsync(string pesel);
        Task<IEnumerable<Personnel>> GetAllAsync();
    }
}
