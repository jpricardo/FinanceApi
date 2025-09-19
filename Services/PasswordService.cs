using System.Security.Cryptography;
using System.Text;

namespace FinanceApi.Services
{
    public static class PasswordService
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
            string salt = Convert.ToBase64String(saltBytes);

            string hash = Convert.ToBase64String(
                Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    saltBytes,
                    100_000,
                    HashAlgorithmName.SHA256,
                    32));

            return (hash, salt);
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            string hash = Convert.ToBase64String(
                Rfc2898DeriveBytes.Pbkdf2(
                    Encoding.UTF8.GetBytes(password),
                    saltBytes,
                    100_000,
                    HashAlgorithmName.SHA256,
                    32));

            return hash == storedHash;
        }
    }
}