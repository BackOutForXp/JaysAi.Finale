//monarch v2.1 – Runtime Environment Tracker
using System;
using System.Diagnostics;

namespace JaysAi.Finale.SystemLogic
{
    public static class SystemStatus
    {
        public static bool IsInDebugMode()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public static bool IsProcessRunning(string processName)
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                return true;
            }
            return false;
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }

        public static string GetUserName()
        {
            return Environment.UserName;
        }

        public static string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        public static bool IsElevated()
        {
            try
            {
                using (var identity = System.Security.Principal.WindowsIdentity.GetCurrent())
                {
                    var principal = new System.Security.Principal.WindowsPrincipal(identity);
                    return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
