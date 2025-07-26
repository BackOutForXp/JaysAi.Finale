// Neural v3.0 — Program.cs
using System;
using System.Windows;
using JaysAi.Finale.UI;

namespace JaysAi.Finale
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                var app = new Application();

                // Optional: Dark theme mode, app styling
                app.ShutdownMode = ShutdownMode.OnMainWindowClose;

                var mainWindow = new MainWindow();
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Fatal Error] {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}
