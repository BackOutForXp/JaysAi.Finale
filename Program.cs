// Monarch v1.0 – Program.cs
// ✅ Verified for Visual Studio 2022/2025 + .NET 8 WPF

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Windows;

namespace JaysAi.Finale
{
    [SupportedOSPlatform("windows")]
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
#if DEBUG
            ShowConsoleWindow();
#endif
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

#if DEBUG
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        private static void ShowConsoleWindow()
        {
            AllocConsole();
            Console.Title = "JaysAi Debug Console";
            Console.WriteLine("Debug Console Initialized...");
        }
#endif
    }
}
