//monarch v1.0
using System;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;

namespace JaysAi.Finale.Security
{
    public static class SecurityManager
    {
        // Generates a secure SHA256 hash
        public static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        // Checks if debugger is attached (basic anti-debug)
        public static bool IsDebuggerAttached()
        {
            return Debugger.IsAttached || IsDebuggerPresent();
        }

        // Low-level Windows anti-debug check
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool IsDebuggerPresent();

        // Random hardware identifier simulation (can be replaced with real HWID)
        public static string GetFakeHardwareId()
        {
            return ComputeHash(Environment.MachineName + Environment.UserName);
        }

        // Timestamp for build verification or session tracking
        public static long GetUnixTimestamp()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
    }
}
