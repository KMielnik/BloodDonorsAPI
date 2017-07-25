using System;
using System.Security.Cryptography;
using BloodDonors.Core.Extensions;

namespace BloodDonors.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        public string GetSalt(string password)
        {
            if (password.Empty())
                throw new Exception("Cannot generate salt from empty salt");

            var saltBytes = new byte[50];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string password, string salt)
        {
            if (password.Empty())
                throw new ArgumentException("Cannot be empty", nameof(password));
            if (salt.Empty())
                throw new ArgumentException("Cannot be empty", nameof(salt));

            var pbkdf2 = new Rfc2898DeriveBytes(password, GetBytes(salt), 10000);
            return Convert.ToBase64String(pbkdf2.GetBytes(50));
        }

        private static byte[] GetBytes(string salt)
        {
            var bytes = new byte[salt.Length * sizeof(char)];
            Buffer.BlockCopy(salt.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}