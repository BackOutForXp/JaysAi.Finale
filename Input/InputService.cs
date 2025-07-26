// neural v3.0
using JaysAi.Finale.Input.Handlers;
using JaysAi.Finale.Input.Models;
using JaysAi.Finale.Helpers;
using System;
using System.Threading;

namespace JaysAi.Finale.Input
{
    public sealed class InputService : IDisposable
    {
        private readonly ControllerInputPoller _poller;
        private readonly ControllerInputLogger _logger;
        private readonly ControllerBridge _bridge;
        private readonly ControllerSignalBus _signalBus;
        private Thread? _inputThread;
        private bool _isRunning;

        public InputService()
        {
            _poller = new ControllerInputPoller();
            _logger = new ControllerInputLogger();
            _bridge = ControllerBridge.Instance;
            _signalBus = ControllerSignalBus.Instance;
        }

        public void Start()
        {
            if (_isRunning) return;

            _isRunning = true;
            _inputThread = new Thread(InputLoop)
            {
                IsBackground = true,
                Name = "InputServiceThread"
            };
            _inputThread.Start();
        }

        private void InputLoop()
        {
            while (_isRunning)
            {
                var inputState = _poller.ReadInput();
                if (inputState != null)
                {
                    _bridge.Update(0, inputState);
                    _logger.Log(inputState);
                    _signalBus.Broadcast(inputState);
                }

                Thread.Sleep(1); // Sub-ms processing if required can be added later
            }
        }

        public void Stop()
        {
            _isRunning = false;
            _inputThread?.Join();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
