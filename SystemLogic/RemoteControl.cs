// neural v3.0
using System;
using System.Collections.Concurrent;
using System.Threading;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.SystemLogic
{
    public sealed class RemoteControl
    {
        private static readonly Lazy<RemoteControl> _instance = new(() => new RemoteControl());
        private readonly ConcurrentQueue<Action> _commandQueue = new();
        private readonly Thread _executionThread;
        private bool _running;

        public static RemoteControl Instance => _instance.Value;

        private RemoteControl()
        {
            _running = true;
            _executionThread = new Thread(ProcessQueue)
            {
                IsBackground = true,
                Name = "RemoteControlThread"
            };
            _executionThread.Start();
        }

        public void EnqueueCommand(Action command)
        {
            _commandQueue.Enqueue(command);
        }

        private void ProcessQueue()
        {
            while (_running)
            {
                try
                {
                    while (_commandQueue.TryDequeue(out var command))
                    {
                        try
                        {
                            command?.Invoke();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"RemoteControl Command Error: {ex.Message}");
                        }
                    }

                    Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    Logger.Warn($"RemoteControl Loop Exception: {ex.Message}");
                }
            }
        }

        public void Shutdown()
        {
            _running = false;
            Logger.Info("RemoteControl shutdown requested.");
        }
    }
}
