// File: System\GameProcessHelper.cs
using System;
using System.Diagnostics;
using System.Linq;

namespace JaysAi.Finale.SystemLogic
{
    public static class GameProcessHelper
    {
        private static readonly string[] SupportedProcesses = { "cod", "valorant", "csgo", "apex" };
        private static Process? _attachedProcess;

        public static Process? AttachedProcess => _attachedProcess;

        public static bool TryAttachToGame()
        {
            foreach (string name in SupportedProcesses)
            {
                var process = Process.GetProcessesByName(name).FirstOrDefault();
                if (process != null && !process.HasExited)
                {
                    _attachedProcess = process;
                    return true;
                }
            }

            _attachedProcess = null;
            return false;
        }

        public static nint GetWindowHandle()
        {
            return _attachedProcess?.MainWindowHandle ?? nint.Zero;
        }

        public static bool IsAttached => _attachedProcess != null && !_attachedProcess.HasExited;

        public static void Detach()
        {
            _attachedProcess = null;
        }
    }
}
