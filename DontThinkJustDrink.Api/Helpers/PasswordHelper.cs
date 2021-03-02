using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace DontThinkJustDrink.Api.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        private const int Iterations = 10000;

        public string Hash(string pw)
        {
            var saltAsBytes = GenerateRandomSaltBytes();
            var salt = Convert.ToBase64String(saltAsBytes);
            var hashed = DeriveHash(pw, saltAsBytes, Iterations);
            return $"{salt.Substring(0, 8)}{hashed}.{salt.Substring(8)}.{Iterations}";
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

        public (bool verified, bool needsUpgrade) Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3) {
                throw new FormatException("Unexpected hash format.");
            }

            var salt = parts[0].Substring(0, 8) + parts[1];
            var saltAsBytes = Convert.FromBase64String(salt);
            var hashed = DeriveHash(password, saltAsBytes, Iterations);

            if (hashed != hash) {
                return (false, false);
            }

            var iterations = Convert.ToInt32(parts[2]);
            var needsUpgrade = iterations < 100;

            return (true, needsUpgrade);
        }
    }
}
