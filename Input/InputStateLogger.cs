//monarch v2.1 – Input Logger Core
using System;
using System.Collections.Generic;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Input
{
    public class InputStateLogger
    {
        private readonly List<InputSnapshot> _inputHistory = new();
        private readonly int _maxSnapshots = 100;

        public void LogCurrentState(InputSnapshot snapshot)
        {
            if (snapshot == null) return;

            if (_inputHistory.Count >= _maxSnapshots)
                _inputHistory.RemoveAt(0); // drop oldest

            _inputHistory.Add(snapshot);
        }

        public IReadOnlyList<InputSnapshot> GetHistory()
        {
            return _inputHistory.AsReadOnly();
        }

        public void Clear()
        {
            _inputHistory.Clear();
        }

        public void PrintHistory()
        {
            foreach (var snap in _inputHistory)
                Logger.Log($"[Input] {snap.Timestamp}: X={snap.X}, Y={snap.Y}, Fire={snap.FireButtonDown}, ADS={snap.ADSButtonDown}");
        }
    }

    public class InputSnapshot
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int X { get; set; }
        public int Y { get; set; }
        public bool FireButtonDown { get; set; }
        public bool ADSButtonDown { get; set; }
        public bool Reloading { get; set; }
        public float StickInputAngle { get; set; } = 0f;
    }
}
