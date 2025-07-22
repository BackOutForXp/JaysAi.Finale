//heavenly v3.0 – InputManager
using System;
using JaysAi.Finale.Input;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Input
{
    public class InputManager
    {
        private readonly InputDispatcher _dispatcher;
        private readonly InputMonitor _monitor;
        private readonly ControllerInputListener _controllerListener;
        private readonly KeyboardPresser _keyboardPresser;
        private readonly MouseEmulator _mouseEmulator;
        private readonly StickAssist _stickAssist;

        public bool IsInitialized { get; private set; }

        public InputManager()
        {
            _dispatcher = new InputDispatcher();
            _monitor = new InputMonitor();
            _controllerListener = new ControllerInputListener();
            _keyboardPresser = new KeyboardPresser();
            _mouseEmulator = new MouseEmulator();
            _stickAssist = new StickAssist();
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            _monitor.Start();
            _controllerListener.StartListening();
            _dispatcher.BindKeyEvents();
            _dispatcher.BindControllerEvents();
            _stickAssist.LoadProfile("Default");

            Logger.Log("InputManager initialized.");
            IsInitialized = true;
        }

        public void Update()
        {
            _controllerListener.Update();
            _stickAssist.Update();
            _monitor.CheckHotkeys();
        }

        public void SimulateKeyPress(ConsoleKey key)
        {
            _keyboardPresser.PressKey(key);
        }

        public void MoveMouseTo(int x, int y)
        {
            _mouseEmulator.MoveTo(x, y);
        }

        public void Shutdown()
        {
            _monitor.Stop();
            _controllerListener.StopListening();
            Logger.Log("InputManager shutdown.");
            IsInitialized = false;
        }
    }
}
