//monarch v2.1 – Visual AI Overlay Bridge
using System.Collections.Generic;
using JaysAi.Finale.Visuals;

namespace JaysAi.Finale.AI
{
    public static class AiOverlay
    {
        private static List<OverlayCommand> _commands = new();

        public static void QueueRectangle(float x, float y, float width, float height, string label = "ENEMY", OverlayColor color = OverlayColor.Red)
        {
            _commands.Add(new OverlayCommand
            {
                X = x,
                Y = y,
                Width = width,
                Height = height,
                Label = label,
                Color = color
            });
        }

        public static void QueueCircle(float centerX, float centerY, float radius = 5f, OverlayColor color = OverlayColor.Green)
        {
            _commands.Add(new OverlayCommand
            {
                X = centerX - radius,
                Y = centerY - radius,
                Width = radius * 2f,
                Height = radius * 2f,
                Label = "",
                Color = color,
                Shape = OverlayShape.Circle
            });
        }

        public static List<OverlayCommand> GetQueuedCommands()
        {
            return new List<OverlayCommand>(_commands);
        }

        public static void Clear()
        {
            _commands.Clear();
        }
    }
}
