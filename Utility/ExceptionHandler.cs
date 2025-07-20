// Monarch v1.0 – ExceptionHandler.cs
using System;
using System.Threading;
using System.Windows;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale.Utility
{
    public static class ExceptionHandler
    {
        public static void Register()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.Current.DispatcherUnhandledException += OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Log($"[FATAL] Unhandled exception: {e.ExceptionObject}");
            MessageBox.Show("A critical error occurred. The application will exit.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Environment.Exit(1);
        }

        private static void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Log($"[UI] Dispatcher exception: {e.Exception}");
            MessageBox.Show("A UI error occurred. Please restart the app.", "UI Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            e.Handled = true;
        }

        private static void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Logger.Log($"[TASK] Unobserved exception: {e.Exception}");
            e.SetObserved();
        }
    }
}
