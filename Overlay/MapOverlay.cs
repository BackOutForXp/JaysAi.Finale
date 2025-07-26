//neural v3.0

using JaysAi.Finale.AI;
using JaysAi.Finale.Core;
using JaysAi.Finale.Helpers;
using JaysAi.Finale.Structures;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JaysAi.Finale.Visuals
{
    public class MapOverlay
    {
        private readonly Canvas _mapCanvas;
        private readonly TargetMemory _targetMemory;
        private readonly PlayerTracker _playerTracker;
        private readonly double _mapScale;
        private readonly double _mapSize;

        public MapOverlay(Canvas mapCanvas, TargetMemory targetMemory, PlayerTracker playerTracker, double mapSize = 300.0, double mapScale = 0.5)
        {
            _mapCanvas = mapCanvas ?? throw new ArgumentNullException(nameof(mapCanvas));
            _targetMemory = targetMemory ?? throw new ArgumentNullException(nameof(targetMemory));
            _playerTracker = playerTracker ?? throw new ArgumentNullException(nameof(playerTracker));
            _mapSize = mapSize;
            _mapScale = mapScale;
        }

        public void Render()
        {
            _mapCanvas.Children.Clear();

            DrawMapBoundary();
            DrawPlayerIcon();
            DrawEnemies();
        }

        private void DrawMapBoundary()
        {
            var border = new Rectangle
            {
                Width = _mapSize,
                Height = _mapSize,
                Stroke = Brushes.White,
                StrokeThickness = 1
            };

            Canvas.SetLeft(border, 0);
            Canvas.SetTop(border, 0);
            _mapCanvas.Children.Add(border);
        }

        private void DrawPlayerIcon()
        {
            var player = _playerTracker.GetLocalPlayer();
            if (player == null) return;

            var centerX = _mapSize / 2;
            var centerY = _mapSize / 2;

            var dot = new Ellipse
            {
                Width = 6,
                Height = 6,
                Fill = Brushes.Cyan
            };

            Canvas.SetLeft(dot, centerX - 3);
            Canvas.SetTop(dot, centerY - 3);
            _mapCanvas.Children.Add(dot);
        }

        private void DrawEnemies()
        {
            var player = _playerTracker.GetLocalPlayer();
            if (player == null) return;

            foreach (var target in _targetMemory.GetVisibleTargets())
            {
                var dx = (target.LastKnownWorldPosition.X - player.Position.X) * _mapScale;
                var dy = (target.LastKnownWorldPosition.Y - player.Position.Y) * _mapScale;

                var mapX = (_mapSize / 2) + dx;
                var mapY = (_mapSize / 2) + dy;

                var enemyDot = new Ellipse
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Red
                };

                Canvas.SetLeft(enemyDot, mapX - 2.5);
                Canvas.SetTop(enemyDot, mapY - 2.5);
                _mapCanvas.Children.Add(enemyDot);
            }
        }
    }
}
