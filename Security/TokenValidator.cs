//neural v3.0
using System;
using System.Security.Cryptography;
using System.Text;

namespace JaysAi.Finale.Security
{
    public static class TokenValidator
    {
        private static readonly string _secretKey = "CHANGE_ME_SECRET"; // Replace with secure key storage

        public static bool ValidateToken(string token, string userId)
        {
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(userId))
                return false;

            var expectedToken = GenerateToken(userId);
            return token == expectedToken;
        }

        public static string GenerateToken(string userId)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userId));
            return Convert.ToBase64String(hash);
        }

        public static bool IsTokenExpired(DateTime tokenTimestamp, int validMinutes = 60)
        {
            return DateTime.UtcNow.Subtract(tokenTimestamp).TotalMinutes > validMinutes;
        }
    }
}
