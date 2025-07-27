// Neural v3.1 — WpfDispatcher.cs
using System;
using System.Windows;
using System.Windows.Threading;

namespace JaysAi.Finale.Utility
{
    public static class WpfDispatcher
    {
        public static void SafeInvoke(Action action)
        {
            if (Application.Current == null)
                return;

            var dispatcher = Application.Current.Dispatcher;

            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.Invoke(action, DispatcherPriority.Normal);
            }
        }

        public static void SafeBeginInvoke(Action action)
        {
            if (Application.Current == null)
                return;

            var dispatcher = Application.Current.Dispatcher;

            if (dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                dispatcher.BeginInvoke(action, DispatcherPriority.Background);
            }
        }
    }
}
