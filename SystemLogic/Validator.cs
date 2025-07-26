// neural v3.0
using System;
using System.Text.RegularExpressions;

namespace JaysAi.Finale.SystemLogic
{
    public static class Validator
    {
        public static bool IsValidEmail(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(input,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public static bool IsValidUsername(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(input,
                @"^[a-zA-Z0-9_]{3,20}$",
                RegexOptions.Compiled);
        }

        public static bool IsValidLicenseKey(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(input,
                @"^[A-Z0-9\-]{10,40}$",
                RegexOptions.Compiled);
        }

        public static bool IsValidHex(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(input,
                @"\A\b[0-9a-fA-F]+\b\Z",
                RegexOptions.Compiled);
        }

        public static bool IsValidPassword(string input, int minLength = 8)
        {
            if (string.IsNullOrWhiteSpace(input) || input.Length < minLength)
                return false;

            return Regex.IsMatch(input,
                @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$",
                RegexOptions.Compiled);
        }
    }
}
