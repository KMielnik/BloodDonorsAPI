namespace BloodDonors.Infrastructure.Services
{
    public interface IJwtService
    {
        string CreateToken(string pesel, string role);
    }
}