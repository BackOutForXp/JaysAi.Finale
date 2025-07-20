//monarch v2.1 – Fully Refactored & Synced

using global::System;
using global::System.Windows;
using global::System.Threading.Tasks;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
                TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

                await MainLauncher.InitializeAsync();

                // Optional: Launch GUI window here if you're using WPF overlay or config
                // var window = new MainWindow();
                // window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup failure: {ex.Message}", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            MessageBox.Show($"Unhandled Exception: {ex?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show($"Unobserved Task Exception: {e.Exception?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.SetObserved();
        }
    }
}
