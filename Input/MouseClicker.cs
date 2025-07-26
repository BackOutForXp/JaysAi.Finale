// neural v3.0
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JaysAi.Finale.Input
{
    public class MouseClicker : IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        private Task? _clickTask;
        private bool _clicking;
        private int _intervalMs = 100;
        private MouseButton _button = MouseButton.Left;

        public enum MouseButton
        {
            Left,
            Right,
            Middle
        }

        public void StartClicking(MouseButton button, int intervalMs)
        {
            if (_clicking) return;

            _button = button;
            _intervalMs = intervalMs;
            _clicking = true;

            _clickTask = Task.Run(() =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    PerformClick(_button);
                    Task.Delay(_intervalMs, _cts.Token).Wait(_cts.Token);
                }
            }, _cts.Token);
        }

        public void StopClicking()
        {
            if (!_clicking) return;

            _clicking = false;
            _cts.Cancel();
            _clickTask?.Wait();
        }

        private void PerformClick(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    MouseButtonHelper.ClickLeft();
                    break;
                case MouseButton.Right:
                    MouseButtonHelper.ClickRight();
                    break;
                case MouseButton.Middle:
                    MouseButtonHelper.ClickMiddle();
                    break;
            }
        }

        public void Dispose()
        {
            StopClicking();
            _cts.Dispose();
        }
    }
}
