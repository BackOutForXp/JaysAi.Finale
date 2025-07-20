// Monarch v1.0 – BuildInfo.cs

using System;
using System.Reflection;

namespace JaysAi.Finale.Utility
{
    public static class BuildInfo
    {
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "Unknown";

        public static DateTime BuildDate
        {
            get
            {
                var filePath = Assembly.GetExecutingAssembly().Location;
                return System.IO.File.GetLastWriteTime(filePath);
            }
        }

        public static string GetDetailedBuildInfo()
        {
            return $"Version: {Version} | Built: {BuildDate:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
