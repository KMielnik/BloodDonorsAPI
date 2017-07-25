namespace BloodDonors.Infrastructure.Services
{
    public interface IEncrypter
    {
        string GetSalt(string password);
        string GetHash(string password, string salt);
    }
}