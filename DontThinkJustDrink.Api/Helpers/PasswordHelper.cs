using DontThinkJustDrink.Api.Settings;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace DontThinkJustDrink.Api.Helpers
{
    public class PasswordHelper : IPasswordHelper
    {
        private readonly int _iterations;

        public PasswordHelper(IHashingSettings hashSettings)
        {
            _iterations = hashSettings.Iterations;
        }

        public string Hash(string pw)
        {
            var saltAsBytes = GenerateRandomSaltBytes();
            var salt = Convert.ToBase64String(saltAsBytes);
            var hashed = DeriveHash(pw, saltAsBytes, _iterations);
            return $"{salt.Substring(0, 8)}{hashed}.{salt.Substring(8)}.{_iterations}";
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

        public (bool verified, bool needsUpgrade) Check(string storedHash, string password)
        {
            var parts = storedHash.Split('.', 3);

            if (parts.Length != 3) {
                throw new FormatException("Unexpected hash format.");
            }

            var salt = parts[0].Substring(0, 8) + parts[1];
            var saltAsBytes = Convert.FromBase64String(salt);
            var iterationsFromStoredHash = Convert.ToInt32(parts[2]);
            var derivedHashFromPassword = DeriveHash(password, saltAsBytes, iterationsFromStoredHash);

            if (derivedHashFromPassword != parts[0].Substring(8)) {
                return (false, false);
            }

            var needsUpgrade = iterationsFromStoredHash != _iterations;

            return (true, needsUpgrade);
        }
    }
}
