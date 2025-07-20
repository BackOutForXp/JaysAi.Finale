// Monarch v1.0 – App.xaml.cs
// ✅ Monarch Fix Checklist
// [x] Linked to Program.cs startup
// [x] WPF Resource Initialization
// [x] Modular-ready for stealth/gui setup
// [x] Exception-safe startup
// [x] LoaderGUI prepped (or replace with your actual MainWindow)

using JaysAi.Finale.Loader;
using System;
using System.Windows;

namespace JaysAi.Finale
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Optional: Load settings, initialize logging, etc.
                LoaderGUI mainWindow = new LoaderGUI(); // Replace if your main window is named differently
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"JaysAi Loader failed to start: {ex.Message}", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}
