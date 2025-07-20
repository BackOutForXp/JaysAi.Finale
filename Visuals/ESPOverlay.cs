// File: Visuals/ESPOverlay.cs
using JaysAi.Finale.AI;
using JaysAi.Finale.Data;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace JaysAi.Finale.Visuals
{
    public class ESPOverlay
    {
        public bool IsEnabled { get; set; } = true;

        private List<Enemy> _visibleEnemies = new();

        public void UpdateEnemies(List<Enemy> enemies)
        {
            _visibleEnemies = enemies;
        }

        public void Render(Canvas canvas)
        {
            if (!IsEnabled || _visibleEnemies == null || canvas == null)
                return;

            canvas.Children.Clear();

            foreach (var enemy in _visibleEnemies)
            {
                if (!enemy.IsVisible || !enemy.IsEnemy) continue;

                var box = new Rectangle
                {
                    Width = 60,
                    Height = 120,
                    Stroke = Brushes.Red,
                    StrokeThickness = 2
                };

                Canvas.SetLeft(box, enemy.ScreenPosition.X - box.Width / 2);
                Canvas.SetTop(box, enemy.ScreenPosition.Y - box.Height / 2);
                canvas.Children.Add(box);

                var nameText = new TextBlock
                {
                    Text = enemy.Name,
                    Foreground = Brushes.White,
                    FontSize = 12
                };
                Canvas.SetLeft(nameText, enemy.ScreenPosition.X - 30);
                Canvas.SetTop(nameText, enemy.ScreenPosition.Y - 70);
                canvas.Children.Add(nameText);
            }
        }
    }
}
