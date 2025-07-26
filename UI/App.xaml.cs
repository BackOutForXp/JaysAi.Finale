// neural v3.0
using System;
using System.Windows;

namespace JaysAi.Finale
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Optional: Handle global exception logging
            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                MessageBox.Show("Unhandled exception: " + ex.ExceptionObject.ToString(), "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            DispatcherUnhandledException += (s, ex) =>
            {
                MessageBox.Show("UI exception: " + ex.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ex.Handled = true;
            };
        }
    }
}
