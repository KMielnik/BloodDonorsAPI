using BloodDonors.Infrastructure.DTO;

namespace BloodDonors.Infrastructure.Services
{
    public interface IJwtService
    {
        JwtDto CreateToken(string pesel, string role);
    }
}