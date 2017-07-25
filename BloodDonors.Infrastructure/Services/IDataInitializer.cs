using System.Threading.Tasks;

namespace BloodDonors.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}