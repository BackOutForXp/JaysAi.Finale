// neural v3.0
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.SystemLogic
{
    public static class ThreadUtils
    {
        /// <summary>
        /// Run an action safely in a background thread (fire-and-forget).
        /// </summary>
        public static void RunBackground(Action action)
        {
            Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ThreadUtils] Exception in background thread: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Starts a long-running task with cancellation support.
        /// </summary>
        public static Task RunLong(Action<CancellationToken> action, CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    action(token);
                }
                catch (OperationCanceledException)
                {
                    // Gracefully cancelled
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ThreadUtils] Exception in long-running task: {ex.Message}");
                }
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        /// <summary>
        /// Run an action on a delay, useful for timers.
        /// </summary>
        public static async Task DelayedRun(Action action, int milliseconds)
        {
            try
            {
                await Task.Delay(milliseconds);
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ThreadUtils] Exception in DelayedRun: {ex.Message}");
            }
        }

        /// <summary>
        /// Sleep the thread safely without exceptions.
        /// </summary>
        public static void SafeSleep(int milliseconds)
        {
            try
            {
                Thread.Sleep(milliseconds);
            }
            catch (ThreadInterruptedException) { }
            catch (Exception ex)
            {
                Console.WriteLine($"[ThreadUtils] Exception in SafeSleep: {ex.Message}");
            }
        }
    }
}
