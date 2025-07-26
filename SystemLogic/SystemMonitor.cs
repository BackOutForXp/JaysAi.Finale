// neural v3.0
using System;
using System.Diagnostics;
using System.Timers;
using JaysAi.Finale.Logging;

namespace JaysAi.Finale.SystemLogic
{
    public class SystemMonitor : IDisposable
    {
        private readonly Timer _pollingTimer;
        private readonly PerformanceCounter _cpuCounter;
        private readonly PerformanceCounter _ramCounter;
        private readonly Process _currentProcess;

        public float CpuUsagePercent { get; private set; }
        public float RamUsageMB { get; private set; }

        public event EventHandler<SystemUsageEventArgs>? UsageUpdated;

        public SystemMonitor(double intervalMs = 1000)
        {
            _currentProcess = Process.GetCurrentProcess();

            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            _pollingTimer = new Timer(intervalMs);
            _pollingTimer.Elapsed += OnTimerElapsed;
            _pollingTimer.AutoReset = true;
        }

        public void Start() => _pollingTimer.Start();
        public void Stop() => _pollingTimer.Stop();

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            try
            {
                CpuUsagePercent = _cpuCounter.NextValue();
                RamUsageMB = (_currentProcess.WorkingSet64 / 1024f) / 1024f;

                UsageUpdated?.Invoke(this, new SystemUsageEventArgs(CpuUsagePercent, RamUsageMB));
            }
            catch (Exception ex)
            {
                Log.Error($"SystemMonitor failed to read usage: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _pollingTimer?.Dispose();
            _cpuCounter?.Dispose();
            _ramCounter?.Dispose();
        }
    }

    public class SystemUsageEventArgs : EventArgs
    {
        public float CpuUsage { get; }
        public float RamUsageMB { get; }

        public SystemUsageEventArgs(float cpu, float ram)
        {
            CpuUsage = cpu;
            RamUsageMB = ram;
        }
    }
}
