//monarch v2.1 – Thread registration + safe cleanup

using global::System;
using global::System.Collections.Generic;
using global::System.Threading;

namespace JaysAi.Finale.SystemLogic
{
    public static class ThreadController
    {
        private static readonly List<Thread> threads = new();

        public static void Register(Thread thread)
        {
            threads.Add(thread);
            thread.Start();
        }

        public static void StopAll()
        {
            foreach (var t in threads)
            {
                if (t.IsAlive)
                {
                    try { t.Abort(); } catch { }
                }
            }

            threads.Clear();
        }
    }
}
