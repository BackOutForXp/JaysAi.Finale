//neural v3.0
using System;
using System.Security.Cryptography;
using System.Text;

namespace JaysAi.Finale.Security
{
    public static class UserIntegrityChecker
    {
        public static string GenerateSystemFingerprint()
        {
            string machineName = Environment.MachineName;
            string userName = Environment.UserName;
            string osVersion = Environment.OSVersion.VersionString;

            string combined = $"{machineName}-{userName}-{osVersion}";
            return ComputeSha256Hash(combined);
        }

        public static bool ValidateFingerprint(string providedHash)
        {
            string currentHash = GenerateSystemFingerprint();
            return string.Equals(providedHash, currentHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            return Convert.ToHexString(bytes);
        }
    }
}
