// Neural v3.0 — SecurityHelper.cs
using System;
using System.Security.Cryptography;
using System.Text;

namespace JaysAi.Finale.Helpers
{
    public static class SecurityHelper
    {
        public static string ComputeSHA256(string input)
        {
            using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return ConvertToHex(hashBytes);
        }

        public static string ComputeSHA512(string input)
        {
            using SHA512 sha512 = SHA512.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha512.ComputeHash(inputBytes);
            return ConvertToHex(hashBytes);
        }

        public static string ComputeMD5(string input)
        {
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return ConvertToHex(hashBytes);
        }

        private static string ConvertToHex(byte[] hash)
        {
            StringBuilder sb = new();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
