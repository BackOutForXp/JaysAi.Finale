//monarch v2.0
using System;
using System.Diagnostics;
using System.Linq;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.SystemLogic
{
    public class ProcessHandler
    {
        public string TargetProcessName { get; set; } = "cod";
        public Process? TargetProcess { get; private set; }

        public bool FindTarget()
        {
            var processes = Process.GetProcessesByName(TargetProcessName);
            TargetProcess = processes.FirstOrDefault();
            return TargetProcess != null;
        }

        public int GetTargetPid()
        {
            return TargetProcess?.Id ?? -1;
        }

        public bool IsRunning()
        {
            return TargetProcess != null && !TargetProcess.HasExited;
        }

        public void HideWindow(nint hwnd)
        {
            // Use native method for hiding window (must be implemented via NativeMethods.cs)
            NativeMethods.ShowWindow(hwnd, 0); // 0 = SW_HIDE
        }

        public void MinimizeWindow(nint hwnd)
        {
            NativeMethods.ShowWindow(hwnd, 6); // 6 = SW_MINIMIZE
        }
    }
}
