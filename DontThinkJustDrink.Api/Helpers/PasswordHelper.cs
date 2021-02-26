using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace DontThinkJustDrink.Api.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        public string Hash(string pw)
        {
            var saltAsBytes = GenerateRandomSaltBytes();
            var salt = Convert.ToBase64String(saltAsBytes);
            var hashed = DeriveHash(pw, saltAsBytes, 10000);
            return $"{salt.Substring(0, 8)}{hashed}{salt.Substring(8)}";
        }

        private byte[] GenerateRandomSaltBytes()
        {
            byte[] salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        /// <summary>
        /// Derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        /// </summary>
        private string DeriveHash(string pw, byte[] salt, int iterations)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pw,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: iterations,
                numBytesRequested: 256 / 8));
        }
    }
}
