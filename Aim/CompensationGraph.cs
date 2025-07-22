//heavenly v3.0
using System.Collections.Generic;
using System.Windows;
using JaysAi.Finale.Utility;

namespace JaysAi.Finale.Aim
{
    public class CompensationGraph
    {
        private readonly List<Point> _points;
        private readonly int _maxPoints;

        public CompensationGraph(int maxPoints = 100)
        {
            _points = new List<Point>(maxPoints);
            _maxPoints = maxPoints;
        }

        public void AddPoint(double x, double y)
        {
            if (_points.Count >= _maxPoints)
                _points.RemoveAt(0);

            _points.Add(new Point(x, y));
        }

        public IReadOnlyList<Point> GetPoints() => _points.AsReadOnly();

        public void Clear() => _points.Clear();

        public bool HasData => _points.Count > 0;

        public void LogLatest()
        {
            if (HasData)
            {
                var latest = _points[^1];
                Logger.Debug($"CompensationGraph: Latest point = ({latest.X}, {latest.Y})");
            }
        }
    }
}
