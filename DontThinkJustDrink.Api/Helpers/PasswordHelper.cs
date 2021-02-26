using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace DontThinkJustDrink.Api.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        public (string, string) GetPasswordSaltAndHash(string pw)
        {
            var saltAsBytes = GetSalt();
            var salt = Convert.ToBase64String(saltAsBytes);
            var hashed = HashPassword(pw, saltAsBytes);
            return (salt, hashed);
        }

        private byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        /// </summary>
        private string HashPassword(string pw, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pw,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}
