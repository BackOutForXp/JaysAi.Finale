//monarch v2.1 – Real-time system load tracking
using System;
using System.Diagnostics;

namespace JaysAi.Finale.SystemLogic
{
    public static class SystemMonitor
    {
        private static readonly PerformanceCounter cpuCounter = new("Processor", "% Processor Time", "_Total");
        private static readonly PerformanceCounter ramCounter = new("Memory", "Available MBytes");

        public static float GetCPUUsage()
        {
            try { return cpuCounter.NextValue(); }
            catch { return -1; }
        }

        public static float GetAvailableRAM()
        {
            try { return ramCounter.NextValue(); }
            catch { return -1; }
        }
    }
}
