// Neural v3.0 — InputSanitizer.cs
using System;
using System.Text.RegularExpressions;

namespace JaysAi.Finale.Helpers
{
    public static class InputSanitizer
    {
        // Removes all non-numeric characters
        public static string DigitsOnly(string input)
        {
            return Regex.Replace(input, @"\D", "");
        }

        // Removes all HTML tags and encodes common script elements
        public static string StripHTML(string input)
        {
            string noTags = Regex.Replace(input, "<.*?>", string.Empty);
            return Regex.Replace(noTags, @"(script|iframe|object|embed)", "", RegexOptions.IgnoreCase);
        }

        // Escapes quotes and replaces risky characters
        public static string SanitizeText(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            return input
                .Replace("\"", "")
                .Replace("'", "")
                .Replace(";", "")
                .Replace("--", "")
                .Replace("/*", "")
                .Replace("*/", "");
        }

        // Validates safe characters for a name (alphanumeric + underscore)
        public static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z0-9_]+$");
        }

        // Cleans for UI input or text field display
        public static string CleanForUI(string input)
        {
            return StripHTML(SanitizeText(input)).Trim();
        }

        // Converts null or empty to fallback value
        public static string NullToDefault(string input, string fallback = "N/A")
        {
            return string.IsNullOrWhiteSpace(input) ? fallback : input;
        }
    }
}
