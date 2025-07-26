// neural v3.0
using System;
using System.Windows;
using System.Windows.Threading;

namespace JaysAi.Finale.SystemLogic.Helpers
{
    public static class WpfThreadHelper
    {
        /// <summary>
        /// Executes an action on the main WPF UI thread. If already on the UI thread, executes immediately.
        /// </summary>
        public static void RunOnUIThread(Action action)
        {
            if (Application.Current == null || action == null)
                return;

            Dispatcher dispatcher = Application.Current.Dispatcher;

            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.Invoke(action);
            }
        }

        /// <summary>
        /// Executes an action asynchronously on the UI thread and returns a DispatcherOperation.
        /// </summary>
        public static DispatcherOperation RunAsync(Action action)
        {
            if (Application.Current == null || action == null)
                return null;

            return Application.Current.Dispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// Executes a function with a return value on the UI thread.
        /// </summary>
        public static T RunOnUIThread<T>(Func<T> func)
        {
            if (Application.Current == null || func == null)
                return default!;

            Dispatcher dispatcher = Application.Current.Dispatcher;

            if (dispatcher.CheckAccess())
            {
                return func();
            }
            else
            {
                return dispatcher.Invoke(func);
            }
        }

        /// <summary>
        /// Queues a function to run later on the UI thread, returning immediately.
        /// </summary>
        public static DispatcherOperation RunAsync<T>(Func<T> func, Action<T> callback)
        {
            return Application.Current?.Dispatcher.BeginInvoke(new Action(() =>
            {
                var result = func();
                callback?.Invoke(result);
            }));
        }
    }
}
